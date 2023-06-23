using BetterConsoleTables;
using Redox_Code_Test.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redox_Code_Test
{

    
    public class EventScheduler 
    {


        private static string _connectionString = @"Data Source = WS135\MSSQLSERVER03; Initial Catalog = redoxTestDB; Persist Security Info = True; User ID = sa;Password=Uiop890-=";

        //Display All Events
        public void ShowEventList()
        {
            List<Event> eventList = getEventList();
            try
            {
                Table table = new Table("Event Id", "Event Name", "Event Description", "Event Location", "Event DateTime");
                Console.WriteLine("Scheduled Event List");
                foreach (var each in eventList)
                {
                    string id = each.Id;
                    string name = each.Name;
                    string description = each.Description;
                    string location = each.Location;
                    string dateTime = each.DateTime;
                    table.AddRow(id, name, description, location, dateTime);
                }
                Console.WriteLine(table.ToString());
                Console.WriteLine("Scheduled Events Total :" + eventList.Count() + ".");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        //Get Event List
        public List<Event> getEventList()
        {
            SqlConnection sqlConnection;

            sqlConnection = new SqlConnection(_connectionString);
            sqlConnection.Open();
            string listQuery = "SELECT * FROM Event";
            SqlCommand listCommand = new SqlCommand(listQuery, sqlConnection);
            listCommand.ExecuteNonQuery();
            SqlDataReader sqlDataReader = listCommand.ExecuteReader();
            List<Event> eventList = new List<Event>();
            while (sqlDataReader.Read())
            {
                string id = sqlDataReader.GetValue(0).ToString();
                string name = sqlDataReader.GetValue(1).ToString();
                string description = sqlDataReader.GetValue(2).ToString();
                string location = sqlDataReader.GetValue(3).ToString();
                string dateTime = sqlDataReader.GetValue(4).ToString();
                Event eachEvent = new Event();
                eachEvent.Id = id;
                eachEvent.Name = name;
                eachEvent.Description = description;
                eachEvent.Location = location;
                eachEvent.DateTime = dateTime;
                eventList.Add(eachEvent);
            };
            sqlConnection.Close();
            var result = eventList.OrderBy(x => x.DateTime).ToList();
            return result;
        }

        //Create New Event
        public void ScheduleEvent()
        {

            SqlConnection sqlConnection;
            try
            {
                sqlConnection = new SqlConnection(_connectionString);
                sqlConnection.Open();
                Console.WriteLine("Enter Event Name:");
                string Name = Console.ReadLine();
                Console.WriteLine("Enter Event Description:");
                string Description = Console.ReadLine();
                Console.WriteLine("Enter Event Location:");
                string Location = Console.ReadLine();
                Console.WriteLine("Enter Event Time:(yyyy-MM-dd) ");
                string DateTime = Console.ReadLine();

                //Datetime input check: is it double booking? is it valid input ? is it a pass date?
                Boolean isDoubleBooking = doubleBookingCheck(DateTime);
                Boolean isDatetimeInputValid = datetimeInputFormatCheck(DateTime);
                Boolean isDatetimePassed = datetimePassCheck(DateTime);


                while (true)
                {
                    if (!isDatetimeInputValid)
                    {
                        string content = "Warning : Invalid date timme input, please pick another time!";
                        PrintAlermText(content);
                        Console.WriteLine("Enter Event Time:(yyyy-MM-dd) or Press ENTER back to Menu");
                        DateTime = Console.ReadLine();
                        isDatetimeInputValid = datetimeInputFormatCheck(DateTime);
                        isDoubleBooking = doubleBookingCheck(DateTime);
                        isDatetimePassed = datetimePassCheck(DateTime);
                        if (DateTime == "")
                        {
                            Console.Clear();
                            Menu.DisplayMenu();
                        }

                    }
                    else if (!isDoubleBooking)
                    {
                        string content = "Warning : The Date Time is already passed, please pick another date in the future!";
                        PrintAlermText(content);
                        Console.WriteLine("Enter Event Time:(yyyy-MM-dd) or Press ENTER back to Menu");
                        DateTime = Console.ReadLine();
                        isDoubleBooking = doubleBookingCheck(DateTime);
                        isDatetimeInputValid = datetimeInputFormatCheck(DateTime);
                        isDatetimePassed = datetimePassCheck(DateTime);
                        if (DateTime == "")
                        {
                            Console.Clear();
                            Menu.DisplayMenu();
                        }
                    }
                    else if (!isDatetimePassed)
                    {
                        string content = "Warning : It is a pass date, please pick another time!";
                        PrintAlermText(content);
                        Console.WriteLine("Enter Event Time:(yyyy-MM-dd) or Press ENTER back to Menu");
                        DateTime = Console.ReadLine();
                        isDoubleBooking = doubleBookingCheck(DateTime);
                        isDatetimeInputValid = datetimeInputFormatCheck(DateTime);
                        isDatetimePassed = datetimePassCheck(DateTime);
                        if (DateTime == "")
                        {
                            Console.Clear();
                            Menu.DisplayMenu();
                        }
                    }
                    else
                    {
                        break;
                    }
                }
                string insertQuery = "INSERT INTO Event([Name],[Description],[Location],[DateTime]) VALUES('"
                                    + Name + "','" + Description + "','" + Location + "','" + DateTime + "')";
                SqlCommand insertCommand = new SqlCommand(insertQuery, sqlConnection);
                insertCommand.ExecuteNonQuery();
                Console.WriteLine("Event scheduled!");
                ShowEventList();
                Console.WriteLine("Please press RETURN back to Menu ");
                sqlConnection.Close();
                Console.Clear();
                Menu.DisplayMenu();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        //Cancel Event
        public void CancelEvent()
        {
            while (true)
            {
                ShowEventList();
                List<Event> eventList = getEventList();
                List<string> idList = new List<string>();
                foreach (var each in eventList)
                {
                    idList.Add(each.Id);
                }
                Console.WriteLine("=================================================================");
                Console.WriteLine("1 - Please enter the Id of event which is to be cancelled : ");
                Console.WriteLine("2 - Please enter RETURN back to Menu ");
                string option = Console.ReadLine();


                if (option == "")
                {
                    Console.Clear();
                    Menu.DisplayMenu();
                }
                else
                {

                    if (idList.IndexOf(option) > 0)
                    {
                        Console.WriteLine("Confirm cancle event: " + option + "? : (Y/N)");
                        string confirm = Console.ReadLine();
                        if (confirm == "Y")
                        {
                            DeleteEventById(option);
                            ShowEventList();

                        }
                        string content = "Check event ID again!";
                        PrintAlermText(content);
                        CancelEvent();
                    }
                    else
                    {
                        string content = "Wrong Id, please pick again or Back to Menu!";
                        PrintAlermText(content);
                        CancelEvent();
                    }
                }
            }
        }
       
        //Delete Event By Id
        public void DeleteEventById(string id)
        {
            SqlConnection sqlConnection = new SqlConnection(_connectionString);
            sqlConnection.Open();
            string deleteQuery = "DELETE FROM Event WHERE ID =" + id;
            SqlCommand deleteCommand = new SqlCommand(deleteQuery, sqlConnection);
            deleteCommand.ExecuteNonQuery();
            string content = "Event is Cancelled!";
            PrintAlermText(content);

        }

        //Display List Of Future Events 
        public void GetUpcomingEvents()
        {
            Table table = new Table("Event Id", "Event Name", "Event Description", "Event Location", "Event DateTime");
            DateTime today = DateTime.Today;
            SqlConnection sqlConnection = new SqlConnection(_connectionString);
            sqlConnection.Open();
            List<Event> eventList = getEventList();
            var upcomingEventList = eventList.Where(x => DateTime.Parse(x.DateTime) > today).ToList();

            foreach (var each in upcomingEventList)
            {
                string id = each.Id;
                string name = each.Name;
                string description = each.Description;
                string location = each.Location;
                string dateTime = each.DateTime;
                table.AddRow(id, name, description, location, dateTime);
            }
            Console.WriteLine("The upcoming Events are listed below:");
            Console.WriteLine(table.ToString());
            Console.WriteLine("Press any key to back to Menu");
            Console.ReadKey();
            Console.Clear();
        }

        //Message in red for alarm
        public void PrintAlermText(string content)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(content);
            Console.WriteLine("xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx");
            Console.ForegroundColor = ConsoleColor.White;
        }

        //Check if input date formar is yyyy-MM-dd
        public bool datetimeInputFormatCheck(string datetime)
        {
            DateTime dDate;

            bool chValidity = DateTime.TryParseExact(datetime,
                                                     "yyyy-MM-dd",
                                                      CultureInfo.InvariantCulture,
                                                      DateTimeStyles.None,
                                                       out dDate);
            return chValidity;
        }

        //Check if input date is double booking.
        public bool doubleBookingCheck(string datetime)
        {
            List<Event> eventList = getEventList();
            List<string> timeList = new List<string>();
            foreach (var each in eventList)
            {
                timeList.Add(each.DateTime);
            }
            if (timeList.IndexOf(datetime) > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        //Check if input date is a pass date.
        public bool datetimePassCheck(string datetime)
        {

            if (DateTime.Parse(datetime) > DateTime.Today)
            {
                return true;
            }
            else
            {
                ;
                return false;
            }

        }

        public void LingqQuery()
        {
            int[] intList = Enumerable.Range(1, 100).ToArray();
            var evenIntList = intList.Where(x => x % 2 == 0).ToList();
            Console.WriteLine("1 -The list of all even integers from 1 to 100 :");
            foreach (var each in evenIntList)
            {
                if (each == 100)
                {
                    Console.Write(each);
                }
                else
                {
                    Console.Write(each + ",");
                }
            }
            Console.WriteLine();
            Console.WriteLine("--------------------------------------------------------");
            Console.WriteLine("2 -The list of all integers from 1 to 100 that are divisible by 3 or 5, but not 3 and 5:");
            foreach (var each in intList)
            {
                if (each%3==0|| each % 5 == 0)
                {
                    if (each % 15 != 0 )
                    {
                        if (each == 100)
                        {
                            Console.Write(each);
                        }
                        else
                        {
                            Console.Write(each + ",");
                        }
                        
                    } 
                }
            }
            Console.ReadKey();
        }
    }

}

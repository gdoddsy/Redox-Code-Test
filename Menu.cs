using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redox_Code_Test
{
    public class Menu
    {

        public static void DisplayMenu()
        {

            
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Menu");
                Console.WriteLine("1 - Show All Event");
                Console.WriteLine("2 - Schedule Event");
                Console.WriteLine("3 - Cancel Event");
                Console.WriteLine("4 - Upcoming Events");
                Console.WriteLine("5 - Linq Query");

                Console.Write("Choose :");
                string menuOption = Console.ReadLine();
                int a = 0;
                if (int.TryParse(menuOption, out a) == false)
                {
                    Console.WriteLine("Please input the listed options : ");
                    DisplayMenu();
                }
                else if (int.Parse(menuOption)>5 || int.Parse(menuOption) < 1)
                {
                    Console.WriteLine("Please input the listed options : ");
                } else 
                {
                    int option = int.Parse(menuOption);
                    EventScheduler eventScheduler = new EventScheduler();
                    switch (option)
                    {
                        case 1:
                            Console.Clear();
                            eventScheduler.ShowEventList();
                            Console.WriteLine("Please press RETURN back to Menu ");
                            Console.ReadKey();
                            Console.Clear();
                            break;
                        case 2:
                            eventScheduler.ScheduleEvent();
                            break;
                        case 3:
                            Console.Clear();
                            eventScheduler.CancelEvent();
                            break;
                        case 4:
                            eventScheduler.GetUpcomingEvents();
                            break;
                        case 5:
                            int[] intList = Enumerable.Range(1, 100).ToArray();
                            LinqQuery.EvenList(intList);
                            LinqQuery.DivisibleList(intList);
                            Console.WriteLine("Please press RETURN back to Menu ");
                            Console.ReadKey();
                            Console.Clear();
                            break;
                    }
                }
                
            }
            
        }


    }


}


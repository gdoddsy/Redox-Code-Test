using Microsoft.AspNetCore.Mvc;
using EventSchedularProject.Models;

namespace EventSchedularProject.Controllers
{
    public class Exercise1Controller : Controller
    {
        public IActionResult Index()
        {
            // list of numbers from 1  to 100
            List<int> newList = new(Exercise1.MakeSequence(1, 100));
            string msg = "";
            foreach (int ival in newList)
            {
                if(msg != "") { //, not for first element
                 msg = msg + ",  " +  ival;
                }
                else
                {
                    msg = ival.ToString();
                }
            }

            ViewBag.ListInt = msg;

            //display even numbers from 1 top 100
            List<int> evenList = new(Exercise1.EvenNumbers(newList));
            string msgeven = "";
            foreach (int ival in evenList)
            {
                if (msgeven != "")
                { //, not for first element
                    msgeven = msgeven + ",  " + ival;
                }
                else
                {
                    msgeven = ival.ToString();
                }
            }

            ViewBag.ListEven = msgeven;

            //// divisible by 3 or 5 but not both
            List<int> listnoboth = new(Exercise1.Divide3or5butnotboth(newList,3,5));
            string msgnoboth = "";
            foreach (int ival in listnoboth)
            {
                if (msgnoboth != "")
                { //, not for first element
                    msgnoboth = msgnoboth + ",  " + ival;
                }
                else
                {
                    msgnoboth = ival.ToString();
                }
            }

            ViewBag.Listnoboth = msgnoboth;

            return View("Exercise1");

        }
    }
}

using System;
using System.Linq;
using EventSchedularProject.Models;

namespace EventSchedularProject.Data
{
    public class DbInitializer
    {
        public static void Initialize(EventSchedularContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Event.Any())
            {
                return;   // DB has been seeded
            }
        }

        }
}

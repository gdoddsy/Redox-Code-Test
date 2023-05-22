using System.ComponentModel.DataAnnotations;
using EventSchedularProject.Models;
using EventSchedularProject.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace EventSchedularProject.Models
{ 
    public class PreventDuplicateBooking : ValidationAttribute
    {
       
        public override string FormatErrorMessage(string name)
    {
        return "Event is already scheduled for this time";
    }

    protected override ValidationResult IsValid(object objValue,
                                                   ValidationContext validationContext)
    {
        var dateValue = objValue as DateTime? ?? new DateTime();

           
                  if (EventSchedularWithoutDB.AllEvents.Where(e => e.DateTime == dateValue).FirstOrDefault() != null)
                  {
                return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
                 }
           

            return ValidationResult.Success;
    }
    }
}

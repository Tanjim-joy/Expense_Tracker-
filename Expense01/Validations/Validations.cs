using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Expense01.Validations
{
    public class PastOneDayAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
                return new ValidationResult("Date is required.");

            var date = (DateTime)value;

            var oneDayAgo = DateTime.Now.AddDays(-2); //10

            DateTime dates = DateTime.Now;//11

            var onedays = dates.AddDays(1);//12


            if (date > oneDayAgo && date < onedays) //||
            {
                return ValidationResult.Success;
            }

            return new ValidationResult("Date must be one day past or future from the current date");
        }
    }
    public class FutOneDayAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
                return new ValidationResult("Date is required.");

            var date = (DateTime)value;

            var oneDay = DateTime.Now.AddDays(+1);

            if (oneDay < date) //||
            {
                return ValidationResult.Success;
            }

            return new ValidationResult("Date must be one day past from the current date.");
        }
    }




}

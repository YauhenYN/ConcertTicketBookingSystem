using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ConcertTicketBookingSystemAPI.CustomValidationAttributes
{
    public class BirthDateAttribute : ValidationAttribute
    {
        DateTime _min;
        DateTime _max;
        public BirthDateAttribute(int fromYear)
        {
            _min = new DateTime(fromYear, 1, 1).ToUniversalTime();
            _max = DateTime.Today.ToUniversalTime();
            ErrorMessage = "Дата за пределами допустимых значений: min - " + _min.ToString() + ", max - " + _max.ToString() + ".";
        }
        public override bool IsValid(object value)
        {
            DateTime? date = value as DateTime?;
            if (date.HasValue && date > _min && date < _max) return true;
            return false;
        }
    }
}

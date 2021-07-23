using System;
using System.ComponentModel.DataAnnotations;

namespace Vidly.Models
{
    public class Min18YearsIfMember : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var customer = validationContext.ObjectInstance as Customer;

            if (customer.MembershipTypeId == MembershipType.Unknown || customer.MembershipTypeId == MembershipType.PayAsYouGo)
                return ValidationResult.Success;

            if (customer.BirthDate == null)
                return new ValidationResult("Date of Birth is required.");

            var age = DateTime.Today.Year - customer.BirthDate.Value.Year;

            return age > 18 ? ValidationResult.Success : new ValidationResult("Customer should at least be 18 years old for a membership.");
        }
    }
}
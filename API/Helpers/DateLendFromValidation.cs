using System.ComponentModel.DataAnnotations;

namespace API.Helpers
{
    public sealed class DateLendFromValidation : ValidationAttribute
    {
        private const string _defaultErrorMessage = "The difference between lendFrom and lendTo must be 14 days";

        private DateTime nullDate = new DateTime(0001,01,01);
        private string _basePropertyName;

        public DateLendFromValidation() : base(_defaultErrorMessage)
        {
        }

        //Override default FormatErrorMessage Method
        public override string FormatErrorMessage(string name)
        {
            return string.Format(_defaultErrorMessage, name, _basePropertyName);
        }

        //Override IsValid
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            var thisDate = (DateTime)value;
            var nowDate  = DateTime.Now;

            if(thisDate.Equals(nullDate)){
                return null;
            }

            //Actual comparision
            if (nowDate.Date >= thisDate.Date)
            {
                var message = FormatErrorMessage(validationContext.DisplayName);
                return new ValidationResult(message);
            }

            return null;
        }
    }
}
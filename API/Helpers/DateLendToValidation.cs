using System.ComponentModel.DataAnnotations;

namespace API.Helpers
{
    public class DateLendToValidation : ValidationAttribute
    {
        private const string _defaultErrorMessage = "Date cannot be more than two weeks from today";

        private DateTime nullDate = new DateTime(0001,01,01);
        private string _basePropertyName;

        public DateLendToValidation(string basePropertyName) : base(_defaultErrorMessage)
        {
            _basePropertyName = basePropertyName;
        }

        //Override default FormatErrorMessage Method
        public override string FormatErrorMessage(string name)
        {
            return string.Format(_defaultErrorMessage, name, _basePropertyName);
        }

        //Override IsValid
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var basePropertyInfo = validationContext.ObjectType.GetProperty(_basePropertyName);
            var startDate = (DateTime)basePropertyInfo.GetValue(validationContext.ObjectInstance, null);
            var thisDate = (DateTime)value;

            
            if(thisDate.Equals(nullDate) && startDate.Equals(nullDate)){
                return null;
            }
            //Actual comparision
            if ((Convert.ToInt32((thisDate-startDate).TotalDays) != 14))
            {
                
                var message = FormatErrorMessage(validationContext.DisplayName);
                return new ValidationResult(message);
            }
            return null;
        }
    }
}
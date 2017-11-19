namespace CameraBazar.Data.Attributes
{
    using System.ComponentModel.DataAnnotations;

    public class DividableByAttribute : ValidationAttribute
    {
        public override string FormatErrorMessage(string name)
        {
            return $"The value of the field {name} is not dividable by {this.DividableValue}.";
        }

        public int DividableValue { get; set; }

        public override bool IsValid(object value)
        {
            if (value is int number)
            {
                return number % this.DividableValue == 0;
            }

            return base.IsValid(value);
        }
    }
}

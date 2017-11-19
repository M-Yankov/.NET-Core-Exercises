namespace CameraBazar.Data.Attributes
{
    using System.ComponentModel.DataAnnotations;

    public class DevidableByAttribute : ValidationAttribute
    {
        public int DevidableValue { get; set; }

        public override bool IsValid(object value)
        {
            if (value is int number)
            {
                return number % this.DevidableValue == 0;
            }

            return base.IsValid(value);
        }
    }
}

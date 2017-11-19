namespace CameraBazar.Data.Attributes
{
    using System.ComponentModel.DataAnnotations;

    public class TextRestrictAttribute : ValidationAttribute
    {
        private const int LowerBoundaryText = 'A';
        private const int UpperBoundaryText = 'Z';
        private const int LowerBoundaryDigits = '0';
        private const int UpperBoundaryDigits = '9';
        private const int DashCharCode = '-';

        // TODO: It will be better, if the you could configure the restrictions

        public override string FormatErrorMessage(string name)
        {
            return $"The field {name} should contains only upper letters, digits and dash (-)";
        }

        public override bool IsValid(object value)
        {
            if (value is string validationValue)
            {
                foreach (char symbol in validationValue)
                {
                    bool isUpperText = LowerBoundaryText <= symbol && symbol <= UpperBoundaryText;
                    bool isDidgit = LowerBoundaryDigits <= symbol && symbol <= UpperBoundaryDigits;
                    bool isDash = symbol == DashCharCode;

                    bool isValidValue = isUpperText || isDidgit || isDash;
                    if (isValidValue)
                    {
                        continue;
                    }

                    // No valid
                    return isValidValue;
                }
            }

            return base.IsValid(value);
        }
    }
}

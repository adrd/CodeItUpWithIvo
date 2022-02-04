﻿namespace CustomValidator
{
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public class ColorAttribute : ValidationAttribute
    {
        private readonly string[] colors;

        public ColorAttribute(params string[] colors) => this.colors = colors;

        public override bool IsValid(object value)
        {
            if (!(value is string valueAsString))
            {
                return true;
            }

            return this.colors.Any(c => c == valueAsString);
        }

        public override string FormatErrorMessage(string name)
        {
            var colorNames = string.Join(", ", this.colors);

            return $"The field {name} should be one of the following: {colorNames}.";
        }
    }
}

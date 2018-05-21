namespace Winl.AzureDevBox.Validations
{
    using System;

    internal sealed class StringParameterCheck : ParameterCheck<string>
    {
        public StringParameterCheck(string name, string value)
            : base(name, value)
        {
        }

        protected override void IntarnalNotNullOrEmptyOrWhitespace()
        {
            if (this.Value == null)
            {
                throw new ArgumentNullException(this.Name);
            }

            if (string.IsNullOrWhiteSpace(this.Value))
            {
                throw new ArgumentException(
                    $"Parameter '{this.Name}' value '{this.Value}' must not be empty or whitespace.",
                    this.Name);
            }
        }
    }
}
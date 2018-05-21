namespace Winl.AzureDevBox.Validations
{
    using System;
    using System.Diagnostics;
    using System.Reflection;
    using Winl.AzureDevBox.Extensions;

    internal class ParameterCheck<TValue> : IParameterCheck
    {
        private readonly string name;

        private readonly TValue value;

        public ParameterCheck(string name, TValue value)
        {
            this.name = name;
            this.value = value;
        }

        protected string Name => this.name;

        protected TValue Value => this.value;

        public IParameterCheck NotNull()
            => RunCheckAction(
                nameof(this.NotNull),
                () => {
                    if (this.Value == null)
                    {
                        throw new ArgumentNullException(this.Name);
                    }
                });

        public IParameterCheck NotNullOrEmptyOrWhitespace()
            => RunCheckAction(
                nameof(this.NotNullOrEmptyOrWhitespace),
                this.IntarnalNotNullOrEmptyOrWhitespace);

        protected virtual void IntarnalNotNullOrEmptyOrWhitespace()
        {
            throw new NotSupportedException();
        }

        private IParameterCheck RunCheckAction(
            string actionName,
            Action checkAction)
        {
            WriteTrace($"Checking action '{actionName}' for parameter '{this.Name}'.");

            try
            {
                checkAction();

                WriteTrace($"Check action '{actionName}' for parameter '{this.Name}' passed.");
            }
            catch (Exception ex)
            {
                WriteTrace($"Check action '{actionName}' for parameter '{this.Name}' failed, throwing exception: '{ex.GetDetail()}'.");
                throw;
            }

            return this;
        }

        private void WriteTrace(string message)
        {
            if (Checks.TraceEnabled)
            {
                Trace.WriteLine(message);
            }
        }
    }
}
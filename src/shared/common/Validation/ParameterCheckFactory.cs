namespace Winl.AzureDevBox.Validation
{
    using System;

    internal static class ParameterCheckFactory
    {
        public static IParameterCheck CreateInstance<TValue>(
            string name,
            TValue value)
        {
            Type valueType = typeof(TValue);

            if (valueType == typeof(string))
            {
                return new StringParameterCheck(
                    name,
                    (string)Convert.ChangeType(value, typeof(string)));
            }

            return new ParameterCheck<TValue>(name, value);
        }
    }
}
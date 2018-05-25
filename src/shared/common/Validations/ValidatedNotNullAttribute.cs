namespace Winl.AzureDevBox.Validations
{
    using System;

    /// <summary>
    /// Defines the workaround attribute to send signal to static analysis that
    /// we are really checking the argument.
    /// </summary>
    /// <remarks>
    /// Reference: https://esmithy.net/2011/03/15/suppressing-ca1062/
    /// </remarks>
    [AttributeUsage(AttributeTargets.Parameter)]
    public sealed class ValidatedNotNullAttribute : Attribute
    {
    }
}
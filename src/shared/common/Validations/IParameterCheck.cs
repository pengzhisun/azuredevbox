namespace Winl.AzureDevBox.Validations
{
    public interface IParameterCheck
    {
        IParameterCheck NotNull();

        IParameterCheck NotNullOrEmptyOrWhitespace();
    }
}
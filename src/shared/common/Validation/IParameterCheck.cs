namespace Winl.AzureDevBox.Validation
{
    public interface IParameterCheck
    {
        IParameterCheck NotNull();

        IParameterCheck NotNullOrEmptyOrWhitespace();
    }
}
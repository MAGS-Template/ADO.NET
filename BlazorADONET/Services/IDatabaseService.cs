namespace BlazorADONET.Services
{
    using Domain_Models;
    public interface IDatabaseService
    {
        List<Person> SearchPersons(string selectedOption, string searchValue);
        List<string> GetColumnNames();
    }
}

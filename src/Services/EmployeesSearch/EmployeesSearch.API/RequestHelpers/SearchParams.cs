namespace EmployeesSearch.API.RequestHelpers
{
    public class SearchParams
    {
        public string SearchTerm { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 4;
        public string Department { get; set; }
        public string Position { get; set; }
        public string OrderBy { get; set; }
        public string FilterBy { get; set; }
    }
}

namespace AddressBook.Application.Infrastructure.Pagination.Parameter
{
    public class PaginatedQueryParameter
    {
        private const int MaxPageSize = 1000000;
        private int _pageSize = 10;
        public int Page { get; set; } = 1;
        public int ItemsPerPage
        {
            get => _pageSize;

            set => _pageSize = value > MaxPageSize ? MaxPageSize : value;
        }
        public string? OrderField { get; set; }
        public string? Fields { get; set; }
        public string? SearchBy { get; set; }
    }
}

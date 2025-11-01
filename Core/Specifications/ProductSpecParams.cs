namespace Core.Specifications
{
    public class ProductSpecParams
    {
        private const int MaxPageSize = 15;
        public int PageIndex { get; set; } = 1;
        private int _pageSize = 5;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }

        public string? Sort { get; set; }

        public int? TypeId { get; set; }

        public int? BrandId { get; set; }

        private string? _search;
        public string? Search
        {
            get => _search;
            set => _search = !string.IsNullOrEmpty(value) ? value.ToLower() : null;
        }

    }
}
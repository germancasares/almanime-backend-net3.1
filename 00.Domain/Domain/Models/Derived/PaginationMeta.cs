namespace Domain.Models.Derived
{
    public class PaginationMeta
    {
        public string BaseUrl { private get; set; }
        public int PageSize { private get; set; }
        public int CurrentPage { private get; set; }
        public int Count { get; set; }
        public string First => $"{BaseUrl}?page=1&size={PageSize}";
        public string Previous => CurrentPage == 1 ? null : $"{BaseUrl}?page{CurrentPage - 1}&size={PageSize}";
        public string Next => (CurrentPage + 1) * PageSize > Count ? null : $"{BaseUrl}?page={CurrentPage + 1}&size={PageSize}";
        public string Last => $"{BaseUrl}?page={Count / PageSize}&size={PageSize}";
    }
}

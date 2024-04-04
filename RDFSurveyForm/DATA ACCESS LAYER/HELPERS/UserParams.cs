namespace RDFSurveyForm.DATA_ACCESS_LAYER.HELPERS
{
    public class UserParams
    {
        private const int MaxPageSize = 2000;
        public int PageNumber { get; set; } = 1;
        private int _pageSize = 50;

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }
    }
}

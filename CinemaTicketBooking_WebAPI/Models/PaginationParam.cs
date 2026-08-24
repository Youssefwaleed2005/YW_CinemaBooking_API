namespace CinemaTicketBooking_WebAPI.Models
{
    public class PaginationParam
    {
        private const int MaxPageSize = 100;
        private int _pageSize = 20;
        private int _page = 1;

        public int Page
        {
            get { return _page; }
            set { _page = value < 1 ? 1 : value; }
        }

        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = value < 1 ? 20 : (value > MaxPageSize ? MaxPageSize : value); }
        }
    }
}
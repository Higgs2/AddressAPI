namespace AddressAPI.Entities.Pagination
{
    /// <summary>
    /// Constraints for when creating paginated responses.
    /// </summary>
    public class PaginationFilter
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public PaginationFilter()
        {
            this.PageNumber = 1;
            this.PageSize = 10;
        }

        public PaginationFilter(int PageNumber, int PageSize)
        {
            this.PageNumber = PageNumber < 1 ? 1 : PageNumber;
            this.PageSize = PageSize > 10 ? 10 : PageSize;
        }
    }
}

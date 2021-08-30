using System;

namespace CollectionApp.DAL.DTO
{
    public class Page
    {
        public int PageNumber { get; set; }
        public int TotalPages { get; set; }

        public int PageSize { get; set; }

        public Page(int count, int pageNumber, int pageSize)
        {
            PageSize = pageSize;
            PageNumber = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        }

        public bool HasPreviousPage { 
            get
            {
                return PageNumber > 1;
            }
        }

        public bool HasNextPage { 
            get
            {
                return PageNumber < TotalPages;
            }
        }
    }
}

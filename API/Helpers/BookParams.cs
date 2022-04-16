using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Enums;

namespace API.Helpers
{
    public class BookParams
    {
         private const int MaxPageSize = 50;
        public int PageNumber { get; set; } = 1;
        private int _pageSize = 5;

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }

        public string Title { get; set; } = "";
        public string Author  { get; set; } = "";

        public BookSearchEnum bookSearchEnum { get; set; }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Wrappers
{
    public class PagedResponse<T> :Response<T>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }


        public PagedResponse(int pageNumber, int pageSize, T data)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            Data = data;
            Message = null;
            Succeeded = true;
            Errors = null;
        }
    }
}

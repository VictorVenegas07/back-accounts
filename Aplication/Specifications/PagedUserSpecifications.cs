using Ardalis.Specification;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Specifications
{
    public class PagedUserSpecifications: Specification<User>
    {
        public PagedUserSpecifications(int pageSize, int pageNumber, string name, string email)
        {
            Query.Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);
            if (!string.IsNullOrEmpty(name))
                Query.Search(x => x.Name, "%" + name + "%");

            if(!string.IsNullOrEmpty(email))
                Query.Search(x => x.Email, "%" + email + "%");

        }

    }
}

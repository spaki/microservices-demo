using System;
using System.Collections.Generic;
using System.Linq;

namespace MSD.Sales.Domain.Dtos.Common
{
    public class PagedResult<T>
    {
        private List<T> listOfItems = null;

        public PagedResult(int page, int totalPages, IEnumerable<T> items)
        {
            Page = page;
            TotalPages = totalPages;
            Items = items;
        }

        public int Page { get; set; }
        public int TotalPages { get; set; }
        public IEnumerable<T> Items { get; set; }

        /// <summary>
        /// List has some easy stuff to use, such as ForEach method.
        /// This method generates an instace of List, on the first call. 
        /// This way it will avoid to return new List instance every call.
        /// </summary>
        public List<T> GetItemsList()
        {
            if (listOfItems == null)
                listOfItems = Items?.ToList();

            return listOfItems;
        }

        public PagedResult<TResult> To<TResult>(Func<T, TResult> conversion) => Items != null ? new PagedResult<TResult>(Page, TotalPages, Items.Select(conversion)) : new PagedResult<TResult>(Page, TotalPages, null);
    }
}

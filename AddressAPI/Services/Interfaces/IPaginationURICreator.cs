using AddressAPI.Entities;
using AddressAPI.Entities.Pagination;
using System;

namespace AddressAPI.Services.Interfaces
{
    public interface IPaginationURICreator
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="route"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public Uri GetPageUri(PaginationFilter filter, string route, object parameters);
    }
}

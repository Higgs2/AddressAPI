using AddressAPI.Entities;
using AddressAPI.Entities.Pagination;
using AddressAPI.Services.Interfaces;
using System;
using System.Collections.Generic;

namespace AddressAPI.Utilities
{
    /// <summary>
    /// Utiliy methods to manage paginated responses.
    /// </summary>
    public class PaginationUtilities
    {
        /// <summary>
        /// Creates a paginated response. Will return no pagination at all if the amount of records is less than the supplied pageSize.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pagedData"></param>
        /// <param name="validFilter"></param>
        /// <param name="totalRecords"></param>
        /// <param name="uriService"></param>
        /// <param name="route"></param>
        /// <param name="queryParameters"></param>
        /// <returns></returns>
        public static PagedResponse<List<T>> CreatePagedReponse<T>(List<T> pagedData, PaginationFilter validFilter, int totalRecords, IPaginationURICreator uriService, string route, object queryParameters)
        {
            var response = new PagedResponse<List<T>>(pagedData, validFilter.PageNumber, validFilter.PageSize);
            var totalPages = ((double)totalRecords / (double)validFilter.PageSize);
            int roundedTotalPages = Convert.ToInt32(Math.Ceiling(totalPages));
            if (totalRecords <= validFilter.PageSize)
            {
                response.FirstPage = null;
                response.LastPage = null;
                response.TotalPages = roundedTotalPages;
                response.TotalRecords = totalRecords;
                return response;
            }
            response.NextPage =
                validFilter.PageNumber >= 1 && validFilter.PageNumber < roundedTotalPages
                ? uriService.GetPageUri(new PaginationFilter(validFilter.PageNumber + 1, validFilter.PageSize), route, queryParameters)
                : null;
            response.PreviousPage =
                validFilter.PageNumber - 1 >= 1 && validFilter.PageNumber <= roundedTotalPages
                ? uriService.GetPageUri(new PaginationFilter(validFilter.PageNumber - 1, validFilter.PageSize), route, queryParameters)
                : null;

            response.FirstPage = uriService.GetPageUri(new PaginationFilter(1, validFilter.PageSize), route, queryParameters);
            response.LastPage = uriService.GetPageUri(new PaginationFilter(roundedTotalPages, validFilter.PageSize), route, queryParameters);
            response.TotalPages = roundedTotalPages;
            response.TotalRecords = totalRecords;
            return response;
        }
    }
}

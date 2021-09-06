using AddressAPI.Entities;
using Microsoft.AspNetCore.WebUtilities;
using System;
using AddressAPI.Services.Interfaces;
using System.Reflection;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Web;
using AddressAPI.Entities.Pagination;

namespace AddressAPI.Services
{
    public class PaginationURICreator : IPaginationURICreator
    {
        private readonly string _baseUri;

        /// <summary>
        /// Creates a PaginationURI creator with a baseURi.
        /// </summary>
        /// <param name="baseUri"></param>
        public PaginationURICreator(string baseUri)
        {
            _baseUri = baseUri;
        }

        /// <summary>
        /// Processes the requested route and it's query parameters to generate HATEOAS-style links.
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="route"></param>
        /// <param name="queryParameters"></param>
        /// <returns></returns>
        public Uri GetPageUri(PaginationFilter filter, string route, object queryParameters)
        {
            var parameters = GetParameters(queryParameters);
            var modifiedUri = new StringBuilder();
            var baseUri = new Uri(string.Concat(_baseUri, route));
            foreach (var parameter in parameters)
            {
                if (parameter.Value != null)
                    baseUri = AddParameter(baseUri, parameter.Key, parameter.Value.ToString());
            }
            baseUri = AddParameter(baseUri, "pageNumber", filter.PageNumber.ToString());
            baseUri = AddParameter(baseUri, "pageSize", filter.PageSize.ToString());
            return baseUri;
        }

        /// <summary>
        /// Retrieves the parameters from the supplied data object.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="bindingAttr"></param>
        /// <returns></returns>
        private Dictionary<string, object> GetParameters(object obj, BindingFlags bindingAttr = BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance)
        {
            return obj.GetType().GetProperties(bindingAttr).ToDictionary
            (
                propInfo => propInfo.Name,
                propInfo => propInfo.GetValue(obj, null)
            );
        }

        /// <summary>
        /// Adds the specified parameter to the Query String.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="paramName">Name of the parameter to add.</param>
        /// <param name="paramValue">Value for the parameter to add.</param>
        /// <returns>Url with added parameter.</returns>
        private Uri AddParameter(Uri url, string paramName, string paramValue)
        {
            var uriBuilder = new UriBuilder(url);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query[paramName] = paramValue;
            uriBuilder.Query = query.ToString();

            return uriBuilder.Uri;
        }
    }
}

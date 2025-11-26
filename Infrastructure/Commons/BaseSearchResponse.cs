using Domain.Commons;
using Elastic.Clients.Elasticsearch;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;
using Z.EntityFramework.Plus;

namespace Infrastructure.Commons
{
    public class BaseSearchResponse<TModel>
    {
        private List<TModel> _data;
        private static ElasticsearchClient? _elasticClient;

        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPage => PageSize > 0 ? TotalCount / PageSize + ((TotalCount % PageSize > 0) ? 1 : 0) : 0;
        public IEnumerable<TModel> Data => _data;

        public BaseSearchResponse(ElasticsearchClient elasticsearchClient)
        {
            _data = new List<TModel>();
            _elasticClient = elasticsearchClient;
        }

        public BaseSearchResponse(int totalCount, int pageSize, int pageIndex, IEnumerable<TModel> data)
        {
            TotalCount = totalCount;
            PageSize = pageSize;
            PageIndex = pageIndex;
            _data = new List<TModel>(data);
        }

        public static async Task<BaseSearchResponse<TModel>> GetResponse(IQueryable<TModel> queryData, BaseCriteria baseCriteria)
        {
            var sort = !string.IsNullOrEmpty(baseCriteria.Sorts) ? string.Join(",", baseCriteria.Sorts).Replace("=", " ") : "Id DESC";

            if (!string.IsNullOrEmpty(baseCriteria.QueryString)) queryData = queryData.Where(baseCriteria.QueryString);

            var totalCount = queryData.DeferredCount().FutureValue();
            var query = queryData.OrderBy(sort).Skip((baseCriteria.PageIndex -1) * baseCriteria.PageSize).Take(baseCriteria.PageSize).Future();

            var result = await query.ToListAsync();

            return new BaseSearchResponse<TModel>(totalCount, baseCriteria.PageSize, baseCriteria.PageIndex, result);
        }

        public static BaseSearchResponse<TModel> GetResponseNoPagination(IEnumerable<TModel> data, BaseCriteria baseCriteria)
        {
            var totalCount = data.Count(); // Đếm tổng số bản ghi
            var result = data.ToList(); // Chuyển đổi thành danh sách nếu cần

            return new BaseSearchResponse<TModel>(totalCount, 0, 0, result);
        }

        public static async Task<BaseSearchResponse<TModel>> GetResponseWithElasticSearch(IQueryable<TModel> queryData, BaseCriteria baseCriteria)
        {
            var sort = !string.IsNullOrEmpty(baseCriteria.Sorts) ? string.Join(",", baseCriteria.Sorts).Replace("=", " ") : "Id DESC";

            if (!string.IsNullOrEmpty(baseCriteria.QueryString)) queryData = queryData.Where(baseCriteria.QueryString);

            var totalCount = queryData.DeferredCount().FutureValue();

            var searchRequest = new SearchRequest(typeof(TModel).Name.ToLower())
            {
                From = (baseCriteria.PageIndex - 1) * baseCriteria.PageSize,
                Size = baseCriteria.PageSize,
                Query = ElasticSearch.ConvertToElasticQuery(baseCriteria.QueryString),
                Sort = ElasticSearch.ParseSort(sort)
            };

            var query = await _elasticClient.SearchAsync<TModel>(searchRequest);

            var result = query.Documents.ToList();

            return new BaseSearchResponse<TModel>(totalCount, baseCriteria.PageSize, baseCriteria.PageIndex, result);
        }
    }
}

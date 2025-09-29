using Domain.Commons;
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
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPage => PageSize > 0 ? TotalCount / PageSize + ((TotalCount % PageSize > 0) ? 1 : 0) : 0;
        public IEnumerable<TModel> Data => _data;

        public BaseSearchResponse()
        {
            _data = new List<TModel>();
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
            var query = queryData.OrderBy(sort).Skip(baseCriteria.PageIndex * baseCriteria.PageSize).Take(baseCriteria.PageSize).Future();

            var result = await query.ToListAsync();

            return new BaseSearchResponse<TModel>(totalCount, baseCriteria.PageSize, baseCriteria.PageIndex, result);
        }

        public static BaseSearchResponse<TModel> GetResponseNoPagination(IEnumerable<TModel> data, BaseCriteria baseCriteria)
        {
            var totalCount = data.Count(); // Đếm tổng số bản ghi
            var result = data.ToList(); // Chuyển đổi thành danh sách nếu cần

            return new BaseSearchResponse<TModel>(totalCount, 0, 0, result);
        }
    }
}

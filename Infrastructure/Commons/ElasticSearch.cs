using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.QueryDsl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Infrastructure.Commons
{
    public class ElasticSearch
    {
        public static Query ConvertToElasticQuery(string sqlLikeQuery)
        {
            if (string.IsNullOrWhiteSpace(sqlLikeQuery))
                return new MatchAllQuery();

            // Chỉ parse đơn giản dạng: (Field="Value")
            // 1=1 sẽ bỏ qua
            var conditions = Regex.Matches(sqlLikeQuery, @"(\w+)=""([^""]+)""")
                .Cast<Match>()
                .Select(m => (Query)new TermQuery(new Field(m.Groups[1].Value))
                {
                    Value = m.Groups[2].Value
                })
                .ToList();

            if (conditions.Count == 0)
                return new MatchAllQuery();

            // Nối bằng AND
            if (conditions.Count == 1)
                return conditions[0];

            return new BoolQuery
            {
                Must = conditions
            };
        }

        public static List<SortOptions> ParseSort(string sortString)
        {
            var sorts = new List<SortOptions>();

            foreach (var part in sortString.Split(','))
            {
                var tokens = part.Trim().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (tokens.Length == 0) continue;

                var fieldName = tokens[0];
                var order = tokens.Length > 1 && tokens[1].ToUpper() == "DESC"
                    ? SortOrder.Desc
                    : SortOrder.Asc;

                sorts.Add(SortOptions.Field(new Field(fieldName), new FieldSort { Order = order }));
            }

            return sorts;
        }
    }
}

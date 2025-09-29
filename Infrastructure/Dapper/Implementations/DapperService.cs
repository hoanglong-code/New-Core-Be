using Dapper;
using Infrastructure.Dapper.Abstractions;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Dapper.Implementations
{
    public class DapperService : IDapperService, IDisposable
    {
        private IDbConnection? connection;
        private IDbTransaction? transaction;
        private readonly IConfiguration _configuration;

        public DapperService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IDbTransaction? Transaction => transaction;

        public IDbTransaction BeginTransaction()
        {
            if (connection == null)
            {
                connection = CreateConnection();
            }
            connection.Open();
            if (transaction == null)
            {
                transaction = connection.BeginTransaction();
            }
            return transaction;
        }

        public IDbConnection CreateConnection()
        {
            if (connection == null)
            {
                connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            }
            return connection;
        }

        public void Commit()
        {
            transaction?.Commit();
            transaction = null;
            connection?.Close();
            connection = null;
        }

        public void Rollback()
        {
            transaction?.Rollback();
            transaction = null;
            connection?.Close();
            connection = null;
        }

        private bool disposed;

        public void Dispose()
        {
            if (!disposed)
            {
                transaction?.Dispose();
                connection?.Dispose();

                transaction = null;
                connection = null;
                disposed = true;
            }

            GC.SuppressFinalize(this);
        }
    }

    /// <summary>
    /// Fix lỗi lưu DateTime bị mất DateTimeKind, tương tự như efcore
    /// </summary>
    public class LocalDateTimeHandler : SqlMapper.TypeHandler<DateTime>
    {
        public override void SetValue(IDbDataParameter parameter, DateTime value)
        {
            // Khi lưu xuống DB, đảm bảo lưu dưới dạng Local
            parameter.Value = DateTime.SpecifyKind(value, DateTimeKind.Local);
        }

        public override DateTime Parse(object value)
        {
            var date = (DateTime)value;

            return date.Kind switch
            {
                DateTimeKind.Local => date,
                DateTimeKind.Utc => date.ToLocalTime(),
                DateTimeKind.Unspecified => DateTime.SpecifyKind(date, DateTimeKind.Local),
                _ => throw new InvalidTimeZoneException($"Unsupported DateTimeKind: {date.Kind}")
            };
        }
    }
    /// <summary>
    /// Fix lỗi lưu DateTime bị mất DateTimeKind, tương tự như efcore
    /// </summary>
    public class NullableLocalDateTimeHandler : SqlMapper.TypeHandler<DateTime?>
    {
        public override void SetValue(IDbDataParameter parameter, DateTime? value)
        {
            parameter.Value = value.HasValue
                ? DateTime.SpecifyKind(value.Value, DateTimeKind.Local)
                : DBNull.Value;
        }

        public override DateTime? Parse(object value)
        {
            if (value is DBNull) return null;

            var date = (DateTime)value;
            return date.Kind switch
            {
                DateTimeKind.Local => date,
                DateTimeKind.Utc => date.ToLocalTime(),
                DateTimeKind.Unspecified => DateTime.SpecifyKind(date, DateTimeKind.Local),
                _ => throw new InvalidTimeZoneException($"Unsupported DateTimeKind: {date.Kind}")
            };
        }
    }
}

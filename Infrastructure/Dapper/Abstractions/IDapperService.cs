using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Dapper.Abstractions
{
    public interface IDapperService
    {
        IDbConnection CreateConnection();
        IDbTransaction? Transaction { get; }
        IDbTransaction BeginTransaction();
        void Commit();
        void Rollback();
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IReponsitories.Base
{
    public interface IUnitOfWork
    {
        IDisposable BeginTransaction(IsolationLevel level);
        void CommitChanges();
        Task CommitChangesAsync();
        void CommitTransaction();
        void RollbackTransaction();
    }
}

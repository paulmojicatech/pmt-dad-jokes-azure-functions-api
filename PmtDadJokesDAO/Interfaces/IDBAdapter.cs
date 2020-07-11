using System.Collections.Generic;
using PmtDadJokesDAO.Models;

namespace PmtDadJokesDAO.Interfaces
{
    public interface IDBAdapter
    {
        IEnumerable<object>[] Get(QueryParameter[] parameters);
        ResultStatus Update(QueryParameter[] parameters);
        ResultStatus Add(QueryParameter[] parameters);
        ResultStatus Delete(QueryParameter[] paramters);
    }
}

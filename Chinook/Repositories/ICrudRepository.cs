using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chinook.Repositories
{
    internal interface ICrudRepository <T, ID>
    {
        IEnumerable<T> GetAll();
        IEnumerable<T> GetById(ID id);
        int Add(T obj);
        int Update(T obj);
        int Delete(ID obj);
    }
}

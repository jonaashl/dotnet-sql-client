using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chinook.Repositories
{
    internal interface ICrudRepository <T, ID>
    {
        List<T> GetAll();
        T GetById(ID id);
        void Add(T obj);
        int Update(T obj);
        int Delete(ID id);
    }
}

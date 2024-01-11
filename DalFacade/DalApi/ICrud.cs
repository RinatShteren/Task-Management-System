using DO;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalApi
{
    public interface ICrud<T> where T : class
    {
        int Create(T item); 
        T? Read(int id);
        T? Read(Func<T, bool> filter);
        IEnumerable<T?> ReadAll(Func<T, bool>? p); 
        void Update(T item);
        void Delete(int id);

    }

}

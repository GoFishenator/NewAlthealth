using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltHealth.Application.Respository
{
    public interface IManager<T> where T : class
    {
        List<T> GetAll();
        T FindBy(int id);
        void Add(T entity);
        void Delete(T entity);
        void Edit(T entity);
    }
}

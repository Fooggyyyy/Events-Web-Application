using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Domain.Interfaces
{
    //Сильно не понял, что выносить в базовый репозиторий
    //Это единственный общий метод между User и Event
    public interface IBaseRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
    }
}

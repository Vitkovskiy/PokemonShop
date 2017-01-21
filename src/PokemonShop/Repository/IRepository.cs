using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Org.BouncyCastle.Security;
using PokemonShop.Domain;

namespace PokemonShop.Repository
{
    public interface IRepository<T> where T : BaseEntity
    {
        IQueryable<T> Set { get; }
        T Get(long id);
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
        IEnumerable<T> GetAll();
    }
}

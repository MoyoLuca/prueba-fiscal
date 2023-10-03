using DataModels.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DataModels.Services
{
    /// <summary>
    /// DatabaConnService - Clase para la conexion a la base de datos por medio de Entity Framework
    /// </summary>
    public class DatabaseConnService<T> where T : class, IModelEntity
    {
        private readonly DbContext _context;

        public DatabaseConnService(DbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Add - Método para agregar una entidad es decir (SaveChanges)
        /// </summary>
        /// <param name="entity"></param>
        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
        }

        /// <summary>
        /// GetByGUID - Método para obtener una entidad por su Id
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public T? GetByGUID(string guid)
        {
            //convert guid into GUID object
            var guidObj = new Guid(guid);

            return _context.Set<T>().SingleOrDefault(entity => entity.IdAddenda == guidObj);
        }

        /// <summary>
        /// GetAll - Método para obtener todas las entidades
        /// </summary>
        /// <returns></returns>
        public List<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        /// <summary>
        /// Update - Método para actualizar una entidad
        /// </summary>
        /// <param name="entity"></param>
        public void Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        /// <summary>
        /// Delete - Método para eliminar una entidad por su Id
        /// </summary>
        /// <param name="guid"></param>
        public bool Delete(string guid)
        {
            var entity = GetByGUID(guid);
            if (entity != null)
            {

                //Set Estado to false
                entity.Estado = false;

                this.Update(entity);
                return true;
            }

            return false;
        }

        /// <summary>
        /// FindList - Método para buscar una lista de entidades que cumplan con un predicado
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public List<T> FindList(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().Where(predicate).ToList();
        }

        /// <summary>
        /// Find - Método para buscar una entidad que cumpla con un predicado
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public T? Find(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().Where(predicate).FirstOrDefault();
        }
    }
}
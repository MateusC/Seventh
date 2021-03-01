using Microsoft.EntityFrameworkCore;
using Seventh.DGuard.Domain.Core;
using Seventh.DGuard.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Seventh.DGuard.Infra.Data.Sql.Repositories
{
    public abstract class Repository<T> : IRepository<T> where T : Entity
    {
        protected readonly DGuardContext _context;

        protected readonly DbSet<T> _set;

        public Repository(DGuardContext context)
        {
            _context = context;

            _set = _context.Set<T>();
        }

        public T Add(T entity)
        {
            return _set.Add(entity).Entity;
        }

        public virtual bool Delete(Guid id)
        {
            var dbEntry = _set.Find(id);

            if (dbEntry is null)
                return false;

            _set.Remove(dbEntry);

            return true;
        }

        public T Get(Guid id)
        {
            return _set.Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            return _set.ToList();
        }

        public virtual void Update(T entity)
        {
            _set.Update(entity);
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() > 0;
        }

        public async Task<bool> SaveChangesAsync()
        {
            try
            {
                var result = await _context.SaveChangesAsync();

                return result > 0;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                foreach (var entry in ex.Entries)
                {
                    if (entry.Entity is Video)
                    {
                        var proposedValues = entry.CurrentValues;
                        var databaseValues = entry.GetDatabaseValues();

                        foreach (var property in proposedValues.Properties)
                        {
                            var proposedValue = proposedValues[property];
                            var databaseValue = databaseValues[property];

                            // TODO: decide which value should be written to database
                            // proposedValues[property] = <value to be saved>;
                        }

                        // Refresh original values to bypass next concurrency check
                        entry.OriginalValues.SetValues(databaseValues);
                    }
                    else
                    {
                        throw new NotSupportedException(
                            "Don't know how to handle concurrency conflicts for "
                            + entry.Metadata.Name);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

            return false;
            
        }
    }
}

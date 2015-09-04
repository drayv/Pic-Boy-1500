using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using PicBoy.Core.Models;

namespace PicBoy.Core.DataAccess
{
    /// <summary>
    /// Represent entity framework repository pattern.
    /// </summary>
    public class EventRepository : IDisposable
    {
        /// <summary>
        /// Database context.
        /// </summary>
        private DbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventRepository"/> class.
        /// </summary>
        public EventRepository()
        {
            _context = new PicBoyContainer();
        }

        /// <summary>
        /// Inserts the event.
        /// </summary>
        /// <param name="entity">The event.</param>
        public void Insert(Event entity)
        {
            _context.Entry(entity).State = EntityState.Added;
        }

        /// <summary>
        /// Gets entities from database.
        /// </summary>
        /// <param name="whereProperties">Where predicate.</param>
        /// <param name="includeProperties">Include properties.</param>
        /// <returns>The list of entities.</returns>
        public List<Event> GetAll(Expression<Func<Event, bool>> whereProperties = null,
            params Expression<Func<Event, object>>[] includeProperties)
        {
            var dbSet = _context.Set<Event>();
            return AggregateQueryProperties(dbSet.AsNoTracking(), whereProperties, includeProperties).ToList();
        }

        /// <summary>
        /// Finds event by id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>The event <see cref="Event"/>.</returns>
        public Event GetById(int id)
        {
            return _context.Set<Event>().Find(id);
        }

        /// <summary>
        /// Updates event in database.
        /// </summary>
        /// <param name="entityToUpdate">Event to update.</param>
        public void Update(Event entityToUpdate)
        {
            _context.Entry(entityToUpdate).State = EntityState.Modified;
        }

        /// <summary>
        /// Deletes event from database by id.
        /// </summary>
        /// <param name="id">Event id.</param>
        public void Delete(int id)
        {
            var dbSet = _context.Set<Event>();
            var entity = new Event { Id = id };
            dbSet.Attach(entity);
            dbSet.Remove(entity);
        }

        /// <summary>
        /// Deletes event from database.
        /// </summary>
        /// <param name="entityToDelete">Entity to delete.</param>
        public void Delete(Event entityToDelete)
        {
            _context.Set<Event>().Attach(entityToDelete);
            _context.Set<Event>().Remove(entityToDelete);
        }

        /// <summary>
        /// Saves all pending changes.
        /// </summary>
        /// <returns>The number of objects in an Added, Modified, or Deleted state.</returns>
        public int Save()
        {
            return _context.SaveChanges();
        }

        /// <summary>
        /// Makes query to table by using "where" predicate and "include(join) properties".
        /// </summary>
        /// <typeparam name="TSource">Entity type.</typeparam>
        /// <param name="table">Entity framework table.</param>
        /// <param name="whereProperties">Where predicate.</param>
        /// <param name="includeProperties">Include properties.</param>
        /// <returns></returns>
        private static IEnumerable<TSource> AggregateQueryProperties<TSource>(IQueryable<TSource> table, Expression<Func<TSource, bool>> whereProperties = null,
            params Expression<Func<TSource, object>>[] includeProperties) where TSource : Event
        {
            var query = includeProperties.Aggregate(table, (current, includeProperty) => current.Include(includeProperty));

            if (whereProperties != null)
            {
                query = query.Where(whereProperties);
            }

            return query;
        }

        /// <summary>
        /// Disposes the current object.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Disposes all external resources.
        /// </summary>
        /// <param name="disposing">The dispose indicator.</param>
        private void Dispose(bool disposing)
        {
            if (!disposing) return;
            if (_context == null) return;
            _context.Dispose();
            _context = null;
        }
    }
}
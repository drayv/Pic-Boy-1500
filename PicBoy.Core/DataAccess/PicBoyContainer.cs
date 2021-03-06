﻿using System;
using System.Data.Entity;
using PicBoy.Core.Models;

namespace PicBoy.Core.DataAccess
{
    /// <summary>
    /// Represents a Unit Of Work with PicBoy database.
    /// </summary>
    public class PicBoyContainer : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PicBoyContainer"/> class.
        /// </summary>
        public PicBoyContainer()
            : base("name=PicBoy")
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChangesAndSeed<PicBoyContainer>());
        }

        /// <summary>
        /// Gets or sets the events.
        /// </summary>
        public DbSet<Event> Events { get; set; }

        /// <summary>
        /// Configure model with fluent API.
        /// </summary>
        /// <param name="modelBuilder">The model builder.</param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Event>().HasKey(f => f.Id);
            modelBuilder.Entity<Event>().Property(e => e.Name).HasMaxLength(50);
        }
    }

    /// <summary>
    /// Creates database with test data if not exist.
    /// </summary>
    /// <typeparam name="TContext">Database context.</typeparam>
    public class DropCreateDatabaseIfModelChangesAndSeed<TContext> : DropCreateDatabaseIfModelChanges<TContext> where TContext : DbContext
    {
        /// <summary>
        /// Database context.
        /// </summary>
        private DbContext _context;

        /// <summary>
        /// Creates database with test data event.
        /// </summary>
        /// <param name="context">PicBoy database context <see cref="PicBoyContainer"/>.</param>
        protected override void Seed(TContext context)
        {
            base.Seed(context);
            _context = context;

            var currentTime = DateTime.Now;
            currentTime = currentTime.AddMilliseconds(-currentTime.Millisecond);

            Insert(new Event { DateBegin = currentTime, DateEnd = currentTime.AddMinutes(25), Name = "Test this new and amazing PicBoy device." });
        }

        /// <summary>
        /// Inserts the event.
        /// </summary>
        /// <param name="entity">The event.</param>
        public void Insert(Event entity)
        {
            _context.Entry(entity).State = EntityState.Added;
        }
    }
}
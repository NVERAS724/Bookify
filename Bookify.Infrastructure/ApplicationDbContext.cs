﻿using Bookify.Application.Abstractions.Clock;
using Bookify.Domain.Abstractions;
using Bookify.Infrastructure.Outbox;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookify.Infrastructure
{
    public sealed class ApplicationDbContext : DbContext, IUnitOfWork
    {
        private readonly IDateTimeProvider _dateTimeProvider;
        private static readonly JsonSerializerSettings JsonSerializerSettings = new()
        {
            TypeNameHandling = TypeNameHandling.All
        };
        public ApplicationDbContext(DbContextOptions options,
            //IPublisher publisher,
            IDateTimeProvider dateTimeProvider) : base(options)
        {
            //_publisher = publisher;
            _dateTimeProvider = dateTimeProvider;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {

            try
            {
                AddDomainEventsAsOutboxMessages();
                var result = await base.SaveChangesAsync(cancellationToken);


                return result;
            }
            catch (DbUpdateConcurrencyException ex)
            {

                throw new DbUpdateConcurrencyException("Concurrency exception occured", ex);
            }


        }


        private void AddDomainEventsAsOutboxMessages()
        {
            var outboxMessages = ChangeTracker
                .Entries<Entity>()
                .Select(entry => entry.Entity)
                .SelectMany(entity =>
                {
                    var domainEvents = entity.GetDomainEvents();

                    entity.ClearDomainEvents();

                    return domainEvents;
                })
                .Select(domainEvent => new OutboxMessage(
                    Guid.NewGuid(),
                    _dateTimeProvider.UtcNow,
                    domainEvent.GetType().Name,
                    JsonConvert.SerializeObject(domainEvent, JsonSerializerSettings)))
                .ToList();

            AddRange(outboxMessages);
        }
    }
}

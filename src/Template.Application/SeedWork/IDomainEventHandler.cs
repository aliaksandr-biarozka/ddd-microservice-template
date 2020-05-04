﻿using System.Threading.Tasks;
using Template.Domain.SeedWork;

namespace Template.Application.SeedWork
{
    internal interface IDomainEventHandler<T> where T : DomainEvent
    {
        Task Handle(T @event);
    }
}

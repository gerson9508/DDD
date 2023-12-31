﻿using MediatR;

namespace _360.API.Domain.Base
{
   public abstract class BaseDomainEvent : INotification
   {
      public BaseDomainEvent()
      {
         EventId = Guid.NewGuid();
         CreatedOn = DateTime.UtcNow;
      }

      public virtual Guid EventId { get; init; }
      public virtual DateTime CreatedOn { get; init; }
   }
}

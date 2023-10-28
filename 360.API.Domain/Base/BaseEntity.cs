namespace _360.API.Domain.Base
{
   public abstract class BaseEntity
   {
#pragma warning disable CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.
      private readonly List<BaseDomainEvent> _events;
#pragma warning restore CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.
      public IReadOnlyList<BaseDomainEvent> Events => _events.AsReadOnly();

      protected void AddEvent(BaseDomainEvent @event)
      {
         _events.Add(@event);
      }

      protected void RemoveEvent(BaseDomainEvent @event)
      {
         _events.Remove(@event);
      }
   }

   public abstract class BaseEntity<TKey> : BaseEntity
   {
      public TKey? Id { get; set; }
   }
}

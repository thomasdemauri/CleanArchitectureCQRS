using EventBus.Abstractions;
using EventBus.Events;

namespace EventBus
{
    public class SubscriptionInfo
    {
        public Type HandlerType { get; }

        public SubscriptionInfo(Type handlerType)
        {
            HandlerType = handlerType;
        }
    }

    public interface IEventBusSubscriptionsManager
    {
        public void AddSubscription<T, TH>() where T : IntegrationEvent where TH : IIntegrationEventHandler<T>;
        public bool HasSubscriptionsForEvent<T>() where T : IntegrationEvent;
        public bool HasSubscriptionsForEvent(string eventName);
        public IEnumerable<SubscriptionInfo> GetHandlersForEvent(string eventName);
        public Type GetEventTypeByName(string eventName);
    }

    public class InMemoryEventBusSubscriptionsManager : IEventBusSubscriptionsManager
    {
        private readonly Dictionary<string, List<SubscriptionInfo>> _handlers;
        private readonly List<Type> _eventTypes;

        public InMemoryEventBusSubscriptionsManager()
        {
            _handlers = new Dictionary<string, List<SubscriptionInfo>>();
            _eventTypes = new List<Type>();
        }

        public void AddSubscription<T, TH>() where T : IntegrationEvent where TH : IIntegrationEventHandler<T>
        {
            var eventName = GetEventKey<T>();

            if (!_eventTypes.Contains(typeof(T)))
            {
                _eventTypes.Add(typeof(T));
            }

            if (!HasSubscriptionsForEvent<T>())
            {
                _handlers.Add(eventName, new List<SubscriptionInfo>());
            }

            if (_handlers[eventName].Any(subscriber => subscriber.HandlerType == typeof(TH)))
            {
                throw new ArgumentException(
                    $"Handler Type {typeof(TH).Name} already registered for '{eventName}'");
            }

            _handlers[eventName].Add(new SubscriptionInfo(typeof(TH)));
        }

        public bool HasSubscriptionsForEvent<T>() where T : IntegrationEvent
        {
            var key = GetEventKey<T>();
            return _handlers.ContainsKey(key);
        }

        public bool HasSubscriptionsForEvent(string eventName)
        {
            return _handlers.ContainsKey(eventName);
        }

        public IEnumerable<SubscriptionInfo> GetHandlersForEvent(string eventName) => _handlers[eventName];

        public Type GetEventTypeByName(string eventName) => _eventTypes.SingleOrDefault(t => t.Name == eventName);

        private string GetEventKey<T>()
        {
            return typeof(T).Name;
        }
    }
}

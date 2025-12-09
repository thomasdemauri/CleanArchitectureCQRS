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

            if (_eventTypes.Contains(typeof(T)))
            {
                _eventTypes.Add(typeof(T));
            }

            if (!_handlers.ContainsKey(eventName))
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

        private string GetEventKey<T>()
        {
            return typeof(T).Name;
        }
    }
}

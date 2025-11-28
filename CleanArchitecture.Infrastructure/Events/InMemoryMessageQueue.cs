using CleanArchitecture.Application.Abstractions.Events;
using System.Threading.Channels;

namespace CleanArchitecture.Infrastructure.Events
{
    public sealed class InMemoryMessageQueue : IInMemoryMessageQueue
    {
        private readonly Channel<IIntegrationEvent> _channel = Channel.CreateUnbounded<IIntegrationEvent>();

        public ChannelWriter<IIntegrationEvent> Writer => _channel.Writer;
        public ChannelReader<IIntegrationEvent> Reader => _channel.Reader;
    }
}

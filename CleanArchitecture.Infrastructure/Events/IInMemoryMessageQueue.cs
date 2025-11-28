using CleanArchitecture.Application.Abstractions.Events;
using System.Threading.Channels;

namespace CleanArchitecture.Infrastructure.Events
{
    public interface IInMemoryMessageQueue
    {
        ChannelWriter<IIntegrationEvent> Writer { get; }
        ChannelReader<IIntegrationEvent> Reader { get; }
    }
}

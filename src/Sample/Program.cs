using System;
using System.Threading.Tasks;
using NServiceBus;

class Program
{
    static async Task Main()
    {
        var endpointConfiguration = new EndpointConfiguration("BinaryFormatterSerializerSample");
        endpointConfiguration.UseSerialization<BinaryFormatterSerializer>();
        endpointConfiguration.EnableInstallers();
        endpointConfiguration.UseTransport<LearningTransport>();
        endpointConfiguration.UsePersistence<InMemoryPersistence>();
        await Run(endpointConfiguration)
            .ConfigureAwait(false);
    }

    static async Task Run(EndpointConfiguration endpointConfiguration)
    {
        var endpoint = await Endpoint.Start(endpointConfiguration)
            .ConfigureAwait(false);
        var myMessage = new MyMessage
        {
            DateSend = DateTime.Now,
        };
        await endpoint.SendLocal(myMessage)
            .ConfigureAwait(false);
        Console.WriteLine("\r\nPress any key to stop program\r\n");
        Console.Read();
    }
}
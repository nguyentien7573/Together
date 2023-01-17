using MassTransit;

namespace Together.OrderService.Core.EventBus
{
    public class OrderConsumerDefinition : ConsumerDefinition<OrderConsumer>
    {
        public OrderConsumerDefinition() 
        {
            EndpointName = "order-service";
            ConcurrentMessageLimit = 8;
        }

        protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator,
        IConsumerConfigurator<OrderConsumer> consumerConfigurator)
        {
            endpointConfigurator.UseMessageRetry(r => r.Intervals(100, 200, 500, 800, 1000));
            endpointConfigurator.UseInMemoryOutbox();
        }
    }
}

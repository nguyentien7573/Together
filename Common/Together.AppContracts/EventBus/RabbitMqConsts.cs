namespace Together.AppContracts.EventBus
{
    public class RabbitMqConsts
    {
        public const string RabbitMqRootUri = "rabbitmq://localhost";
        public const string RabbitMqOrderUri = "rabbitmq://localhost/orderQueue";
        public const string UserName = "guest";
        public const string Password = "guest";
        public const string NotificationServiceQueue = "notification.service";
    }
}

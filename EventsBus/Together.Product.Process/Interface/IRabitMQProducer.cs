namespace Together.Product.Process.Interface
{
    public interface IRabitMQProducer
    {
        public void SendProductMessage<T>(T message);
    }
}

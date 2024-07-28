using MassTransit;

namespace WebApi;

public class PaymentSucceededConsumer : IConsumer<PaymentSucceeded>
{
    private readonly ILogger<PaymentSucceededConsumer> logger;

    public PaymentSucceededConsumer(ILogger<PaymentSucceededConsumer> logger)
    {
        this.logger = logger;
    }
    public Task Consume(ConsumeContext<PaymentSucceeded> context)
    {
        logger.LogInformation("Payment succeeded for customer {Name}", context.Message.Name);
        return Task.CompletedTask;
    }
}
using MassTransit;
using WebApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMassTransit(x =>
{
    x.UsingInMemory();

    x.AddRider(rider =>
    {
        rider.AddConsumer<PaymentSucceededConsumer>();
        rider.UsingKafka((context, k) =>
        {
            k.Host("localhost:9092");

            k.TopicEndpoint<PaymentSucceeded>(
                topicName: "payment-succeeded", 
                groupId: "payment-group", 
                configure: e =>
            {
                e.ConfigureConsumer<PaymentSucceededConsumer>(context);
            });
        });
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();
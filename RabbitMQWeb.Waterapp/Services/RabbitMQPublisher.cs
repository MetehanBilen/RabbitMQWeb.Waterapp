﻿using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace RabbitMQWeb.Waterapp.Services;

public class RabbitMQPublisher
{
    private readonly RabbitMQClientService _rabbitMQClientService;

    public RabbitMQPublisher(RabbitMQClientService rabbitMQClientService)
    {
        _rabbitMQClientService = rabbitMQClientService;
    }

    public void Publish(productImageCreatedEvent productImageCreatedevent) 
    {
        var channel = _rabbitMQClientService.Connect();

        var bodyString = JsonSerializer.Serialize(productImageCreatedevent);

        var bodyByte = Encoding.UTF8.GetBytes(bodyString);

        var properties = channel.CreateBasicProperties();
        properties.Persistent = true;
        channel.BasicPublish(exchange: RabbitMQClientService.ExchangeName
                            ,routingKey: RabbitMQClientService.RoutingWatermark
                            ,basicProperties:properties
                            ,body: bodyByte);
    }
}

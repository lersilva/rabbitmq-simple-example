using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

var factory = new ConnectionFactory{ HostName = "localhost"};

using var ConnectionFactory = factory.CreateConnection();

using var channel = ConnectionFactory.CreateModel();

channel.QueueDeclare(queue: "letterbox", durable:false, exclusive:false, autoDelete:false, arguments:null);

var consumer = new EventingBasicConsumer(channel);

consumer.Received += (model, ea) => {
    var body = ea.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);
    Console.WriteLine($"Messages Received: {message}");
};

channel.BasicConsume(queue: "letterbox", autoAck: true, consumer: consumer);
using System;
using System.Text;
using RabbitMQ.Client;

var factory = new ConnectionFactory{ HostName = "localhost" };

using var ConnectionFactory = factory.CreateConnection();

using var channel = ConnectionFactory.CreateModel();

channel.QueueDeclare(queue: "letterbox", durable:false, exclusive:false, autoDelete:false, arguments:null);

var message = "Hello World Message";

var encodedMessage = Encoding.UTF8.GetBytes(message);  

channel.BasicPublish("", "letterbox", null, encodedMessage);

Console.WriteLine($"Published message: {message}");


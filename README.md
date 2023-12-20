# Domain-Driven Design with ASP.NET Core

This repository contains the demo source code about Domain-Driven Design with ASP.NET Core microservices architecture.


# Cloud:
  Currently using Azure Service Bus when new order is submitted.Send message to queue 'ordersqueue'. 
  
  Azure function is subscribed to this query which reads the message, log it and send it to other query 'productquantityupdatequeue'. 
  
  Application is subscribed to 'productquantityupdatequeue'
  via Background task with hosted services (Consuming a scoped service in a background task) inside the Infrastructure Layer - 'EventBusQueueService'
  
  (https://learn.microsoft.com/en-us/aspnet/core/fundamentals/host/hosted-services?view=aspnetcore-8.0&viewFallbackFrom=aspnetcore-2.1&tabs=visual-studio)

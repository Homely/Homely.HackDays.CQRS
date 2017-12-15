# Homely.HackDays.CQRS
Basic ASP.NET Core API demonstrating some CQRS concepts:
- Seperate read/write models
- MediatR for in-process messaging

Flow => "Question answered"
- User does a POST to /api/questions/answers
- Command handler saves to (fake) repository and adds to queue
- Console application picks up message from queue, adds read model to Table Storage

Flow => "Get question"
- User does a GET to /api/questions/5
- Query handler reads from table storage

URL: http://homely-hackdays-cqrs.azurewebsites.net/
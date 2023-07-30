# Introduction 
TODO: API Introduction<br/>
This project is an implementation of a service that reads messages from Azure Event Hub and then adds those messages to Azure Table Storage. The service uses .NET 7 and Azure SDKs to handle Event Hubs and Table Storage.

# Getting Started
TODO: API Getting Started<br/>

1. Installation Process

Clone this repository to your local system.<br/>
Open the cloned repository in Visual Studio or any other code editor that supports .NET 7.

2. Software Dependencies

- .NET 7.0<br/>
- Azure.Data.Tables<br/>
- Azure.Messaging.EventHubs<br/>
- Microsoft.Extensions.Hosting<br/>
- Microsoft.VisualStudio.Azure.Containers.Tools.Targets<br/>
- Newtonsoft.Json<br/>
- FluentAssertions (for testing)<br/>
- Moq (for testing)<br/>
- xunit (for testing)

# Build and Test
TODO: API Build and Test<br/>
To build the project, open a terminal in the project directory and type dotnet build.<br/>
To run the project, type dotnet run in the terminal. Make sure you have properly configured the connection to Azure Event Hub and Azure Table Storage in secrets.json.<br/>
To run the unit tests, type dotnet test in the terminal. The tests are written using the Xunit framework.
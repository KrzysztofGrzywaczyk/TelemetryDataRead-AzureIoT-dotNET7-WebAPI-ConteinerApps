# TelemetryDataRead-AzureIoT-dotNET7-WebAPI-ConteinerApps
This project is complete solution for storing and reading real weather telemetry data from Azure IoT Edge device _(e.g. deployed on raspberry pi)_ using web api.
Project is divided into two solutions:
- SERVICES - this solution contains two projects:
    - Service to write data in AzureStorage Database.
    - WebAPI to read telemetry data from Storage Database based on given: deviceID, startDate, endDate.
- DEVICES - this solution contains for projects of Azure IoT Edge modules. Those are responsible for:
    - reading data from the co2 sensor
    - reading data from the temperature sensor,
    - data aggregation and sending it to the service that saves it to the database
    - emulating artificial, realistic data simulating a working device.
---
**Every project contains tests.**

**Applications run in ContainerApps** and deployment of aplications and modules is **automated using Azure Pipelines**

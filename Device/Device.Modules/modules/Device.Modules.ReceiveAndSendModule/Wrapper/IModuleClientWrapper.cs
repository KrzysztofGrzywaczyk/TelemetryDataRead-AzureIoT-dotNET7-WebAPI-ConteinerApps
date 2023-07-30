using Microsoft.Azure.Devices.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Device.Modules.ReceiveAndSendModule.Wrapper
{
    public interface IModuleClientWrapper
    {
        public Task OpenAsync(CancellationToken stoppingToken);

        public Task CloseAsync(CancellationToken stoppingToken);

        public Task SendEventAsync(string? outputData, Message messageToSend);

        public Task SetInputMessageHandlerAsync(string? inputName, MessageHandler messageHandler, IModuleClientWrapper client, CancellationToken cancellationToken);
    }
}

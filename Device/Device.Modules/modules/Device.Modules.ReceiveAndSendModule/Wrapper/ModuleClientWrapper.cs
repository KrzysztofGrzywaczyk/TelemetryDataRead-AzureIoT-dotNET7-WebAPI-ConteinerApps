using Microsoft.Azure.Devices.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Device.Modules.ReceiveAndSendModule.Wrapper
{
    public class ModuleClientWrapper : IModuleClientWrapper, IDisposable
    {
        private ModuleClient moduleClient;

        public ModuleClientWrapper(ITransportSettings[] transportSettings)
        {
            this.moduleClient = ModuleClient.CreateFromEnvironmentAsync(transportSettings).GetAwaiter().GetResult();
        }

        public async Task CloseAsync(CancellationToken cancellationToken)
        {
            if (this.moduleClient != null)
            {
                await this.moduleClient.CloseAsync(cancellationToken);
            }
        }

        public async Task OpenAsync(CancellationToken cancellationToken)
        {
            if (this.moduleClient != null)
            {
                await this.moduleClient.OpenAsync(cancellationToken);
            }
        }

        public async Task SendEventAsync(string? outputData, Message messageToSend)
        {
            if (this.moduleClient != null)
            {
                await this.moduleClient.SendEventAsync(outputData, messageToSend);
            }
        }

        public async Task SetInputMessageHandlerAsync(string? inputName, MessageHandler messageHandler, IModuleClientWrapper client, CancellationToken cancellationToken)
        {
            if (this.moduleClient != null)
            {
                await this.moduleClient.SetInputMessageHandlerAsync(inputName, messageHandler, client, cancellationToken);
            }
        }

        public void Dispose()
        {
            this.moduleClient?.Dispose();
        }
    }
}

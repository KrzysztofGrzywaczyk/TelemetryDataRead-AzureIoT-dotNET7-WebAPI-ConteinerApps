using Microsoft.Azure.Devices.Client;
using Device.Modules.ReceiveAndSendModule.Wrapper;

namespace Device.Modules.ReceiveAndSendModule
{
    public interface ISendData
    {
        public Task ProcessData(IModuleClientWrapper client);
    }
}

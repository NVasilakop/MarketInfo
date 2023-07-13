using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.Extensions.Hosting;

namespace WorkerServices
{
    public sealed class StockPubSubWorker : BackgroundService
    {
        private IHubContext<StockHub> _stockHub;
        public StockPubSubWorker(IHubContext<StockHub> stockHubContext)
        {
            _stockHub = stockHubContext;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(1000);
                var eventMessage = new EventMessage();
                eventMessage.Title = "whatever";
               await _stockHub.Clients.All.SendAsync("onMessageReceived", eventMessage, stoppingToken);
            }
        }


    }
}

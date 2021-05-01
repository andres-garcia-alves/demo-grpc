using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gRPC.Service;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace gRPC.UI.Pages
{
    public class IndexModel : PageModel
    {
        private readonly Greeter.GreeterClient client;
        private readonly ILogger<IndexModel> _logger;

        [BindProperty]
        public string Nombre { get; set; }
        public string Mensaje { get; set; }

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;

            var channel = GrpcChannel.ForAddress("https://localhost:5001");
            client = new Greeter.GreeterClient(channel);
        }

        public void OnGet()
        {

        }

        public async Task OnPost()
        {
            if (string.IsNullOrEmpty(this.Nombre)) { return; }

            // request
            var helloRequest = new HelloRequest() { Name = this.Nombre };
            var result = await this.client.SayHelloAsync(helloRequest);

            // response
            this.Mensaje = result.Message;
        }
    }
}

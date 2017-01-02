using System;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Crossover.WebAPI.Services.Abstractions;

namespace Crossover.WebAPI.Services.Handlers
{
    internal class BasicAuthenticationHandler : DelegatingHandler
    {
        private readonly IAuthenticationService _service;

        public BasicAuthenticationHandler(IAuthenticationService service)
        {
            _service = service;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            var authHeader = request.Headers.Authorization;
            if ((authHeader != null) && (authHeader.Scheme == "Basic"))
            {
                var encodedCredentials = authHeader.Parameter;
                var credentialBytes = Convert.FromBase64String(encodedCredentials);
                var credentials = Encoding.ASCII.GetString(credentialBytes).Split(':');
                if (_service.Authenticate(credentials[0], credentials[1]))
                {
                    string[] roles = null; // TODO 
                    IIdentity identity = new GenericIdentity(credentials[0], "Basic");
                    IPrincipal user = new GenericPrincipal(identity, roles);
                    HttpContext.Current.User = user;
                    return base.SendAsync(request, cancellationToken);
                }
            }
            return Unauthorized(request);
        }

        private Task<HttpResponseMessage> Unauthorized(HttpRequestMessage request)
        {
            var response = request.CreateResponse(HttpStatusCode.Unauthorized);
            response.Headers.Add("WWW-Authenticate", "Basic");
            var task = new TaskCompletionSource<HttpResponseMessage>();
            task.SetResult
            (
                response
            );
            return task.Task;
        }
    }
}
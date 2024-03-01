using BlazingTrails.Application.Commands.Trail.Requests;
using MediatR;
using System.Net.Http.Json;

namespace BlazingTrails.Application.Commands.Trail.Handlers
{
    public class EditTrailHandler : IRequestHandler<EditTrailRequest, EditTrailResponse>
    {
        private readonly IHttpClientFactory httpClientFactory;

        public EditTrailHandler(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        public async Task<EditTrailResponse> Handle(EditTrailRequest request, CancellationToken cancellationToken)
        {
            var client = httpClientFactory.CreateClient("SecureAPIClient");
            var response = await client.PutAsJsonAsync(EditTrailRequest.route, request, cancellationToken);
            if (response.IsSuccessStatusCode)
                return new EditTrailResponse(true, null);
            else
                return new EditTrailResponse(false, "There was a problem editing your trail!");
        }
    }
}

using BlazingTrails.Application.Commands.Trail.Requests;
using MediatR;
using System.Net.Http.Json;

namespace BlazingTrails.Application.Commands.Trail.Handlers
{
    public class EditTrailHandler : IRequestHandler<EditTrailRequest, EditTrailResponse>
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public EditTrailHandler(IHttpClientFactory _httpClientFactory)
        {
            this._httpClientFactory = _httpClientFactory;
        }

        public async Task<EditTrailResponse> Handle(EditTrailRequest request, CancellationToken cancellationToken)
        {
            var client = _httpClientFactory.CreateClient("SecureAPIClient");
            var response = await client.PutAsJsonAsync(EditTrailRequest.route, request, cancellationToken);

            if (response.IsSuccessStatusCode)
                return new EditTrailResponse(true, null);
            else
                return new EditTrailResponse(false, "There was a problem editing your trail!");
        }
    }
}

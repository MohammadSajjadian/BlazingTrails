using BlazingTrails.Application.Commands.Trail.Requests;
using MediatR;
using System.Net.Http.Json;

namespace BlazingTrails.Application.Commands.Trail.Handlers
{
    public class EditTrailHandler : IRequestHandler<EditTrailRequest, EditTrailResponse>
    {
        private readonly HttpClient _httpClient;
        public EditTrailHandler(HttpClient _httpClient)
        {
            this._httpClient = _httpClient;
        }

        public async Task<EditTrailResponse> Handle(EditTrailRequest request, CancellationToken cancellationToken)
        {
            var response = await _httpClient.PutAsJsonAsync(EditTrailRequest.route, request, cancellationToken);
            if (response.IsSuccessStatusCode)
                return new EditTrailResponse(true, null);
            else
                return new EditTrailResponse(false, "There was a problem editing your trail!");
        }
    }
}

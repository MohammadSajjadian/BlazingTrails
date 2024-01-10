using BlazingTrails.Application.Query.Trail.Requests;
using MediatR;
using System.Net.Http.Json;

namespace BlazingTrails.Application.Query.Trail.Handlers
{
    public class GetTrailHandler : IRequestHandler<GetTrailRequest, GetTrailResponse>
    {
        private readonly HttpClient _httpClient;
        public GetTrailHandler(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<GetTrailResponse> Handle(GetTrailRequest request, CancellationToken cancellationToken)
        {
            var response = await _httpClient.GetFromJsonAsync<GetTrailResponse>(GetTrailRequest.route.Replace("{trailId}", request.trailId.ToString()));
            return response;
        }
    }
}

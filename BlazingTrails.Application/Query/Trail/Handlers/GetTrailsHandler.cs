using BlazingTrails.Application.Query.Trail.Requests;
using MediatR;
using System.Net.Http.Json;

namespace BlazingTrails.Application.Query.Trail.Handlers
{
    public class GetTrailsHandler : IRequestHandler<GetTrailsRequest, GetTrailsResponse>
    {
        private readonly HttpClient _httpClient;
        public GetTrailsHandler(HttpClient _httpClient)
        {
            this._httpClient = _httpClient;
        }

        public async Task<GetTrailsResponse?> Handle(GetTrailsRequest request, CancellationToken cancellationToken)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<GetTrailsResponse>(GetTrailsRequest.route, cancellationToken: cancellationToken);
            }
            catch (HttpRequestException)
            {
                return default!;
            }
        }
    }
}

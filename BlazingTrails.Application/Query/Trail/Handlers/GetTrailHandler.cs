using BlazingTrails.Application.Query.Trail.Requests;
using MediatR;
using System.Net.Http.Json;
using System.Text.Json;

namespace BlazingTrails.Application.Query.Trail.Handlers
{
    public class GetTrailHandler : IRequestHandler<GetTrailRequest, GetTrailResponse>
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public GetTrailHandler(IHttpClientFactory _httpClientFactory)
        {
            this._httpClientFactory = _httpClientFactory; ;
        }

        public async Task<GetTrailResponse?> Handle(GetTrailRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("SecureAPIClient");
                return await client.GetFromJsonAsync<GetTrailResponse>(GetTrailRequest.route.Replace("{trailId}", request.trailId.ToString()), cancellationToken: cancellationToken);
            }
            catch
            {
                return new GetTrailResponse(null, "There was a problem get your trail.");
            }
        }
    }
}

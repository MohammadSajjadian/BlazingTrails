using BlazingTrails.Application.Query.Trail.Requests;
using MediatR;
using System.Net.Http.Json;

namespace BlazingTrails.Application.Query.Trail.Handlers
{
    public class GetTrailHandler : IRequestHandler<GetTrailRequest, GetTrailResponse>
    {
        private readonly IHttpClientFactory httpClientFactory;
        public GetTrailHandler(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory; ;
        }

        public async Task<GetTrailResponse?> Handle(GetTrailRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var client = httpClientFactory.CreateClient("SecureAPIClient");
                return await client.GetFromJsonAsync<GetTrailResponse>(GetTrailRequest.route.Replace("{trailId}", request.trailId.ToString()), cancellationToken: cancellationToken);
            }
            catch (HttpRequestException)
            {
                return default!;
            }
        }
    }
}

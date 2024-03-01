using BlazingTrails.Application.Commands.Trail.Requests;
using MediatR;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Json;

namespace BlazingTrails.Application.Commands.Trail.Handlers
{
    public class AddTrailHandler : IRequestHandler<AddTrailRequest, AddTrailResponse>
    {
        private readonly IHttpClientFactory httpClientFactory;
        public AddTrailHandler(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        public async Task<AddTrailResponse> Handle(AddTrailRequest request, CancellationToken cancellationToken)
        {
            if (!IsRequestValid(request)) CreateErrorResponse("Please enter the requested values ​​correctly.");

            var response = await SendRequest(request, cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                var trailId = await response.Content.ReadFromJsonAsync<int>(cancellationToken: cancellationToken);
                return CreateSuccessResponse(trailId);
            }
            else return CreateErrorResponse("There was a problem saving your trail.");
        }

        private bool IsRequestValid(AddTrailRequest request)
        {
            var validation = new AddTrailRequestValidator();
            var status = validation.Validate(request);

            return status.IsValid;
        }

        public AddTrailResponse CreateErrorResponse(string errorMessage) =>
            new(-1, errorMessage);

        public async Task<HttpResponseMessage> SendRequest(AddTrailRequest request, CancellationToken cancellationToken)
        {
            var client = httpClientFactory.CreateClient("SecureAPIClient");
            var response = await client.PostAsJsonAsync(AddTrailRequest.route, request, cancellationToken);
            return response;
        }

        public AddTrailResponse CreateSuccessResponse(int trailId) =>
            new(trailId, null);
    }
}

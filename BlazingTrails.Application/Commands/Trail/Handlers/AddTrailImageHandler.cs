using BlazingTrails.Application.Commands.Trail.Requests;
using MediatR;

namespace BlazingTrails.Application.Commands.Trail.Handlers
{
    public class AddTrailImageHandler : IRequestHandler<UploadTrailImageRequest, UploadTrailImageResponse>
    {
        private readonly IHttpClientFactory httpClientFactory;
        public AddTrailImageHandler(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        public async Task<UploadTrailImageResponse> Handle(UploadTrailImageRequest request, CancellationToken cancellationToken)
        {
            if (!IsImageSizeConfirmed(request)) return CreateErrorResponse("Image size should not be more than 2 MB.");

            var file = ReadFile(request, cancellationToken);
            var content = CreateContent(file, request);
            var response = await SendUploadRequest(request, content, cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                var imageName = await response.Content.ReadAsStringAsync(cancellationToken);
                return CreateSuccessResponse(imageName);
            }
            else return CreateErrorResponse("Your trail was saved, but there was a problem uploading the image.");
        }


        public bool IsImageSizeConfirmed(UploadTrailImageRequest request) =>
            request.trailImg.Size < 2 * Math.Pow(1024, 2); // 2 MB

        public UploadTrailImageResponse CreateErrorResponse(string errorMessage) =>
            new(null, errorMessage);

        public Stream ReadFile(UploadTrailImageRequest request, CancellationToken cancellationToken) =>
            request.trailImg.OpenReadStream(request.trailImg.Size, cancellationToken);

        public MultipartFormDataContent CreateContent(Stream file, UploadTrailImageRequest request)
        {
            return new MultipartFormDataContent
            {
                { new StreamContent(file), "image", request.trailImg.Name }
            };
        }

        public async Task<HttpResponseMessage> SendUploadRequest(UploadTrailImageRequest request, MultipartFormDataContent content, CancellationToken cancellationToken)
        {
            var client = httpClientFactory.CreateClient("SecureAPIClient");
            var route = UploadTrailImageRequest.route.Replace("{trailId}", request.trailId.ToString());
            return await client.PostAsync(route, content, cancellationToken);
        }

        public UploadTrailImageResponse CreateSuccessResponse(string imageName) => new(imageName, null);
    }
}

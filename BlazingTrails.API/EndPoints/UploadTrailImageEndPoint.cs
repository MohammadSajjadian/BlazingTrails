using Ardalis.ApiEndpoints;
using BlazingTrails.Application.Commands.Trail.Requests;
using BlazingTrails.Infra.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace BlazingTrails.API.EndPoints
{
    public class UploadTrailImageEndPoint : EndpointBaseAsync.WithRequest<int>.WithActionResult<string>
    {
        private readonly BlazingTrailsContext db;
        public UploadTrailImageEndPoint(BlazingTrailsContext db)
        {
            this.db = db;
        }

        [Authorize]
        [HttpPost(UploadTrailImageRequest.route)]
        public override async Task<ActionResult<string>> HandleAsync(int trailId, CancellationToken cancellationToken = default)
        {
            var trail = db.trails.Find(trailId);
            if (trail is null)
                return BadRequest("Trail dose not found.");

            if (!trail.Owner.Equals(HttpContext.User.Identity!.Name, StringComparison.CurrentCultureIgnoreCase) &&
                !HttpContext.User.IsInRole("Administrator"))
                return Unauthorized();

            var file = Request.Form.Files[0];
            if (file.Length is 0)
                return BadRequest("Image not found.");

            var fileName = $"{Guid.NewGuid()}.jpg";
            var saveLocation = Path.Combine(Directory.GetCurrentDirectory(), "images", fileName);

            var resizeOptions = new ResizeOptions()
            {
                Mode = ResizeMode.Stretch,
                Size = new Size(640, 426),
            };

            var image = Image.Load(file.OpenReadStream());
            image.Mutate(x => x.Resize(resizeOptions));
            await image.SaveAsJpegAsync(saveLocation, cancellationToken);

            trail.Image = fileName;
            await db.SaveChangesAsync(cancellationToken);

            return Ok(fileName);
        }
    }
}

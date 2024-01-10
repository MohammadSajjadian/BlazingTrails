using Ardalis.ApiEndpoints;
using BlazingTrails.Application.Commands.Trail.Requests;
using BlazingTrails.Application.Enum;
using BlazingTrails.Domain.Entities;
using BlazingTrails.Infra.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazingTrails.API.EndPoints
{
    public class EditTrailEndpoint : EndpointBaseAsync.WithRequest<EditTrailRequest>.WithActionResult<bool>
    {
        private readonly BlazingTrailsContext db;
        public EditTrailEndpoint(BlazingTrailsContext db)
        {
            this.db = db;
        }

        [HttpPut(EditTrailRequest.route)]
        public override async Task<ActionResult<bool>> HandleAsync(EditTrailRequest request, CancellationToken cancellationToken = default)
        {
            var trail = await db.trails.Include(x => x.Routes).SingleOrDefaultAsync(x => x.Id == request.trailDto.Id, cancellationToken: cancellationToken);
            if (trail is null)
                return BadRequest("Trail could not be found.");

            trail.Name = request.trailDto.Name;
            trail.Description = request.trailDto.Description;
            trail.Location = request.trailDto.Location;
            trail.TimeInMinutes = request.trailDto.TimeInMinutes;
            trail.Length = request.trailDto.Length;
            trail.Routes.Clear();
            trail.Routes = request.trailDto.Routes.Select(x => new RouteInstruction
            {
                Stage = x.Stage,
                Description = x.Description,
                Trail = trail
            }).ToList();

            if (request.trailDto.ImageAction == ImageAction.Remove)
            {
                System.IO.File.Delete(Path.Combine(Directory.GetCurrentDirectory(), "Images", trail.Image!));
                trail.Image = null;
            }
            await db.SaveChangesAsync(cancellationToken);
            return Ok(true);
        }
    }
}

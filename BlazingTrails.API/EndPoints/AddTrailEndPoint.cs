using Ardalis.ApiEndpoints;
using BlazingTrails.Application.Commands.Trail.Requests;
using BlazingTrails.Domain.Entities;
using BlazingTrails.Infra.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Mvc;
using Trail = BlazingTrails.Domain.Entities.Trail;

namespace BlazingTrails.API.EndPoints
{
    public class AddTrailEndpoint : EndpointBaseAsync.WithRequest<AddTrailRequest>.WithActionResult<int>
    {
        private readonly BlazingTrailsContext db;
        public AddTrailEndpoint(BlazingTrailsContext db)
        {
            this.db = db;
        }

        [Authorize]
        [HttpPost(AddTrailRequest.route)]
        public override async Task<ActionResult<int>> HandleAsync(AddTrailRequest request, CancellationToken cancellationToken = default)
        {
            var trail = new Trail
            {
                Name = request.trailDto.Name,
                Description = request.trailDto.Description,
                Location = request.trailDto.Location,
                TimeInMinutes = request.trailDto.TimeInMinutes,
                Length = request.trailDto.Length,
                waypoints = request.trailDto.Waypoints.Select(wp => new Waypoint
                {
                    latitude = wp.Latitude,
                    longitude = wp.Longitude
                }).ToList()
            };
            await db.trails.AddAsync(trail, cancellationToken);
            await db.SaveChangesAsync(cancellationToken);
            return Ok(trail.Id);
        }
    }
}

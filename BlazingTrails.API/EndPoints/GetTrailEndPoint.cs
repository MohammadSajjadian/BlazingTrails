using Ardalis.ApiEndpoints;
using BlazingTrails.Application.Query.Trail.Requests;
using BlazingTrails.Infra.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BlazingTrails.API.EndPoints
{
    public class GetTrailEndPoint : EndpointBaseAsync.WithRequest<int>.WithActionResult<GetTrailResponse>
    {
        private readonly BlazingTrailsContext _db;
        public GetTrailEndPoint(BlazingTrailsContext _db)
        {
            this._db = _db;
        }

        [Authorize]
        [HttpGet(GetTrailRequest.route)]
        public override async Task<ActionResult<GetTrailResponse>> HandleAsync(int trailId, CancellationToken cancellationToken = default)
        {
            var trail = await _db.trails.
                Include(x => x.waypoints).
                SingleOrDefaultAsync(x => x.Id == trailId, cancellationToken: cancellationToken);

            if (trail is null)
                return BadRequest("Trail could not be found.");

            if (!trail.Owner.Equals(HttpContext.User.Identity!.Name, StringComparison.CurrentCultureIgnoreCase) &&
                !HttpContext.User.IsInRole("Administrator"))
                return Unauthorized();

            var response = new GetTrailResponse(new GetTrailRequest.Trail(trail.Id,
            trail.Name,
            trail.Location,
            trail.Image,
            trail.TimeInMinutes,
            trail.Length,
            trail.Description,
            trail.waypoints.Select(wp => new GetTrailRequest.Waypoint(wp.latitude, wp.longitude)).ToList()), null);

            return Ok(response);
        }
    }
}

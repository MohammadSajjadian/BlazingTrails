using Ardalis.ApiEndpoints;
using BlazingTrails.Application.Query.Trail.Requests;
using BlazingTrails.Infra.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazingTrails.API.EndPoints
{
    public class GetTrailsEndPoint : EndpointBaseAsync.WithoutRequest.WithActionResult<GetTrailsResponse>
    {
        private readonly BlazingTrailsContext _db;
        public GetTrailsEndPoint(BlazingTrailsContext _db)
        {
            this._db = _db;
        }

        [HttpGet(GetTrailsRequest.route)]
        public override async Task<ActionResult<GetTrailsResponse>> HandleAsync(CancellationToken cancellationToken = default)
        {
            var trails = await _db.trails.
                Include(x => x.waypoints).
                ToListAsync(cancellationToken);
            var response = new GetTrailsResponse(trails.Select(trail => new GetTrailsRequest.Trail(
            trail.Id,
            trail.Name,
            trail.Image,
            trail.Location,
            trail.TimeInMinutes,
            trail.Length,
            trail.Description,
            trail.Owner,
            trail.waypoints.Select(wp => new GetTrailsRequest.Waypoint(wp.latitude, wp.longitude)).ToList())).ToList());

            return Ok(response);
        }
    }
}

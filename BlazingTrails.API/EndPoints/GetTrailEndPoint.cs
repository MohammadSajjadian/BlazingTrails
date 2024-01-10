using Ardalis.ApiEndpoints;
using BlazingTrails.Application.Query.Trail.Requests;
using BlazingTrails.Infra.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace BlazingTrails.API.EndPoints
{
    public class GetTrailEndPoint : EndpointBaseAsync.WithRequest<int>.WithActionResult<GetTrailResponse>
    {
        private readonly BlazingTrailsContext _db;
        public GetTrailEndPoint(BlazingTrailsContext _db)
        {
            this._db = _db;
        }

        [HttpGet(GetTrailRequest.route)]
        public override async Task<ActionResult<GetTrailResponse>> HandleAsync(int trailId, CancellationToken cancellationToken = default)
        {
            var trail = await _db.trails.
                Include(x => x.Routes).
                SingleOrDefaultAsync(x => x.Id == trailId, cancellationToken: cancellationToken);

            if (trail is null)
                return BadRequest("Trail could not be found.");

            var response = new GetTrailResponse(new Trail(trail.Id,
            trail.Name,
            trail.Location,
            trail.Image,
            trail.TimeInMinutes,
            trail.Length,
            trail.Description,
            trail.Routes.Select(x => new RouteInstruction(x.Id, x.Stage, x.Description)).ToList()), null);

            return Ok(response);
        }
    }
}

using Ardalis.ApiEndpoints;
using BlazingTrails.Application.Commands.Trail.Requests;
using BlazingTrails.Domain.Entities;
using BlazingTrails.Infra.Context;
using Microsoft.AspNetCore.Mvc;

namespace BlazingTrails.API.EndPoints
{
    public class AddTrailEndpoint : EndpointBaseAsync.WithRequest<AddTrailRequest>.WithActionResult<int>
    {
        private readonly BlazingTrailsContext db;
        public AddTrailEndpoint(BlazingTrailsContext db)
        {
            this.db = db;
        }

        [HttpPost(AddTrailRequest.route)]
        public override async Task<ActionResult<int>> HandleAsync(AddTrailRequest request, CancellationToken cancellationToken = default)
        {
            var trail = await AddTrailAsync(request, cancellationToken);
            await AddRouteInstructionAsync(trail, request, cancellationToken);
            await db.SaveChangesAsync(cancellationToken);
            return Ok(trail.Id);
        }

        private async Task<Trail> AddTrailAsync(AddTrailRequest request, CancellationToken cancellationToken = default)
        {
            var trail = new Trail
            {
                Name = request.trailDto.Name,
                Description = request.trailDto.Description,
                Location = request.trailDto.Location,
                TimeInMinutes = request.trailDto.TimeInMinutes,
                Length = request.trailDto.Length
            };

            await db.trails.AddAsync(trail, cancellationToken);
            return trail;
        }

        private async Task AddRouteInstructionAsync(Trail trail, AddTrailRequest request, CancellationToken cancellationToken = default)
        {
            var routeInstructions = request.trailDto.Routes.Select(x => new RouteInstruction
            {
                Stage = x.Stage,
                Description = x.Description,
                Trail = trail
            });

            await db.routeInstructions.AddRangeAsync(routeInstructions, cancellationToken);
        }
    }
}

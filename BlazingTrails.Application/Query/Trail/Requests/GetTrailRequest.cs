using MediatR;

namespace BlazingTrails.Application.Query.Trail.Requests
{
    public record GetTrailRequest(int trailId) : IRequest<GetTrailResponse>
    {
        public const string route = "/api/trails/{trailId}";

        public record Trail(int Id, string Name, string Location, string Image, int
        TimeInMinutes, int Length, string Description, List<Waypoint> Waypoints);
        public record Waypoint(decimal Latitude, decimal Longitude);
    }

    public record GetTrailResponse(GetTrailRequest.Trail? trail, string? errorMessage);
}

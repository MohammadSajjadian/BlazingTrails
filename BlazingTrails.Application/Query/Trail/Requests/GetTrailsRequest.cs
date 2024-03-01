using MediatR;

namespace BlazingTrails.Application.Query.Trail.Requests
{
    public record GetTrailsRequest : IRequest<GetTrailsResponse>
    {
        public const string route = "/api/trails";

        public record Trail(int Id, string? Name, string Image, string Location, int
        TimeInMinutes, int Length, string Description, string Owner, List<Waypoint> Waypoints);
        public record Waypoint(decimal Latitude, decimal Longitude);
    }

    public record GetTrailsResponse(List<GetTrailsRequest.Trail> trails);
}

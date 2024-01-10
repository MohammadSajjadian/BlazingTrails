using MediatR;

namespace BlazingTrails.Application.Query.Trail.Requests
{
    public record GetTrailRequest(int trailId) : IRequest<GetTrailResponse>
    {
        public const string route = "/api/trails/{trailId}";
    }

    public record GetTrailResponse(Trail? trail, string? errorMessage);
    public record Trail(int id, string name, string location, string? image, int timeInMinutes, int length, string description, List<RouteInstruction> routeInstructions);
    public record RouteInstruction(int Id, int Stage, string Description);
}

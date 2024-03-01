using BlazingTrails.Application.Enum;

namespace BlazingTrails.Application.DTOs
{
    public class TrailDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public string? image { get; set; }
        public int Length { get; set; }
        public int TimeInMinutes { get; set; }
        public string Location { get; set; } = "";
        public List<WaypointDto> Waypoints { get; set; } = new();

        public ImageAction ImageAction { get; set; }
    }
}

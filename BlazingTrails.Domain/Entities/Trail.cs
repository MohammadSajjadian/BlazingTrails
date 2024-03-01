namespace BlazingTrails.Domain.Entities
{
    public class Trail
    {
        public int Id { get; set; }
        public string Owner { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string? Image { get; set; }
        public string Location { get; set; } = default!;
        public int TimeInMinutes { get; set; }
        public int Length { get; set; }

        public ICollection<Waypoint> waypoints { get; set; } = default!;
    }
}

using System.ComponentModel.DataAnnotations.Schema;

namespace BlazingTrails.Domain.Entities
{
    public class Waypoint
    {
        public int id { get; set; }
        public decimal latitude { get; set; }
        public decimal longitude { get; set; }

        [ForeignKey(nameof(trailId))]
        public int trailId { get; set; }
        public Trail trail { get; set; } = default!;
    }
}

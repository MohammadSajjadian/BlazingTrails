using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazingTrails.Domain.Entities
{
    public class Trail
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string? Image { get; set; }
        public string Location { get; set; } = default!;
        public int TimeInMinutes { get; set; }
        public int Length { get; set; }

        public ICollection<RouteInstruction> Route { get; set; } = default!;
    }
}

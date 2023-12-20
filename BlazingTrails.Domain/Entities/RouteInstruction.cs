using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazingTrails.Domain.Entities
{
    public class RouteInstruction
    {
        public int Id { get; set; }
        public int Stage { get; set; }
        public string Description { get; set; } = default!;

        [ForeignKey(nameof(TrailId))]
        public int TrailId { get; set; }
        public Trail? Trail { get; set; }
    }
}

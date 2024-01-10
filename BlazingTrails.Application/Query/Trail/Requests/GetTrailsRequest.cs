using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazingTrails.Application.Query.Trail.Requests
{
    public record GetTrailsRequest : IRequest<GetTrailsResponse>
    {
        private const string route = "/api/trails";

        public record Trail(int id, string name, string? image, string location, int timeInMinutes, int length, string description);
    }

    public record GetTrailsResponse(List<Trail> trails);
}

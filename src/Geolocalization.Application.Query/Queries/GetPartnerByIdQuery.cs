using Geolocalization.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Geolocalization.Application.Query.Queries
{
    public class GetPartnerByIdQuery : IRequest<Partner>
    {
        public GetPartnerByIdQuery(int id)
        {
            Id = id;
        }

        public int Id { get; }
    }
}

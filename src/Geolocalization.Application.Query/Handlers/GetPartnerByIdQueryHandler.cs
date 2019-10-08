using Geolocalization.Application.Query.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Geolocalization.Application.Query.Handlers
{
    public class GetPartnerByIdQueryHandler : IRequestHandler<GetPartnerByIdQuery>
    {
        public Task<Unit> Handle(GetPartnerByIdQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}

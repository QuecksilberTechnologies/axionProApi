﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.Features.TenantCmd.Queries
{
    public class GetAllCompanyQuery : IRequest<string>
    {
    }
    public class GetAllCompanyQueryHandler : IRequestHandler<GetAllCompanyQuery, string>
    {
        public Task<string> Handle(GetAllCompanyQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}

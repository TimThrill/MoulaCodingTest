using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moula.Payment.GateWay.Application.ViewModels
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<Domain.AggregatesModel.PaymentAggerate.Payment, PaymentViewModel>();
        }
    }
}

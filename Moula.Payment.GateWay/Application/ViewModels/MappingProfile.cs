using AutoMapper;
using Moula.Payment.Domain;
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
            CreateMap<Domain.AggregatesModel.PaymentAggerate.Payment, PaymentViewModel>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => Enum.GetName(typeof(PaymentStatus), src.Status)));
        }
    }
}

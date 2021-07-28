using AutoMapper;
using Smart_Electrician_Support_System.Models;
using Smart_Electrician_Support_System.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Smart_Electrician_Support_System.MapperProfiles
{
    public class EmpCategoryProfile : Profile
    {
        public EmpCategoryProfile()
        {
            CreateMap<EmpCategoryModel, EmpCategoryViewModel>()
                //.ForMember(dest =>
                //    dest.EmpCat_ID,
                //    opt => opt.MapFrom(src => src.EmpCat_ID))
                //.ForMember(dest =>
                //    dest.EmpCat_Type,
                //    opt => opt.MapFrom(src => src.EmpCat_Type))
                //.ForMember(dest =>
                //    dest.EmpCat_Descr,
                //    opt => opt.MapFrom(src => src.EmpCat_Descr))
                //.ForMember(dest =>
                //    dest.EmpCat_Status,
                //    opt => opt.MapFrom(src => src.EmpCat_Status))
                .ReverseMap();
            ;
        }
    }
}

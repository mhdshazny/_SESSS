using AutoMapper;
using Smart_Electrician_Support_System.Models;
using Smart_Electrician_Support_System.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Smart_Electrician_Support_System.MapperProfiles
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<EmpCategoryModel, EmpCategoryViewModel>()
                    .ReverseMap();
            CreateMap<AppointmentModel, AppointmentViewModel>()
                    .ReverseMap();
            CreateMap<CustomerModel, CustomerViewModel>()
                    .ReverseMap();
            CreateMap<EmpIdentityModel, EmpIdentityViewModel>()
                    .ReverseMap();
            CreateMap<EmployeeModel, EmployeeViewModel>()
                    .ReverseMap();
            CreateMap<InvoiceModel, InvoiceViewModel>()
                    .ReverseMap();
            CreateMap<JobModel, JobViewModel>()
                    .ReverseMap();
            CreateMap<ProductsModel, ProductsViewModel>()
                    .ReverseMap();
            CreateMap<UsedProductsModel, UsedProductsViewModel>()
                    .ReverseMap();
            CreateMap<DashTargetModel, DashTargetViewModel>()
                    .ReverseMap();
            CreateMap<ProductCategoryModel, ProductCategoryViewModel>()
                    .ReverseMap();
        }
    }
}

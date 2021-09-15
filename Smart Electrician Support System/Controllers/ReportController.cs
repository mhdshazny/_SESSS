using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Rotativa.AspNetCore;
using Smart_Electrician_Support_System.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Smart_Electrician_Support_System.Controllers
{
    public class ReportController : Controller
    {
        private DbConnectionClass _context;
        private IMapper _mapper;
        private ProductsService _prdService;
        public ReportController(DbConnectionClass context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _prdService = new ProductsService(context, mapper);

        }
        public IActionResult Index()
        {
            return new ViewAsPdf();
        }
        public IActionResult StockReport()
        {
            var data = ProductsService.GetList();
            return new ViewAsPdf(data);
        }
    }
}

using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RadzenDemo.Data;

namespace RadzenDemo
{
    public partial class ExportSampleController : ExportController
    {
        private readonly SampleContext context;

        public ExportSampleController(SampleContext context)
        {
            this.context = context;
        }
        [HttpGet("/export/Sample/orders/csv")]
        [HttpGet("/export/Sample/orders/csv(fileName='{fileName}')")]
        public FileStreamResult ExportOrdersToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(context.Orders, Request.Query), fileName);
        }

        [HttpGet("/export/Sample/orders/excel")]
        [HttpGet("/export/Sample/orders/excel(fileName='{fileName}')")]
        public FileStreamResult ExportOrdersToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(context.Orders, Request.Query), fileName);
        }
        [HttpGet("/export/Sample/orderdetails/csv")]
        [HttpGet("/export/Sample/orderdetails/csv(fileName='{fileName}')")]
        public FileStreamResult ExportOrderDetailsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(context.OrderDetails, Request.Query), fileName);
        }

        [HttpGet("/export/Sample/orderdetails/excel")]
        [HttpGet("/export/Sample/orderdetails/excel(fileName='{fileName}')")]
        public FileStreamResult ExportOrderDetailsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(context.OrderDetails, Request.Query), fileName);
        }
        [HttpGet("/export/Sample/products/csv")]
        [HttpGet("/export/Sample/products/csv(fileName='{fileName}')")]
        public FileStreamResult ExportProductsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(context.Products, Request.Query), fileName);
        }

        [HttpGet("/export/Sample/products/excel")]
        [HttpGet("/export/Sample/products/excel(fileName='{fileName}')")]
        public FileStreamResult ExportProductsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(context.Products, Request.Query), fileName);
        }
    }
}

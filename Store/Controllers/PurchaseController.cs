using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.Models.Context;
using Store.Models.Entities.Invoice;
using Store.Models.Entities.Invoices;
using Store.Models.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PurchaseController : ControllerBase
    {
        private StoreDbContext _storeDbContext;
        public PurchaseController(StoreDbContext storeDbContext)
        {
            _storeDbContext = storeDbContext;
        }

        int id = 0;
        //Add Invoice Headers by Admin
        [Authorize(Roles = "Admin,Editor")]
        [HttpPost]
        public IActionResult Header([FromForm] InvoiceHeader invoice)
        {
            //id += 1;
            var invoiceobj = new InvoiceHeader
            {
                UserID = invoice.UserID,
                Date = invoice.Date,
                DateTime = DateTime.Now,
                InvoiceNumber = id + 1,
                Description = invoice.Description
            };

            _storeDbContext.InvoiceHeaders.Add(invoiceobj);
            _storeDbContext.SaveChanges();
            return Ok(invoiceobj);
        }

        //Add Invoice Subs by Admin
        [Authorize(Roles = "Admin,Editor")]
        [HttpPost]
        public IActionResult Sub([FromForm] InvoiceSub invoice)
        {
            var feestr = from p in _storeDbContext.Products
                         where p.ID == invoice.ProductID
                         select p.Price;

            //Convert Query to float To Use in Fee
            double feedou = Convert.ToDouble(feestr);
            float fee = (float)feedou;

            int count = invoice.Mount;

            var invoiceobj = new InvoiceSub
            {
                InvoiceHeaderID = invoice.InvoiceHeaderID,
                ProductID = invoice.ProductID,
                Fee = fee,
                Mount = invoice.Mount,
                Price = count * fee,
                DisCount = invoice.DisCount
            };

            _storeDbContext.InvoiceSubs.Add(invoiceobj);
            _storeDbContext.SaveChanges();
            return Ok("OK.");
        }

        //Get Invoice by Admin
        [Authorize(Roles = "Admin,Editor")]
        [HttpGet("{id}")]
        public IActionResult GetInvoice(int id)
        {
            var invoicenumber = _storeDbContext.InvoiceHeaders.Find(id);
            if(invoicenumber == null)
            {
                return NotFound("Record Not Found Against This Id.");
            }
            else
            {
                var invoice = from ih in _storeDbContext.InvoiceHeaders
                              join isu in _storeDbContext.InvoiceSubs
                              on ih.ID equals isu.InvoiceHeaderID

                              join u in _storeDbContext.Users
                              on ih.UserID equals u.ID

                              join p in _storeDbContext.Products
                              on isu.ProductID equals p.ID

                              where (ih.ID == id)

                              select new
                              {
                                  UserName = u.Name,
                                  Phone = u.Phone,
                                  Email = u.Email,

                                  Fee = isu.Fee,
                                  Mount = isu.Mount,
                                  Price = isu.Price,

                                  Date = ih.Date,
                                  DateTime = ih.DateTime,
                                  InvoiceDescription = ih.Description,

                                  ProductName = p.Name,
                                  ProductCode = p.ProductCode,
                                  Weight = p.Wieght,
                                  ProductDescription = p.Description,
                                  Content = p.Content
                              };
                return Ok(invoice);
            }
        }
    }
}
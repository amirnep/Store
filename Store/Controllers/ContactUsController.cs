using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.Models.Context;
using Store.Models.Entities.Contact_Us;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ContactUsController : ControllerBase
    {
        private StoreDbContext _storeDbContext;
        public ContactUsController(StoreDbContext storeDbContext)
        {
            _storeDbContext = storeDbContext;
        }

        //Post Message By All Users and Contact Us
        [Authorize]
        [HttpPost]
        public IActionResult Contact([FromForm] Contact contact)
        {
            var contactobj = new Contact
            {
                Subject = contact.Subject,
                Message = contact.Message,
                UserID = contact.UserID,
                Seen = "new"
            };

            _storeDbContext.Contacts.Add(contactobj);
            _storeDbContext.SaveChanges();
            return Ok("Message Send Successfully.");
        }

        //Get Messages By Admin
        [Authorize(Roles = "Admin,Editor,Author")]
        [HttpGet("{id}")]
        public IActionResult Messages(int id)
        {
            var contact = _storeDbContext.Contacts.Find(id);
            if (contact == null)
            {
                return NotFound("No record found against this Id");
            }
            else
            {
                var messages = from c in _storeDbContext.Contacts
                               join u in _storeDbContext.Users
                               on c.UserID equals u.ID

                               where c.ID == id

                               select new
                               {
                                   Name = u.Name,
                                   Phone = u.Phone,
                                   Email = u.Email,

                                   Subject = c.Subject,
                                   Message = c.Message,
                                   Seen = c.Seen
                               };
                contact.Seen = "Old";
                _storeDbContext.SaveChanges();
                return Ok(messages);
            }
        }
    }
}
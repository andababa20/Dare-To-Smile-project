using System;
using System.Collections.Generic;
using System.Configuration;
using System.Configuration.Provider;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using Castle.Facilities.TypedFactory.Internal;
using DHTMLX.Common;
using DHTMLX.Scheduler;
using Ical.Net;
using SmileT.Models;

namespace SmileT.Controllers
{
    public class ContactController : Controller
    {
        public ActionResult Contact()
        {
            var model = new ContactData();
            return PartialView("_ContactForm", model);
        }

        [HttpPost]
        public ActionResult SubmitContactForm(ContactData model)
        {
            if (ModelState.IsValid)
            {
                SaveToDatabase(model);
                return RedirectToAction("SuccessIndex");
            }
            return PartialView("_ContactForm", model);
        }
        private void SaveToDatabase(ContactData model)
        {
            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;Initial Catalog=SmileDb;Integrated Security=True;MultipleActiveResultSets=True;Application Name=EntityFramework";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Contacts(Name, Phone,Email_address, Enquiry) VALUES (@Name, @Phone,@Email, @Enquiry)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Name", model.Name);
                command.Parameters.AddWithValue("@Phone", model.Phone);
                command.Parameters.AddWithValue("@Email", model.Email);
                command.Parameters.AddWithValue("@Enquiry", model.Enquiry);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public ActionResult SuccessIndex()
        {
            return View();
        }
    }
}


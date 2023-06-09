﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Services;
using static WebApplication1.Pages.defaultModel;
using CsvHelper;
using System.Globalization;
using System.Text.RegularExpressions;

namespace WebApplication1.Pages
{
    public class EmailMessage
    {
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Select_service { get; set; }
        public string Select_price { get; set; }
        public string Comments { get; set; }
        

    }
    [IgnoreAntiforgeryToken]
    public class contactModel : DefaultModel
    {

        public contactModel(IDataReader reader) : base(reader, "contact")
        {

        }

        public void OnGet()
        {
            title = _dataReader.GetData(_pageName)["title"];

        }
        [BindProperty]
        public EmailMessage Message { get; set; }
        public bool isEmail(string email)
        {
            return Regex.IsMatch(email, "(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*|\"(?:[\\x01-\\x08\\x0b\\x0c\\x0e-\\x1f\\x21\\x23-\\x5b\\x5d-\\x7f]|\\\\[\\x01-\\x09\\x0b\\x0c\\x0e-\\x7f])*\")@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\\[(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?|[a-z0-9-]*[a-z0-9]:(?:[\\x01-\\x08\\x0b\\x0c\\x0e-\\x1f\\x21-\\x5a\\x53-\\x7f]|\\\\[\\x01-\\x09\\x0b\\x0c\\x0e-\\x7f])+)\\])",
                    RegexOptions.CultureInvariant | RegexOptions.Singleline);
        }
        public IActionResult OnPost()
        {
            if (Message.Comments == null || Message.Comments == "")
            {
                Message.Comments = "Empty";
            }

            if (Message.First_Name == null || Message.First_Name == "")
            {
                return Content("<div class=\"error_message\">You must enter your name.</div>");
            }
            else if (Message.Last_Name == null || Message.Last_Name == "")
            {
                return Content("<div class=\"error_message\">You must enter your last name.</div>");
            }
            else if (!isEmail(Message.Email))
            {
                return Content("<div class=\"error_message\">You must check your Email.</div>");
            }
            else
            {
                WriteFile("contact.csv", Message);
                return Content($@"<fieldset>
                        <div id='success_page'> 
                        <h1>Email Sent Successfully.</h1>
                        <p>Thank you <strong>{Message.First_Name} {Message.Last_Name}</strong>, your message has been submitted to us.<p>
                        </div>
                    <fieldset>");
            }
        }

        private void WriteFile(string filepath, EmailMessage message)
        {
            var first = !System.IO.File.Exists(filepath);
            using (var writer = new StreamWriter(filepath, true))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                if (first)
                {
                    csv.WriteHeader<EmailMessage>();
                    csv.NextRecord();
                }
                csv.WriteRecord(message);
                csv.NextRecord();
            }
        }


    }
}

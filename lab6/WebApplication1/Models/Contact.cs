using System.ComponentModel.DataAnnotations.Schema;
namespace WebApplication1.Models
{
    public class Contact
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Select_service { get; set; }

        public string Select_price { get; set; }

        public string Comments { get; set; }
    }
}

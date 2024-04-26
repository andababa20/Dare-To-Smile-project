using DHTMLX.Scheduler;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using static DHTMLX.Scheduler.Controls.GridViewColumn;

namespace SmileT.Models
{
public class ContactData
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public int Phone { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Enquiry { get; set; }
    }
}
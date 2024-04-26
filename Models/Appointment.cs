using DHTMLX.Scheduler;
using System;
using static DHTMLX.Scheduler.Controls.GridViewColumn;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SmileT.Models
{
    public class Appointment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DHXJson(Alias = "id")]
        public int Id { get; set; }

        [DHXJson(Alias = "text")]
        public string Description { get; set; }

        [DHXJson(Alias = "Start")]
        public DateTime Start { get; set; }

        [DHXJson(Alias = "End")]
        public DateTime End { get; set; }
    }
}
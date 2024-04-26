using DHTMLX.Scheduler;
using System;
using static DHTMLX.Scheduler.Controls.GridViewColumn;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace SmileT.Models
{
    public class CalendarContext : DbContext
    {
        public CalendarContext() : base("CalendarEntities")
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<CalendarEntities>());
        }
        public DbSet<CalendarEntities> Appointments { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


using DHTMLX.Scheduler;
using DHTMLX.Common;
using DHTMLX.Scheduler.Data;
using DHTMLX.Scheduler.Controls;

using SmileT.Models;
namespace SmileT.Controllers
{
    public class CalendarController : Controller
    {
        SmileDbEntities2 smileDB = new SmileDbEntities2();

        public ActionResult Index()
        {
            var scheduler = new DHXScheduler(this);
            scheduler.Config.first_hour = 8;
            scheduler.Config.last_hour = 20;

            scheduler.InitialDate = DateTime.Now;

            scheduler.LoadData = true;
            scheduler.EnableDataprocessor = true;

            return View(scheduler);
        }

        public ContentResult Data()
        {
            var data = new SchedulerAjaxData(
                    new List<CalendarEvent>{ 
                       
                    }
                );
            return (ContentResult)data;
        }

        public ContentResult Save(int? id, FormCollection actionValues)
        {
            var action = new DataAction(actionValues);
            
            try
            {
                var changedEvent = (CalendarEvent)DHXEventsHelper.Bind(typeof(CalendarEvent), actionValues);

     

                switch (action.Type)
                {
                    case DataActionTypes.Insert:
                        Event EV = new Event();
                        EV.id = changedEvent.id;
                        EV.start_date = changedEvent.start_date;
                        EV.end_date = changedEvent.end_date;
                        EV.text = changedEvent.text;
                        smileDB.Events.Add(EV);
                        smileDB.SaveChanges();
                        break;
                    case DataActionTypes.Delete:
                        var details = smileDB.Events.Where(x => x.id == id).FirstOrDefault();
                        smileDB.Events.Remove(details);
                        smileDB.SaveChanges();
                        break;
                    default:   
                        var data = smileDB.Events.Where(x => x.id == id).FirstOrDefault();
                        data.start_date = changedEvent.start_date;
                        data.end_date = changedEvent.end_date;
                        data.text = changedEvent.text;
                        smileDB.SaveChanges();
                        break;
                }
            }
            catch
            {
                action.Type = DataActionTypes.Error;
            }
            return (ContentResult)new AjaxSaveResponse(action);
        }
    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpress.Web.Mvc;
using FinanceKpi.Models;

namespace FinanceKpi.Controllers
{
    public class AttachmentController : Controller
    {
        // GET: Attachment
        public ActionResult Index()
        {
            Session["StartDate"] = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddMonths(-1);
            Session["EndDate"] = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddDays(-1);
            return View();
        }

        public ActionResult GridViewPartialView()
        {
            // DXCOMMENT: Pass a data model for GridView in the PartialView method's second parameter
            return PartialView("GridViewPartialView", GetAttachments());
            //return PartialView("GridViewPartialView");
        }

        [HttpPost]
        public ActionResult Query()
        {
            Session["StartDate"] = EditorExtension.GetValue<DateTime>("StartDate");
            Session["EndDate"] = EditorExtension.GetValue<DateTime>("EndDate");
            Session["model"] = GetAttachments();
            return View("Index", Session["model"]);
        }

        private List<Attachment> GetAttachments()
        {
            var startDate = (DateTime)Session["StartDate"];
            var endDate = ((DateTime)Session["EndDate"]).AddDays(1);
            var entities = new BPMDBEntities();
            var reimAttachments = from r in entities.iReimbursement
                join t in entities.BPMInstTasks on r.TaskID equals t.TaskID
                where r.Date >= startDate && r.Date < endDate && (t.State == "Running" || t.State == "Approved")
                select new Attachment
                {
                    TaskId = (int) r.TaskID,
                    Date = (DateTime) r.Date,
                    AccountName = r.AppName,
                    Department = r.AppDepartment,
                    Type = "报销单",
                    Attach = r.Attachment
                };
            var outAttachments = from o in entities.iOutside
                join t in entities.BPMInstTasks on o.TaskID equals t.TaskID
                where o.Date >= startDate && o.Date < endDate && (t.State == "Running" || t.State == "Approved")
                select new Attachment
                {
                    TaskId = o.TaskID,
                    Date = (DateTime)o.Date,
                    AccountName = o.AppName,
                    Department = o.AppDepartment,
                    Type = "差旅报销单",
                    Attach = o.Attachment
                };

            return reimAttachments.Union(outAttachments).ToList();
        }

        [HttpPost]
        public ActionResult ExportTo()
        {
            return GridViewExtension.ExportToXlsx(AttachmentGridViewHelper.GridViewSettings, Session["model"]);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpress.Web.Mvc;
using FinanceKpi.Models;

namespace FinanceKpi.Controllers
{
    public class RefundController : Controller
    {
        // GET: Refund
        public ActionResult Index()
        {
            Session["StartDate"] = DateTime.Today.AddMonths(-1);
            Session["EndDate"] = DateTime.Today;
            return View();
        }

        public ActionResult GridViewPartialView()
        {
            // DXCOMMENT: Pass a data model for GridView in the PartialView method's second parameter
            return PartialView("GridViewPartialView", GetRefunds());
            //return PartialView("GridViewPartialView");
        }

        [HttpPost]
        public ActionResult Query()
        {
            Session["StartDate"] = EditorExtension.GetValue<DateTime>("StartDate");
            Session["EndDate"] = EditorExtension.GetValue<DateTime>("EndDate");
            return View("Index", GetRefunds());
        }

        private List<Refund> GetRefunds()
        {
            var startDate = (DateTime) Session["StartDate"];
            var endDate = ((DateTime) Session["EndDate"]).AddDays(1);
            var ignoreActions = new[] { "sysPickBackRestart", "sysTransfer" };
            var applyActions = new[] { "同意", "sysDirectSend" };
            var finAccount = new[] {"ynzhou", "zwle", "yfzhao", "sbzhao", "zhli"};
            var entities = new BPMDBEntities();
            var refunds = from step in entities.BPMInstProcSteps
                where step.NodeName.StartsWith("财务检查") && finAccount.Contains(step.HandlerAccount) && !ignoreActions.Contains(step.SelAction) && step.ReceiveAt >= startDate && step.ReceiveAt < endDate
                select new Refund
                {
                    TaskID = step.TaskID,
                    StepID = step.StepID,
                    FinAccount = step.HandlerAccount,
                    ProcessName = step.ProcessName,
                    ReceiveTime = step.ReceiveAt,
                    Refunded = step.RecedeFromStep == null ? "正常" : "被退",
                    Refunding = applyActions.Contains(step.SelAction) ? "正常" : "退单"
                };
            return refunds.ToList();
        }
    }
}
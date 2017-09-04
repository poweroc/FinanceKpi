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
            Session["StartDate"] = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddMonths(-1);
            Session["EndDate"] = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddDays(-1);
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
            var refunds = (from step in entities.BPMInstProcSteps
                where step.NodeName.StartsWith("财务检查") && finAccount.Contains(step.HandlerAccount) && !ignoreActions.Contains(step.SelAction) && step.FinishAt >= startDate && step.FinishAt < endDate
                select new Refund
                {
                    TaskID = step.TaskID,
                    StepID = step.StepID,
                    FinAccount = step.HandlerAccount,
                    ProcessName = step.ProcessName,
                    FinishTime = (DateTime) step.FinishAt,
                    Refunded = step.RecedeFromStep == null ? "正常" : "被退",
                    Refunding = applyActions.Contains(step.SelAction) ? "正常" : "退单"
                }).ToList();
            foreach (var refund in refunds)
            {
                var task = entities.BPMInstTasks.FirstOrDefault(t => t.TaskID == refund.TaskID);
                if (task != null)
                {
                    var user = entities.BPMSysUsers.FirstOrDefault(u => u.Account == task.OwnerAccount);
                    if (user != null)
                    {
                        refund.AccountName = user.DisplayName;
                    }
                    var member = entities.BPMSysMemberIDMap.FirstOrDefault(m => m.ID == task.OwnerPositionID);
                    if (member != null)
                    {
                        var full = member.MemberFullName;
                        refund.Department = full.Substring(23, full.LastIndexOf("/", StringComparison.Ordinal) - 23);
                    }
                }
            }
            return refunds;
        }
    }
}
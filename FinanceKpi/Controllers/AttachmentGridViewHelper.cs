using DevExpress.Data;
using DevExpress.Web;
using DevExpress.Web.Mvc;

namespace FinanceKpi.Controllers
{
    public class AttachmentGridViewHelper
    {
        private static GridViewSettings _gridViewSettings;
        public static GridViewSettings GridViewSettings => _gridViewSettings ?? (_gridViewSettings = CreateGridViewSettings());

        private static GridViewSettings CreateGridViewSettings()
        {
            GridViewSettings settings = new GridViewSettings();
            settings.Name = "GridView";
            settings.CallbackRouteValues = new { Controller = "Attachment", Action = "GridViewPartialView" };
            settings.KeyFieldName = "TaskId";
            settings.Width = System.Web.UI.WebControls.Unit.Percentage(100);
            settings.SettingsPager.PageSize = 50;
            settings.Settings.VerticalScrollBarMode = ScrollBarMode.Visible;

            settings.Columns.Add(column =>
            {
                column.FieldName = "TaskId";
                column.SortOrder = ColumnSortOrder.Ascending;
                column.SortIndex = 0;
            });
            settings.Columns.Add("Type").Caption = "类型";
            settings.Columns.Add("Date").Caption = "提交日期";
            settings.Columns.Add("Department").Caption = "部门";
            settings.Columns.Add("AccountName").Caption = "提交人";
            settings.Columns.Add("Count").Caption = "附件数量";

            return settings;
        }
    }
}
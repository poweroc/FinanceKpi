using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FinanceKpi.Models
{
    public class Asset
    {
        public int TaskID { get; set; }
        [Display(Name = "申请人")]
        public string AppName { get; set; }
        [Display(Name = "申请部门")]
        public string AppDepartment { get; set; }
        [Display(Name = "类型")]
        public string Type { get; set; }
        [Display(Name = "固定资产编号")]
        public string FixedAssetId { get; set; }
        [Display(Name = "中文名称")]
        public string Name { get; set; }
        [Display(Name = "规格")]
        public string Specification { get; set; }
        [Display(Name = "型号")]
        public string Model { get; set; }
        [Display(Name = "使用部门")]
        public string Department { get; set; }
        [Display(Name = "存放地点")]
        public string Location { get; set; }
        [Display(Name = "购置日期")]
        public DateTime PurchaseDate { get; set; }
        [Display(Name = "教育部资产明细分类")]
        public string EduCode { get; set; }
        [Display(Name = "财政部资产分类号")]
        public string FinCode { get; set; }
        [Display(Name = "单价")]
        public double Price { get; set; }
        [Display(Name = "保管人")]
        public string Keeper { get; set; }
        [Display(Name = "责任人")]
        public string Responsibler { get; set; }
        [Display(Name = "经费账号")]
        public string FundingAccount { get; set; }
        [Display(Name = "总金额")]
        public double TotalAmount { get; set; }
        [Display(Name = "使用方向")]
        public string UseDirection { get; set; }
        [Display(Name = "设备来源")]
        public string EquipmentSource { get; set; }
        [Display(Name = "生产国别")]
        public string Country { get; set; }
        [Display(Name = "供应单位")]
        public string SupplyCompany { get; set; }
        [Display(Name = "是否进口免税设备")]
        public string IsTaxFee { get; set; }
        [Display(Name = "发票号")]
        public string InvoiceNumber { get; set; }
    }
}
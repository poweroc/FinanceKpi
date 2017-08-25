using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FinanceKpi.Models;

namespace FinanceKpi.Controllers
{
    public class AssetController : Controller
    {
        // GET: Asset
        public ActionResult Index()
        {
            return View(GetAllAssets());
        }

        public List<Asset> GetAllAssets()
        {
            var assets = new List<Asset>();

            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["BPM"].ConnectionString))
            {
                connection.Open();

                var sql = @"SELECT a.TaskID, a.AppName, a.AppDepartment, a.Type, d.FixedAssetId, d.Name, d.Specification, d.Type Model, d.Department, d.Location, d.PurchaseDate, d.EduCode, d.FinCode, d.Count, d.UnitPrice, d.Amount, d.Keeper, d.Responsibler, a.FundingAccount, a.TotalAmount, a.UseDirection, a.EquipmentSource, a.Country, a.SupplyCompany, a.IsTaxFee, a.InvoiceNumber
FROM dbo.iAsset a LEFT JOIN dbo.iAssetDetail d ON a.TaskID = d.TaskID
LEFT JOIN dbo.BPMInstTasks t ON a.TaskID = t.TaskID
WHERE t.State = 'Approved' AND a.Operation = '新增'
ORDER BY a.TaskID DESC, d.Id";
                var command = new SqlCommand(sql, connection);
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var assetIds = Convert.ToString(reader["FixedAssetId"]).Split("-".ToCharArray());
                    if (assetIds.Length == 2)
                    {
                        var begin = Convert.ToInt32(assetIds[0]);
                        var end = Convert.ToInt32(assetIds[1]);

                        for (int i = begin; i <= end; i++)
                        {
                            assets.Add(new Asset
                            {
                                TaskID = Convert.ToInt32(reader["TaskID"]),
                                AppName = Convert.ToString(reader["AppName"]),
                                AppDepartment = Convert.ToString(reader["AppDepartment"]),
                                Type = Convert.ToString(reader["Type"]),
                                FixedAssetId = i.ToString(),
                                Name = Convert.ToString(reader["Name"]),
                                Specification = Convert.ToString(reader["Specification"]),
                                Model = Convert.ToString(reader["Model"]),
                                Department = Convert.ToString(reader["Department"]),
                                Location = Convert.ToString(reader["Location"]),
                                PurchaseDate = Convert.ToDateTime(reader["PurchaseDate"]),
                                EduCode = Convert.ToString(reader["EduCode"]),
                                FinCode = Convert.ToString(reader["FinCode"]),
                                Price = Convert.ToDouble(reader["UnitPrice"]),
                                Keeper = Convert.ToString(reader["Keeper"]),
                                Responsibler = Convert.ToString(reader["Responsibler"]),
                                FundingAccount = Convert.ToString(reader["FundingAccount"]),
                                TotalAmount = Convert.ToDouble(reader["TotalAmount"]),
                                UseDirection = Convert.ToString(reader["UseDirection"]),
                                EquipmentSource = Convert.ToString(reader["EquipmentSource"]),
                                Country = Convert.ToString(reader["Country"]),
                                SupplyCompany = Convert.ToString(reader["SupplyCompany"]),
                                IsTaxFee = Convert.ToInt32(reader["IsTaxFee"]) == 0 ? "否" : "是",
                                InvoiceNumber = Convert.ToString(reader["InvoiceNumber"]),
                            });
                        }
                    }
                    else
                    {
                        assets.Add(new Asset
                        {
                            TaskID = Convert.ToInt32(reader["TaskID"]),
                            AppName = Convert.ToString(reader["AppName"]),
                            AppDepartment = Convert.ToString(reader["AppDepartment"]),
                            Type = Convert.ToString(reader["Type"]),
                            FixedAssetId = Convert.ToString(reader["FixedAssetId"]),
                            Name = Convert.ToString(reader["Name"]),
                            Specification = Convert.ToString(reader["Specification"]),
                            Model = Convert.ToString(reader["Model"]),
                            Department = Convert.ToString(reader["Department"]),
                            Location = Convert.ToString(reader["Location"]),
                            PurchaseDate = Convert.ToDateTime(reader["PurchaseDate"]),
                            EduCode = Convert.ToString(reader["EduCode"]),
                            FinCode = Convert.ToString(reader["FinCode"]),
                            Price = Convert.ToDouble(reader["UnitPrice"]),
                            Keeper = Convert.ToString(reader["Keeper"]),
                            Responsibler = Convert.ToString(reader["Responsibler"]),
                            FundingAccount = Convert.ToString(reader["FundingAccount"]),
                            TotalAmount = Convert.ToDouble(reader["TotalAmount"]),
                            UseDirection = Convert.ToString(reader["UseDirection"]),
                            EquipmentSource = Convert.ToString(reader["EquipmentSource"]),
                            Country = Convert.ToString(reader["Country"]),
                            SupplyCompany = Convert.ToString(reader["SupplyCompany"]),
                            IsTaxFee = Convert.ToInt32(reader["IsTaxFee"]) == 0 ? "否" : "是",
                            InvoiceNumber = Convert.ToString(reader["InvoiceNumber"]),
                        });
                    }
                }

                reader.Close();
                connection.Close();
            }

            return assets;
        }
    }
}
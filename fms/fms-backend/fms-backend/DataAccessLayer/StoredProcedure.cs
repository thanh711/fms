using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class StoredProcedure
    {
        // Campus
        public static string GetListCampus = "Proc_Campus_GetListCampus";
        public static string SaveCampus = "Proc_Campus_SaveCampus";
        public static string GetListCampusInService = "Proc_Campus_GetListCampusInService";


        // Area
        public static string GetListArea = "Proc_Area_GetListArea";
        public static string GetListAreaInService = "Proc_Area_GetListAreaInService";
        public static string SaveArea = "Proc_Area_SaveArea";

        // Location
        public static string GetListLocation = "Proc_Location_GetListLocation";
        public static string GetListLocationInService = "Proc_Location_GetListLocationInService";
        public static string SaveLocation = "Proc_Location_SaveLocation";

        // User
        public static string GetListUser = "Proc_User_GetListUser";
        public static string SaveUser = "Proc_User_SaveUser";

        // Category
        public static string SaveCategory = "Proc_Category_SaveCategory";
        public static string GetListCategories = "Proc_Category_GetListCategory";

        // Asset
        public static string SaveAsset = "Proc_Asset_SaveAsset";
        public static string GetListAsset = "Proc_Asset_GetListAsset";

        //Trouble
        public static string SaveTrouble = "Proc_Trouble_SaveTrouble";
        public static string GetListTrouble = "Proc_Trouble_GetListTrouble";
        public static string Trouble_ChangeTechnician = "Proc_Trouble_Assign_Technician";
        public static string Trouble_Delete = "Proc_Trouble_DeleteTrouble";
        public static string Trouble_Count = "Proc_Dashboard_CountReport";

        // Checklist
        public static string Checklist_GetList = "Proc_Checklist_GetListChecklists";
        public static string Checklist_GetWeekly = "Proc_Checklist_GetWeeklyChecklist";
        public static string Checklist_GetDetail = "Proc_Checklist_GetDetail";
        public static string Checklist_SaveSummary = "Proc_Checklist_SaveSummary";
        public static string Checklist_SaveDetail = "Proc_Checklist_SaveItem";
        public static string Checklist_SaveTemplate = "Proc_Checklist_SaveTemplate";
        public static string Checklist_DeleteTemplate = "Proc_Checklist_DeleteTemplate";

        public static string Checklist_CreateChecklist_Daily = "Proc_Checklist_CreateChecklist_Daily";
        public static string Checklist_CreateChecklist_Weekly = "Proc_Checklist_CreateChecklist_Weekly";
        public static string Checklist_Delete = "Proc_Checklist_Delete";

        // Technical Report
        public static string TechReport_Checklist_GetList = "Proc_Technical_Report_GetListChecklists";

        // Warehouse
        public static string Warehouse_Import_Save = "Proc_Warehouse_Import";
        public static string Warehouse_Export_Save = "Proc_Warehouse_Export";
        public static string Warehouse_GetList_History = "Proc_Warehouse_History";
        public static string Warehouse_GetList_Remaining = "Proc_Warehouse_GetRemaining";
        public static string Warehouse_GetList_Config = "Proc_Warehouse_GetConfig";
        public static string Warehouse_Import_ChangeReady = "Proc_Warehouse_Import_ChangeReady";

        // Map
        public static string Map_GetMap = "Proc_Map_GetMap";
        public static string Map_SaveArea = "Proc_Map_SaveArea";

        // Dash
        public static string Dash_GetChecklistNotify = "Proc_Dash_GetChecklistNotify";
        public static string Dash_GetWarehouseNotify = "Proc_Dash_GetWarehouseNotify";
    }
}

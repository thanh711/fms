using DataAccessLayer;
using DataAccessLayer.Model.Configuration;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject1.SampleTestData
{
    internal class WorkflowData
    {
        private string _cnnString;
        private readonly IConfiguration _configuration;
        private BaseService<Workflow> _baseService;
        public WorkflowData(IConfiguration configuration)
        {
            _configuration = configuration;
            _cnnString = _configuration.GetConnectionString("DB");
            _baseService = new BaseService<Workflow>(configuration);
        }
        public void ClearTestData()
        {
            string query = "delete from [Configuration.Workflows] where [Type] like 'ThanhNC-Test%'";

            _baseService.Delete(query);
        }
        public void CreateTestData()
        {

            foreach (var item in Lists)
            {
                CreateTestItem(item);
            }
        }
        public void CreateTestItem(Workflow model)
        {
            string query = "insert into [Configuration.Workflows]([Type],[CreatedBy],[Created]) values('"+ model.Type+ "','" + model.CreatedBy + "',GETDATE())";

            _baseService.Delete(query);
            //   insert into[Configuration.Workflows]([Type],[CreatedBy],[Created]) values
            // ('ThanhNC-Test-FU-HL', 'thanhnche140350', GETDATE())
        }
        internal static Workflow model1 = new Workflow
        {
            Type = "ThanhNC-Test-FU-HL",
            CreatedBy = "thanhnche140350",
        };
        internal static Workflow model2 = new Workflow
        {
            Type = "ThanhNC-Test-FU-HL",
            CreatedBy = "thanhnche140350",
        };
        internal static Workflow model3 = new Workflow
        {
            Type = "ThanhNC-Test-FU-HL",
            CreatedBy = "thanhnche140350",
        };
        internal static Workflow model4 = new Workflow
        {
            Type = "ThanhNC-Test-FU-HL",
            CreatedBy = "thanhnche140350",
        };
        internal static Workflow model5 = new Workflow
        {
            Type = "ThanhNC-Test-FU-HL",
            CreatedBy = "thanhnche140350",
        };
        internal static List<Workflow> Lists = new List<Workflow> { model1, model2, model3, model4, model5 };

    }
}

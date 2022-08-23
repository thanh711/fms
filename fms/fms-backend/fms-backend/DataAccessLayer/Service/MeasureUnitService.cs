using DataAccessLayer.Model.Configuration;
using DataAccessLayer.Model.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Service
{
    public class MeasureUnitService
    {
        private readonly IConfiguration _configuration;
        private BaseService<MeasureUnit> _baseService;
        public MeasureUnitService(IConfiguration configuration)
        {
            _configuration = configuration;
            _baseService = new BaseService<MeasureUnit>(configuration);
        }

        public ApiResult<MeasureUnit> GetAll()
        {
            string query = "SELECT * FROM [Configuration.MeasureUnits]";
            return _baseService.GetAll(query);
        }

        public ApiResult<MeasureUnit> GetByName(string name)
        {
            string query = "SELECT * FROM [Configuration.MeasureUnits] WHERE [Name] = N'" +name +"'";
            return _baseService.GetBy(query);
        }
    }
}

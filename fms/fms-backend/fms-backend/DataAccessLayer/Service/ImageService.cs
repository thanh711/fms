using DataAccessLayer.IService;
using DataAccessLayer.Model.Model;
using DataAccessLayer.Model.Troubles;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Service
{
    public class ImageService : IImageService
    {
        private string _cnnString;
        private readonly IConfiguration _configuration;
        private BaseService<Image> _baseService;
        public ImageService(IConfiguration configuration)
        {
            _configuration = configuration;
            _cnnString = _configuration.GetConnectionString("DB");
            _baseService = new BaseService<Image>(configuration);
        }
        public ApiResult<Image> Delete(string id)
        {
            string query = "delete from [Trouble.Images] where ID = '" + id + "'";
            return _baseService.Delete(query);
        }

        public ApiResult<Image> Save(Image image)
        {
            string query = "INSERT INTO [Trouble.Images] ([ID],[ReportID],[Path]) " +
                "VALUES ('" + image.ReportID + "_" + image.ID + "'," + image.ReportID + ",'" + image.Path + "')";
            return _baseService.Save(query);
        }

        public ApiResult<Image> SaveTroubleshootingImage(Image image)
        {
            string query = "INSERT INTO [Trouble.Images] ([ID],[TroubleshootingID],[Path]) " +
                "VALUES ('" + image.TroubleshotingID + "_" + image.ID + "'," + image.TroubleshotingID + ",'" + image.Path + "')";
            return _baseService.Save(query);
        }

        public ApiResult<Image> GetListImageReport(int reportId)
        {
            string query = "SELECT * FROM [Trouble.Images] WHERE ReportID = " + reportId;
            return _baseService.GetAll(query);
        }

        public ApiResult<Image> GetListImageTroubleShoting(int troubleshotId)
        {
            string query = "SELECT * FROM [Trouble.Images] WHERE TroubleshootingID = " + troubleshotId;
            return _baseService.GetAll(query);
        }

        public bool CheckExist(string imageId)
        {
            
            string query = "SELECT * FROM [Trouble.Images] WHERE ID = '" + imageId + "'";
            return _baseService.GetBy(query).Data != null;
        }
    }
}

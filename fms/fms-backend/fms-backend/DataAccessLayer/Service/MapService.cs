using Dapper;
using DataAccessLayer.IService;
using DataAccessLayer.Model.Configuration;
using DataAccessLayer.Model.Map;
using DataAccessLayer.Model.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Service
{
    public class MapService : IMapService
    {
        private readonly BaseService<AreaType> _typeService;
        private readonly string _cnnString;
        private readonly IConfiguration _configuration;
        private readonly BaseService<Area> _areaService;
        private readonly BaseService<MapFloor> _floorService;
        public MapService(IConfiguration configuration)
        {
            _typeService = new BaseService<AreaType>(configuration);
            _configuration = configuration;
            _cnnString = _configuration.GetConnectionString("DB");
            _areaService = new BaseService<Area>(configuration);
            _floorService = new BaseService<MapFloor>(configuration);
        }

        public ApiResult<AreaType> GetAllAreaType()
        {
            string query = "SELECT * FROM [Configuration.AreaTypes]";
            return _typeService.GetAll(query);
        }

        public ApiResult<MapApiResult> GetMap(FilterModel filter)
        {
            SqlConnection connection = new SqlConnection(_cnnString);
            ApiResult<MapApiResult> res = null;

            var parameters = new DynamicParameters();
            parameters.Add("@CampusName", filter.CampusName);
            parameters.Add("@LocationCode", filter.LocationCode);
            parameters.Add("@Floor", filter.Floor);
            parameters.Add("@Row", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("@Column", dbType: DbType.Int32, direction: ParameterDirection.Output);

            try
            {
                connection.Open();
                MapApiResult apiResult = new MapApiResult();
                var list = connection.Query<Square>(StoredProcedure.Map_GetMap, parameters, commandType: CommandType.StoredProcedure).ToList();
                apiResult.Row = parameters.Get<int>("@Row");
                apiResult.Column = parameters.Get<int>("@Column");
                apiResult.Squares = list;

                res = new ApiResult<MapApiResult>
                {
                    Status = 200,
                    Data = apiResult
                };

            }
            catch (Exception e)
            {
                res = new ApiResult<MapApiResult>
                {
                    Status = 400,
                    Message = e.Message
                };
            }
            finally
            {
                connection.Close();
            }
            return res;
        }

        public ApiResult<Area> GetListAreaSelect(FilterModel filter)
        {
            string query = "SELECT AR.* FROM [Configuration.Areas] AR " +
                "INNER JOIN[Configuration.Locations] LO ON AR.LocationID = LO.ID " +
                "INNER JOIN[Configuration.Campuses] CA ON LO.CampusID = CA.ID " +
                "WHERE AR.InService = 1 AND LO.Code = '" + filter.LocationCode + "' AND CA.[Name] = N'" + filter.CampusName + "' AND AR.[Floor] = " + filter.Floor;
            return _areaService.GetAll(query);
        }

        public ApiResult<MapApiResult> SaveMap(SaveMapModel saveModel)
        {
            // get Floor
            string query = "SELECT * FROM [Map.Floors] " +
                "WHERE CampusID = (SELECT ID FROM[Configuration.Campuses] WHERE[Name] LIKE N'" + saveModel.Filter.CampusName + "') " +
                "AND LocationID = (SELECT ID FROM[Configuration.Locations] WHERE[Code] LIKE '" + saveModel.Filter.LocationCode + "')" +
                "AND [Floor] = " + saveModel.Filter.Floor;
            var floor = _floorService.GetBy(query);
            if (floor.Data != null)
            {

                string msgErr = "";
                string updateQuery = "UPDATE [Map.Floors] SET [SizeRow] = " + saveModel.Data?.Row + "" +
                ",[SizeColumn] = " + saveModel.Data?.Column + ", [Updated] = GETDATE()," +
                "[UpdatedBy] = '" + saveModel.CurrentUser + "' WHERE ID = " + floor.Data.ID;
                var upFlRes = _floorService.SaveByQuery(updateQuery);
                if (upFlRes.Status == 200)
                {
                    foreach (var item in saveModel.Data?.Squares)
                    {
                        try
                        {
                            var parameters = new DynamicParameters();
                            parameters.Add("@ID", item.ID);
                            parameters.Add("@AreaID", item.AreaID);
                            parameters.Add("@X", item.X);
                            parameters.Add("@Y", item.Y);
                            parameters.Add("@Name", item.Name);
                            parameters.Add("@SizeX", item.SizeX);
                            parameters.Add("@SizeY", item.SizeY);
                            parameters.Add("@HaveDoor", item.IsHaveDoor);
                            parameters.Add("@DoorX", item.DoorX);
                            parameters.Add("@DoorY", item.DoorY);
                            parameters.Add("@AreaTypeID", item.AreaTypeID);
                            parameters.Add("@FloorID", floor.Data.ID);
                            parameters.Add("@DirectDoor", item.DirectDoor);
                            parameters.Add("@CurrentUser", saveModel.CurrentUser);
                            var saveRes = _floorService.Save(StoredProcedure.Map_SaveArea, parameters);
                            if (saveRes.Status == 400)
                            {
                                msgErr += "Error at save AreaID = " + item.AreaID;
                            }
                        }
                        catch (Exception ex)
                        {
                            msgErr += "Error at save AreaID = " + item.AreaID;
                        }
                    }
                    

                }

                foreach (var id in saveModel.DeleteList)
                {
                    string delQuery = "DELETE FROM [Map.Doors] WHERE AreaID = " + id;
                    var deRes = _floorService.Delete(delQuery);

                    if (deRes.Status == 400) return new ApiResult<MapApiResult> { Status = 400, Message = deRes.Message };

                    delQuery = "DELETE FROM [Map.Areas] WHERE ID = " + id;
                    deRes = _floorService.Delete(delQuery);
                    if (deRes.Status == 400) return new ApiResult<MapApiResult> { Status = 400, Message = deRes.Message };
                }

                return new ApiResult<MapApiResult>
                {
                    Status = 200,
                    Message = msgErr
                };
            }
                return new ApiResult<MapApiResult>
                {
                    Status = 400,
                    Message = "Floor is undefine!"
                };
            

        }
    }
}

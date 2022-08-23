using Dapper;
using DataAccessLayer.Model.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class BaseService<T>
    {
        private readonly string _cnnString;
        private readonly IConfiguration _configuration;
        public BaseService(IConfiguration configuration)
        {
            _configuration = configuration;
            _cnnString = _configuration.GetConnectionString("DB");
        }

        public ApiResult<T> GetAll(string query)
        {
            SqlConnection connection = new SqlConnection(_cnnString);
            ApiResult<T> res = null;
            try
            {
                connection.Open();
                var result = connection.Query<T>(query).ToList();
                res = new ApiResult<T>
                {
                    Status = 200,
                    ListData = result
                };
            }
            catch (Exception e)
            {
                res = new ApiResult<T>
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

        public ApiResult<T> Delete(string query)
        {
            SqlConnection connection = new SqlConnection(_cnnString);
            ApiResult<T> res = null;
            try
            {
                connection.Open();
                var result = connection.Execute(query);
                res = new ApiResult<T>
                {
                    Status = 200,
                    Message = Constants.MESS_DEL_SUS
                };
            }
            catch (Exception ex)
            {
                res = new ApiResult<T>
                {
                    Status = 400,
                    Message = ex.Message
                };
            }
            finally
            {
                connection.Close();
            }
            return res;
        }


        public ApiResult<T> Save(string prod, DynamicParameters parameters)
        {
            SqlConnection connection = new SqlConnection(_cnnString);
            ApiResult<T> res = null;
            try
            {
                connection.Open();
                var result = connection
                        .Execute(prod, parameters, commandType: CommandType.StoredProcedure);
                res = new ApiResult<T>
                {
                    Status = 200,
                    Message = Constants.MESS_SAVE_SUS
                };
            }
            catch (Exception ex)
            {
                res = new ApiResult<T>
                {
                    Status = 400,
                    Message = ex.Message
                };
            }
            finally
            {
                connection.Close();
            }
            return res;
        }

        public ApiResult<T> GetList(Paging paging, string prod, DynamicParameters parameters)
        {
            SqlConnection connection = new SqlConnection(_cnnString);
            ApiResult<T> res = null;
            parameters.Add("@Total", dbType: DbType.Int32, direction: ParameterDirection.Output);

            try
            {
                connection.Open();
                var list = connection.Query<T>(prod, parameters, commandType: CommandType.StoredProcedure).ToList();
                paging.RowsCount = parameters.Get<int>("@Total");
                res = new ApiResult<T>
                {
                    Status = 200,
                    ListData = list,
                    Paging = paging
                };

            }
            catch (Exception e)
            {
                res = new ApiResult<T>
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

        public ApiResult<T> GetBy(string query)
        {
            SqlConnection connection = new SqlConnection(_cnnString);
            ApiResult<T> res = null;
            try
            {
                connection.Open();
                T entity = connection.Query<T>(query).ToList().FirstOrDefault();
                res = new ApiResult<T>
                {
                    Status = 200,
                    Data = entity
                };
            }
            catch (Exception e)
            {
                res = new ApiResult<T>
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

        public ApiResult<T> ChangeActive(string query)
        {
            SqlConnection connection = new SqlConnection(_cnnString);
            ApiResult<T> res = null;
            try
            {
                connection.Open();
                var result = connection.Execute(query);
                res = new ApiResult<T>
                {
                    Status = 200,
                    Message = Constants.MESS_SAVE_SUS
                };
            }
            catch (Exception ex)
            {
                res = new ApiResult<T>
                {
                    Status = 400,
                    Message = ex.Message
                };
            }
            finally
            {
                connection.Close();
            }
            return res;
        }


        public ApiResult<T> Save(string query)
        {
            SqlConnection connection = new SqlConnection(_cnnString);
            ApiResult<T> res = null;
            try
            {
                connection.Open();
                var result = connection.Execute(query);
                res = new ApiResult<T>
                {
                    Status = 200,
                    Message = Constants.MESS_SAVE_SUS
                };
            }
            catch (Exception ex)
            {
                res = new ApiResult<T>
                {
                    Status = 400,
                    Message = ex.Message
                };
            }
            finally
            {
                connection.Close();
            }
            return res;
        }

        public ApiResult<T> SaveReturnEntity(string prod, DynamicParameters parameters)
        {
            SqlConnection connection = new SqlConnection(_cnnString);
            ApiResult<T> res = null;
            try
            {
                connection.Open();
                var result = connection
                        .Query<T>(prod, parameters, commandType: CommandType.StoredProcedure);
                res = new ApiResult<T>
                {
                    Status = 200,
                    Message = Constants.MESS_SAVE_SUS,
                    Data = result.ToList().FirstOrDefault()
                };
            }
            catch (Exception ex)
            {
                res = new ApiResult<T>
                {
                    Status = 400,
                    Message = ex.Message
                };
            }
            finally
            {
                connection.Close();
            }
            return res;
        }

        public ApiResult<T> Insert(string query)
        {
            SqlConnection connection = new SqlConnection(_cnnString);
            ApiResult<T> res = null;
            try
            {
                connection.Open();
                var result = connection.Execute(query);
                res = new ApiResult<T>
                {
                    Status = 200,
                    Message = Constants.MESS_SAVE_SUS
                };
            }
            catch (Exception ex)
            {
                res = new ApiResult<T>
                {
                    Status = 400,
                    Message = ex.Message
                };
            }
            finally
            {
                connection.Close();
            }
            return res;
        }

        public ApiResult<T> SaveByQuery(string query)
        {
            SqlConnection connection = new SqlConnection(_cnnString);
            ApiResult<T> res = null;
            try
            {
                connection.Open();
                var result = connection.Execute(query);
                res = new ApiResult<T>
                {
                    Status = 200,
                    Message = Constants.MESS_SAVE_SUS
                };
            }
            catch (Exception ex)
            {
                res = new ApiResult<T>
                {
                    Status = 400,
                    Message = "Fail to sync: {" + ex.Message + "}"
                };
            }
            finally
            {
                connection.Close();
            }
            return res;
        }

        public ApiResult<T> GetListUnReturnTotal(string prod, DynamicParameters parameters)
        {
            SqlConnection connection = new SqlConnection(_cnnString);
            ApiResult<T> res = null;

            try
            {
                connection.Open();
                var list = connection.Query<T>(prod, parameters, commandType: CommandType.StoredProcedure).ToList();
                res = new ApiResult<T>
                {
                    Status = 200,
                    ListData = list
                };

            }
            catch (Exception e)
            {
                res = new ApiResult<T>
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

        public ApiResult<T> GetListNoPrams(string prod)
        {
            SqlConnection connection = new SqlConnection(_cnnString);
            ApiResult<T> res = null;

            try
            {
                connection.Open();
                var list = connection.Query<T>(prod, commandType: CommandType.StoredProcedure).ToList();
                res = new ApiResult<T>
                {
                    Status = 200,
                    ListData = list
                };

            }
            catch (Exception e)
            {
                res = new ApiResult<T>
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
    }
}

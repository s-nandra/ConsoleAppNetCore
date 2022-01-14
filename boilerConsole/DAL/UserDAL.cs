using boilerConsole.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace boilerConsole.DAL
{
    public class UserDAL
    {
        private string _sqlConnectionString;
        public UserDAL(IConfiguration iconfiguration)
        {
            _sqlConnectionString = iconfiguration.GetConnectionString("SQLCon");
        }

        //Model User
        public List<UserModel> GetUserList()
        {
            var listUserModel = new List<UserModel>();
            try
            {
                using (SqlConnection con = new SqlConnection(_sqlConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_getUserList", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        listUserModel.Add(new UserModel
                        {
                            Id = Convert.ToInt32(rdr[0]),
                            Name = rdr[1].ToString(),
                            Surname = rdr[2].ToString(),
                            Active = Convert.ToBoolean(rdr[3])
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listUserModel;
        }
    }
}

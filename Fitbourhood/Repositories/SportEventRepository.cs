using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Dapper;
using Fitbourhood.Models;

namespace Fitbourhood.Repositories
{
    public static class SportEventRepository
    {
        public static string ConnectionString = "Data Source=(LocalDB)\\MSSQLLocalDB";
        public static List<string> ErrorList = new List<string>();

        public static List<SportEvent> GetAllSportEvents()
        {
            List<SportEvent> resultList = new List<SportEvent>();
            string sqlSelectUserByEmail = "SELECT * FROM [FitbourhoodDB].[dbo].[SportEvents]";
            int affectedRows = 0;
            using (var connection = new SqlConnection(ConnectionString))
            {
                resultList = connection.Query<SportEvent>(sqlSelectUserByEmail).ToList();
            }

            return resultList;
        }
    }
}
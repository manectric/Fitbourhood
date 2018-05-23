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
            string sqlSelectAllSportEvents = "SELECT * FROM [FitbourhoodDB].[dbo].[SportEvents]";

            using (var connection = new SqlConnection(ConnectionString))
            {
                resultList = connection.Query<SportEvent>(sqlSelectAllSportEvents).ToList();
            }

            return resultList;
        }

        public static List<SportEvent> GetSportEventsFiltered(string discipline, string eventDate, string eventTime)
        {
            List<SportEvent> resultList = new List<SportEvent>();
            string sqlSelectSportEventsFiltered = "SELECT * FROM [FitbourhoodDB].[dbo].[SportEvents] WHERE";

            using (var connection = new SqlConnection(ConnectionString))
            {
                resultList = connection.Query<SportEvent>(sqlSelectSportEventsFiltered).ToList();
            }

            return resultList;
        }
    }
}
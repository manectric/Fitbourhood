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
            string sqlSelectAllSportEvents = "SELECT * FROM [FitbourhoodDB].[dbo].[SportEvents] AND HasEnded = 0";

            using (var connection = new SqlConnection(ConnectionString))
            {
                resultList = connection.Query<SportEvent>(sqlSelectAllSportEvents).ToList();
            }

            return resultList;
        }

        public static List<SportEvent> GetSportEventsFiltered(SortedList<string, string> paramDictionary)
        {
            List<SportEvent> resultList = new List<SportEvent>();
            string sqlSelectSportEventsFiltered = "SELECT * FROM [FitbourhoodDB].[dbo].[SportEvents]";

            if (paramDictionary.Any(x => x.Value != null))
            {
                sqlSelectSportEventsFiltered += " WHERE ";
                foreach (var param in paramDictionary)
                {
                    if (!string.IsNullOrWhiteSpace(param.Value))
                    {
                        sqlSelectSportEventsFiltered += string.Format("[{0}] = {1}", param.Key, param.Value);
                        var nextParam = paramDictionary.ElementAtOrDefault(paramDictionary.IndexOfKey(param.Key) + 1);
                        if (nextParam.Key != null && !string.IsNullOrWhiteSpace(nextParam.Value))
                        {
                            sqlSelectSportEventsFiltered += " AND ";
                        }
                    }
                }
            }

            using (var connection = new SqlConnection(ConnectionString))
            {
                resultList = connection.Query<SportEvent>(sqlSelectSportEventsFiltered).ToList();
            }

            return resultList;
        }
    }
}
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

        public static List<SportEventListModel> GetAllSportEvents()
        {
            List<SportEventListModel> resultList = new List<SportEventListModel>();
            string sqlSelectAllSportEvents = "SELECT * FROM [FitbourhoodDB].[dbo].[SportEvents] WHERE HasEnded = 0";

            using (var connection = new SqlConnection(ConnectionString))
            {
                resultList = connection.Query<SportEventListModel>(sqlSelectAllSportEvents).ToList();
            }

            return resultList;
        }

        public static List<SportEventListModel> GetSportEventsFiltered(SortedList<string, string> paramDictionary)
        {
            List<SportEventListModel> resultList = new List<SportEventListModel>();
            string sqlSelectSportEventsFiltered = "SELECT * FROM [FitbourhoodDB].[dbo].[SportEvents]";

            if (paramDictionary.Any(x => x.Value != null))
            {
                sqlSelectSportEventsFiltered += " WHERE ";
                foreach (var param in paramDictionary)
                {
                    if (!string.IsNullOrWhiteSpace(param.Value))
                    {
                        sqlSelectSportEventsFiltered += string.Format("[{0}] {1}", param.Key, param.Value);
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
                resultList = connection.Query<SportEventListModel>(sqlSelectSportEventsFiltered).ToList();
            }

            return resultList;
        }
    }
}
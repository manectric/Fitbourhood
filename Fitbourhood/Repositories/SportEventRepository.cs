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
            ErrorList = new List<string>();
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
            ErrorList = new List<string>();
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

        public static SportEventModel GetSportEventDetails(int id)
        {
            SportEventModel result;

            string sqlGetSportEvent = "SELECT * FROM [FitbourhoodDB].[dbo].[SportEvents] WHERE ID = @ID";

            using (var connection = new SqlConnection(ConnectionString))
            {
                result = connection.QueryFirstOrDefault<SportEventModel>(sqlGetSportEvent, new { ID = id });
            }

            return result;
        }

        public static bool AddSportEvent(SportEventModel sportEvent)
        {
            ErrorList = new List<string>();
            bool result = false;

            if (sportEvent != null)
            {
                string sqlAddSportEvent = "INSERT INTO [FitbourhoodDB].[dbo].[SportEvents] (CreatorID, DDisciplineID, Date, Time, MaxCapacity, Address, CoordinateLatitude, CoordinateLongitude, Description, HasEnded) Values (@CreatorID, @DDisciplineID, @Date, @Time, @MaxCapacity, @Address, @CoordinateLatitude, @CoordinateLongitude, @Description, @HasEnded);";

                using (var connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    var affectedRows = connection.Execute(sqlAddSportEvent, new
                    {
                        CreatorID = sportEvent.CreatorID,
                        DDisciplineID = sportEvent.DDiscipline,
                        Date = sportEvent.Date,
                        Time = sportEvent.Time,
                        MaxCapacity = sportEvent.MaxCapacity,
                        Address = sportEvent.Address,
                        CoordinateLatitude = sportEvent.CoordinateLatitude,
                        CoordinateLongitude = sportEvent.CoordinateLongitude,
                        Description = sportEvent.Description,
                        HasEnded = sportEvent.HasEnded 
                    });
                    if (affectedRows > 0)
                        result = true;
                    else
                        ErrorList.Add("Wystąpił błąd podczas tworzenia nowego wydarzenia. Spróbuj ponownie.");
                }
            }

            return result;
        }
    }
}
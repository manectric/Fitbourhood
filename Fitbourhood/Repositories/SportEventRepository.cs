using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Dapper;
using Fitbourhood.Models;
using Microsoft.Ajax.Utilities;

namespace Fitbourhood.Repositories
{
    public static class SportEventRepository
    {
        public static string ConnectionString = "Data Source=DESKTOP-VDGQKFQ; Initial Catalog=FitbourhoodDB; User id = sa; Password = s0ng0Kussj!00;";
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

        public static List<ContactDetails> GetContactDetailsForSportEvent(int sportEventId, int userId)
        {
            List<ContactDetails> result = new List<ContactDetails>();

            string sqlGetContactDetails = 
              " SELECT Name, PhoneNumber "
            + " FROM[dbo].[Users] u LEFT JOIN[dbo].[SportEvents] se ON se.ID = @SportEventID "
            + " WHERE se.ID = @SportEventID "
            + " AND "
            + " ( "
            +      " (se.CreatorID = @UserID AND u.ID IN(SELECT UserID FROM[dbo].[Users_SportEvents] WHERE UserID <> @UserID AND SportEventID = @SportEventID)) "
            + " OR "
            +      " (se.CreatorID <> @UserID AND u.ID IN(SELECT CreatorID FROM[dbo].[SportEvents] WHERE ID = @SportEventID)) "
            + " )";

            using (var connection = new SqlConnection(ConnectionString))
            {
                result = connection.QueryMultiple(sqlGetContactDetails, new {SportEventID = sportEventId, UserID = userId}).Read<ContactDetails>().ToList();
            }

            return result;
        }

        public static bool AddSportEvent(SportEventModel sportEvent)
        {
            ErrorList = new List<string>();
            bool result = false;

            if (sportEvent != null)
            {
                string sqlAddSportEvent = "INSERT INTO [FitbourhoodDB].[dbo].[SportEvents] (CreatorID, DDisciplineID, Date, Time, MaxCapacity, Address, CoordinateLatitude, CoordinateLongitude, Description, HasEnded) Values (@CreatorID, @DDisciplineID, @Date, @Time, @MaxCapacity, @Address, @CoordinateLatitude, @CoordinateLongitude, @Description, @HasEnded); SELECT CAST(SCOPE_IDENTITY() as int)";

                using (var connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    var insertedSportEventID = connection.Query<int>(sqlAddSportEvent, new
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
                    }).Single();
                    if (insertedSportEventID > 0)
                    {
                        result = AddUserToSportEvent(sportEvent.CreatorID, insertedSportEventID);
                    }
                    else
                        ErrorList.Add("Wystąpił błąd podczas tworzenia nowego wydarzenia. Spróbuj ponownie.");
                }
            }

            return result;
        }

        public static bool ChangeUserParticipationStatusInSportEvent(int userId, int sportEventId, bool join)
        {
            bool result = false;

            if (join && !IsUserParticipatingInSportEvent(userId, sportEventId))
                result = AddUserToSportEvent(userId, sportEventId);
            else if (!join && IsUserParticipatingInSportEvent(userId, sportEventId))
                result = RemoveUserFromSportEvent(userId, sportEventId);

            return result;
        }

        public static bool IsUserParticipatingInSportEvent(int userId, int sportEventId)
        {
            bool result = false;

            string sqlGetSportEvent = "SELECT * FROM [FitbourhoodDB].[dbo].[Users_SportEvents] WHERE [UserID] = @UserID AND [SportEventID] = @SportEventID";

            using (var connection = new SqlConnection(ConnectionString))
            {
                var queryResult = connection.QueryFirstOrDefault(sqlGetSportEvent, new { UserID = userId, SportEventID = sportEventId });
                if (queryResult != null)
                    result = true;
            }

            return result;
        }

        public static bool AddUserToSportEvent(int userId, int sportEventId)
        {
            bool result = false;

            string sqlInsertUserSportEvent = "INSERT INTO [FitbourhoodDB].[dbo].[Users_SportEvents] (UserID, SportEventID, IsNotificationSended, IsNotificationSendedToCreator) Values (@UserID, @SportEventID, @IsNotificationSended, @IsNotificationSendedToCreator);";
            string sqlSelectCurrentNumberOfUsersInEvent = "SELECT Count(u_se.ID) as UserCount, se.MaxCapacity FROM [FitbourhoodDB].[dbo].[Users_SportEvents] u_se LEFT JOIN [FitbourhoodDB].[dbo].[SportEvents] se ON u_se.SportEventID = se.ID WHERE SportEventID = @SportEventID GROUP BY se.MaxCapacity";

            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var canAddUser = true;
                var selectResult = connection.QueryFirstOrDefault(sqlSelectCurrentNumberOfUsersInEvent,
                    new {SportEventID = sportEventId});

                if (selectResult != null)
                {
                    if (selectResult.MaxCapacity - selectResult.UserCount == 0)
                    {
                        canAddUser = false;
                        ErrorList.Add("Brak miejsc.");
                    }
                }

                if (canAddUser)
                {
                    var affectedRows = connection.Execute(sqlInsertUserSportEvent, new { UserID = userId, SportEventID = sportEventId, IsNotificationSended = false, IsNotificationSendedToCreator = false });
                    if (affectedRows > 0)
                        result = true;
                }
                
            }

            return result;
        }

        public static bool RemoveUserFromSportEvent(int userId, int sportEventId)
        {
            bool result = false;

            string sql = "DELETE FROM [FitbourhoodDB].[dbo].[Users_SportEvents] WHERE UserID = @UserID AND SportEventID = @SportEventID";

            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var affectedRows = connection.Execute(sql, new { UserID = userId, SportEventID = sportEventId });
                if (affectedRows > 0)
                    result = true;
            }

            return result;
        }
    }
}
﻿using EntryDesignConcept.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EntryDesignConcept.AppContext
{
    public class UserContext : DataAccessHelper
    {
        #region -- Private Members --
        private static string _pkField = "userID";
        private static string _tableName = "tbluser";
        private PositionContext dbPosition = new PositionContext();
        private OrganizationContext dbOrg = new OrganizationContext();
        private TeamContext dbTeam = new TeamContext();
        #endregion

        #region -- Properties --
        // maps fields in the database to be inserted or updated.
        private List<string> TargetFields
        {
            get
            {
                return new List<string> { "name",
                                          "positionID",
                                          "businessunitID",
                                          "teamid"};
            }
        }
        public IEnumerable<User> GetAllUsers
        {
            get
            {
                List<User> users = new List<User>();
                using (MySqlConnection myConn = MySqlConn)
                {
                    MySqlCommand cmd = new MySqlCommand
                    {
                        CommandText = QueryBuilder.SelectAll(_tableName),
                        Connection = myConn
                    };
                    myConn.Open();
                    MySqlDataReader rd = cmd.ExecuteReader();

                    while (rd.Read())
                    {
                        User user = new User();

                        user.ID = Convert.ToInt32(rd[_pkField]);
                        user.Fullname = rd["name"].ToString();
                        user.PositionID = dbPosition.GetAllActivities.Single(p => p.ID == Convert.ToInt32(rd["positionID"]));
                        user.OrganizationID = dbOrg.Fetch.Single(o => o.ID == Convert.ToInt32(rd["businessunitID"]));
                        user.TeamID = dbTeam.Fetch.Single(t => t.ID == Convert.ToInt32(rd["teamid"]));
                        users.Add(user);
                    }

                    myConn.Close();
                }

                return users;
            }
        }
        #endregion

        #region -- Methods --
        public void Insert(User data)
        {
            ExecuteNonQuery(QueryBuilder.Insert(_tableName, TargetFields), SetParams(data));
        }

        public void Update(User data, int id)
        {
            ExecuteNonQuery(QueryBuilder.Update(_tableName, TargetFields, id, _pkField), SetParams(data));
        }
        // sets parameters for insert/update
        private Dictionary<string, object> SetParams(User data)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();

            result.Add("@name", data.Fullname);
            result.Add("@positionID", data.PositionID);
            result.Add("@businessunitID", data.PositionID);
            result.Add("@teamid", data.PositionID);

            return result;
        }
        #endregion    

    }
}
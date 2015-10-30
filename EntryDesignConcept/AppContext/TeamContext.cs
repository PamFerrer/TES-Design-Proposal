using EntryDesignConcept.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace EntryDesignConcept.AppContext
{
    public class TeamContext : DataAccessHelper
    {
        #region -- Private Members --
        private static string _pkField = "id";
        private static string _tableName = "tblteam";

        #endregion

        #region -- Properties --
        // maps fields in the database to be inserted or updated.
        private List<string> TargetFields
        {
            get
            {
                return new List<string> { "name" };
            }
        }
        public IEnumerable<Team> Fetch
        {
            get
            {
                List<Team> teams = new List<Team>();
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
                        Team team = new Team();

                        team.ID = Convert.ToInt32(rd[_pkField]);
                        team.Name = rd["name"].ToString();
                        teams.Add(team);
                    }

                    myConn.Close();
                }

                return teams;
            }
        }
        #endregion

        #region -- Methods --
        public void Insert(Team data)
        {
            ExecuteNonQuery(QueryBuilder.Insert(_tableName, TargetFields), SetParams(data));
        }

        public void Update(Team data, int id)
        {
            ExecuteNonQuery(QueryBuilder.Update(_tableName, TargetFields, id, _pkField), SetParams(data));
        }
        // sets parameters for insert/update
        private Dictionary<string, object> SetParams(Team data)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();

            result.Add("@name", data.Name);

            return result;
        }
        #endregion    


    }
}
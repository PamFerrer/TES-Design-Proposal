using EntryDesignConcept.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EntryDesignConcept.AppContext
{
    public class SalesOrderContext : DataAccessHelper
    {
        #region -- Private Members --
        private static string _pkField = "id";
        private static string _tableName = "tblsalesorder";
        private UserContext dbUser = new UserContext();
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
                                          "managerid",
                                          "businessunitid",
                                          "teamid",
                                          "status"};
            }
        }
        public IEnumerable<SalesOrder> GetAllUsers
        {
            get
            {
                List<SalesOrder> users = new List<SalesOrder>();
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
                        SalesOrder user = new SalesOrder();

                        user.ID = Convert.ToInt32(rd[_pkField]);
                        user.Name = rd["name"].ToString();
                        user.Manager = dbUser.GetAllUsers.Single(u => u.ID == Convert.ToInt32(rd["managerid"]));                      
                        user.Organization = dbOrg.Fetch.Single(o => o.ID == Convert.ToInt32(rd["businessunitid"]));
                        user.Team = dbTeam.Fetch.Single(t => t.ID == Convert.ToInt32(rd["teamid"]));
                        user.Status = Convert.ToBoolean(rd["status"]);

                        users.Add(user);
                    }

                    myConn.Close();
                }

                return users;
            }
        }
        #endregion

        #region -- Methods --
        public void Insert(SalesOrder data)
        {
            ExecuteNonQuery(QueryBuilder.Insert(_tableName, TargetFields), SetParams(data));
        }

        public void Update(SalesOrder data, int id)
        {
            ExecuteNonQuery(QueryBuilder.Update(_tableName, TargetFields, id, _pkField), SetParams(data));
        }
        // sets parameters for insert/update
        private Dictionary<string, object> SetParams(SalesOrder data)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();

            result.Add("@name", data.Name);
            result.Add("@managerid", data.Manager.ID);
            result.Add("@businessunitid", data.Organization.ID);
            result.Add("@positionID", data.Team.ID);
            result.Add("@status", data.Status);



            return result;
        }
        #endregion    

    }
}
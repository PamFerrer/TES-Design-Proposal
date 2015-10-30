using EntryDesignConcept.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EntryDesignConcept.AppContext
{
    public class OrganizationContext : DataAccessHelper
    {
        #region -- Private Members --
        private static string _pkField = "id";
        private static string _tableName = "tblbusinessunit";

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
        public IEnumerable<Organization> Fetch
        {
            get
            {
                List<Organization> orgs = new List<Organization>();
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
                        Organization org = new Organization();

                        org.ID = Convert.ToInt32(rd[_pkField]);
                        org.Name = rd["name"].ToString();
                        orgs.Add(org);
                    }

                    myConn.Close();
                }

                return orgs;
            }
        }
        #endregion

        #region -- Methods --
        public void Insert(Organization data)
        {
            ExecuteNonQuery(QueryBuilder.Insert(_tableName, TargetFields), SetParams(data));
        }

        public void Update(Organization data, int id)
        {
            ExecuteNonQuery(QueryBuilder.Update(_tableName, TargetFields, id, _pkField), SetParams(data));
        }
        // sets parameters for insert/update
        private Dictionary<string, object> SetParams(Organization data)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();

            result.Add("@name", data.Name);

            return result;
        }
        #endregion    

    }
}
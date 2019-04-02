using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace NetFramework
{
    public static class Utils
    {
        public static List<Dictionary<string, object>> Convert(this DataTable dt)
        {
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;
            foreach (DataRow dr in dt.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    row.Add(col.ColumnName, dr[col]);
                }
                rows.Add(row);
            }
            return rows;
        }
    }
}
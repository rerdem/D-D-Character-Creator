using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easy_DnD_Character_Creator.DataManagement
{
    public static class SQLiteCommandExtensions
    {
        public static void AddParametersWithValues(SQLiteCommand command, string parameterName, List<string> values)
        {
            List<string> parameterNames = new List<string>();

            for (int i = 0; i < values.Count; i++)
            {
                string currentParameterName = parameterName + i.ToString();
                command.Parameters.AddWithValue(currentParameterName, values.ElementAt(i));
                parameterNames.Add(currentParameterName);
            }

            command.CommandText = command.CommandText.Replace(parameterName, string.Join(",", parameterNames));
        }
    }
}
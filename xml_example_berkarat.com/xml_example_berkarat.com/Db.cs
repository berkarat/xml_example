using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace xml_example_berkarat.com
{

    class Db
    {

        public static string ConnectionString()
        {
            string connectionString = "";
            try
            {
                connectionString = "Server=" + Properties.Settings.Default.DBServer +
                ";Database=" + Properties.Settings.Default.DBName +
                ";User Id=" + Properties.Settings.Default.DBUsername +
                ";Password=" + Properties.Settings.Default.DBPassword +
                ";Persist Security Info = true";
            }
            catch (Exception ex)
            {

                connectionString = ex.Message;
            }


            return connectionString;
        }


    }
}

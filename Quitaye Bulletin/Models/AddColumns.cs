using Quitaye_School;
using Quitaye_School.User_Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quitaye_Medical.Models
{
    public class AddColumns
    {
        public static void Addcolumn(DataGridView dgv)
        {
            DeleteColumn delete = new DeleteColumn();
            delete.HeaderText = "Sup";
            delete.Name = "Sup";

            EditColumn edit = new EditColumn();
            edit.HeaderText = "Edit";
            edit.Name = "Edit";
            edit.Width = 20;

            dgv.Columns.Add(edit);
            dgv.Columns.Add(delete);
            dgv.Columns["Edit"].Width = 20;
            dgv.Columns["Sup"].Width = 30;
        }
        public static void AddDetail(DataGridView dgv)
        {
            DossierColumn dos = new DossierColumn();
            dos.HeaderText = "Details";
            dos.Name = "Details";

            dgv.Columns.Add(dos);
            dgv.Columns["Details"].Width = 40;
        }

        
        public static Task<bool> AddColAsync(string table, string column, string type)
        {
            return Task.Factory.StartNew(() => AddCol(table, column, type));
        }
        private static bool AddCol(string table, string column, string type)
        {
            System.Data.SqlClient.SqlConnection cn = new System.Data.SqlClient.SqlConnection();
            cn.ConnectionString = LogIn.mycontrng;
            cn.Open();
            string str = "ALTER TABLE  [dbo].[" + table + "] ADD  " + column + " " + type + "  NULL ";
            SqlCommand cmd = new SqlCommand(str, cn);
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {

            }

            cn.Close();
            return true;
        }
    }
}

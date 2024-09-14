using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static PrintAction.PlusColumns;

namespace Quitaye_School.Models
{
    public static class DataGrid
    {
        public static void FillCbxAsync(ComboBox cbx, DataTable dt, string displayMember = "Name", string valueMember = "Id")
        {
            cbx.DataSource = dt;
            cbx.DisplayMember = displayMember;
            cbx.ValueMember = valueMember;
            cbx.Text = null;
        }

        public static void ShowDataGrid(MyTable table, DataGridView[] dg, bool show_Column_id = false, bool show_dossier = false, bool show_delete = false, bool show_edit = false, bool show_select = false)
        {
            if (table.Table.Rows.Count != 0)
            {
                DataGridView[] dataGridViewArray = dg;
                for (int i = 0; i < (int)dataGridViewArray.Length; i++)
                {
                    DataGridView item = dataGridViewArray[i];
                    item.Columns.Clear();
                    item.DataSource = table.Table;
                    if (item.Columns["Id"] != null)
                    {
                        item.Columns["Id"].Visible = show_Column_id;
                    }
                    if (show_select)
                    {
                        DataGridViewCheckBoxColumn cell = new DataGridViewCheckBoxColumn()
                        {
                            HeaderText = "Selection",
                            Name = "Selection"
                        };
                        item.Columns.Add(cell);
                        item.Columns["Selection"].Width = 60;
                    }
                    if (show_dossier)
                    {
                        DossierColumn delete = new DossierColumn()
                        {
                            HeaderText = "Détails",
                            Name = "Dossier"
                        };
                        item.Columns.Add(delete);
                        item.Columns["Dossier"].Width = 50;
                    }
                    if (show_edit)
                    {
                        EditColumn delete = new EditColumn()
                        {
                            HeaderText = "Edit",
                            Name = "Edit"
                        };
                        item.Columns.Add(delete);
                        item.Columns["Edit"].Width = 50;
                    }
                    if (show_delete)
                    {
                        DeleteColumn delete = new DeleteColumn()
                        {
                            HeaderText = "Sup",
                            Name = "Sup"
                        };
                        item.Columns.Add(delete);
                        item.Columns["Sup"].Width = 50;
                    }
                }
            }
            else
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("Tableau Vide");
                dt.Rows.Add(new object[] { "Aucune donnée dans ce tableau" });
                DataGridView[] dataGridViewArray1 = dg;
                for (int j = 0; j < (int)dataGridViewArray1.Length; j++)
                {
                    DataGridView item = dataGridViewArray1[j];
                    item.Columns.Clear();
                    item.DataSource = dt;
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quitaye_School.Models
{
    public class MesureTable
    {
        private DataTable dataTable;

        public DataTable Table
        {
            get { return dataTable; }
            set { dataTable = value; }
        }

        private List<RowsList> rowsLists;

        public List<RowsList> RowsLists
        {
            get { return rowsLists; }
            set { rowsLists = value; }
        }
    }

    public class RowsList
    {
        private int _id;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private bool _default;

        public bool Default
        {
            get { return _default; }
            set { _default = value; }
        }

        private string _nom;

        public string Mesure
        {
            get { return _nom; }
            set { _nom = value; }
        }

        private string _type;

        public string Type
        {
            get { return _type; }
            set { _type = value; }
        }


    }
}

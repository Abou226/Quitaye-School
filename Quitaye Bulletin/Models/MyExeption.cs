using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quitaye_School.User_Interface;

namespace Quitaye_School.Models
{
    public class MyExeption
    {

        public  static Task<bool> ErrorReseauAsync(Exception ex)
        {
            return Task.Factory.StartNew(() => ErrorReseau(ex));
        }
        private static bool ErrorReseau(Exception ex)
        {
            var w32ex = ex as SqlException;
            if (w32ex == null)
            {
                w32ex = ex.InnerException as SqlException;
                int code = w32ex.ErrorCode;
            }
            if (w32ex != null)
            {
                int code = w32ex.ErrorCode;
                // do stuff

                if (code == -2146232060)
                {
                    return true;
                }
                else return false;
            }
            else return false;
        }
    }
}

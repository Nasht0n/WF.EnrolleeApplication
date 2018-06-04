using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WF.EnrolleeApplication.DataAccess.EntityFramework;

namespace TestingDAL
{
    class Program
    {
        static void Main(string[] args)
        {
            
            bool connectionIsOpen = ConnectionTest();
            if (connectionIsOpen) Console.WriteLine("Соединение установленно");
            Console.ReadKey();
        }

        private static bool ConnectionTest()
        {
            Console.WriteLine("Тестирование подключения");
            bool result = false;
            string connectionString = $@"data source=(local);initial catalog=dbEnrollee2018;persist security info=True;user id=sa;password=mysqlpw;MultipleActiveResultSets=True;App=EntityFramework";
            try
            {
                using (var context = new EnrolleeContext(connectionString))
                {
                    result = true;
                }
            }
            catch(SqlException ex)
            {
                result = false;
            }
            return result;
        }
    }
}

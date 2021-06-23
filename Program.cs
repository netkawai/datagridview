using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Data;
using System.Data.Odbc;

namespace CSWinFormDataGridView
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {


            // The connection string assumes that the Access
            // Northwind.mdb is located in the c:\Data folder.
            string connectionString =
                "Driver={Microsoft Access Driver (*.mdb)};"
                + "Dbq=c:\\Data\\Northwind.mdb;Uid=Admin;Pwd=;";

            // Provide the query string with a parameter placeholder.
            string queryString =
                "SELECT ProductID, UnitPrice, ProductName from products "
                    + "WHERE UnitPrice > ? "
                    + "ORDER BY UnitPrice DESC;";

            // Specify the parameter value.
            int paramValue = 5;

            // Create and open the connection in a using block. This
            // ensures that all resources will be closed and disposed
            // when the code exits.
            using (OdbcConnection connection =
                new OdbcConnection(connectionString))
            {
                // Create the Command and Parameter objects.
                OdbcCommand command = new OdbcCommand(queryString, connection);
                command.Parameters.AddWithValue("@pricePoint", paramValue);

                // Open the connection in a try/catch block.
                // Create and execute the DataReader, writing the result
                // set to the console window.
                try
                {
                    connection.Open();
                    OdbcDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Console.WriteLine("\t{0}\t{1}\t{2}",
                            reader[0], reader[1], reader[2]);
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                Console.ReadLine();
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.Run(new CustomDataGridViewColumn.MainForm());
            //Application.Run(new DataGridViewPaging.MainForm());
            //Application.Run(new EditingControlHosting.MainForm());
            //Application.Run(new JustInTimeDataLoading.MainForm());
            //Application.Run(new MultipleLayeredColumnHeader.MainForm());
        }
    }
}

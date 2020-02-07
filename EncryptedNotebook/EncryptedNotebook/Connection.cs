using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EncryptedNotebook
{
    public class Connection
    {
        public void Connect(List<Notes> ts)
        {

            var notes = new List<Notes>();
            Notes note = new Notes();

            foreach (var not in ts)
            {
                SqlConnection sqlCon = new SqlConnection(@"Data Source=localhost\SQLEXPRESS; Initial Catalog=NotesTable; Integrated Security = True;");
                try
                {
                    if (sqlCon.State == ConnectionState.Closed)
                        sqlCon.Open();
                    String query = "INSERT INTO NotesTable (Date, Author, Body) VALUES (@Date, @Author, @Body)";
                    SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                    sqlCmd.Parameters.AddWithValue("@Date", DateTime.Now);
                    sqlCmd.Parameters.AddWithValue("@Author", note.Author);
                    sqlCmd.Parameters.AddWithValue("@Body", note.Body);
                    int count = Convert.ToInt32(sqlCmd.ExecuteScalar());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    sqlCon.Close();
                }
            }
        }
    }
}

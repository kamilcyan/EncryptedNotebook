using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Linq;

namespace EncryptedNotebook
{
    public partial class MainWindow : Window
    {
        public List<Notes> notes { get; set; }

        LogWindow log = new LogWindow();


        public MainWindow()
        {
            DataContext = this;
            InitializeComponent();
            notes = new List<Notes>();
            //dataGrid.ItemsSource = notes;
            //Notes note = (Notes)dataGrid.SelectedItem;
            string user = log.UserBox.Text;

            SqlConnection sqlCon = new SqlConnection(@"Data Source=localhost\SQLEXPRESS; Initial Catalog=NotesDB; Integrated Security = True;");
            try
            {
                if (sqlCon.State == ConnectionState.Closed)
                    sqlCon.Open();

                string query = "SELECT * FROM NotesTable";
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.ExecuteNonQuery();

                SqlDataAdapter dataAdp = new SqlDataAdapter(sqlCmd);
                DataTable dt = new DataTable("NotesTable");
                dataAdp.Fill(dt);
                dataGrid.ItemsSource = dt.DefaultView;
                dataAdp.Update(dt);

                sqlCon.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }


        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string user = log.UserBox.Text;
            if (NoteBox.Text != "")
            {
                Notes note = new Notes();
                note.Body = NoteBox.Text;
                notes.Add(note);
                SqlConnection sqlCon = new SqlConnection(@"Data Source=localhost\SQLEXPRESS; Initial Catalog=NotesDB; Integrated Security = True;");
                try
                {
                    if (sqlCon.State == ConnectionState.Closed)
                        sqlCon.Open();

                    DateTime Date = DateTime.Now;
                    string query = "INSERT INTO NotesTable (Date, Author, Body) VALUES (@Date, @Author, @Body)";
                    SqlCommand sqlCmd = new SqlCommand(query, sqlCon);

                    sqlCmd.CommandType = CommandType.Text;
                    sqlCmd.Parameters.AddWithValue("@Date", Date);
                    sqlCmd.Parameters.AddWithValue("@Author", user);
                    sqlCmd.Parameters.AddWithValue("@Body", note.Body);
                    Convert.ToInt32(sqlCmd.ExecuteScalar());

                    dataGrid.Items.Refresh();
                    
                    NoteBox.Text = "";
                    NoteBox.IsReadOnly = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }


            
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            NoteBox.IsReadOnly = false;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            NoteBox.IsReadOnly = false;
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            string user = log.UserBox.Text;

            Notes note = new Notes();
            note = (Notes)dataGrid.SelectedItem;
            if (note != null)
            {
                notes.Remove(note);
                NoteBox.Text = "";
                dataGrid.Items.Refresh();
                SqlConnection sqlCon = new SqlConnection(@"Data Source=localhost\SQLEXPRESS; Initial Catalog=NotesDB; Integrated Security = True;");
                try
                {
                    if (sqlCon.State == ConnectionState.Closed)
                        sqlCon.Open();
                    string Author = user;
                    string Body = note.Body;
                    DateTime Date = DateTime.Now;
                    String query = "DELETE FROM NotesTable WHERE @Date = Date";
                    SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                    sqlCmd.CommandType = CommandType.Text;
                    sqlCmd.Parameters.AddWithValue("@Date", Date);
                    sqlCmd.Parameters.AddWithValue("@Author", user);
                    sqlCmd.Parameters.AddWithValue("@Body", note.Body);
                    Convert.ToInt32(sqlCmd.ExecuteScalar());
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

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Notes notes = new Notes();
           
            try
            {
                notes = (Notes)dataGrid.SelectedItem;
                NoteBox.Text = notes.Body;
            }
            catch { }

        }
    }
}

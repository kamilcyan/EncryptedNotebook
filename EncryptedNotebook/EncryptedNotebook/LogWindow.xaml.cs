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
using System.Windows.Shapes;

namespace EncryptedNotebook
{
    /// <summary>
    /// Logika interakcji dla klasy LogWindow.xaml
    /// </summary>
    public partial class LogWindow : Window
    {
        public static string username;
        public static string recby
        {
            get { return username; }
            set { username = value; }
        }

        public LogWindow()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection sqlCon = new SqlConnection(@"Data Source=localhost\SQLEXPRESS; Initial Catalog=LoginDB; Integrated Security = True;");
            try
            {
                if (sqlCon.State == ConnectionState.Closed)
                    sqlCon.Open();
                
                String query = "SELECT COUNT(1) FROM tblUser WHERE Username=@Username AND Password=@Password";
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.CommandType = CommandType.Text;
                sqlCmd.Parameters.AddWithValue("@Username", UserBox.Text);
                recby = UserBox.Text;
                sqlCmd.Parameters.AddWithValue("@Password", PasswordBox.Password);
                int count = Convert.ToInt32(sqlCmd.ExecuteScalar());
                if(count == 1)
                {
                    MainWindow dashboard = new MainWindow();
                    
                    dashboard.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Username or password incorrect");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlCon.Close();
            }
        }

        private void AddUserButton_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection sqlCon = new SqlConnection(@"Data Source=localhost\SQLEXPRESS; Initial Catalog=LoginDB; Integrated Security = True;");
            try
            {
                if (sqlCon.State == ConnectionState.Closed)
                    sqlCon.Open();
                string user = UserBox.Text;
                var Password = PasswordBox.Password;
                String query = "INSERT INTO tblUser (UserName, Password) VALUES (@Username, @Password)";
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.Parameters.AddWithValue("@Username", user);
                sqlCmd.Parameters.AddWithValue("@Password", Password);
                int count = Convert.ToInt32(sqlCmd.ExecuteScalar());
                LogWindow logWindow = new LogWindow();
                logWindow.Show();
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

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

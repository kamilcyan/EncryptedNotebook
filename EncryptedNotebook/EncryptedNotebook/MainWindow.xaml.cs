using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
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

namespace EncryptedNotebook
{
    public partial class MainWindow : Window
    {
        public List<Notes> notes { get; set; }

        public MainWindow()
        {

            DataContext = this;
            InitializeComponent();
            //notes = new List<Notes>();
            //Notes note = new Notes();
            //string user = LogWindow.recby;
            
            dataGrid.ItemsSource = notes;
            //Notes note = (Notes)dataGrid.SelectedItem;



        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            LogWindow logWindow = new LogWindow();
            Connection connection = new Connection();
            

            notes = new List<Notes>();
            Notes note = new Notes();
            note.Body = NoteBox.Text;
            note.Author = LogWindow.recby;
            notes.Add(note);
            
            dataGrid.Items.Refresh();

            

            JsonSerializer serializer = new JsonSerializer();
            serializer.Converters.Add(new JavaScriptDateTimeConverter());
            serializer.NullValueHandling = NullValueHandling.Ignore;
            string output = JsonConvert.SerializeObject(notes);
            
            MessageBox.Show(output);

            NoteBox.Text = "";
            NoteBox.IsReadOnly = true;

            
            


            //DataGridTextColumn textColumn = new DataGridTextColumn();
            //textColumn.Header = "Body";
            //textColumn.Width = 50;
            //dataGrid.Columns.Add(textColumn);


            //this.dataGrid.Rows.Add()

            //dataGrid.ItemsSource = jPersonComplex.Body.ToString();
        }

        //public void view(string outp)
        //{
        //    var jNotes = JsonConvert.DeserializeObject<Notes>(outp);
        //    dataGrid.ItemsSource =  jNotes;
        //}

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
            Notes note = new Notes();
            note = (Notes)dataGrid.SelectedItem;
            if (note != null)
            {
                notes.Remove(note);
                NoteBox.Text = "";
                dataGrid.Items.Refresh();
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

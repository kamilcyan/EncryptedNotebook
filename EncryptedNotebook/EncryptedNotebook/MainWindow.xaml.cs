using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.IO;
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


            notes = new List<Notes>();
            dataGrid.ItemsSource = notes;
            //Notes note = (Notes)dataGrid.SelectedItem;

            // read file into a string and deserialize JSON to a type
            //Notes note1 = JsonConvert.DeserializeObject<Notes>(File.ReadAllText(@"D:\git\EncryptedNotebook\notes.json"));

            // deserialize JSON directly from a file
            
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            LogWindow logWindow = new LogWindow();
            Connection connection = new Connection();
            

            Notes note = new Notes();

            note.Body = NoteBox.Text;
            note.Author = LogWindow.recby;
            notes.Add(note);
            dataGrid.ItemsSource = notes;
            dataGrid.Items.Refresh();
            dataGrid.UpdateLayout();
            save();

            

            //JsonSerializer serializer = new JsonSerializer();
            //serializer.Converters.Add(new JavaScriptDateTimeConverter());
            //serializer.NullValueHandling = NullValueHandling.Ignore;
            //string output = JsonConvert.SerializeObject(notes);
            
            MessageBox.Show("done");

            NoteBox.Text = "";
            NoteBox.IsReadOnly = true;

            
            


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

        public void save(/*List<Notes> note*/)
        {

            File.WriteAllText(@"D:\git\EncryptedNotebook\notes.json", JsonConvert.SerializeObject(notes));

            // serialize JSON directly to a file
            using (StreamWriter file = File.CreateText(@"D:\git\EncryptedNotebook\notes.jso"))
            {
                JsonSerializer serializer1 = new JsonSerializer();
                serializer1.Serialize(file, notes);
            }
        }

        public void read()
        {
            using (StreamReader file = File.OpenText(@"D:\git\EncryptedNotebook\notes.json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                Notes note2 = (Notes)serializer.Deserialize(file, typeof(Notes));

                dataGrid.ItemsSource = note2.ToString();
            }
        }
    }
}

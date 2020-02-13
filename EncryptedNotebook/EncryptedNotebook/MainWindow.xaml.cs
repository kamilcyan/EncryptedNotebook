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

            firstRead();
        }

        private void firstRead()
        {
            using (StreamReader file = File.OpenText(@"D:\git\EncryptedNotebook\notes.json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                Notes[] temporary = (Notes[])serializer.Deserialize(file, typeof(Notes[]));
                
                foreach (var element in temporary)
                {
                    notes.Add(element);
                }

                dataGrid.ItemsSource = notes;
                dataGrid.UpdateLayout();
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            Connection connection = new Connection();

            Notes note = new Notes();

            note.Body = NoteBox.Text;
            note.Author = LogWindow.recby;
            notes.Add(note);
            
            dataGrid.Items.Refresh();
            dataGrid.UpdateLayout();

            MessageBox.Show("done");

            NoteBox.Text = "";
            NoteBox.IsReadOnly = true;

            dataGrid.ItemsSource = notes;
            dataGrid.Items.Refresh();
            dataGrid.UpdateLayout();

            save();
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            NoteBox.IsReadOnly = false;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            NoteBox.Text = "";
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
                save();
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

        public void save()
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
                Notes[] dupa = (Notes[])serializer.Deserialize(file, typeof(Notes[]));

                //var nott = new List<Notes>();
                foreach (var element in dupa)
                {
                    notes.Add(element);
                }
            }
        }
    }
}

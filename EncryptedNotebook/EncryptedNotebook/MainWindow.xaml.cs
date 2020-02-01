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
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<Notes> notes { get; set; }

        public MainWindow()
        {
            DataContext = this;
            InitializeComponent();

            notes = new List<Notes>();
            dataGrid.ItemsSource = notes;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (NoteBox.Text != null)
            {
                Notes note = new Notes();
                note.Body = NoteBox.Text;
                notes.Add(note);
                NoteBox.Text = "";
                NoteBox.IsReadOnly = true;
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
            
        }
    }
}

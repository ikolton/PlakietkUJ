using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace PlakietkUJ
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void CreatePlateButton_Click(object sender, RoutedEventArgs e)
        {
            //PlateEditorWindow plateEditorWindow = new PlateEditorWindow();
            //plateEditorWindow.Show();
            PlateEditorWindow alternatePlateEditorWindow = new PlateEditorWindow();
            alternatePlateEditorWindow.Show();
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
using PlakietkUJ.PrintableElements;
using ColorPicker;

namespace PlakietkUJ
{
    /// <summary>
    /// Logika interakcji dla klasy PlateEditorWindow.xaml
    /// </summary>
    public partial class PlateEditorWindow : Window
    {
        List<PrintableElement> printableElements = new List<PrintableElement>();


        public PlateEditorWindow()
        {
            InitializeComponent();
            
        }

        

        private void AddTextField_Click(object sender, RoutedEventArgs e)
        {
            TextField textField = new TextField(0, 0, 10, 10, "Ustaw tekst", Brushes.Black, 14, new FontFamily("Arial"), Brushes.White);
            printableElements.Add(textField);
            Canvas canvas = CreateAndAddTextFieldToPlate(textField);

            PrintableObjectsControls.AddTextFieldControlsToEditablePanel(canvas, textField, EditableObjectsPanel);
            
            textField.PropertyChanged += () =>
            {
                EditTextFieldUiElement(canvas, textField);
            };
        }

        private void EditTextFieldUiElement(Canvas canvas, TextField textField)
        {
            TextBlock textBlock = (TextBlock)canvas.Children[0];
            textBlock.Text = textField.Text;
            textBlock.Foreground = textField.FontColor;
            textBlock.FontSize = textField.FontSize;
            textBlock.FontFamily = textField.Font;

            canvas.Width = textField.Width;
            canvas.Height = textField.Height;
            canvas.Background = textField.BackgroundColor;

            Canvas.SetLeft(canvas, textField.PosX);
            Canvas.SetTop(canvas, textField.PosY);
        }

        private Canvas CreateTextFieldUiElement(TextField textField)
        {
            TextBlock textBlock = new TextBlock
            {
                Text = textField.Text,
                Foreground = textField.FontColor,
                FontSize = textField.FontSize,
                FontFamily = textField.Font
            };

            Canvas canvas = new Canvas
            {
                Width = textField.Width,
                Height = textField.Height,
                Background = textField.BackgroundColor
            };
           

            canvas.Children.Add(textBlock);


            Canvas.SetLeft(canvas, textField.PosX);
            Canvas.SetTop(canvas, textField.PosY);

            return canvas;
        }




        private Canvas CreateAndAddTextFieldToPlate(TextField textField)
        {
            Canvas canvas = CreateTextFieldUiElement(textField);

            BackgroundBox.Children.Add(canvas);
            return canvas;
        }

        


    }
}

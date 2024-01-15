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
using Microsoft.Win32;
using Newtonsoft.Json;
using System.Printing;
using PlakietkUJ.HelperClass;

namespace PlakietkUJ
{
    public partial class PlateEditorWindow : Window
    {
        List<PrintableElement> printableElements = new List<PrintableElement>();


        public PlateEditorWindow()
        {
            InitializeComponent();
            AddBackgroundElement(CreateBasicBackgroundPrintableElement(100));
        }

        #region BackgroundElement


        private BackgroundElement CreateBasicBackgroundPrintableElement(int dpi)
        {
            int shorter = PageDpiHelper.GetShorterSideForA4(100);
            int longer = PageDpiHelper.GetLongerSide(shorter);
            return new BackgroundElement(0, 0, longer, shorter, Brushes.White);
        }

        void AddBackgroundElement(BackgroundElement backgroundElement)
        {
                printableElements.Add(backgroundElement);
                BackgroundBox.Width = backgroundElement.Width;
                BackgroundBox.Height = backgroundElement.Height;
                BackgroundBox.Background = backgroundElement.BackgroundColor;

                UIElement controls = PrintableObjectsControls.AddBackgroundElementControlsToEditablePanel(backgroundElement);

                EditableObjectsPanel.Children.Add(controls);

                backgroundElement.PropertyChanged += () =>
                {
                    EditBackgroundElementUiElement(backgroundElement);
                };


        }

        private void EditBackgroundElementUiElement(BackgroundElement backgroundElement)
        {
            BackgroundBox.Width = backgroundElement.Width;
            BackgroundBox.Height = backgroundElement.Height;
            BackgroundBox.Background = backgroundElement.BackgroundColor;
        }

        #endregion


        #region TextField

        private void AddTextField_Click(object sender, RoutedEventArgs e)
        {
            TextField textField = new TextField(0, 0, 10, 10, "Ustaw tekst", Brushes.Black, 14, new FontFamily("Arial"), Brushes.Transparent);
            AddTextField(textField);
        }

        private void AddTextField(TextField textField)
        {

            printableElements.Add(textField);
            Canvas canvas = CreateAndAddTextFieldToPlate(textField);

            Expander controls = PrintableObjectsControls.AddTextFieldControlsToEditablePanel(canvas, textField);
            EditableObjectsPanel.Children.Add(controls);

            textField.PropertyChanged += () =>
            {
                EditTextFieldUiElement(canvas, textField);
                controls.Header = "Edytuj tekst: " + textField.Text;
            };

            textField.Deleted += () =>
            {
                EditableObjectsPanel.Children.Remove(controls);
                printableElements.Remove(textField);
            };

            textField.Copy += () =>
            {
                TextField newTextField = new TextField(textField.PosX, textField.PosY + 50, textField.Width, textField.Height, textField.Text, textField.FontColor, textField.FontSize, textField.Font, textField.BackgroundColor);
                AddTextField(newTextField);
            };

            textField.BringToFront += () =>
            {
                BackgroundBox.Children.Remove(canvas);
                BackgroundBox.Children.Add(canvas);
            };
        }

        private void EditTextFieldUiElement(Canvas canvas, TextField textField)
        {
            TextBlock textBlock = (TextBlock)canvas.Children[0];
            textBlock.Text = textField.Text;
            textBlock.Foreground = textField.FontColor;
            textBlock.FontSize = textField.FontSize;
            textBlock.FontFamily = textField.Font;
            textBlock.FontStyle = textField.FontStyle;
            textBlock.FontWeight = textField.FontWeight;

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
                FontFamily = textField.Font,
                FontStyle = textField.FontStyle,
                FontWeight = textField.FontWeight
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

        #endregion


        #region ImageElement

        private void AddImageElement_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.png;*.jpeg;*.jpg;*.gif;*.bmp)|*.png;*.jpeg;*.jpg;*.gif;*.bmp|All Files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                ImageElement imageElement = new ImageElement(0, 0, 200, 200,1, new BitmapImage(new Uri(openFileDialog.FileName)), Brushes.Transparent);
                AddImageElement(imageElement);
            }
        }

        private void AddImageElement(ImageElement imageElement)
        {
            printableElements.Add(imageElement);
            Canvas canvas = CreateAndAddImageElementToPlate(imageElement);

            UIElement controls = PrintableObjectsControls.AddImageElementControlsToEditablePanel(canvas, imageElement);
            EditableObjectsPanel.Children.Add(controls);

            imageElement.PropertyChanged += () =>
            {
                EditImageElementUiElement(canvas, imageElement);
            };

            imageElement.Deleted += () =>
            {
                EditableObjectsPanel.Children.Remove(controls);
                printableElements.Remove(imageElement);
            };

            imageElement.Copy += () =>
            {
                ImageElement newImageElement = new ImageElement(imageElement.PosX, imageElement.PosY + 50, imageElement.Width, imageElement.Height, imageElement.ImageScale, imageElement.ImageSource, imageElement.BackgroundColor);
                AddImageElement(newImageElement);
            };

            imageElement.BringToFront += () =>
            {
                BackgroundBox.Children.Remove(canvas);
                BackgroundBox.Children.Add(canvas);
            };
        }

        private void EditImageElementUiElement(Canvas canvas, ImageElement imageElement)
        {
            Image image = (Image)canvas.Children[0];
            image.Source = imageElement.ImageSource;
            image.Height = imageElement.ImageSource.Height * imageElement.ImageScale;
            image.Width = imageElement.ImageSource.Width * imageElement.ImageScale;
           

            canvas.Width = imageElement.Width;
            canvas.Height = imageElement.Height;
            canvas.Background = imageElement.BackgroundColor;

            Canvas.SetLeft(canvas, imageElement.PosX);
            Canvas.SetTop(canvas, imageElement.PosY);
        }

        private Canvas CreateAndAddImageElementToPlate(ImageElement imageElement)
        {
            Canvas canvas = CreateImageElementUiElement(imageElement);

            BackgroundBox.Children.Add(canvas);
            return canvas;
        }

        private Canvas CreateImageElementUiElement(ImageElement imageElement)
        {
            Image image = new Image
            {
                Source = imageElement.ImageSource,
                Height = imageElement.ImageSource.Height * imageElement.ImageScale,
                Width = imageElement.ImageSource.Width * imageElement.ImageScale
        };


            Canvas canvas = new Canvas
            {
                Width = imageElement.Width,
                Height = imageElement.Height,
                Background = imageElement.BackgroundColor
            };

            Canvas.SetLeft(canvas, imageElement.PosX);
            Canvas.SetTop(canvas, imageElement.PosY);

            canvas.Children.Add(image);

            return canvas;
        }

        #endregion


        #region Print

        private void PrintButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.IsEnabled = false;
                PrintDialog printDialog = new PrintDialog();
                if (printDialog.ShowDialog() == true)
                {

                    printDialog.PrintVisual(BackgroundBox, "Plakietka");
                }
            }
            finally
            {
                this.IsEnabled = true;
            }
        }

        #endregion


        #region Save

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            var settings = new JsonSerializerSettings();
            settings.TypeNameHandling = TypeNameHandling.Objects;
            
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Json Files (*.json)|*.json|All Files (*.*)|*.*";
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.FileName = "Plakietka";
            if (saveFileDialog.ShowDialog() == true)
            {
                string json = JsonConvert.SerializeObject(printableElements, Formatting.Indented, settings);

                System.IO.File.WriteAllText(saveFileDialog.FileName, json);
            }


        }

        #endregion

        #region Load
        
        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            BackgroundBox.Children.Clear();
            EditableObjectsPanel.Children.Clear();
            printableElements.Clear();

            

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Json Files (*.json)|*.json|All Files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                string json = System.IO.File.ReadAllText(openFileDialog.FileName);
                List<PrintableElement> loadedPrintableElements = JsonConvert.DeserializeObject<List<PrintableElement>>(json);
                
                foreach (PrintableElement printableElement in loadedPrintableElements)
                {
                    if (printableElement.Type == "TextField")
                    {
                        TextField textField = (TextField)printableElement;
                        AddTextField(textField);
                    }
                    else if (printableElement.Type == "ImageElement")
                    {
                        ImageElement imageElement = (ImageElement)printableElement;
                        AddImageElement(imageElement);
                    }
                    else if (printableElement.Type == "BackgroundElement")
                    {
                        BackgroundElement backgroundElement = (BackgroundElement)printableElement;
                        AddBackgroundElement(backgroundElement);
                    }
                   
                }

                printableElements = loadedPrintableElements;
            }
        }





        #endregion



    }
}

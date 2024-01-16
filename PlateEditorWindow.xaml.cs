using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using PlakietkUJ.PrintableElements;
using Microsoft.Win32;
using Newtonsoft.Json;
using PlakietkUJ.HelperClass;


namespace PlakietkUJ
{
    public partial class PlateEditorWindow : Window
    {
        List<PrintableElement> printableElements = new List<PrintableElement>();

        #region Localization
        public string SaveButtonText { get; set; } = LocalizationHelper.GetLocalizedString("SaveButton");
        public string LoadButtonText { get; set; } = LocalizationHelper.GetLocalizedString("LoadButton");
        public string PrintButtonText { get; set; } = LocalizationHelper.GetLocalizedString("PrintButton");
        public string AddTextFieldButtonText { get; set; } = LocalizationHelper.GetLocalizedString("AddTextFieldButton");
        public string AddImageElementButtonText { get; set; } = LocalizationHelper.GetLocalizedString("AddImageButton");
        public string ChangeLanguageComboBoxText { get; set; } = LocalizationHelper.GetLocalizedString("Language");
        public string EnglishLanguageText { get; set; } = LocalizationHelper.GetLocalizedString("English");
        public string PolishLanguageText { get; set; } = LocalizationHelper.GetLocalizedString("Polish");

        public void Language_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LanguageComboBox.SelectedIndex == 0)
            {
                LocalizationHelper.ChangeCulture("pl-PL");
            }
            else if (LanguageComboBox.SelectedIndex == 1)
            {
                LocalizationHelper.ChangeCulture("en-US");
            }   
            
        }

        public void SetComboBoxCurrentLanguage()
        {
            var currLang = Properties.Settings.Default.LanguageCode.ToString();
            if (currLang == "pl-PL")
            {

                ChangeLanguageComboBoxText = PolishLanguageText;
            }
            else
            {
                ChangeLanguageComboBoxText = EnglishLanguageText;
            }
            
        }

        #endregion


        public PlateEditorWindow()
        {
            

            InitializeComponent();
            AddBackgroundElement(CreateBasicBackgroundPrintableElement(100));
            SetComboBoxCurrentLanguage();
            DataContext = this;
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
            TextField textField = new TextField(0, 0, 200, 10, LocalizationHelper.GetLocalizedString("DefaultSetText"), Brushes.Black, 14, new FontFamily("Arial"), Brushes.Transparent);
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
                controls.Header = LocalizationHelper.GetLocalizedString("EditText") + textField.Text;
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
            textBlock.HorizontalAlignment = HorizontalAlignment.Center;
            textBlock.VerticalAlignment = VerticalAlignment.Center;
            textBlock.TextAlignment = TextAlignment.Center;
            textBlock.Background = textField.BackgroundColor;

           

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
                FontWeight = textField.FontWeight,
                TextAlignment = TextAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Background = textField.BackgroundColor
            };

            Canvas canvas = new Canvas();
           

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
            

            

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Json Files (*.json)|*.json|All Files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                BackgroundBox.Children.Clear();
                EditableObjectsPanel.Children.Clear();
                printableElements.Clear();

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

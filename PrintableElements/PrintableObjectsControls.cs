using ColorPicker;
using System.Collections;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;
using System.Windows.Media.Imaging;
using PlakietkUJ.HelperClass;
using Microsoft.Win32;
using Xceed.Wpf.Toolkit; 
namespace PlakietkUJ.PrintableElements
{
    public class PrintableObjectsControls
    {
        #region TextField

        
        private static readonly FontStyle[] FontStyle = new FontStyle[]
        {
            FontStyles.Normal,
            FontStyles.Italic,
            FontStyles.Oblique,
        };

        private static readonly FontWeight[] FontWeightValues = new FontWeight[]
        {
            FontWeights.Normal,
            FontWeights.Bold,
        };



        public static Expander AddTextFieldControlsToEditablePanel(Canvas canvas, TextField textField)
        {
            Expander mainExpander = new Expander();
            mainExpander.Margin = new Thickness(0, 10, 0, 0);
            mainExpander.Header = LocalizationHelper.GetLocalizedString("EditText") + textField.Text;

            StackPanel mainStackPanel = new StackPanel();
            mainStackPanel.Orientation = Orientation.Vertical;

            mainExpander.Content = mainStackPanel;
             
            //Font controls
            AddLabelAndTextBox(mainStackPanel, LocalizationHelper.GetLocalizedString("TextLabel"), textField.Text, out TextBox textTextBox);
            AddLabelAndComboBox(mainStackPanel, LocalizationHelper.GetLocalizedString("FontLabel"), Fonts.SystemFontFamilies, textField.Font, out ComboBox fontComboBox);
            AddLabelAndComboBox(mainStackPanel, LocalizationHelper.GetLocalizedString("FontStyleLabel"), FontStyle, textField.FontStyle, out ComboBox fontStyleComboBox);
            AddLabelAndComboBox(mainStackPanel, LocalizationHelper.GetLocalizedString("FontWeightLabel"), FontWeightValues, textField.FontWeight, out ComboBox fontWeightComboBox);
            AddLabelAndIntegerUpDown(mainStackPanel, LocalizationHelper.GetLocalizedString("FontSizeLabel"), (int)textField.FontSize, out IntegerUpDown fontSizeUpDown);

            //Position and size controls
            AddExpanderWithStackPanel(mainStackPanel, LocalizationHelper.GetLocalizedString("TextPosAndColorLabel"), out Expander positionAndSizeExpander, out StackPanel positionAndSizeStackPanel);

            AddLabelAndIntegerUpDown(positionAndSizeStackPanel, LocalizationHelper.GetLocalizedString("HorizontalPosLabel"), (int)textField.PosX, out IntegerUpDown posXUpDown);
            AddLabelAndIntegerUpDown(positionAndSizeStackPanel, LocalizationHelper.GetLocalizedString("VerticalPosLabel"), (int)textField.PosY, out IntegerUpDown posYUpDown);
            //AddLabelAndIntegerUpDown(positionAndSizeStackPanel, "Szerokość tła: ", (int)textField.Width, out IntegerUpDown widthUpDown);
           // AddLabelAndIntegerUpDown(positionAndSizeStackPanel, "Wysokość tła: ", (int)textField.Height, out IntegerUpDown heightUpDown);
            AddLabelAndColorPicker(positionAndSizeStackPanel, LocalizationHelper.GetLocalizedString("TextColorLabel"), textField.FontColor.Color, out ColorPicker.StandardColorPicker fontColorPicker);
            AddLabelAndColorPicker(positionAndSizeStackPanel, LocalizationHelper.GetLocalizedString("BackgroundColorLabel"), textField.BackgroundColor.Color, out ColorPicker.StandardColorPicker backgroundColorPicker);
            AddBringToFrontButton(positionAndSizeStackPanel, textField);

            AddConfirmTextButton(mainStackPanel, canvas, textField, posXUpDown, posYUpDown,/* widthUpDown, heightUpDown,*/ textTextBox, fontComboBox, fontSizeUpDown, fontColorPicker, backgroundColorPicker, fontStyleComboBox, fontWeightComboBox);
            AddCopyButton(mainStackPanel, canvas, textField);

            AddDeleteButton(mainStackPanel, canvas, textField);


            //return stackPanel;
            return mainExpander;
        }

       

        

        private static void AddConfirmTextButton(StackPanel stackPanel, Canvas canvas, TextField textField, IntegerUpDown posXUpDown,
            IntegerUpDown posYUpDown, /*IntegerUpDown widthUpDown, IntegerUpDown heightUpDown,*/ TextBox textTextBox, ComboBox fontComboBox,
            IntegerUpDown fontSizeUpDown, StandardColorPicker fontColorPicker, StandardColorPicker backgroundColorPicker, ComboBox fontStyleComboBox, ComboBox fontWeightComboBox)
        {
            Button confirmButton = new Button();
            confirmButton.Content = LocalizationHelper.GetLocalizedString("ConfirmButton");
            confirmButton.Click += (sender, e) =>
            {
                textField.PosX = posXUpDown.Value.Value;
                textField.PosY = posYUpDown.Value.Value;
                //textField.Width = widthUpDown.Value.Value;
                //textField.Height = heightUpDown.Value.Value;
                textField.Text = textTextBox.Text;
                textField.Font = (FontFamily)fontComboBox.SelectedItem;
                textField.FontWeight = (FontWeight)fontWeightComboBox.SelectedItem;
                textField.FontStyle = (FontStyle)fontStyleComboBox.SelectedItem;
                textField.FontSize = fontSizeUpDown.Value.Value;
                textField.FontColor = new SolidColorBrush(fontColorPicker.SelectedColor);
                textField.BackgroundColor = new SolidColorBrush(backgroundColorPicker.SelectedColor);

                //run property changed event
                textField.OnPropertyChanged();

            };

            stackPanel.Children.Add(confirmButton);
        }



        #endregion

        #region ImageElement

        internal static UIElement AddImageElementControlsToEditablePanel(Canvas canvas, ImageElement imageElement)
        {
            Expander expander = new Expander();
            expander.Margin = new Thickness(0, 10, 0, 0);
            expander.Header = LocalizationHelper.GetLocalizedString("EditImage");

            StackPanel stackPanel = new StackPanel();
            stackPanel.Orientation = Orientation.Vertical;
            expander.Content = stackPanel;

            
            AddLabelAndSlider(stackPanel, LocalizationHelper.GetLocalizedString("ImageScaleLabel"), 0.1, 2, imageElement.ImageScale, out Slider imageScaleSlider);
           
            AddLabelAndIntegerUpDown(stackPanel, LocalizationHelper.GetLocalizedString("HorizontalPosLabel"), (int)imageElement.PosX, out IntegerUpDown posXUpDown);
            AddLabelAndIntegerUpDown(stackPanel, LocalizationHelper.GetLocalizedString("VerticalPosLabel"), (int)imageElement.PosY, out IntegerUpDown posYUpDown);
            //AddLabelAndIntegerUpDown(stackPanel, "Szerokość tła: ", (int)imageElement.Width, out IntegerUpDown widthUpDown);
            //AddLabelAndIntegerUpDown(stackPanel, "Wysokość tła: ", (int)imageElement.Height, out IntegerUpDown heightUpDown);
            //AddLabelAndColorPicker(stackPanel, "Kolor tła: ", imageElement.BackgroundColor.Color, out ColorPicker.StandardColorPicker backgroundColorPicker);
            AddBringToFrontButton(stackPanel, imageElement);
           
            
            AddChangeImageButton(stackPanel, imageElement);
            
            AddConfirmImageButton(stackPanel, canvas, imageElement, posXUpDown, posYUpDown,/* widthUpDown, heightUpDown, backgroundColorPicker,*/ imageScaleSlider);
            AddCopyButton(stackPanel, canvas, imageElement);
            AddDeleteButton(stackPanel, canvas, imageElement);

            //return stackPanel;
            return expander;
        }

        private static void AddLabelAndSlider(StackPanel stackPanel, string labelText, double min, int max, double imageScale, out Slider scaleSlider)
        {
            Label label = new Label();
            label.Content = labelText;

            scaleSlider = new Slider();
            scaleSlider.Minimum = min;
            scaleSlider.Maximum = max;
            scaleSlider.Value = imageScale;

            stackPanel.Children.Add(label);
            stackPanel.Children.Add(scaleSlider);
        }

        private static void AddChangeImageButton(StackPanel stackPanel, ImageElement imageElement)
        {
            //ChangeImageSource
            Button changeImageButton = new Button();
            changeImageButton.Content = LocalizationHelper.GetLocalizedString("ChangeImageButton");
            changeImageButton.Margin = new Thickness(0, 10, 0, 10);

            changeImageButton.Click += (sender, e) =>
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Image Files (*.png;*.jpeg;*.jpg;*.gif;*.bmp)|*.png;*.jpeg;*.jpg;*.gif;*.bmp|All Files (*.*)|*.*";

                if (openFileDialog.ShowDialog() == true)
                {
                    imageElement.ImageSource = new BitmapImage(new System.Uri(openFileDialog.FileName));
                }
            };

            stackPanel.Children.Add(changeImageButton);
        }

        private static void AddConfirmImageButton(StackPanel stackPanel, Canvas canvas, ImageElement imageElement, IntegerUpDown posXUpDown, IntegerUpDown posYUpDown, /*IntegerUpDown widthUpDown, IntegerUpDown heightUpDown, StandardColorPicker backgroundColorPicker,*/ Slider imageScaleSlider)
        {
            Button confirmButton = new Button();
            confirmButton.Content = LocalizationHelper.GetLocalizedString("ConfirmButton");
            confirmButton.Click += (sender, e) =>
            {
                imageElement.PosX = double.Parse(posXUpDown.Text);
                imageElement.PosY = double.Parse(posYUpDown.Text);
                //imageElement.Width = double.Parse(widthUpDown.Text);
                //imageElement.Height = double.Parse(heightUpDown.Text);
                //imageElement.BackgroundColor = new SolidColorBrush(backgroundColorPicker.SelectedColor);
                imageElement.ImageScale = imageScaleSlider.Value;

                //run property changed event
                imageElement.OnPropertyChanged();
            };

            stackPanel.Children.Add(confirmButton);
        }

        #endregion


        #region BackgroundElement

        internal static UIElement AddBackgroundElementControlsToEditablePanel(BackgroundElement printableElement)
        {
            Expander expander = new Expander();
            expander.Header = LocalizationHelper.GetLocalizedString("EditBackgroundLabel");

            StackPanel stackPanel = new StackPanel();
            stackPanel.Orientation = Orientation.Vertical;
            expander.Content = stackPanel;

            int dpi = PageDpiHelper.GetDpiForA4((int)Math.Min(printableElement.Width, printableElement.Height));

            AddLabelAndIntegerUpDown(stackPanel, LocalizationHelper.GetLocalizedString("DPILabel"), dpi, out IntegerUpDown dpiUpDown);
            AddLabelAndColorPicker(stackPanel, LocalizationHelper.GetLocalizedString("BackgroundColorLabel"), printableElement.BackgroundColor.Color, out ColorPicker.StandardColorPicker backgroundColorPicker);
            AddLabelAndCheckBox(stackPanel, LocalizationHelper.GetLocalizedString("VerticalCheckBox"), printableElement.IsVertical, out CheckBox isVerticalCheckBox);

            AddConfirmBackGroundButton(stackPanel, printableElement, dpiUpDown, isVerticalCheckBox, backgroundColorPicker);


            return expander;
        }

        private static void AddConfirmBackGroundButton(StackPanel stackPanel, BackgroundElement printableElement, IntegerUpDown dpiUpDown, CheckBox isVerticalCheckBox, StandardColorPicker backgroundColorPicker)
        {
            Button confirmButton = new Button();
            confirmButton.Content = LocalizationHelper.GetLocalizedString("ConfirmButton");
            confirmButton.Click += (sender, e) =>
            {
                printableElement.BackgroundColor = new SolidColorBrush(backgroundColorPicker.SelectedColor);
                printableElement.IsVertical = (bool)isVerticalCheckBox.IsChecked;

                int dpi = dpiUpDown.Value.Value;
                int shorterSidePixels = PageDpiHelper.GetShorterSideForA4(dpi);
                int longerSidePixels = PageDpiHelper.GetLongerSide(shorterSidePixels);


                if (printableElement.IsVertical)
                {
                    printableElement.Width = shorterSidePixels;
                    printableElement.Height = longerSidePixels;
                }
                else
                {
                    printableElement.Width = longerSidePixels;
                    printableElement.Height = shorterSidePixels;
                }


                printableElement.OnPropertyChanged();
            };

            stackPanel.Children.Add(confirmButton);
        }

        #endregion


        #region Controls for all elements
        

        private static void AddDeleteButton(StackPanel stackPanel, Canvas canvas, PrintableElement element)
        {
            Button deleteButton = new Button();
            deleteButton.Content = LocalizationHelper.GetLocalizedString("DeleteButton");
            deleteButton.Click += (sender, e) =>
            {
                canvas.Children.Remove(canvas.Children[0]);
                element.OnDeleted();
            };

            deleteButton.Margin = new Thickness(0, 10, 0, 0);   

            stackPanel.Children.Add(deleteButton);
        }
        

        private static void AddLabelAndTextBox(StackPanel parentPanel, string labelText, string textBoxText, out TextBox textBox)
        {
            Label label = new Label();
            label.Content = labelText;

            textBox = new TextBox();
            textBox.Text = textBoxText;

            parentPanel.Children.Add(label);
            parentPanel.Children.Add(textBox);
        }

        private static void AddLabelAndComboBox(StackPanel parentPanel, string labelText, IEnumerable itemsSource, object selectedItem, out ComboBox comboBox)
        {
            Label label = new Label();
            label.Content = labelText;

            comboBox = new ComboBox();
            comboBox.ItemsSource = itemsSource;
            comboBox.SelectedItem = selectedItem;

            parentPanel.Children.Add(label);
            parentPanel.Children.Add(comboBox);
        }

        private static void AddLabelAndColorPicker(StackPanel parentPanel, string labelText, Color selectedColor, out ColorPicker.StandardColorPicker colorPicker)
        {
            Label label = new Label
            {
                Content = labelText
            };


            colorPicker = new ColorPicker.StandardColorPicker
            {
                SelectedColor = selectedColor
            };

            ResourceDictionary colorPickerStyle = new ResourceDictionary();
            colorPickerStyle.Source = new Uri("pack://application:,,,/ColorPicker;component/Styles/DefaultColorPickerStyle.xaml");
            colorPicker.Style = colorPickerStyle["DefaultColorPickerStyle"] as Style;

            colorPickerStyle = new ResourceDictionary();
            colorPickerStyle.Source = new Uri("pack://application:,,,/PlakietkUJ;component/Theme/ColorPickerStyle.xaml");
            colorPicker.Style = colorPickerStyle["MyLightColorPickerStyle"] as Style;


            parentPanel.Children.Add(label);
            parentPanel.Children.Add(colorPicker);
                
            

        }


        private static void AddLabelAndCheckBox(StackPanel stackPanel, string labelText, bool isVertical, out CheckBox isVerticalCheckBox)
        {
            Label label = new Label();
            label.Content = labelText;

            isVerticalCheckBox = new CheckBox();
            isVerticalCheckBox.IsChecked = isVertical;

            stackPanel.Children.Add(label);
            stackPanel.Children.Add(isVerticalCheckBox);
        }

        private static void AddLabelAndIntegerUpDown(StackPanel parentPanel, string labelText, int initialValue, out IntegerUpDown integerUpDown)
        {
            Label label = new Label();
            label.Content = labelText;

            integerUpDown = new IntegerUpDown();
            integerUpDown.Value = initialValue;

            parentPanel.Children.Add(label);
            parentPanel.Children.Add(integerUpDown);
        }

        private static void AddCopyButton(StackPanel stackPanel, Canvas canvas, PrintableElement element)
        {
            Button copyButton = new Button();
            copyButton.Margin = new Thickness(0, 10, 0, 0);
            copyButton.Content = LocalizationHelper.GetLocalizedString("CopyButton");
            copyButton.Click += (sender, e) =>
            {
                element.OnCopy();
            };

            stackPanel.Children.Add(copyButton);
        }

        private static void AddExpanderWithStackPanel(StackPanel mainStackPanel, string headerText, out Expander positionAndSizeExpander, out StackPanel positionAndSizeStackPanel)
        {
            positionAndSizeExpander = new Expander();
            positionAndSizeExpander.Margin = new Thickness(0, 10, 0, 20);
            positionAndSizeExpander.Header = new TextBlock() { Text = headerText, FontSize = 15, FontWeight = FontWeights.Bold, TextAlignment = TextAlignment.Center };
            positionAndSizeStackPanel = new StackPanel();
            positionAndSizeStackPanel.Orientation = Orientation.Vertical;
            positionAndSizeExpander.Content = positionAndSizeStackPanel;
            mainStackPanel.Children.Add(positionAndSizeExpander);
        }

        private static void AddBringToFrontButton(StackPanel positionAndSizeStackPanel, PrintableElement printableElement)
        {
            Button bringToFrontButton = new Button();
            bringToFrontButton.Margin = new Thickness(0, 10, 0, 0);
            bringToFrontButton.Content = LocalizationHelper.GetLocalizedString("BringToFrontButton");
            bringToFrontButton.Click += (sender, e) =>
            {
                printableElement.OnBringToFront();
            };

            positionAndSizeStackPanel.Children.Add(bringToFrontButton);
        }

        #endregion
    }
}

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
        
        public static UIElement AddTextFieldControlsToEditablePanel(Canvas canvas, TextField textField)
        {
            Expander expander = new Expander();
            expander.Header = "Edytuj tekst";

            StackPanel stackPanel = new StackPanel();
            stackPanel.Orientation = Orientation.Vertical;

            expander.Content = stackPanel;
             

            AddLabelAndTextBox(stackPanel, "Tekst: ", textField.Text, out TextBox textTextBox);
            AddLabelAndIntegerUpDown(stackPanel, "Pozycja Pozioma: ", (int)textField.PosX, out IntegerUpDown posXUpDown);
            AddLabelAndIntegerUpDown(stackPanel, "Pozycja Pionowa: ", (int)textField.PosY, out IntegerUpDown posYUpDown);
            AddLabelAndIntegerUpDown(stackPanel, "Szerokość tła: ", (int)textField.Width, out IntegerUpDown widthUpDown);
            AddLabelAndIntegerUpDown(stackPanel, "Wysokość tła: ", (int)textField.Height, out IntegerUpDown heightUpDown);
            AddLabelAndComboBox(stackPanel, "Czcionka: ", Fonts.SystemFontFamilies, textField.Font, out ComboBox fontComboBox);
            AddLabelAndIntegerUpDown(stackPanel, "Rozmiar: ", (int)textField.FontSize, out IntegerUpDown fontSizeUpDown);
            AddLabelAndColorPicker(stackPanel, "Kolor Napisu: ", textField.FontColor.Color, out ColorPicker.StandardColorPicker fontColorPicker);
            AddLabelAndColorPicker(stackPanel, "Kolor tła: ", textField.BackgroundColor.Color, out ColorPicker.StandardColorPicker backgroundColorPicker);

            AddConfirmTextButton(stackPanel, canvas, textField, posXUpDown, posYUpDown, widthUpDown, heightUpDown, textTextBox, fontComboBox, fontSizeUpDown, fontColorPicker, backgroundColorPicker);
            AddDeleteButton(stackPanel, canvas, textField);


            //return stackPanel;
            return expander;
        }

        private static void AddConfirmTextButton(StackPanel stackPanel, Canvas canvas, TextField textField, IntegerUpDown posXUpDown, IntegerUpDown posYUpDown, IntegerUpDown widthUpDown, IntegerUpDown heightUpDown, TextBox textTextBox, ComboBox fontComboBox, IntegerUpDown fontSizeUpDown, StandardColorPicker fontColorPicker, StandardColorPicker backgroundColorPicker)
        {
            Button confirmButton = new Button();
            confirmButton.Content = "Zatwierdź";
            confirmButton.Click += (sender, e) =>
            {
                textField.PosX = posXUpDown.Value.Value;
                textField.PosY = posYUpDown.Value.Value;
                textField.Width = widthUpDown.Value.Value;
                textField.Height = heightUpDown.Value.Value;
                textField.Text = textTextBox.Text;
                textField.Font = (FontFamily)fontComboBox.SelectedItem;
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
            expander.Header = "Edytuj obraz";

            StackPanel stackPanel = new StackPanel();
            stackPanel.Orientation = Orientation.Vertical;
            expander.Content = stackPanel;

            
            AddLabelAndSlider(stackPanel, "Skala obrazu: ",0.1, 2, imageElement.ImageScale, out Slider imageScaleSlider);
           
            AddLabelAndIntegerUpDown(stackPanel, "Pozycja Pozioma: ", (int)imageElement.PosX, out IntegerUpDown posXUpDown);
            AddLabelAndIntegerUpDown(stackPanel, "Pozycja Pionowa: ", (int)imageElement.PosY, out IntegerUpDown posYUpDown);
            AddLabelAndIntegerUpDown(stackPanel, "Szerokość tła: ", (int)imageElement.Width, out IntegerUpDown widthUpDown);
            AddLabelAndIntegerUpDown(stackPanel, "Wysokość tła: ", (int)imageElement.Height, out IntegerUpDown heightUpDown);
            AddLabelAndColorPicker(stackPanel, "Kolor tła: ", imageElement.BackgroundColor.Color, out ColorPicker.StandardColorPicker backgroundColorPicker);
            AddChangeImageButton(stackPanel, imageElement);
            
            AddConfirmImageButton(stackPanel, canvas, imageElement, posXUpDown, posYUpDown, widthUpDown, heightUpDown, backgroundColorPicker, imageScaleSlider);
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
            changeImageButton.Content = "Zmień obraz";
            changeImageButton.Margin = new Thickness(0, 0, 0, 10);

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

        private static void AddConfirmImageButton(StackPanel stackPanel, Canvas canvas, ImageElement imageElement, IntegerUpDown posXUpDown, IntegerUpDown posYUpDown, IntegerUpDown widthUpDown, IntegerUpDown heightUpDown, StandardColorPicker backgroundColorPicker, Slider imageScaleSlider)
        {
            Button confirmButton = new Button();
            confirmButton.Content = "Zatwierdź";
            confirmButton.Click += (sender, e) =>
            {
                imageElement.PosX = double.Parse(posXUpDown.Text);
                imageElement.PosY = double.Parse(posYUpDown.Text);
                imageElement.Width = double.Parse(widthUpDown.Text);
                imageElement.Height = double.Parse(heightUpDown.Text);
                imageElement.BackgroundColor = new SolidColorBrush(backgroundColorPicker.SelectedColor);
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
            expander.Header = "Edytuj tło";

            StackPanel stackPanel = new StackPanel();
            stackPanel.Orientation = Orientation.Vertical;
            expander.Content = stackPanel;

            int dpi = PageDpiHelper.GetDpiForA4((int)Math.Min(printableElement.Width, printableElement.Height));

            AddLabelAndIntegerUpDown(stackPanel, "DPI: ", dpi, out IntegerUpDown dpiUpDown);
            AddLabelAndColorPicker(stackPanel, "Kolor tła: ", printableElement.BackgroundColor.Color, out ColorPicker.StandardColorPicker backgroundColorPicker);
            AddLabelAndCheckBox(stackPanel, "Pionowo: ", printableElement.IsVertical, out CheckBox isVerticalCheckBox);

            AddConfirmBackGroundButton(stackPanel, printableElement, dpiUpDown, isVerticalCheckBox, backgroundColorPicker);


            return expander;
        }

        private static void AddConfirmBackGroundButton(StackPanel stackPanel, BackgroundElement printableElement, IntegerUpDown dpiUpDown, CheckBox isVerticalCheckBox, StandardColorPicker backgroundColorPicker)
        {
            Button confirmButton = new Button();
            confirmButton.Content = "Zatwierdź";
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
            deleteButton.Content = "Usuń";
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
            Label label = new Label();
            label.Content = labelText;

            colorPicker = new ColorPicker.StandardColorPicker();
            colorPicker.SelectedColor = selectedColor;

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

        #endregion
    }
}

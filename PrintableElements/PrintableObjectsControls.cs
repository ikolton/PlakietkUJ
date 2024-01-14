using ColorPicker;
using System.Collections;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;
using PlakietkUJ.HelperClass;

namespace PlakietkUJ.PrintableElements
{
    public class PrintableObjectsControls
    {
        public static UIElement AddTextFieldControlsToEditablePanel(Canvas canvas, TextField textField)
        {
            Expander expander = new Expander();
            expander.Header = "Edytuj tekst";

            StackPanel stackPanel = new StackPanel();
            stackPanel.Orientation = Orientation.Vertical;

            expander.Content = stackPanel;
             

            AddLabelAndTextBox(stackPanel, "Tekst: ", textField.Text, out TextBox textTextBox);
            AddLabelAndTextBox(stackPanel, "Pozycja Pozioma: ", textField.PosX.ToString(), out TextBox posXTextBox);
            AddLabelAndTextBox(stackPanel, "Pozycja Pionowa: ", textField.PosY.ToString(), out TextBox posYTextBox);
            AddLabelAndTextBox(stackPanel, "Szerokość tła: ", textField.Width.ToString(), out TextBox widthTextBox);
            AddLabelAndTextBox(stackPanel, "Wysokość tła: ", textField.Height.ToString(), out TextBox heightTextBox);
            AddLabelAndComboBox(stackPanel, "Czcionka: ", Fonts.SystemFontFamilies, textField.Font, out ComboBox fontComboBox);
            AddLabelAndTextBox(stackPanel, "Rozmiar: ", textField.FontSize.ToString(), out TextBox fontSizeTextBox);
            AddLabelAndColorPicker(stackPanel, "Kolor Napisu: ", textField.FontColor.Color, out ColorPicker.StandardColorPicker fontColorPicker);
            AddLabelAndColorPicker(stackPanel, "Kolor tła: ", textField.BackgroundColor.Color, out ColorPicker.StandardColorPicker backgroundColorPicker);

            AddConfirmTextButton(stackPanel, canvas, textField, posXTextBox, posYTextBox, widthTextBox, heightTextBox, textTextBox, fontComboBox, fontSizeTextBox, fontColorPicker, backgroundColorPicker);
            AddDeleteButton(stackPanel, canvas, textField);


            //return stackPanel;
            return expander;
        }

        internal static UIElement AddImageElementControlsToEditablePanel(Canvas canvas, ImageElement imageElement)
        {
            Expander expander = new Expander();
            expander.Header = "Edytuj obraz";

            StackPanel stackPanel = new StackPanel();
            stackPanel.Orientation = Orientation.Vertical;
            expander.Content = stackPanel;

            AddLabelAndTextBox(stackPanel, "Skala obrazu: ", imageElement.ImageScale.ToString(), out TextBox imageScaleTextBox);
            AddLabelAndTextBox(stackPanel, "Pozycja Pozioma: ", imageElement.PosX.ToString(), out TextBox posXTextBox);
            AddLabelAndTextBox(stackPanel, "Pozycja Pionowa: ", imageElement.PosY.ToString(), out TextBox posYTextBox);
            AddLabelAndTextBox(stackPanel, "Szerokość tła: ", imageElement.Width.ToString(), out TextBox widthTextBox);
            AddLabelAndTextBox(stackPanel, "Wysokość tła: ", imageElement.Height.ToString(), out TextBox heightTextBox);
            AddLabelAndColorPicker(stackPanel, "Kolor tła: ", imageElement.BackgroundColor.Color, out ColorPicker.StandardColorPicker backgroundColorPicker);
           
            AddConfirmTextImageButton(stackPanel, canvas, imageElement, posXTextBox, posYTextBox, widthTextBox, heightTextBox, backgroundColorPicker, imageScaleTextBox);
            AddDeleteButton(stackPanel, canvas, imageElement);

            //return stackPanel;
            return expander;
        }

        private static void AddConfirmTextImageButton(StackPanel stackPanel, Canvas canvas, ImageElement imageElement, TextBox posXTextBox, TextBox posYTextBox, TextBox widthTextBox, TextBox heightTextBox, StandardColorPicker backgroundColorPicker, TextBox imageScaleTextBox)
        {
            Button confirmButton = new Button();
            confirmButton.Content = "Zatwierdź";
            confirmButton.Click += (sender, e) =>
            {
                imageElement.PosX = double.Parse(posXTextBox.Text);
                imageElement.PosY = double.Parse(posYTextBox.Text);
                imageElement.Width = double.Parse(widthTextBox.Text);
                imageElement.Height = double.Parse(heightTextBox.Text);
                imageElement.BackgroundColor = new SolidColorBrush(backgroundColorPicker.SelectedColor);
                imageElement.ImageScale = double.Parse(imageScaleTextBox.Text);

                //run property changed event
                imageElement.OnPropertyChanged();

            };

            stackPanel.Children.Add(confirmButton);
        }

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

        private static void AddConfirmTextButton(StackPanel stackPanel, Canvas canvas, TextField textField, TextBox posXTextBox, TextBox posYTextBox, TextBox widthTextBox, TextBox heightTextBox, TextBox textTextBox, ComboBox fontComboBox, TextBox fontSizeTextBox, StandardColorPicker fontColorPicker, StandardColorPicker backgroundColorPicker)
        {
            Button confirmButton = new Button();
            confirmButton.Content = "Zatwierdź";
            confirmButton.Click += (sender, e) =>
            {
                textField.PosX = double.Parse(posXTextBox.Text);
                textField.PosY = double.Parse(posYTextBox.Text);
                textField.Width = double.Parse(widthTextBox.Text);
                textField.Height = double.Parse(heightTextBox.Text);
                textField.Text = textTextBox.Text;
                textField.Font = (FontFamily)fontComboBox.SelectedItem;
                textField.FontSize = double.Parse(fontSizeTextBox.Text);
                textField.FontColor = new SolidColorBrush(fontColorPicker.SelectedColor);
                textField.BackgroundColor = new SolidColorBrush(backgroundColorPicker.SelectedColor);

                //run property changed event
                textField.OnPropertyChanged();

            };

            stackPanel.Children.Add(confirmButton);
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

        internal static UIElement AddBackgroundElementControlsToEditablePanel(BackgroundElement printableElement)
        {
            Expander expander = new Expander();
            expander.Header = "Edytuj tło";

            StackPanel stackPanel = new StackPanel();
            stackPanel.Orientation = Orientation.Vertical;
            expander.Content = stackPanel;

            int dpi = PageDpiHelper.GetDpiForA4((int)Math.Min(printableElement.Width,printableElement.Height));

            AddLabelAndTextBox(stackPanel, "DPI: ", dpi.ToString(), out TextBox dpiTextBox);
            AddLabelAndColorPicker(stackPanel, "Kolor tła: ", printableElement.BackgroundColor.Color, out ColorPicker.StandardColorPicker backgroundColorPicker);
            AddLabelAndCheckBox(stackPanel, "Pionowo: ", printableElement.IsVertical, out CheckBox isVerticalCheckBox);

           AddConfirmBackGroundButton(stackPanel, printableElement,dpiTextBox, isVerticalCheckBox, backgroundColorPicker);

            
            return expander;
        }

        private static void AddConfirmBackGroundButton(StackPanel stackPanel, BackgroundElement printableElement, TextBox dpiTextBox, CheckBox isVerticalCheckBox, StandardColorPicker backgroundColorPicker)
        {
            Button confirmButton = new Button();
            confirmButton.Content = "Zatwierdź";
            confirmButton.Click += (sender, e) =>
            {
                printableElement.BackgroundColor = new SolidColorBrush(backgroundColorPicker.SelectedColor);
                printableElement.IsVertical = (bool)isVerticalCheckBox.IsChecked;

                int dpi = int.Parse(dpiTextBox.Text);
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

        private static void AddLabelAndCheckBox(StackPanel stackPanel, string labelText, bool isVertical, out CheckBox isVerticalCheckBox)
        {
            Label label = new Label();
            label.Content = labelText;

            isVerticalCheckBox = new CheckBox();
            isVerticalCheckBox.IsChecked = isVertical;

            stackPanel.Children.Add(label);
            stackPanel.Children.Add(isVerticalCheckBox);
        }
    }
}

using ColorPicker;
using System.Collections;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;

namespace PlakietkUJ.PrintableElements
{
    public class PrintableObjectsControls
    {
        public static void AddTextFieldControlsToEditablePanel(Canvas canvas, TextField textField, StackPanel editableObjectsPanel)
        {
            StackPanel stackPanel = new StackPanel();
            stackPanel.Orientation = Orientation.Vertical;

            AddLabelAndTextBox(stackPanel, "Tekst: ", textField.Text, out TextBox textTextBox);
            AddLabelAndTextBox(stackPanel, "Pozycja Pozioma: ", textField.PosX.ToString(), out TextBox posXTextBox);
            AddLabelAndTextBox(stackPanel, "Pozycja Pionowa: ", textField.PosY.ToString(), out TextBox posYTextBox);
            AddLabelAndTextBox(stackPanel, "Szerokość tła: ", textField.Width.ToString(), out TextBox widthTextBox);
            AddLabelAndTextBox(stackPanel, "Wysokość tła: ", textField.Height.ToString(), out TextBox heightTextBox);
            AddLabelAndComboBox(stackPanel, "Czcionka: ", Fonts.SystemFontFamilies, textField.Font, out ComboBox fontComboBox);
            AddLabelAndTextBox(stackPanel, "Rozmiar: ", textField.FontSize.ToString(), out TextBox fontSizeTextBox);
            AddLabelAndColorPicker(stackPanel, "Kolor Napisu: ", textField.FontColor.Color, out ColorPicker.SquarePicker fontColorPicker);
            AddLabelAndColorPicker(stackPanel, "Kolor tła: ", textField.BackgroundColor.Color, out ColorPicker.SquarePicker backgroundColorPicker);

            AddConfirmButton(stackPanel, canvas, textField, posXTextBox, posYTextBox, widthTextBox, heightTextBox, textTextBox, fontComboBox, fontSizeTextBox, fontColorPicker, backgroundColorPicker);

            editableObjectsPanel.Children.Add(stackPanel);
        }

        private static void AddConfirmButton(StackPanel stackPanel, Canvas canvas, TextField textField, TextBox posXTextBox, TextBox posYTextBox, TextBox widthTextBox, TextBox heightTextBox, TextBox textTextBox, ComboBox fontComboBox, TextBox fontSizeTextBox, SquarePicker fontColorPicker, SquarePicker backgroundColorPicker)
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

        private static void AddLabelAndColorPicker(StackPanel parentPanel, string labelText, Color selectedColor, out ColorPicker.SquarePicker colorPicker)
        {
            Label label = new Label();
            label.Content = labelText;

            colorPicker = new ColorPicker.SquarePicker();
            colorPicker.SelectedColor = selectedColor;

            parentPanel.Children.Add(label);
            parentPanel.Children.Add(colorPicker);
        }
    }
}

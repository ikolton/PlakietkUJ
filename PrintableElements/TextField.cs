using System.Windows;
using System.Windows.Media;


namespace PlakietkUJ.PrintableElements
{
    public class TextField : PrintableElement
    {
        public string Text { get; set; }
        public SolidColorBrush FontColor { get; set; }
        public double FontSize { get; set; }

        public FontFamily Font { get; set; }

        public FontStyle FontStyle { get; set; }

        public FontWeight FontWeight { get; set; }

        public TextField(double posX, double posY, double width, double height, string text, SolidColorBrush fontColor, double fontSize, FontFamily font, SolidColorBrush backgroundColor, string type = "TextField", FontStyle fontStyle = default, FontWeight fontWeight = default)
            : base(posX, posY, width, height, backgroundColor, type)
        {
            Text = text;
            FontColor = fontColor;
            FontSize = fontSize;
            Font = font;
            FontStyle = fontStyle;
            FontWeight = fontWeight;
        }
    }
}
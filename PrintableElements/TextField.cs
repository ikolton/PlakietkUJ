using System.Windows;
using System.Windows.Media;
using System.ComponentModel;

namespace PlakietkUJ.PrintableElements
{
    public class TextField : PrintableElement
    {
        public string Text { get; set; }
        public SolidColorBrush FontColor { get; set; }
        public double FontSize { get; set; }

        public FontFamily Font { get; set; }

        public TextField(double posX, double posY, double width, double height, string text, SolidColorBrush fontColor, double fontSize, FontFamily font, SolidColorBrush backgroundColor)
            : base(posX, posY, width, height, backgroundColor)
        {
            Text = text;
            FontColor = fontColor;
            FontSize = fontSize;
            Font = font;
        }
    }
}
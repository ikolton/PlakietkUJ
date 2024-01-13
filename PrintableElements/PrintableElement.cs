using System.ComponentModel;
using System.Windows;
using System.Windows.Media;

namespace PlakietkUJ.PrintableElements
{
    public class PrintableElement
    {
        public event Action? PropertyChanged;
        public double PosX { get; set; }
        public double PosY { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public SolidColorBrush BackgroundColor { get; set; }

        public PrintableElement(double posX, double posY, double width, double height, SolidColorBrush backgroundColor)
        {
            PosX = posX;
            PosY = posY;
            Width = width;
            Height = height;
            BackgroundColor = backgroundColor;
        }

        public void OnPropertyChanged()
        {
            PropertyChanged?.Invoke();
        }

        

    }
}
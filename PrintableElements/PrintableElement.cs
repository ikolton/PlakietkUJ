using JsonSubTypes;
using Newtonsoft.Json;
using System.Windows.Media;

namespace PlakietkUJ.PrintableElements
{
    [JsonConverter(typeof(JsonSubtypes), "Type")]
    public class PrintableElement
    {
        public event Action? PropertyChanged;
        public event Action? Deleted;
        public event Action? Copy;
        public event Action? BringToFront;

        public string Type { get;}
        public double PosX { get; set; }
        public double PosY { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public SolidColorBrush BackgroundColor { get; set; }

        public PrintableElement(double posX, double posY, double width, double height, SolidColorBrush backgroundColor, string type = "PrintableElement")
        {
            PosX = posX;
            PosY = posY;
            Width = width;
            Height = height;
            BackgroundColor = backgroundColor;
            Type = type;
        }

        public void OnPropertyChanged()
        {
            PropertyChanged?.Invoke();
        }

        public void OnDeleted()
        {
            Deleted?.Invoke();
        }

        public void OnCopy()
        {
            Copy?.Invoke();
        }

        public void OnBringToFront()
        {
            BringToFront?.Invoke();
        }

    }
}
using System.Windows.Media;

namespace PlakietkUJ.PrintableElements
{
    public class BackgroundElement: PrintableElement
    {
        public bool IsVertical { get; set; }
        public BackgroundElement(double posX, double posY, double width, double height, SolidColorBrush backgroundColor,
            string type = "BackgroundElement", bool isVertical = false) : base(posX, posY, width, height, backgroundColor, type)
        {
            IsVertical = isVertical;
        }
    }
}

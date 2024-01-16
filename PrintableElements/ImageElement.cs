
using System.Windows.Media;

namespace PlakietkUJ.PrintableElements
{
    public class ImageElement : PrintableElement
    {
        public ImageSource ImageSource { get; set; }
        public double ImageScale { get; set; }

        public ImageElement(double posX, double posY, double width, double height,double ImageScale, ImageSource imageSource, SolidColorBrush backgroundColor, string type = "ImageElement")
            : base(posX, posY, width, height, backgroundColor, type)
        {
            ImageSource = imageSource;
            this.ImageScale = ImageScale;
        }
    }
}

namespace PlakietkUJ.HelperClass
{
    public class PageDpiHelper
    {
        public static int GetLongerSide(int shorterSidePixels)
        {
            return (int)(shorterSidePixels * 1.4142);
        }

        public static int GetShorterSide(int longerSidePixels)
        {
            return (int)(longerSidePixels / 1.4142);
        }

        public static int GetShorterSideForA4(int dpi)
        {
            //double a4WidthInches = 8.27;
            double a4HeightInches = 11.69;

            int a4HeightPixels = (int)(a4HeightInches * dpi);

            return a4HeightPixels;
        }

        public static int GetDpiForA4(int shorterSidePixels)
        {
            
           //double a4WidthInches = 8.27;
            double a4HeightInches = 11.69;

            int dpi = (int)(shorterSidePixels / (a4HeightInches));

            return dpi;
        }


    }
}

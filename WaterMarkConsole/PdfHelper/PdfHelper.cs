using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Fonts;
using PdfSharp.Pdf;

internal static class PdfHelper
{
    public static double GetPageWidth(PdfPage page) => page.Rotate == 90 || page.Rotate == 270 ? page.Height.Point : page.Width.Point;

    public static double GetPageHeight(PdfPage page) => page.Rotate == 90 || page.Rotate == 270 ? page.Width.Point : page.Height.Point;

    public static XFont InitFont()
    {
        GlobalFontSettings.FontResolver = new FontResolver();
        var fontOptions = new XPdfFontOptions(PdfFontEncoding.Unicode, PdfFontEmbedding.TryComputeSubset);
        var font = new XFont("Arial-bold", 10, XFontStyleEx.Regular, fontOptions);
        return font;
    }

    public static XBrush InitBrush()
    {
        return new XSolidBrush(XColor.FromArgb(255, 255, 0, 0));
    }

    public static XGraphics ProvidePdfPageGraphics(PdfPage page)
    {
        var gfx = XGraphics.FromPdfPage(page, XGraphicsPdfPageOptions.Append);
        return gfx;
    }

    public static XGraphics ProvidePdfPageGraphicsWithRotationFix(PdfPage page)
    {
        var gfx = XGraphics.FromPdfPage(page, XGraphicsPdfPageOptions.Append);
        if (page.Rotate != 0)
        {
            XPoint pagePoint = page.Orientation == PageOrientation.Portrait ?
                                new XPoint(page.Height.Point / 2, page.Width.Point / 2) :
                                new XPoint(page.Width.Point / 2, page.Height.Point / 2);
            // Adjust the transformation matrix based on page rotation
            gfx.RotateAtTransform(page.Rotate * -1, pagePoint);
            if (page.Orientation == PageOrientation.Landscape && (page.Rotate == 90 || page.Rotate == 270))
            {
                gfx.TranslateTransform(Math.Abs(page.Height.Point - page.Width.Point) / 2, -Math.Abs(page.Height.Point - page.Width.Point) / 2);
            }
            else if (page.Orientation == PageOrientation.Portrait && page.Rotate == 90)
            {
                gfx.TranslateTransform(-Math.Abs(page.Height.Point - page.Width.Point) / 2, -Math.Abs(page.Height.Point - page.Width.Point) / 2);
            }
            else if (page.Orientation == PageOrientation.Portrait && page.Rotate == 270)
            {
                gfx.TranslateTransform(Math.Abs(page.Height.Point - page.Width.Point) / 2, Math.Abs(page.Height.Point - page.Width.Point) / 2);
            }
            else if (page.Orientation == PageOrientation.Portrait && page.Rotate == 180)
            {
                gfx.TranslateTransform(Math.Abs(page.Height.Point - page.Width.Point), -Math.Abs(page.Height.Point - page.Width.Point));
            }
        }
        return gfx;
    }

    public static void DrawWaterMarkInTopLeft(string watermark, XGraphics gfx, XFont font, XBrush brush)
    {
        XSize size = gfx.MeasureString(watermark, font);

        double padding = 10;
        double xPosition = padding;
        double yPosition = padding;

        // Draw the string.
        var rectangle = new XRect(
            new XPoint(xPosition, yPosition),
            new XSize(size.Width, size.Height)
        );
        gfx.DrawString(watermark, font, brush, rectangle, XStringFormats.TopLeft);
    }

    public static void DrawWaterMarkInTopRight(string watermark, PdfPage page, XGraphics gfx, XFont font, XBrush brush)
    {
        XSize size = gfx.MeasureString(watermark, font);

        double pageWidth = GetPageWidth(page);

        double padding = 10;
        double xPosition = pageWidth - size.Width - padding;
        double yPosition = padding;

        // Draw the string.
        var rectangle = new XRect(
            new XPoint(xPosition, yPosition),
            new XSize(size.Width, size.Height)
        );
        gfx.DrawString(watermark, font, brush, rectangle, XStringFormats.TopLeft);
    }

    public static void DrawWaterMarkInBottomLeft(string watermark, PdfPage page, XGraphics gfx, XFont font, XBrush brush)
    {
        XSize size = gfx.MeasureString(watermark, font);

        double pageHeight = GetPageHeight(page);

        double padding = 10;
        double xPosition = padding;
        double yPosition = pageHeight - size.Height - padding;

        // Draw the string.
        var rectangle = new XRect(
            new XPoint(xPosition, yPosition),
            new XSize(size.Width, size.Height)
        );
        gfx.DrawString(watermark, font, brush, rectangle, XStringFormats.TopLeft);
    }

    public static void DrawWaterMarkInBottomRight(string watermark, PdfPage page, XGraphics gfx, XFont font, XBrush brush)
    {
        XSize size = gfx.MeasureString(watermark, font);

        double pageWidth = GetPageWidth(page);
        double pageHeight = GetPageHeight(page);

        double padding = 10;
        double xPosition = pageWidth - size.Width - padding;
        double yPosition = pageHeight - size.Height - padding;

        // Draw the string.
        var rectangle = new XRect(
            new XPoint(xPosition, yPosition),
            new XSize(size.Width, size.Height)
        );
        gfx.DrawString(watermark, font, brush, rectangle, XStringFormats.TopLeft);
    }
}

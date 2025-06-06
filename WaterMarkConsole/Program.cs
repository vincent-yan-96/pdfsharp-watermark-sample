using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;


// Get a fresh copy of the sample PDF file.
const string filename = "HelloWorld.pdf";
string file = Path.Combine(Directory.GetCurrentDirectory(), filename);

// Create the font for drawing the watermark.
XFont font = PdfHelper.InitFont();

// Create Brush
XBrush brush = PdfHelper.InitBrush();

// Open an existing document for editing and loop through its pages.
using PdfDocument document = PdfReader.Open(file);

// Set version to PDF 1.4 (Acrobat 5) because we use transparency.
if (document.Version < 14)
{
    document.Version = 14;
}

PdfPage page = document.Pages[0];

// Get an XGraphics object for drawing beneath the existing content.
using XGraphics gfx = PdfHelper.ProvidePdfPageGraphicsWithRotationFix(page);

PdfHelper.DrawWaterMarkInTopLeft("Top Left", gfx, font, brush);
PdfHelper.DrawWaterMarkInTopRight("Top Right", page, gfx, font, brush);
PdfHelper.DrawWaterMarkInBottomLeft("Bottom Left", page, gfx, font, brush);
PdfHelper.DrawWaterMarkInBottomRight("Bottom Right", page, gfx, font, brush);

document.Save("Watermark.pdf");

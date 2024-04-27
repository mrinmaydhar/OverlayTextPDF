using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas;
using iText.Kernel.Pdf.Extgstate;

namespace ConsoleApp1
{
    public class Program
    {
        public static void Main()
        {
            string directoryPath = @"C:\Users\mrinm\Downloads\New folder\scratch";

            // Get all PDF files in the directory
            string[] pdfFiles = Directory.GetFiles(directoryPath, "*.pdf");

            foreach (string pdfPath in pdfFiles)
            {
                // Handle the file
                HandleFile(pdfPath, directoryPath);
            }
        }

        public static void HandleFile(string pdfPath, string directoryPath)
        {
            // Create a new PdfReader object
            PdfReader reader = new(pdfPath);

            // Specify the path to the nested folder
            string nestedFolderPath = Path.Combine(directoryPath, "ModifiedFiles");

            // Make sure the nested folder exists
            Directory.CreateDirectory(nestedFolderPath);

            // Get the name of the original file
            string fileName = Path.GetFileName(pdfPath);

            // Create a new PdfDocument object
            PdfDocument pdfDoc = new(reader, new PdfWriter(Path.Combine(nestedFolderPath, fileName + "_modified.pdf")));

            // Modify the pages
            ModifyPages(pdfDoc);

            // Close the pdfDoc
            pdfDoc.Close();
        }
        public static void ModifyPages(PdfDocument pdfDoc)
        {
            // Loop through all pages
            for (int i = 1; i <= pdfDoc.GetNumberOfPages(); i++)
            {
                // Get the current page
                PdfPage page = pdfDoc.GetPage(i);

                // Create a new PdfCanvas object
                PdfCanvas pdfCanvas = new(page);

                // Define the text and font size
                string text = "For ICICI Lombard use only";
                float fontSize = 36;

                // Estimate the width and height of the text box
                float textBoxWidth = text.Length * fontSize / 2; // Adjust as needed
                float textBoxHeight = fontSize * 1.2f; // Increase the height by 20% to accommodate descenders

                // Define the position for the text
                float xPosition = page.GetPageSize().GetWidth() * 0.05f; // 5% from the left
                float yPosition = page.GetPageSize().GetTop() - page.GetPageSize().GetHeight() * 0.05f; // 5% from the top

                // Adjust the xPosition for the rectangle to add some padding
                float rectXPosition = xPosition - fontSize / 2; // Add a space worth of character padding

                // Adjust the yPosition for the rectangle to align with the descenders of the text
                float rectYPosition = yPosition - textBoxHeight * 0.3f; // Decrease by 20% of the height of the text box

                // Create a rectangle for the background
                iText.Kernel.Geom.Rectangle rect = new(rectXPosition, rectYPosition, textBoxWidth, textBoxHeight); // Increase the width of the rectangle by the padding

                // Set the fill color to grey with transparency
                PdfExtGState state = new PdfExtGState().SetFillOpacity(0.5f); // 0.5f for 50% transparency
                pdfCanvas.SaveState()
                    .SetExtGState(state)
                    .Rectangle(rect)
                    .SetColor(ColorConstants.GRAY, true) // true for fill color
                    .Fill()
                    .RestoreState();

                // Add the text
                pdfCanvas.SaveState()
                    .BeginText().SetFontAndSize(PdfFontFactory.CreateFont(), fontSize)
                    .MoveText(xPosition, yPosition)
                    .ShowText(text)
                    .EndText()
                    .RestoreState();
            }
        }

    }
}
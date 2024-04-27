# PDF Modifier

This is a console application that modifies PDF files in a specified directory. The modifications include adding a watermark text to each page of the PDF.

## Dependencies

The project uses the following namespaces:

- `iText.Kernel.Colors`
- `iText.Kernel.Font`
- `iText.Kernel.Pdf`
- `iText.Kernel.Pdf.Canvas`
- `iText.Kernel.Pdf.Extgstate`
- `iText.Kernel.Geom`

## How it works

1. The `Main` method gets all PDF files in the specified directory.
2. For each PDF file, it calls the `HandleFile` method.
3. The `HandleFile` method creates a new `PdfReader` object for the current PDF file and a new `PdfDocument` object for the output file.
4. It then calls the `ModifyPages` method to modify the pages of the PDF document.
5. The `ModifyPages` method loops through all pages of the PDF document. For each page, it creates a new `PdfCanvas` object and adds a watermark text to the page.
6. The watermark text is "sample text", and it is added with a font size of 36. The text is positioned 5% from the left and top of the page.
7. The watermark text is added on a grey rectangle with 50% transparency to make it look like a watermark.
8. Finally, the `PdfDocument` object is closed, saving the modifications to the new file.

## Usage

To use this application, simply run the `Program.cs` file in your .NET environment. Make sure to update the `directoryPath` variable in the `Main` method to the directory containing the PDF files you want to modify.

## Auto-generated README

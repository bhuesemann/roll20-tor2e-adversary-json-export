using System.Collections.Generic;
using System.Linq;
using System.IO;
using UglyToad.PdfPig;
using UglyToad.PdfPig.Content;

namespace roll20_adv_import_c
{
    public static class PdfConverter
    {
        public static void convert(string pdfpath, string txtpath)
        {
            using (PdfDocument document = PdfDocument.Open(pdfpath))
            {
                using (StreamWriter w = new StreamWriter(txtpath))
                {
                    foreach (Page page in document.GetPages())
                    {
                        IReadOnlyList<Letter> letters = page.Letters;
                        string line = string.Join(string.Empty, letters.Select(x => x.Value));
                        w.WriteLine(line);
                    }
                }
            }
        }
    }
}

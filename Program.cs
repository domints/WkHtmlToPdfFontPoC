using System;
using System.IO;
using DinkToPdf;

namespace WkHtmlToPdfPoC
{
    class Program
    {
        static void Main(string[] args)
        {
            UseDink();
        }

        static void UseDink()
        {
            var converter = new SynchronizedConverter(new PdfTools());
            converter.Convert(GetDinkDoc("base64", "12.6"));
            converter.Convert(GetDinkDoc("disk", "12.6"));
            converter.Convert(GetDinkDoc("googleFonts", "12.6"));
            converter.Convert(GetDinkDoc("url", "12.6"));
        }

        static HtmlToPdfDocument GetDinkDoc(string stylesheet, string version)
        {
            return new HtmlToPdfDocument()
            {
                GlobalSettings = {
                    ColorMode = ColorMode.Color,
                    Orientation = Orientation.Landscape,
                    PaperSize = PaperKind.A4Plus,
                    Out = $"Out-Dink-{version}-{stylesheet}.pdf"
                },
                Objects = {
                    new ObjectSettings() {
                        PagesCount = true,
                        HtmlContent = File.ReadAllText("input.html"),
                        WebSettings = { DefaultEncoding = "utf-8", UserStyleSheet = Path.Combine("assets", stylesheet + ".css") },
                        HeaderSettings = { FontSize = 9, Right = "Page [page] of [toPage]", Line = true, Spacing = 2.812 },
                        LoadSettings = new LoadSettings {
                            BlockLocalFileAccess = false
                        }
                    }
                }
            };
        }
    }
}

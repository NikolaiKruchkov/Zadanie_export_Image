using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExportImage
{
    [Transaction(TransactionMode.Manual)]
    public class Main : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            Document doc = commandData.Application.ActiveUIDocument.Document;
            View view = doc.ActiveView;
            string dEsktoppath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), view.Name);


            using (var ts = new Transaction(doc, "export Image"))
            {
                ts.Start();
                var imageOption = new ImageExportOptions
                {
                    ZoomType = ZoomFitType.FitToPage,
                    FilePath = dEsktoppath,
                    FitDirection = FitDirectionType.Horizontal,
                    HLRandWFViewsFileType = ImageFileType.JPEGMedium,
                    ExportRange = ExportRange.CurrentView,
                    ViewName = view.Name
                };              
                    doc.ExportImage(imageOption);                
                ts.Commit();
            }

            return Result.Succeeded;
        }
    }
}

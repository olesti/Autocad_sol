using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Application = Autodesk.AutoCAD.ApplicationServices.Application;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.Windows;
using Autodesk.AutoCAD.Customization;

using adWin = Autodesk.AutoCAD.Windows;

using Autodesk.AutoCAD.EditorInput;
using System.Diagnostics;
using Autodesk.AutoCAD.Customization;
using RibbonControl = Autodesk.AutoCAD.Customization.RibbonControl;
using Autodesk.AutoCAD.GraphicsInterface;
using System.Windows;
namespace Autocadsolarian
{
    public class Class1
    {
        [CommandMethod("PositionApplicationWindow")]
        public static void PositionApplicationWindow()
        {
            // Set the position of the Application window
            System.Windows.Point ptApp = new System.Windows.Point(0, 0);
            Autodesk.AutoCAD.ApplicationServices.Application.MainWindow.DeviceIndependentLocation = ptApp;

            // Set the size of the Application window
            System.Windows.Size szApp = new System.Windows.Size(400, 400);
            Autodesk.AutoCAD.ApplicationServices.Application.MainWindow.DeviceIndependentSize = szApp;
        }
        [CommandMethod("solarian")]
        public static void solarian()
        {
            var dialog = new UserControl1();
            var result = Application.ShowModalWindow(dialog);

        }


    }
}

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
        [CommandMethod("atalay")]
        public static void atalay()
        {
            var dialog = new UserControl1();
            var result = Application.ShowModalWindow(dialog);
            // First, the button items are created:
            Autodesk.AutoCAD.Customization.RibbonButton button1 = new RibbonButton();
            button1.Text = "Button1";

            RibbonButton button2 = new RibbonButton;
            button2.Text = "Button2";

            // These are then added to a row:
            RibbonRow row = new RibbonRow();
            row.RowItems.Add(button1);
            row.RowItems.Add(button2);

            // This row is added to a panel source, which is then added to a panel:
            RibbonPanelSource panelSource = new RibbonPanelSource();
            panelSource.Title = "Panel1";
            panelSource.Rows.Add(row);

            RibbonPanel panel = new RibbonPanel();
            panel.Source = panelSource;

            // Last, the panel is added to a tab, which is added to the ribbon:
            RibbonTab tab = new RibbonTab();
            tab.Title = "Tab1";
            tab.Panels.Add(panel);

            RibbonControl ribbon = new RibbonControl();
            ribbon.Tabs.Add(tab);

        }


    }
}

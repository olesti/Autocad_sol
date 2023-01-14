using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.Customization;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.Runtime;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Application = Autodesk.AutoCAD.ApplicationServices.Application;
using Polyline = Autodesk.AutoCAD.DatabaseServices.Polyline;

namespace Autocadsolarian
{
    /// <summary>
    /// UserControl1.xaml etkileşim mantığı
    /// </summary>
    public partial class UserControl1 : Window
    {
        public UserControl1()
        {
            InitializeComponent();
        }
        SQLiteConnection m_dbCon = new SQLiteConnection("Data Source=Solarian.sqlite;Version=3;");
        double width { get; set; }
        double depth { get; set; }
        double height { get; set; }
        private void txt_depth_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txt_width_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            createfile();
            createValue();
            readValue();
            // Get the current document and database, and start a transaction
            Document acDoc = Application.DocumentManager.MdiActiveDocument;
            Database acCurDb = acDoc.Database;
            using (Transaction acTrans = acCurDb.TransactionManager.StartTransaction())
            {
                // Open the Block table record for read
                BlockTable acBlkTbl;
                acBlkTbl = acTrans.GetObject(acCurDb.BlockTableId,
                                             OpenMode.ForRead) as BlockTable;

                // Open the Block table record Model space for write
                BlockTableRecord acBlkTblRec;
                acBlkTblRec = acTrans.GetObject(acBlkTbl[BlockTableRecord.ModelSpace],
                                                OpenMode.ForWrite) as BlockTableRecord;

                // Create a 3D solid wedge
                Solid3d acSol3D = new Solid3d();
                acSol3D.SetDatabaseDefaults();
                acSol3D.CreateWedge(width * 100, depth * 100, height * 100);

                // Position the center of the 3D solid at (5,5,0) 
                acSol3D.TransformBy(Matrix3d.Displacement(new Point3d(width, depth, height) -
                                                          Point3d.Origin));
                // Add the new object to the block table record and the transaction
                acBlkTblRec.AppendEntity(acSol3D);
                acTrans.AddNewlyCreatedDBObject(acSol3D, true);

                // Open the active viewport
                ViewportTableRecord acVportTblRec;
                acVportTblRec = acTrans.GetObject(acDoc.Editor.ActiveViewportId,
                                                  OpenMode.ForWrite) as ViewportTableRecord;

                // Rotate the view direction of the current viewport
                acVportTblRec.ViewDirection = new Vector3d(-1, -1, 1);
                acDoc.Editor.UpdateTiledViewportsFromDatabase();

                // Save the new objects to the database
                acTrans.Commit();
            }
        }
        [CommandMethod("MYACADWPF", CommandFlags.Modal)]
        public void MyACADUtilitiesWPF()
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Editor ed;
            if (doc != null)
            {
                ed = doc.Editor;
                var dialog = new UserControl1();


            }
        }

        public void createfile()
        {
            if (!File.Exists(@"C:\Users\ataly\source\repos\Autocadsolarian\Autocadsolarian\bin\Debug\Solarian.sqlite"))
            {
                SQLiteConnection.CreateFile("Solarian.sqlite");

            }
            m_dbCon.Open();
            CreateTable();
        }
        void CreateTable()
        {
            SQLiteCommand sqliteCmd1 = m_dbCon.CreateCommand();

            sqliteCmd1.CommandText = "DELETE FROM axis";
            sqliteCmd1.ExecuteNonQuery();


            SQLiteCommand sqliteCmd = m_dbCon.CreateCommand();

            sqliteCmd.CommandText = "CREATE TABLE IF NOT EXISTS axis (x Double Primary Key, y Double,z Double)";
            sqliteCmd.ExecuteNonQuery();
        }
        void createValue()
        {
            if (!string.IsNullOrEmpty(txt_depth.Text) && !string.IsNullOrEmpty(txt_height.Text) && !string.IsNullOrEmpty(txt_width.Text))
            {
                var cmd = new SQLiteCommand(m_dbCon);

                cmd.CommandText = $"INSERT INTO axis(x,y,z) VALUES({txt_width.Text.Replace(',', '.')},{txt_height.Text.Replace(',', '.')},{txt_depth.Text.Replace(',', '.')})";
                cmd.ExecuteNonQuery();
            }
            else
            {
                MessageBox.Show("Please fill every textbox ");
            }

        }
        void readValue()
        {
            string stm = "SELECT * FROM axis ";

            var cmd = new SQLiteCommand(stm, m_dbCon);
            SQLiteDataReader rdr = cmd.ExecuteReader();
            /*
                        SQLiteDataAdapter sda = new SQLiteDataAdapter(cmd);
                        DataTable dt = new DataTable("axis");
                        sda.Fill(dt);
                        DataGridHome.ItemsSource = dt.DefaultView;
                        sqlite.Close();*/
            while (rdr.Read())
            {

                width = rdr.GetDouble(0);

                height = rdr.GetDouble(1);

                depth = rdr.GetDouble(2);

            }
            MessageBox.Show(width.ToString() + "//" + height.ToString() + "//" + depth.ToString());
            m_dbCon.Close();

        }
        /* private void addPanel2(RibbonTab ribTab)
         {
             RibbonButton button1 = new RibbonButton;
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

         }*/
    }
}

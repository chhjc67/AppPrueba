using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Xml;
using System.Xml.Linq;

namespace WpfApplication2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Loaded +=new RoutedEventHandler(MainWindow_Loaded);
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {

            var dataSet = new List<Currency>()
            {
                new Currency() { CurrencyID = "COL", Name = "Peso"},
                new Currency() { CurrencyID = "CO1", Name = "Peso1"},
                new Currency() { CurrencyID = "CO2", Name = "Peso2"},
                new Currency() { CurrencyID = "CO3", Name = "\t\t\tPeso3"},
                new Currency() { CurrencyID = "CO4", Name = "Peso4"},
                new Currency() { CurrencyID = "CO5", Name = "Peso5"},
                new Currency() { CurrencyID = "CO6", Name = "Peso6"},
                new Currency() { CurrencyID = "CO7 WWW WWW XXX TTT XXX SS DDD EEE", Name = "Peso7"},
            };
            var path = Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).FullName).FullName;
            string fileName = String.Format("{0}\\Currency.rdlc", path);
            ///https://www.c-sharpcorner.com/UploadFile/nipuntomar/report-viewer-control-in-wpf/
            ///https://www.codeproject.com/script/Articles/ViewDownloads.aspx?aid=752970

            var xx = XElement.Load(fileName).FirstNode.NextNode;

            //foreach (XElement level1 in XElement.Load(fileName).FirstAttribute)
            //{
            //    var yy = level1.Name;

            //    foreach (XElement level2 in level1.Elements())
            //    {

            //    }
            //}


            var textReader = new XmlTextReader(fileName);
            XDocument xDocument = XDocument.Load(textReader);

            //string declaration = xDocument.Declaration.ToString();
            //var test = from elem in xDocument.Root.Elements()
            //           select new
            //           {
            //               Node = elem.Name.LocalName,
            //               Result = elem.DescendantNodes()
            //           };

            this.ReportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dataSet));
            using (var stream = new MemoryStream())
            {
                xDocument.Save(stream);
                stream.Position = 0;
                this.ReportViewer.LocalReport.LoadReportDefinition(stream);
            }

            this.ReportViewer.Print+= new ReportPrintEventHandler(ReportViewer_Print);

            this.ReportViewer.RefreshReport();
        }

        void ReportViewer_Print(object sender, ReportPrintEventArgs e)
        {
            var xx = sender;
        }

    }

    public class Currency
    {
        public string CurrencyID { get; set; }
        public string Name { get; set; }
    }

    public class Struct_Node
    {
        public string Node { get; set; }
    }

}

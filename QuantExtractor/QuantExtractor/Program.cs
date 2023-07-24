using System;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using PdfSharp;
using System.Windows.Forms;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using QuantExtractor;
using System.Globalization;

namespace QuantExtractor
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = true;

            List<Sample> samples = new List<Sample>();

            if (DialogResult.OK != dialog.ShowDialog()) return;
            
            foreach (var path in dialog.FileNames)
            {
                samples.Add(ExtractSampleData(path));
                Console.WriteLine(samples[^1].ToString());
                Console.WriteLine();
            }

            Concentration concentration = GenerateConcentrationData(samples);

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Path.GetDirectoryName(dialog.FileName);
            saveFileDialog.Filter = "Comma Separated Values (*.csv)|*.csv";
            
            if (DialogResult.OK != saveFileDialog.ShowDialog()) return;

            using var writer = new StreamWriter(saveFileDialog.FileName);
            
            foreach (var s in concentration.Samples)
            {
                var retentionTime = s.RetentionTimes[concentration.InternalStandard];
                var response = s.Responses[concentration.InternalStandard];
                writer.WriteLine(concentration.InternalStandard + "," + s.Id + "," + retentionTime + "," + response);
            }
            writer.WriteLine();

            foreach (var analyte in concentration.Analytes)
            {
                foreach (var s in concentration.Samples)
                {
                    var retentionTime = s.RetentionTimes[analyte];
                    var response = s.Responses[analyte];
                    writer.WriteLine(analyte + "," + s.Id + "," + retentionTime + "," + response);
                }
                writer.WriteLine();

            }
        }

        private static string OpenPdf(string filePath)
        {
            return PdfTextract.PdfTextExtractor.GetText(filePath);
        }

        private static Sample ExtractSampleData(string filename)
        {
            var pdfText = OpenPdf(filename);

            var sample = new Sample();

            var tokenizedText = Regex.Split(pdfText, @"\s{2,}");

            var list = new List<string>(tokenizedText);

            sample.Id = list.Find(s => s.Contains("Data File :")).Split("_")[1];

            var iInternalStandard = list.IndexOf("1) Morphine-D3");
            var iMorphine = list.IndexOf("2) Morphine");
            var iCodeine = list.IndexOf("3) Codeine");
            var iThebaine = list.IndexOf("4) Thebaine");

            sample.RetentionTimes["Morphine-D3"] = float.Parse(list[iInternalStandard + 1]);
            sample.RetentionTimes["Morphine"] = float.Parse(list[iMorphine + 1]);
            sample.RetentionTimes["Codeine"] = float.Parse(list[iCodeine + 1]);
            sample.RetentionTimes["Thebaine"] = float.Parse(list[iThebaine + 1]);

            sample.Responses["Morphine-D3"] = Int32.Parse(list[iInternalStandard + 3]);
            sample.Responses["Morphine"] = Int32.Parse(list[iMorphine + 3]);
            sample.Responses["Codeine"] = Int32.Parse(list[iCodeine + 3]);
            sample.Responses["Thebaine"] = Int32.Parse(list[iThebaine + 3]);

            return sample;
        }

        private static Concentration GenerateConcentrationData(List<Sample> samples)
        {
            Concentration concentration = new Concentration();

            foreach (var s in samples)
            {
                concentration.Samples.Add(s);
            }
            return concentration;
        }
    }
}

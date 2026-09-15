using System.IO;
using UnityEditor;

namespace DN
{
    public static class SpreadsheetImporter
    {
        [MenuItem("Dino Nuggets/Update Spreadsheets")]
        public static void UpdateSpreadsheets()
        {
            GSpreadSheetsToJson google = new GSpreadSheetsToJson();
            google.Init();
            google.DownloadToJson();                                                // DownloadToJson() method needs to be public
            EditorUtility.ClearProgressBar();
            foreach (var sheetName in google.ranges)                                // Local varibale ranges needs to be exposed as a public field
            {
                string path = google.outputDir + sheetName;
                string objFile = path + ".asset";                                   // outputDir field needs to be public
                string jsonFile = path + ".txt";                                    // outputDir field needs to be public
                var sheet = AssetDatabase.LoadAssetAtPath<Spreadsheet>(objFile);
                sheet.Deserialize(File.ReadAllText(jsonFile));
                File.Delete(jsonFile);
            }
            AssetDatabase.Refresh();
        }
    }
}

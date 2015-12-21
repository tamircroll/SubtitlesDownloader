using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using SubtitlesDownloader.Enums;

namespace SubtitlesDownloader
{
    public class SetupData
    {
        public static string trueStr = "true";
        public static string falseStr = "false";

        private readonly string m_DataFilePath;

        public SetupData()
        {
            string dataFolderPath = string.Format(@"{0}\SubtitlesDownloader",
                Environment.GetFolderPath(Environment.SpecialFolder.UserProfile));
            if (!Directory.Exists(dataFolderPath)) Directory.CreateDirectory(dataFolderPath);

            m_DataFilePath = string.Format(@"{0}\\data.txt", dataFolderPath);

            if (!File.Exists(m_DataFilePath)) InitNewDataFile();

            populateVariables();
        }

        public string Path { get; set; }
        public List<string> Languages { get; set; }
        public string NoSubFolders { get; set; }
        public string BackgroundRun { get; set; }

        private void populateVariables()
        {
            string[] allDataLines = File.ReadAllLines(m_DataFilePath);

            if (allDataLines.Count() < 4)
            {
                InitNewDataFile();
                allDataLines = File.ReadAllLines(m_DataFilePath);
            }

            Path = allDataLines[(int) DataType.Path];
            Languages = allDataLines[(int) DataType.Languages].Split(',').ToList();
            NoSubFolders = allDataLines[(int) DataType.SubFolders];
            BackgroundRun = allDataLines[(int) DataType.BackgroundRun];
        }

        private void InitNewDataFile()
        {
            Path = @"C:\";
            Languages = new List<string> {"hebrew", "english"};
            NoSubFolders = trueStr;
            BackgroundRun = falseStr;

            SaveData();
        }

        public void SaveData()
        {
            File.WriteAllText(m_DataFilePath, ToString());
        }

        public override string ToString()
        {
            if (Path == null || Languages == null || Languages.Count == 0 || NoSubFolders == null ||
                BackgroundRun == null)
                populateVariables();

            string langsStr = Languages[0];

            for (int i = 1; i < Languages.Count(); i++)
            {
                langsStr += "," + Languages[i];
            }

            return string.Format("{1}{0}{2}{0}{3}{0}{4}", Environment.NewLine, Path, langsStr, NoSubFolders,
                BackgroundRun);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace grantsLibrary
{
    public class CSVProcessor
    {
        private System.IO.StreamReader csvReader;
        private String csvPath;
        protected String[][] csvArray;
        protected String lastError;

        
        public CSVProcessor(String csvPathString)
        {
            csvPath = csvPathString;
        }
        public CSVProcessor()
        {
        }

        public bool CsvArrayCreator()
        {
            try
            {
                csvReader = new System.IO.StreamReader(csvPath);
            }
            catch(Exception x)
            {
                lastError = x.Message;
                return false;
            }
            String[] tempWineArray;
            List<String> wineList = new List<string>();
            while (!csvReader.EndOfStream)
            {
                wineList.Add(csvReader.ReadLine());
            }
            csvReader.Close();
            csvArray = new String[wineList.Count][];
            for (int i = 0; i < wineList.Count; i++)
            {
                tempWineArray = wineList[i].Split(',');
                csvArray[i] = tempWineArray;
            }
            return true;
        }


        public String[][] CsvArray
        {
            get { return csvArray; }
        }
        public String LastError
        {
            get { return lastError; }
        }
        public String CsvPath
        {
            get { return csvPath; }
            set { csvPath = value; }
        }
    }
}

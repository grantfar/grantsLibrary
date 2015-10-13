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
        /// <summary>
        /// Deletes the contents of the file and writes a new CSV
        /// </summary>
        /// <param name="toWrite"></param>
        /// <returns>if successful</returns>
        public bool CsvWriteOveride(String[] toWrite)
        {
            return CsvWrite(toWrite,FileMode.OpenOrCreate);
        }

        public bool CsvWriteAppendEnd(String[] toWrite)
        {
            return CsvWrite(toWrite,FileMode.Append);
        }

        public bool CsvWriteAppendBegining(String[] toWrite)
        {
            String[] tmp = mergeLines(csvArray);
            return CsvWriteOveride(toWrite) & CsvWriteAppendEnd(tmp);
        }

        public bool CsvWrite(String[] toWrite,FileMode writeType)
        {
            StreamWriter writeCsv;
            try
            {
                writeCsv = new StreamWriter(File.Open(csvPath,writeType));
            }
            catch (Exception x)
            {
                lastError = x.Message;
                return false;
            }
            foreach (String s in toWrite)
                writeCsv.WriteLine(s);
            writeCsv.Close();
            return CsvArrayCreator();
        }

        private String[] mergeLines(String[][] toMerge)
        {
            String[] returnString = new String[toMerge.GetLength(1)];
            for(int i = 0; i < toMerge.GetLength(1); i++)
            {
                String tmp = "";
                for(int k = 0; k < toMerge.GetLength(0)-1; k++)
                    tmp += toMerge[i][k] + ",";
                tmp += toMerge[i][toMerge.GetLength(0)-1];
                returnString[i] = tmp;
            }
            return returnString;
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

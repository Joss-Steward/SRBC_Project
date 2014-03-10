using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;
using System.Data.Sql;

namespace SRBC_DataParser {
    class DataParser {
        String sourceFilePath;
        char[] delimiters;

        public Dictionary<String, int> stationToIDDict;
        public List<String[]> values;
        public String[] fields;

        int indexOfStationNames;

        public int linesScanned{
            get {
                return values.Count;
            }
        }

        public DataParser(String setSourceFilePath, char[] setDelimiters) {
            sourceFilePath = setSourceFilePath;
            delimiters = setDelimiters;

            stationToIDDict = new Dictionary<string, int>();            
            values = new List<string[]>();
            indexOfStationNames = 0;
        }

        //Parses the file
        public void parseFile() {
            if (!File.Exists(sourceFilePath))
                throw new FileNotFoundException("Could not find specified file. File Path: {" + sourceFilePath + "}");

            StreamReader file = new StreamReader(sourceFilePath);

            scanFirstLine(file);

            int stationId = 0;

            while (!file.EndOfStream) {
                String line = file.ReadLine();
                String[] pieces = line.Split(delimiters);
                
                values.Add(pieces);

                if (!stationToIDDict.ContainsKey(pieces[indexOfStationNames])) {
                    stationToIDDict.Add(pieces[indexOfStationNames], stationId);
                    stationId++;
                }
            }

            file.Close();
        }

        //Scans the first line and initializes the fields list
        private int scanFirstLine(StreamReader file) {
            String line = file.ReadLine();
            String[] lineFields = line.Split(delimiters);

            fields = lineFields;

            int i = 0;

            foreach (String n in fields) {
                if (n == "StationName") {
                    indexOfStationNames = i;
                    break;
                }

                i++;
            }

            return fields.Length;
        }
    }
}

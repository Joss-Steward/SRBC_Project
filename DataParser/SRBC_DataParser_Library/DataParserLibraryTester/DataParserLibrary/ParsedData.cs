using System;
using System.Collections.Generic;
using System.Text;

namespace DataParserLibrary {
    class ParsedData {
        public List<String[]> parsedValues;
        public Dictionary<String, int> columnNameToIndexDict;
        public List<String> uniqueStations;

        int numberOfColumns;

        public ParsedData() {
            parsedValues = new List<string[]>();
            columnNameToIndexDict = new Dictionary<string, int>();
            uniqueStations = new List<string>();

            numberOfColumns = 0;
        }

        //Returns the current number of unique stations
        public int addStation(String stationName){
            if (!uniqueStations.Contains(stationName))
                uniqueStations.Add(stationName);

            return uniqueStations.Count;
        }

        //Returns the current number of unique columns
        public int addColumn(String columnName, int columnIndex) {
            if (!columnNameToIndexDict.ContainsKey(columnName)) {
                if (!columnNameToIndexDict.ContainsValue(columnIndex)) {
                    columnNameToIndexDict.Add(columnName, columnIndex);
                    numberOfColumns++;
                } else { //The column index has already been added
                    //Throw a duplicate column index error
                }
            } else { //The column name has already been added
                //Throw a duplicate column name error
            }

            return columnNameToIndexDict.Count;
        }

        //Returns true if successful
        public bool addRow(String[] row) {
            if (row.Length != numberOfColumns)
                return false; //Bad thing, throw exception

            parsedValues.Add(row);

            return true;
        }

        public String[] this[int index] {
            get {
                return parsedValues[index];
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data;

namespace SRBC_DataParser {
    class DataParser {
        String sourceFilePath;
        char[] delimiters;

        public Dictionary<String, int> stationToIDDict;
        public List<String[]> values;
        public String[] fields;

        int indexOfStationNames;

        public int linesScanned {
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

            syncStations();
        }

        //Ensures all the stations listed in the file are also in the database
        private int syncStations() {
            //Just run a loop with a simple SQL statement to update the station list
            SqlConnection dbConnection = new SqlConnection("Data Source=(localdb)\\v11.0;Initial Catalog=SRBC_DB;Integrated Security=True;Connect Timeout=15;Encrypt=False;");
            dbConnection.Open();
                   
            foreach (KeyValuePair<String, int> name in stationToIDDict) {
                SqlCommand insertCommand
                    = new SqlCommand("USE SRBC_DB;" +
                        "IF NOT EXISTS " +
                        "(   SELECT  1 " +
                        "FROM    StationMetaData " +
                        "WHERE   (StationMetaData.StationName = @name) " +
                        ") BEGIN " +
                        "INSERT StationMetaData (StationName, StationLocation) " +
                        "VALUES (@name, NULL) " +
                        "END;", dbConnection);

                insertCommand.Parameters.Add(new SqlParameter("@name", name.Key));

                insertCommand.ExecuteNonQuery();
            }

            dbConnection.Close();
            return 0;
        }

        private void uploadData() {
            //Just run a loop with a simple SQL statement to upload the parsed data
            SqlConnection dbConnection = new SqlConnection("Data Source=(localdb)\\v11.0;Initial Catalog=SRBC_DB;Integrated Security=True;Connect Timeout=15;Encrypt=False;");
            dbConnection.Open();



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

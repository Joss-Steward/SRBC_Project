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
        public Dictionary<String, int> fields;

        public int linesScanned {
            get {
                return values.Count;
            }
        }

        public DataParser(String setSourceFilePath, char[] setDelimiters) {
            sourceFilePath = setSourceFilePath;
            delimiters = setDelimiters;

            stationToIDDict = new Dictionary<string, int>();
            fields = new Dictionary<string, int>();
            values = new List<string[]>();
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

                int indexOfStationNames = fields["StationName"];
                if (!stationToIDDict.ContainsKey(pieces[indexOfStationNames])) {
                    stationToIDDict.Add(pieces[indexOfStationNames], stationId);
                    stationId++;
                }
            }

            file.Close();

            syncStations();
            uploadData();
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

            //foreach (String[] line in values) {
            String[] line = values[1];
                SqlCommand insertCommand
                    = new SqlCommand("USE SRBC_DB;" +
                        "INSERT INTO dbo.WaterQualityData " +
                        "(StationID, SampleTime, Temperature, SpecificConductivity, PH, Turbidity, DisolvedOxygen) VALUES " +
                        "((SELECT a.ID FROM StationMetaData a WHERE a.StationName = @_stationname), " +
                        "@_sampletime, @_temperature, @_specificconductivity, @_ph, @_turbidity, @_dissolvedoxygen);", dbConnection);


                insertCommand.Parameters.Add(new SqlParameter("@_stationname", "Bobs Creek                                        "));//line[fields["StationName"]]));
                insertCommand.Parameters.Add(new SqlParameter("@_sampletime", line[fields["SampleTime"]]));
                insertCommand.Parameters.Add(new SqlParameter("@_temperature", line[fields["Temperature"]]));
                insertCommand.Parameters.Add(new SqlParameter("@_specificconductivity", line[fields["SpecificConductivity"]]));
                insertCommand.Parameters.Add(new SqlParameter("@_ph", line[fields["ph"]]));
                insertCommand.Parameters.Add(new SqlParameter("@_turbidity", line[fields["Turbidity"]]));
                insertCommand.Parameters.Add(new SqlParameter("@_dissolvedoxygen", line[fields["DissolvedOxygen"]]));

                insertCommand.ExecuteNonQuery();
            //}

            dbConnection.Close();
        }

        //Scans the first line and initializes the fields list
        private int scanFirstLine(StreamReader file) {
            String line = file.ReadLine();
            String[] lineFields = line.Split(delimiters);

            int i = 0;
            foreach (String n in lineFields) {
                fields.Add(n, i);
            }

            return fields.Count;
        }
    }
}

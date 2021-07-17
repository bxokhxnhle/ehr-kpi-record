#nullable enable

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static System.Console;

namespace Bme121
{
    // Represent one record from the dataset by Office of the National Coordinator (ONC) 
    // Regional Extension Centers (REC) on Key Performance Indicators (KPIs) by county.
    // The ONC REC program provides assistance to health care providers to adopt
    // and meaningfully use certified Electronic Health Records (EHR) technology.
    // Data covers the date range from April 2012 to June 2015.
    // The Office of the National Coordinator for Health Information Technology
    // is a division of the U.S. Department of Health and Human Services.
    // See https://dashboard.healthit.gov/datadashboard/documentation/
    // ONC-REC-kpi-county-data-documentation.php. 
    
    class EhrKpiRecord
    {
        public string State                                { get; private set; }
        public string StateCode                            { get; private set; }
        public string CountyName                           { get; private set; }
        public string StateFips                            { get; private set; }
        public string CountyFips                           { get; private set; }
        public string Fips                                 { get; private set; }
        public string Period                               { get; private set; }
        public int?   NumProvidersSignedUp                 { get; private set; }
        public int?   NumPrimaryCareProvidersSignedUp      { get; private set; }
        public int?   NumProvidersGoLive                   { get; private set; }
        public int?   NumPrimaryCareProvidersGoLive        { get; private set; }
        public int?   NumProvidersMeaningfulUse            { get; private set; }
        public int?   NumPrimaryCareProvidersMeaningfulUse { get; private set; }
        
        public EhrKpiRecord
        (
            string state,
            string stateCode,
            string countyName,
            string stateFips,
            string countyFips,
            string fips,
            string period,
            int?   numProvidersSignedUp,
            int?   numPrimaryCareProvidersSignedUp,
            int?   numProvidersGoLive,
            int?   numPrimaryCareProvidersGoLive,
            int?   numProvidersMeaningfulUse,
            int?   numPrimaryCareProvidersMeaningfulUse
        )
        {
            State                                = state;        
            StateCode                            = stateCode;        
            CountyName                           = countyName;        
            StateFips                            = stateFips;        
            CountyFips                           = countyFips;        
            Fips                                 = fips;        
            Period                               = period;        
            NumProvidersSignedUp                 = numProvidersSignedUp;        
            NumPrimaryCareProvidersSignedUp      = numPrimaryCareProvidersSignedUp;        
            NumProvidersGoLive                   = numProvidersGoLive;        
            NumPrimaryCareProvidersGoLive        = numPrimaryCareProvidersGoLive;        
            NumProvidersMeaningfulUse            = numProvidersMeaningfulUse;        
            NumPrimaryCareProvidersMeaningfulUse = numPrimaryCareProvidersMeaningfulUse;       
        }
    }
    
    static class Program
    {
        static void Main( )
        {
            // Load the ONC REC KPI dataset on EHR adoption by county.
            
            List< EhrKpiRecord > ehrKpiRecords = new List< EhrKpiRecord >( );
            
            // TO DO: Complete this code
            
            const string path = "REC_KPI_County.csv";
            const FileMode mode = FileMode.Open;
            const FileAccess access = FileAccess.Read;
            
            using FileStream file = new FileStream(path, mode, access);
            
            using StreamReader reader = new StreamReader(file);
            reader.ReadLine(); // skip header line
            
            // Variables to store nullable values
            int? numProvidersSignedUp;
            int? numPrimaryCareProvidersSignedUp;
            int? numProvidersGoLive;
            int? numPrimaryCareProvidersGoLive;
            int? numProvidersMeaningfulUse;
            int? numPrimaryCareProvidersMeaningfulUse;
            
            while(! reader.EndOfStream)
            {
                string line = reader.ReadLine()!;
                string[] cols = line.Split(",")!;
                
                // Trim " "
                for (int i = 0; i < cols.Length; i++)
                {
                    cols[i] = cols[i].Trim('"');
                }
                
                string state                                                = cols[0];
                string stateCode                                            = cols[1];
                string countyName                                           = cols[2];
                string stateFips                                            = cols[3];
                string countyFips                                           = cols[4];
                string fips                                                 = cols[5];
                string period                                               = cols[6];
                
                if (cols[7] == "NA") numProvidersSignedUp                      = null;
                else numProvidersSignedUp                  = Convert.ToInt32(cols[7]);
                
                if (cols[8] == "NA") numPrimaryCareProvidersSignedUp           = null;
                else numPrimaryCareProvidersSignedUp       = Convert.ToInt32(cols[8]);
                
                if (cols[9] == "NA") numProvidersGoLive                        = null;
                else numProvidersGoLive                    = Convert.ToInt32(cols[9]);
                
                if (cols[10] == "NA") numPrimaryCareProvidersGoLive            = null;
                else numPrimaryCareProvidersGoLive        = Convert.ToInt32(cols[10]);
                
                if (cols[11] == "NA") numProvidersMeaningfulUse                = null;
                else numProvidersMeaningfulUse            = Convert.ToInt32(cols[11]);
                
                if (cols[12] == "NA") numPrimaryCareProvidersMeaningfulUse     = null;
                else numPrimaryCareProvidersMeaningfulUse = Convert.ToInt32(cols[12]);
                
                EhrKpiRecord record = new EhrKpiRecord(state, stateCode, countyName, 
                    stateFips, countyFips, fips, period, numProvidersSignedUp, 
                    numPrimaryCareProvidersSignedUp, numProvidersGoLive, numPrimaryCareProvidersGoLive, 
                    numProvidersMeaningfulUse, numPrimaryCareProvidersMeaningfulUse);
                    
                // Add object to the list
                ehrKpiRecords.Add(record);
            }
            
            WriteLine( "ehrKpiRecords.Count = {0:n0}", ehrKpiRecords.Count );
            
            // Display all unique ( State, StateCode, StateFips ) three-tuples.
            
            HashSet< ( string, string, string ) > states 
                = new HashSet< ( string, string, string ) >( );
                
            foreach( EhrKpiRecord r in ehrKpiRecords )
            {
                states.Add( ( r.State, r.StateCode, r.StateFips ) );
            }
            
            foreach( ( string, string, string ) s in states )
            {
                WriteLine( s );
            }
        }
    }
}

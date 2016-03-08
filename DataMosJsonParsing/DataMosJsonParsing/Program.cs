using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;
using Newtonsoft.Json;

namespace DataMosJsonParsing
{

    class DataCells2113
    {
        [JsonProperty("global_id")]
        public string global_id { get; set; }

        [JsonProperty("EventName")]
        public string EventName { get; set; }

        [JsonProperty("EventCategory")]
        public string EventCategory { get; set; }

        [JsonProperty("Period")]
        public string Period { get; set; }

        [JsonProperty("Location")]
        public string Location { get; set; }

        [JsonProperty("ExtraInfo")]
        public string ExtraInfo { get; set; }
    }
    

    class Data2113
    {
        [JsonProperty("Id")]
        public string Id { get; set; }

        [JsonProperty("Number")]
        public string Number { get; set; }

        [JsonProperty("Cells")]
        public DataCells2113 Cells { get; set; }
    }
    

    class Program
    {
        static void Print(string key, string value, ConsoleColor color = ConsoleColor.White)
        {
            Console.ResetColor();
            Console.Write(key + " : ");
            Console.ForegroundColor = color;
            Console.WriteLine(value);
            Console.ResetColor();
        }

        static void Main(string[] args)
        {
            int dataset_id = 2113;
            string url = string.Format("http://api.data.mos.ru/v1/datasets/{0}/rows", dataset_id);

            WebClient client = new WebClient();
            client.Encoding = Encoding.UTF8;

            string json = client.DownloadString(url);

            Data2113[] objs = JsonConvert.DeserializeObject<Data2113[]>(json);

            foreach (Data2113 rec in objs)
            {
                Print("Id", rec.Id);
                Print("Number", string.Format("{0}/{1}", rec.Number, objs.Length), ConsoleColor.Green);
                Print("Cells", string.Empty);
                Print("\tGlobalId", rec.Cells.global_id);
                Print("\tEventName", rec.Cells.EventName, ConsoleColor.Yellow);
                Print("\tCategory", rec.Cells.EventCategory);
                Print("\tPeriod", rec.Cells.Period);
                Print("\tLocation", rec.Cells.Location);
                Print("\tExtraInfo", rec.Cells.ExtraInfo);

                Console.WriteLine();
                Console.ReadKey(true);


            }

        }
    }
}
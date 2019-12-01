using Closest.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;


namespace Closest.Controllers
{
    public class ClosestController : ApiController
    {
        static readonly input input1 = new input();
        
        [HttpPost]
        [ActionName("Result")]
          static string[] Main(string[] args)
        {
            string BaseAddress = input1.Address;


            Dictionary<string, int> Dict = new Dictionary<string, int>();
                List<string> address = File.ReadAllLines(@"D:\Learn\address list australia.txt", System.Text.Encoding.Default).ToList();
                foreach (string add in address)
                {
                    string url = "https://maps.googleapis.com/maps/api/distancematrix/xml?origins=" + BaseAddress + "&destinations=" + add + "&key=AIzaSyBE1J5Pe_GZXBR_x9TXOv6TU5vtCSmEPW4";
                    //string url = "https://api.mapbox.com/v4/mapbox.mapbox-streets-v8/12/1171/1566.mvt?" + BaseAddress + "&destinations=" + add + "&access_token=pk.eyJ1Ijoic2F3YW4xMSIsImEiOiJjazNsemsxeW0xYTVlM2lxb3E0Ynl3bmZzIn0.SldV6EoOmFUaEbv2qzrDEw";

                    WebRequest request = WebRequest.Create(url);
                    using (WebResponse response = (HttpWebResponse)request.GetResponse())
                    {

                        using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                        {
                            DataSet dsResult = new DataSet();
                            dsResult.ReadXml(reader);
                            int distance = int.Parse(dsResult.Tables["distance"].Rows[0]["text"].ToString());
                            Dict.Add(add, distance);

                        }
                    }

                }

            
                var ordered = Dict.OrderBy(x => x.Value);
                string[] result = new string[5];
            for (int i = 0; i < 5; i++)
            {
                var a = Dict.ElementAt(0);
                    //Console.WriteLine(a.Key + " - " + a.Value);
                    result[i] = a.Key + "-" + a.Value;
            }
                return result;


        }
    }
}

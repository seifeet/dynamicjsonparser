using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using System.Collections;

namespace DynamicJsonParser
{
    class Program
    {
        static void Main(string[] args)
        {
            const string json =
            "{" +
            "     \"firstName\": \"John\"," +
            "     \"lastName\" : \"Smith\"," +
            "     \"age\"      : 25," +
            "     \"address\"  :" +
            "     {" +
            "         \"streetAddress\": \"21 2nd Street\"," +
            "         \"city\"         : \"New York\"," +
            "         \"state\"        : \"NY\"," +
            "         \"postalCode\"   : \"11229\"" +
            "     }," +
            "     \"phoneNumber\":" +
            "     [" +
            "         {" +
            "           \"type\"  : \"home\"," +
            "           \"number\": \"212 555-1234\"" +
            "         }," +
            "         {" +
            "           \"type\"  : \"fax\"," +
            "           \"number\": \"646 555-4567\"" +
            "         }" +
            "     ]" +
            " }";

            var serializer = new JavaScriptSerializer();
            serializer.RegisterConverters(new[] { new DynamicJsonConverter() });

            dynamic data = serializer.Deserialize<object>(json);

            Console.WriteLine(data.firstName); // John
            Console.WriteLine(data.lastName); // Smith
            Console.WriteLine(data.age); // 25
            Console.WriteLine(data.address.postalCode); // 11229

            Console.WriteLine(data.phoneNumber.Count); // 2

            Console.WriteLine(data.phoneNumber[0].type); // home
            Console.WriteLine(data.phoneNumber[1].type); // fax

            foreach (var pn in data.phoneNumber)
            {
                Console.WriteLine(pn.number); // 212 555-1234, 646 555-4567
            }

            Console.WriteLine(data.ToString());

            // and creating JSON formatted data

            dynamic jdata = new DynamicJsonObject();
            dynamic item1 = new DynamicJsonObject();
            dynamic item2 = new DynamicJsonObject();

            ArrayList items = new ArrayList();

            item1.Name = "Drone";
            item1.Price = 92000.3;
            item2.Name = "Jet";
            item2.Price = 19000000.99;

            items.Add(item1);
            items.Add(item2);

            jdata.Date = "06/06/2004";
            jdata.Items = items;

            Console.WriteLine(jdata.ToString());
        }

    }
}

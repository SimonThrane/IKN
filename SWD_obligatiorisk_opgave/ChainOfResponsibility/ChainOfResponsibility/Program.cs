using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Xml;
using System.Xml.Serialization;

namespace ChainOfResponsibility
{
    class Program
    {
        static void Main(string[] args)
        {
            //Setup Chain of Responsibility
            ChainHandler<Car> handler1=new JsonParser<Car>();
            ChainHandler<Car> handler2 = new XMLParser<Car>();
            ChainHandler<Car> handler3 = new TextParser<Car>();

            handler1.SetNextChain(handler2);
            handler2.SetNextChain(handler3);

            XmlSerializer xs = new XmlSerializer(typeof(Car));

            List <Car> carlist = new List<Car>();

            //Generate data.

            var c = new Car();
            c.Age = 5;
            c.Model = "Audi";
            c.Owner = "Casper";

            var c1 = new Car();
            c.Age = 3;
            c.Model = "VW";
            c.Owner = "Simon";

            var c2 = new Car();
            c.Age = 8;
            c.Model = "BMW";
            c.Owner = "Lars";

            string xml1;

            var data = new List<Data>
            {
                new ChainOfResponsibility.Data
                {
                    InputData = JsonConvert.SerializeObject(c),
                    InputType = "json"
                }
            };


            using (var sw = new StringWriter())
            {
                using (var writer = XmlWriter.Create(sw))
                {
                    xs.Serialize(writer, c1);
                    xml1 = sw.ToString();
                }
            }

            data.Add(new Data
            {
                InputType = "xml",
                InputData = xml1
            });

            data.Add(new Data
            {
                InputType = "Text",
                InputData = "Dette er en test textstreng"
            });

            data.Add(new Data
            {
                InputType = "ASCII",
                InputData = "sfjkdbgfkbg"
            });

            foreach(var item in data)
            {
               carlist.Add(handler1.Parse(item.InputData,item.InputType));
            }

            Console.WriteLine("\n\n");

            foreach (var item in carlist)
            {
                if(item != null)
                Console.WriteLine(item.Owner + " " + item.Model);
                else
                {
                    Console.WriteLine("Ikke korrekt object");
                }
            }


        }

    }
}

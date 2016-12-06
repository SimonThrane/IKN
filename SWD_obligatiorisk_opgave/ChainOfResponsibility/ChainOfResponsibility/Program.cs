using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

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

            //Generate data.

            var c = new Car();
            c.Age = 5;
            c.Model = "Audi";
            c.Owner = "Casper";

            List<Data> data = new List<Data>();


            


        }

    }
}

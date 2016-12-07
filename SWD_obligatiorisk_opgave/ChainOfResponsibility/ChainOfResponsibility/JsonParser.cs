using System;
using Newtonsoft.Json;

namespace ChainOfResponsibility
{
    class JsonParser<T> : ChainHandler<T>
    {
        public override T Parse(string inputdata, string inputtype)
        {
            if (inputtype.ToLower() == "json")
            {
                Console.WriteLine("This is JSON, I am parsing this");
                T data= JsonConvert.DeserializeObject<T>(inputdata);
                return data;
            }
            else
            {
                return NextChain.Parse(inputdata, inputtype);
            }
        }
    }
}
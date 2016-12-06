﻿using System;
using Newtonsoft.Json;

namespace ChainOfResponsibility
{
    class JsonParser<T> : ChainHandler<T>
    {
        public override T Parse(string inputdata, string inputtype)
        {
            if (inputtype == "JSON")
            {
                Console.WriteLine("This i JSON I am parsing this");
                T data=JsonConvert.DeserializeObject<T>(inputdata);
                return data;
            }
            else
            {
                return NextChain.Parse(inputdata, inputtype);
            }
        }
    }
}
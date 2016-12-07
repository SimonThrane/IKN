using System;

namespace ChainOfResponsibility
{
    class TextParser<T> : ChainHandler<T>
    {
        public override T Parse(string inputdata, string inputtype)
        {
            if (inputtype.ToLower() == "text")
            {
                Console.WriteLine("This is Text: "+inputdata);
                return default(T);

            }
            else
            {
                Console.WriteLine("No passing succes");
                return default(T);
            }
        }
    }
}
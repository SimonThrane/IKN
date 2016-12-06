using System;

namespace ChainOfResponsibility
{
    class TextParser<T> : ChainHandler<T>
    {
        public override T Parse(string inputdata, string inputtype)
        {
            if (inputtype == "TEXT")
            {
                Console.WriteLine("This i Text: "+inputdata);
                return NextChain.Parse(inputdata, inputtype);

            }
            else
            {
                return NextChain.Parse(inputdata, inputtype);
            }
        }
    }
}
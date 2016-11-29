namespace ChainOfResponsibility
{
    public abstract class ChainHandler<T>
    {
        public ChainHandler<T> NextChain { get; set; }

        public void SetNextChain(ChainHandler<T> nextChain)
        {
            NextChain = nextChain;
        }

        public abstract T Parse(string inputdata, string inputtype);

    }

    class XMLParser<T> : ChainHandler<T>
    {
        public override T Parse(string inputdata, string inputtype)
        {
            throw new System.NotImplementedException();
        }
    }

    class JsonParser<T> : ChainHandler<T>
    {
        public override T Parse(string inputdata, string inputtype)
        {
            throw new System.NotImplementedException();
        }
    }
}
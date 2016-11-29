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
}
namespace ChainOfResponsibility
{
    class JsonParser<T> : ChainHandler<T>
    {
        public override T Parse(string inputdata, string inputtype)
        {
            throw new System.NotImplementedException();
        }
    }
}
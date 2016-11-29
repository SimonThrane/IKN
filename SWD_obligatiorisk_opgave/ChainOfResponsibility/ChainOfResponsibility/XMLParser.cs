namespace ChainOfResponsibility
{
    class XMLParser<T> : ChainHandler<T>
    {
        public override T Parse(string inputdata, string inputtype)
        {
            throw new System.NotImplementedException();
        }
    }
}
public interface IPen : IListlike, IMenuable
{
    public string GetName();
    public Type GetGenericType();
    public string Describe();
    public string DescribeSeparated();
}
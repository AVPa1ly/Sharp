namespace CfgManager
{
    //interface required for each configuration class
    public interface IValidate
    {
        public string GetOptions<T>();
    }
}

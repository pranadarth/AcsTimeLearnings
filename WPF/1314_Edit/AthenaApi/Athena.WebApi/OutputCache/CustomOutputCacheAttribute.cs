namespace Athena.WebApi.OutputCache
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class DynamicOutputCacheTagAttribute : Attribute
    {
        public string ParameterName { get; }

        public DynamicOutputCacheTagAttribute(string parameterName)
        {
            ParameterName = parameterName;
        }
    }
}

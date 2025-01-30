namespace Core.Config
{
    public interface IConfig
    {
        System.Type ConfigType { get; }
    }

    public interface IConfigData
    {
        string ID { get; }
    }

    public interface NonConfig { }
}

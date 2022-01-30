
public class Singleton
{
    private static Singleton _instance = null;
    private Data _data = new Data();
    public Data Data => _data;

    private Singleton(){}

    public static Singleton GetInstance() => _instance ??= new Singleton();
}

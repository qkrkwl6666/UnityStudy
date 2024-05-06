public abstract class DataTable
{
    public static readonly string FormatPath = "Tables/{0}";
    public abstract void Load(string path);
}

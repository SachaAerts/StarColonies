namespace StarColonies.Web.Factories;

public interface IJsonContentFactory
{
    object Create(bool success, object? payload = null);
}
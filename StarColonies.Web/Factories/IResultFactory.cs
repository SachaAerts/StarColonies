using Microsoft.AspNetCore.Mvc;

namespace StarColonies.Web.Factories;

public interface IResultFactory<out T, in TO>
{
    T Create(bool success);
    T Create(bool success, string message);
    T Create(bool success, TO serializerSettings);
}
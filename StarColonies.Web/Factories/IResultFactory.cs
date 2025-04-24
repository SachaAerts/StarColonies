using Microsoft.AspNetCore.Mvc;

namespace StarColonies.Web.Factories;

public interface IResultFactory<T, TO>
{
    T Create(bool success, string message);
    T Create(bool succes, TO serializerSettings);
}
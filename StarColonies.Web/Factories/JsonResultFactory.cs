using Microsoft.AspNetCore.Mvc;

namespace StarColonies.Web.Factories;

public class JsonResultFactory : IJsonResultFactory
{
    public JsonResult Create(bool success, string message)
        => new JsonResult(new { success = success, message = message });

    public JsonResult Create(bool succes, object? serializerSettings)
        => new JsonResult(new { succes = succes, result = serializerSettings });
}
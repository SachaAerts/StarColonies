using Microsoft.AspNetCore.Mvc;

namespace StarColonies.Web.Factories;

public class JsonResultFactory : IResultFactory<JsonResult, object>
{
    public JsonResult Create(bool success, string message)
        => new (new { success, message });

    public JsonResult Create(bool succes, object? serializerSettings)
        => new (new { succes, result = serializerSettings });
}
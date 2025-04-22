using Microsoft.AspNetCore.Mvc;

namespace StarColonies.Web.Factories;

public interface IJsonResultFactory
{
    JsonResult Create(bool success, string message);
    JsonResult Create(bool succes, object? serializerSettings);
}
using Microsoft.AspNetCore.Mvc;

namespace StarColonies.Web.Factories;

public interface JsonResultFactory
{
    JsonResult Create(bool success, string message);
    JsonResult Create(bool succes);
}
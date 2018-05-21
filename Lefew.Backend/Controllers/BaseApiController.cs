using Microsoft.AspNetCore.Mvc;

namespace Lefew.RestApi.Controllers
{
    public abstract class BaseApiController : Controller
    {
        /// <summary>
        /// Retorna 204 No Content
        /// </summary>
        /// <returns></returns>
        protected IActionResult Deleted()
        {
            return NoContent();
        }

        /// <summary>
        /// Retorna 201 No Created
        /// </summary>
        /// <returns></returns>
        protected IActionResult Created<T>(string route, int id, T obj) where T : class
        {
            return CreatedAtRoute(route, new { id = id }, obj);
        }
    }
}

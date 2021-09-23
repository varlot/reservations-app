using AspNetCoreApiStarter.Bll.Itf.Bll;
using AspNetCoreApiStarter.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspNetCoreApiStarter.Controllers
{
    /// <summary>
    /// Data Controller.
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class DataController : Controller
    {
        private readonly IRoomBll roomBll;

        /// <summary>
        /// Initializes a new instance of the <see cref="DataController"/> class.
        /// </summary>
        public DataController(IRoomBll roomBll)
        {
            this.roomBll = roomBll;
        }

        /// <summary>
        /// Set data in DB.
        /// </summary>
        /// <returns>Http 200 if Ok.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> LoadData()
        {
            return await this.roomBll.LoadData() ? this.Ok() : this.BadRequest();
        }
    }
}

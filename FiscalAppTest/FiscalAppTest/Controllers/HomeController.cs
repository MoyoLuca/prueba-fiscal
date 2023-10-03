using DataModels.DbModels;
using DataModels.Services;
using DataModels.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace FiscalAppTest.Controllers
{
    public class HomeController : Controller
    {
        private readonly DatabaseConnService<Addendas> _databaseConnService;

        public HomeController(ApplicationDbContext dbContext)
        {
            _databaseConnService = new DatabaseConnService<Addendas>(dbContext);
        }

        [Produces("application/json")]
        [HttpGet]
        [Route("get/{guid}")]
        public IActionResult Getresult([FromRoute] string guid)
        {
            Addendas result = default;

            try
            {
                //convert guid into GUID object
                var guidObj = new Guid(guid);

                result = _databaseConnService.Find(x => x.IdAddenda == guidObj);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(result);
        }

        [Produces("application/json")]
        [HttpPost]
        [Route("get/list")]
        public IActionResult GetresultList([FromBody] SearchAddenda input)
        {
            List<Addendas> Addendas = default;

            try
            {

                //Usamos el método FindList para obtener una lista de registros de la base de datos
                Addendas = _databaseConnService.FindList(x => x.Usuario.Contains(input.Search) || x.NombreAddenda.Contains(input.Search)).Take(input.PageSize).ToList();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(Addendas);
        }


        [Produces("application/json")]
        [HttpPost]
        [Route("save")]
        public IActionResult SaveAddendas([FromBody] Addendas input)
        {

            try
            {
                if (input.IdAddenda is null)
                {
                    input.IdAddenda = Guid.NewGuid();
                }

                //Usamos el método Add para agregar un nuevo registro a la base de datos
                _databaseConnService.Add(input);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }

        [HttpPost]
        [Route("update")]
        public IActionResult UpdateAddendas([FromBody] Addendas input)
        {

            try
            {
                //Usamos el método Update para actualizar un registro en la base de datos
                _databaseConnService.Update(input);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }

        [Produces("application/json")]
        [HttpDelete]
        [Route("delete/{guid}")]
        public IActionResult DeleteAddendas([FromRoute] string guid)
        {

            try
            {
                //Usamos el método Delete para eliminar un registro de la base de datos
                bool deleted = _databaseConnService.Delete(guid);

                if (!deleted)
                {
                    return BadRequest("No se pudo eliminar el registro o no existe!");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }
    }
}

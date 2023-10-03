using DataModels.Services;
using DataModels.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace FiscalXmlWeb.Controllers
{
    public class FiscalController : Controller
    {
        [Produces("application/json")]
        [HttpPost]
        [Route("get")]
        public IActionResult GetPDFDocument([FromBody] XMLDocumentReq input)
        {
            // Instanciar la clase FiscalService
            FiscalService fiscalService = new FiscalService();

            // Llamar al método InvokeProcessDocument para obtener el documento PDF en base64
            string base64Pdf = fiscalService.InvokeProcessDocument(input.XMLString);

            if (base64Pdf != null)
            {
                try
                {
                    // Convertir el resultado base64 en un arreglo de bytes
                    byte[] pdfBytes = fiscalService.ConvertResponsetoBytes(base64Pdf);

                    // Devolver el arreglo de bytes como una respuesta JSON
                    return Ok(new { pdfBytes });
                }
                catch (Exception ex)
                {
                    // Manejar cualquier excepción que pueda ocurrir al convertir el PDF
                    return BadRequest($"Error al convertir el PDF: {ex.Message}");
                }
            }
            else
            {
                return BadRequest("Error al invocar el servicio web para obtener el PDF");
            }
        }
    }
}

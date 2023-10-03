using System.Data.SqlTypes;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using DataModels.ViewModels;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Newtonsoft.Json;

namespace DataModels.Services
{
    public class FiscalService
    {
        public string InvokeProcessDocument(string XML)
        {
            // URL del servicio web
            string serviceUrl = "https://int.certifac.mx:9006/";

            var xmlData = GetBase64EncodedXml(XML);

            try
            {
                // Crear una solicitud HTTP POST
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(serviceUrl);
                request.Method = "POST";
                request.ContentType = "text/xml; charset=utf-8"; // Establecer el tipo de contenido correcto

                // Crear el cuerpo de la solicitud con los datos XML
                string xmlRequestBody = $@"
            <soapenv:Envelope xmlns:soapenv='http://schemas.xmlsoap.org/soap/envelope/' xmlns:int='http://int.certifac.mx'>
                <soapenv:Header/>
                <soapenv:Body>
                    <int:ProcessDocument>
                        <int:cfdi64>{xmlData}</int:cfdi64>
                        <int:cfdiState>EMIT</int:cfdiState>
                        <int:isFree>false</int:isFree>
                        <int:logo64></int:logo64>
                        <int:informacionAdicional xsi:nil='true' xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance'/>
                        <int:infoNofiscal></int:infoNofiscal>
                    </int:ProcessDocument>
                </soapenv:Body>
            </soapenv:Envelope>";

                // Convertir el cuerpo de la solicitud en un arreglo de bytes
                byte[] xmlBytes = Encoding.UTF8.GetBytes(xmlRequestBody);

                // Escribir los datos XML en el cuerpo de la solicitud
                using (Stream requestStream = request.GetRequestStream())
                {
                    requestStream.Write(xmlBytes, 0, xmlBytes.Length);
                    requestStream.Flush();
                }

                // Obtener la respuesta del servicio web
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        // Leer la respuesta del servicio web
                        using (StreamReader streamReader = new StreamReader(response.GetResponseStream()))
                        {
                            string base64Pdf = streamReader.ReadToEnd();
                            return base64Pdf;
                        }
                    }
                    else
                    {
                        throw new Exception($"Error en la respuesta del servicio: {response.StatusCode}");
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción que pueda ocurrir
                Console.WriteLine($"Error al invocar el servicio web: {ex.Message}");
                return null;
            }
        }

        // Método para obtener el XML codificado en base64 (debes proporcionar tu propio XML)
        private string GetBase64EncodedXml(string XML)
        {
            //Convert XML string to base64
            var xmlBytes = Encoding.UTF8.GetBytes(XML);
            string base64Xml = Convert.ToBase64String(xmlBytes);
            return base64Xml;
        }

        public byte[] ConvertResponsetoBytes(string responseBase64)
        {
            // Convertir la cadena base64 en un arreglo de bytes
            byte[] result = Convert.FromBase64String(responseBase64);
            return result;
        }
    }
}

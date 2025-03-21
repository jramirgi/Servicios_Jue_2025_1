using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Management;

namespace Servicios_Jue.Clases
{
    public class clsUpload
    {
        public string Datos { get; set; }
        public string Proceso { get; set; }
        public HttpRequestMessage request { get; set; }
        private List<string> Archivos;
        public async Task<HttpResponseMessage> GrabarArchivo()
        {
            if (!request.Content.IsMimeMultipartContent())
            {
                return request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, "No se envió un archivo para procesar");
            }
            string root = HttpContext.Current.Server.MapPath("~/Archivos");
            var provider = new MultipartFormDataStreamProvider(root);
            try
            {
                //Lee el contenido de los archivos
                await request.Content.ReadAsMultipartAsync(provider);
                if (provider.FileData.Count > 0)
                {
                    Archivos = new List<string>();
                    foreach (MultipartFileData file in provider.FileData)
                    {
                        string fileName = file.Headers.ContentDisposition.FileName;
                        if (fileName.StartsWith("\"") && fileName.EndsWith("\""))
                        {
                            fileName = fileName.Trim('"');
                        }
                        if (fileName.Contains(@"/") || fileName.Contains(@"\"))
                        {
                            fileName = Path.GetFileName(fileName);
                        }
                        if (File.Exists(Path.Combine(root, fileName)))
                        {
                            //El archivo ya existe en el servidor, no se va a cargar, se va a eliminar el temporal y se devolverá un error
                            File.Delete(Path.Combine(root, file.LocalFileName));
                            return request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, "El archivo ya existe");
                        }
                        //Agrego en una lista el nombre de los archivos que se cargaron 
                        Archivos.Add(fileName);
                        //Renombra el archivo temporal
                        File.Move(file.LocalFileName, Path.Combine(root, fileName));
                    }
                    //Se genera el proceso de gestión en la base de datos
                    string RptaBD = ProcesarBD();
                    //Termina el ciclo, responde que se cargó el archivo correctamente
                    return request.CreateResponse(System.Net.HttpStatusCode.OK, "Se cargaron los archivos en el servidor, " + RptaBD);
                }
                else
                {
                    return request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, "No se envió un archivo para procesar");
                }
            }
            catch (Exception ex)
            {
                return request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        private string ProcesarBD()
        {
            switch(Proceso.ToUpper())
            {
                case "PRODUCTO":
                    clsProducto producto = new clsProducto();
                    return producto.GrabarImagenProducto(Convert.ToInt32(Datos), Archivos);
                default:
                    return "No se ha definido el proceso en la base de datos";
            }
        }
    }
}
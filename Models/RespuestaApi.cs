using System.Net;

namespace PropiedadesMinimalApi.Models
{
  
    public class RespuestaApi
    {
        public bool Success { get; set; }
        public object Resultado { get; set; }
        public HttpStatusCode Codigo_Estado { get; set; }
        public List<string> Errores { get; set; }


        public RespuestaApi()
        {
            Errores = new List<string>();
        }
    }
}

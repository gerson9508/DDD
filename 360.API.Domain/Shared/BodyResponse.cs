namespace _360.API.Domain.Shared
{
   public class SZGResponse
   {
      public string Response { get; set; } = "Ocurrió un error inesperado.";
      public int Status { get; set; } = 500;
      public object? Data { get; set; }

      //public string TraceId { get; set; } = new Guid().ToString();
      //[System.ComponentModel.DataAnnotations.Required]
      //public string Type { get; set; } = "https://tools.ietf.org/html/rfc7231#section-6.6.1";
   }
}

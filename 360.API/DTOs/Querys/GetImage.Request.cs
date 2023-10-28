namespace _360.API.DTOs.Querys
{
   public class ImageRequest
   {
      public int? SensorId { get; set; }
      public int? ClasificationId { get; set; }
      public int? Year { get; set; }
   }

   public class ImageTIFFRequest
   {
      [System.ComponentModel.DataAnnotations.Required]
      public int BatchFileId { get; set; }
   }
}

namespace proyecto2.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Usuario
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        [Required]
        [StringLength(100)]
        public string Apellido { get; set; }

        [Required]
        [Phone]
        public string Telefono { get; set; }

        [Required]
        [Range(18, 120)]
        public int Edad { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Contraseña { get; set; }
    }

}
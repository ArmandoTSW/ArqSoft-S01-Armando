using System.ComponentModel.DataAnnotations;

namespace Catalogo.Models
{
    public class Item
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El título es obligatorio")]
        [Display(Name = "Título")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "Debes especificar un género")]
        [Display(Name = "Género")]
        public string Genero { get; set; }
        //holaSS
        [Range(1950, 2030, ErrorMessage = "El año debe estar entre 1950 y 2030")]
        [Display(Name = "Año de Lanzamiento")]
        public int Ano { get; set; }

        [Required(ErrorMessage = "La consola es obligatoria")]
        [Display(Name = "Plataforma / Consola")]
        public string Consola { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Descripción del Juego")]
        public string? Descripcion { get; set; }
    }
}
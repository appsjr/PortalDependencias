using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Pendencias.Models
{
    public class DTOPendencia
    {
        public Guid ID { get; set; }

        [Required]
        public String Titulo { get; set; }

        [Required]
        [StringLength(1000, ErrorMessage = "A quantidade máxima de caracteres é 1000.")]
        public String Descricao { get; set; }

        public StatusDaPendencia Status { get; set; }

        [Required]
        [Display(Name = "Responsável")]
        public String Responsavel { get; set; }

        [Display(Name = "Observações")]
        [StringLength(1000, ErrorMessage = "A quantidade máxima de caracteres é 1000.")]
        public String Obs { get; set; }

        [Display(Name = "Data de criação")]
        public DateTime Data { get; set; }

        [Display(Name = "Categoria")]
        public int CategoriaID { get; set; }
    }
}
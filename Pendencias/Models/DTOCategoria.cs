using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Pendencias.Models
{
    [Table("DTOCategorias")]
    public class DTOCategoria
    {
        [Required]
        [Display(Name = "Descrição")]
        public String Descricao { get; set; }

        [Key]
        [Display(Name = "Código")]
        public int Codigo { get; set; }
    }
}
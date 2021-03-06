//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Security;

namespace MMC2.Models
{
    using System;
    using System.Collections.Generic;

    public partial class Historico
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Horas Trabalhadas")]
        public double QtdHoras { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Data lançamento")]
        public System.DateTime DataLancamento { get; set; }

        [Required]
        [Display(Name = "Descrição da atividade")]
        [DataType(DataType.MultilineText)]
        public string Descrição { get; set; }

        [Required]
        [Display(Name = "Tarefa requerinte")]
        public int Tarefa_Id { get; set; }

        [Required]
        [Display(Name = "Executor da atividade")]
        public int Usuario_Id { get; set; }

        public virtual Tarefa Tarefa { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}

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
    
    public partial class Cliente
    {
        public Cliente()
        {
            this.Enderecos = new HashSet<Endereco>();
            this.Projetos = new HashSet<Projeto>();
        }
    
        public int Id { get; set; }

        [Required]
        [Display(Name = "Nome de identifica��o")]
        public string Nome { get; set; }

        [Required]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        
        [Display(Name = "Site do Cliente")]
        public string Site { get; set; }

        [Required]
        [Display(Name = "CNPJ")]
        public string Cnpj { get; set; }

        [Required]
        [Display(Name = "Data de criacao")]
        [DataType(DataType.Date)]
        public System.DateTime DataHora { get; set; }

        [Required]
        [Display(Name = "Ativo")]
        public bool Ativo { get; set; }
    
        public virtual ICollection<Endereco> Enderecos { get; set; }
        public virtual ICollection<Projeto> Projetos { get; set; }
    }
}
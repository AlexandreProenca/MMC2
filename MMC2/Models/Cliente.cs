//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MMC2.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Cliente
    {
        public Cliente()
        {
            this.Projetos = new HashSet<Projeto>();
        }
    
        public int Id { get; set; }
        public string Nome { get; set; }
        public Nullable<int> Endereco_Id { get; set; }
        public string Email { get; set; }
        public string Site { get; set; }
        public string Cnpj { get; set; }
        public System.DateTime DataHora { get; set; }
        public bool Ativo { get; set; }
    
        public virtual Endereco Endereco { get; set; }
        public virtual ICollection<Projeto> Projetos { get; set; }
    }
}

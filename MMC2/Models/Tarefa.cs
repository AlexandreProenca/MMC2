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
    
    public partial class Tarefa
    {
        public Tarefa()
        {
            this.Historicos = new HashSet<Historico>();
        }
    
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public Nullable<System.DateTime> DataInicio { get; set; }
        public Nullable<System.DateTime> DataFinal { get; set; }
        public int Porcentagem { get; set; }
        public int Status_Id { get; set; }
        public int Projeto_Id { get; set; }
        public string Prioridade { get; set; }
        public int Usuario_Id { get; set; }
        public Nullable<double> TempoEstimado { get; set; }
    
        public virtual ICollection<Historico> Historicos { get; set; }
        public virtual Projeto Projeto { get; set; }
        public virtual Status Status { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
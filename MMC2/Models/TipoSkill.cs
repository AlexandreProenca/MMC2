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
    
    public partial class TipoSkill
    {
        public TipoSkill()
        {
            this.Skills = new HashSet<Skill>();
            this.Tarefas = new HashSet<Tarefa>();
        }
    
        public int Id { get; set; }
        public string Nome { get; set; }
    
        public virtual ICollection<Skill> Skills { get; set; }
        public virtual ICollection<Tarefa> Tarefas { get; set; }
    }
}

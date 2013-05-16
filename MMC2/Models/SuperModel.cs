using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MMC2.Models
{
    public class SuperModel
    {
        public Cliente Cliente { get; set; }
        public IEnumerable<MMC2.Models.Endereco> Endereco { get; set; }
    }
}
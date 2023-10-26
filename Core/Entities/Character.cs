using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Character : BaseEntity
    {
        public string Name { get; set; }
        public decimal Hp { get; set; }
        public decimal Ad { get; set; }
        public decimal Ap { get; set; }
        public decimal HpGain { get; set; }
    }
}

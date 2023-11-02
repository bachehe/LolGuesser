using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Character : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        public string PictureUrl { get; set; }
        public decimal Hp { get; set; }
        public decimal HpGain { get; set; }
        public decimal Mana { get; set; }
        public decimal ManaGain { get; set; }
        public decimal Ad { get; set; }
        public decimal As { get; set; }
        public decimal Armor { get; set; }
        public decimal ArmorGain { get; set; }
        public decimal Mr { get; set; }
        public decimal MS { get; set; }
        public decimal Range { get; set; }

        public Character()
        {

        }
    }
}

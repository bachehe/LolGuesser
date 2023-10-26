namespace API.DTO
{
    public class CharacterDto
    {
        public string Name { get; set; } = string.Empty;
        public decimal Hp { get; set; }
        public decimal Ad { get; set; }
        public decimal Ap { get; set; }
        public decimal HpGain { get; set; }  
    }
}

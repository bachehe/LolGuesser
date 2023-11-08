namespace API.DTO
{
    public class ChampionItemDto
    {
        public string Item { get; set; }    
        public string ItemPictureUrl { get; set; }
        public IEnumerable<object> Character { get; set; }
    }
}

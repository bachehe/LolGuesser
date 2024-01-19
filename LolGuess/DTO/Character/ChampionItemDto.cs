namespace API.DTO.Character
{
    public class ChampionItemDto
    {
        public IEnumerable<string> Item { get; set; }
        public IEnumerable<string> ItemPictureUrl { get; set; }
        public IEnumerable<object> Character { get; set; }
    }
}

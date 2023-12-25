namespace erbildaphneAPI.Entity.Entities
{
    public class Write : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public string PictureUrl { get; set; }
        public int AuthorId { get; set; }
        public bool IsPublished { get; set; } = false;
        public bool IsBoosted { get; set; } = false;
        public bool IsChosen { get; set; } = false;
        public virtual Author Author { get; set; }







    }
}

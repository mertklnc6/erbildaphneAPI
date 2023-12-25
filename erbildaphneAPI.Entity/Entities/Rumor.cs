namespace erbildaphneAPI.Entity.Entities
{
    public class Rumor : BaseEntity
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string Content { get; set; }

        public string PictureUrl { get; set; }

        public bool IsPublished { get; set; } = false;

    }
}

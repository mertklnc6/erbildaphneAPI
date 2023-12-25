namespace erbildaphneAPI.Entity.Entities
{
    public class Author : BaseEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }
        public string Bio { get; set; }

        public string Instagram { get; set; }
        public string Website { get; set; }
        public string Linkedin { get; set; }
        public string Facebook { get; set; }

        public string PictureUrl { get; set; }


        public virtual ICollection<Write> Writes { get; set; }


    }
}

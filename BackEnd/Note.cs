using System.Data.Linq.Mapping;

namespace BackEnd
{
    [Table]
    public class Note
    {
        [Column(IsDbGenerated = true, IsPrimaryKey = true)]
        public int Id { get; set; }
        [Column]
        public string Title { get; set; }
        [Column]
        public string Content { get; set; }

        public override string ToString()
        {
            return Title;
        }
    }
}

using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Todo
    {
        public int Id { get; set; }
        public string CreatorId { get; set; }

        [Required]
        public string Title { get; set; }

        public bool? Completed { get; set; }

    }
}
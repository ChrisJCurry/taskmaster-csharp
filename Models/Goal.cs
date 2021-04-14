namespace Models
{
    public class Goal
    {
        public int Id { get; set; }

        public int TodoId { get; set; }

        public string Description { get; set; }

        public string CreatorId { get; set; }

        public bool Completed { get; set; }
    }

    public class TodoGoal : Goal
    {
        public Todo Todo { get; set; }
    }
}
namespace TaskAndUsers.Models
{
    public class TaskUsersModel
    {
        public int Id { get; set; }
        public string TaskName { get; set; }
        public string Description { get; set; }
        public string Status {  get; set; }
        public string UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        



    }
}

namespace MeuToDo.Models
{
    public class ToDo
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public bool Done { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;

    }
}
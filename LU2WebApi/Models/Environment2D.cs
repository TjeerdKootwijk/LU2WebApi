namespace LU2WebApi.Models
{
    public class Environment2D
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public float MaxHeight { get; set; }
        public float MaxWidth { get; set; }


        //public int UserId { get; set; }
        //public User User { get; set; }
    }

    public class Environment2DDTO
    {
        public string Name { get; set; }
        public float MaxHeight { get; set; }
        public float MaxWidth { get; set; }

        //public int UserId { get; set; }
        //public User User { get; set; }
    }
}

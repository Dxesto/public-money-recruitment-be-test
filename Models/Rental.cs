using Models.Interfaces;

namespace Models
{
    public class Rental : IEntity
    {
        public int Id { get; set; }

        public int Units { get; set; }
    }
}

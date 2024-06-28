namespace Api_intro__Migrations__Crud.Models
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public bool SoftDelete { get; set; } = false;
    }
}

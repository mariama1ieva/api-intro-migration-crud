namespace Api_intro__Migrations__Crud.Models
{
    public class Country : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<City> Cities { get; set; }
    }
}

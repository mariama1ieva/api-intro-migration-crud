namespace Api_intro__Migrations__Crud.Models
{
    public class City : BaseEntity
    {
        public string Name { get; set; }
        public int CountryId { get; set; }
    }
}

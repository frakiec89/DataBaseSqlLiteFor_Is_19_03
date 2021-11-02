using System.ComponentModel.DataAnnotations;

namespace DataBaseSqlLiteFor_Is_19_03.DB.Model
{
    public class Company
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return $"ID:{Id}, Название:{Name}";
        }
    }
}

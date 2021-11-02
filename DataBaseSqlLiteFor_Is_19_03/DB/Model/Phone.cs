using System.ComponentModel.DataAnnotations;


namespace DataBaseSqlLiteFor_Is_19_03.DB.Model
{
   public   class Phone
   {

        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public int Price { get; set; }

        public int CompanyId { get; set; }
        public Company Company { get; set; }

        public override string ToString()
        {
            return $"{Title}";
        }

    }
}

namespace CountryDataAccess.Entities
{
    using CountryDataAccess.Constants;
    using Microsoft.EntityFrameworkCore;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// 
    /// </summary>
    [Table(TableConstants.Country)]
    [Index(nameof(Name), IsUnique = true)]
    public class Country
    {
        /// <summary> Идентификатор. </summary>
        [Key]
        public long Id { get; set; }

        /// <summary> Наименование. </summary>
        [Required]
        public required string Name { get; set; }

        public ICollection<Province> Provinces { get; set; }
    }
}

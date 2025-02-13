namespace CountryDataAccess.Entities
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using CountryDataAccess.Constants;
    using Microsoft.EntityFrameworkCore;

    /// <summary> Узел. </summary>
    [Table(TableConstants.Province)]
    [Index(nameof(CountryId), nameof(Name), IsUnique = true)]
    public class Province
    {

        /// <summary> Идентификатор. </summary>
        [Key]
        public long Id { get; set; }

        /// <summary> Идентификатор процесса. </summary>
        [Required]
        [Column("country_id")]
        [ForeignKey(nameof(Country))]
        public long CountryId { get; set; }

        /// <summary> Рабочий процесс. </summary>
        public required Country Country { get; set; } = null!;

        /// <summary> Наименование. </summary>
        [Required]
        public required string Name { get; set; }
    }
}

using System;
using System.ComponentModel.DataAnnotations;

namespace AA.Crud.Domain
{
    public class FoodDescription
    {
        [Key]
        public string Number { get; set; }

        [StringLength(4)]
        public string Group { get; set; }

        [StringLength(60)]
        public string Name { get; set; }

        [StringLength(200)]
        public string Description { get; set; }

        public decimal? ProteinFactor { get; set; }

        public decimal? CarbFactor { get; set; }

        public decimal? FatFactor { get; set; }

        public DateTimeOffset SomeDate { get; set; }
    }
}

namespace Tracking
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("login")]
    public partial class login
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int no { get; set; }

        [Key]
        [StringLength(50)]
        public string email { get; set; }

        [Required]
        [StringLength(500)]
        public string password { get; set; }

        [Required]
        [StringLength(15)]
        public string phone { get; set; }

        public DateTime date_joined { get; set; }

        [Required]
        [StringLength(50)]
        public string login_role { get; set; }

        [Required]
        [StringLength(15)]
        public string online { get; set; }
    }
}

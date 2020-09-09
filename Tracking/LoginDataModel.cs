namespace Tracking
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class LoginDataModel : DbContext
    {
        public LoginDataModel()
            : base("name=LoginDataModel")
        {
        }

        public virtual DbSet<login> logins { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<login>()
                .Property(e => e.email)
                .IsUnicode(false);

            modelBuilder.Entity<login>()
                .Property(e => e.password)
                .IsUnicode(false);

            modelBuilder.Entity<login>()
                .Property(e => e.phone)
                .IsUnicode(false);

            modelBuilder.Entity<login>()
                .Property(e => e.login_role)
                .IsUnicode(false);

            modelBuilder.Entity<login>()
                .Property(e => e.online)
                .IsUnicode(false);
        }
    }
}

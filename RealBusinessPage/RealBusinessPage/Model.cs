namespace RealBusinessPage
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model : DbContext
    {
        public Model()
            : base("name=LibModel")
        {
        }

        public virtual DbSet<AUTHOR> AUTHORs { get; set; }
        public virtual DbSet<ACCOUNT> ACCOUNTs { get; set; }
        public virtual DbSet<BOOK> BOOKs { get; set; }
        public virtual DbSet<BORROW> BORROWs { get; set; }
        public virtual DbSet<BORROWER> BORROWERs { get; set; }
        public virtual DbSet<CATEGORY> CATEGORies { get; set; }
        public virtual DbSet<CLASSIFICATION> CLASSIFICATIONs { get; set; }
        public virtual DbSet<COPY> COPies { get; set; }
        public virtual DbSet<STATUS> STATUS { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AUTHOR>()
                .HasMany(e => e.BOOKs)
                .WithMany(e => e.AUTHORs)
                .Map(m => m.ToTable("BOOK_AUTHOR").MapLeftKey("Aid").MapRightKey("ISBN"));

            modelBuilder.Entity<BORROWER>()
                .HasMany(e => e.BORROWs)
                .WithRequired(e => e.BORROWER)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CATEGORY>()
                .HasMany(e => e.BORROWERs)
                .WithOptional(e => e.CATEGORY)
                .HasForeignKey(e => e.CategoryId);

            modelBuilder.Entity<COPY>()
                .HasMany(e => e.BORROWs)
                .WithRequired(e => e.COPY)
                .WillCascadeOnDelete(false);
        }
    }
}

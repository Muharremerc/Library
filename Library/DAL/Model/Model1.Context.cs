﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL.Model
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class LibEntities1 : DbContext
    {
        private LibEntities1()
            : base("name=LibEntities1")
        {
        }

        private static LibEntities1 db;
        public static LibEntities1 getDBEntity()
        {
            if (db == null)
                db = new LibEntities1();
            return db;

        }



        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Author> Authors { get; set; }
        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<BookLocation> BookLocations { get; set; }
        public virtual DbSet<Library> Libraries { get; set; }
        public virtual DbSet<Renter> Renters { get; set; }
        public virtual DbSet<RenterBook> RenterBooks { get; set; }
    }
}

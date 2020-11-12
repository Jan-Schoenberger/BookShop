using System;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace BookShoppingNeu
{

    public class BookshoppingContext : DbContext
    {
        // Der Kontext wurde für die Verwendung einer Model1-Verbindungszeichenfolge aus der 
        // Konfigurationsdatei ('App.config' oder 'Web.config') der Anwendung konfiguriert. Diese Verbindungszeichenfolge hat standardmäßig die 
        // Datenbank 'BookShoopingAuftragNeu._Model.Model1' auf der LocalDb-Instanz als Ziel. 
        // 
        // Wenn Sie eine andere Datenbank und/oder einen anderen Anbieter als Ziel verwenden möchten, ändern Sie die Model1-Zeichenfolge 
        // in der Anwendungskonfigurationsdatei.




        // Fügen Sie ein 'DbSet' für jeden Entitätstyp hinzu, den Sie in das Modell einschließen möchten. Weitere Informationen 
        // zum Konfigurieren und Verwenden eines Code First-Modells finden Sie unter 'http://go.microsoft.com/fwlink/?LinkId=390109'.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Catagory> Categories { get; set; }
        public DbSet<Format> Formats { get; set; }
         public DbSet<History> Histories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite("Data source = BookShopping.db ");
        }

       


    }

}


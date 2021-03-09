using System.ComponentModel.DataAnnotations;

namespace BookShoppingNeu
{
    // Format Entity
    // Format Entity
    public class Format
    {
       [Key]
        public int Id { get; set; }
        public string FormatArt { get; set; }
       
        //[ForeignKey("BuchID")]
        //public int BuchID { get; set; }

        //public Buch Buch { get; set; }

        //public override string ToString()
        //{
        //    return "Buch Format :" + FormatArt;
        //}
    }
}
// Test Kommentar.

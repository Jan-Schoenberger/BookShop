using System.ComponentModel.DataAnnotations;

namespace BookShoppingNeu
{
    // Format Entity
    public class Format
    {
       
        public int Id { get; set; }
        public string FormatType { get; set; }
       
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

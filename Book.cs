using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace BookShoppingNeu
{
    // Buch Entity 
    public class Book
    {
        public int BookID { get; set; }
        public string Author { get; set; }
        public string Title { get; set; }
        public long EAN  {get; set; }
        public string Publisher { get; set; }
        public DateTime Date { get; set; }
        public string Price { get; set; }
        public string FormatArt{ get; set;}



        public virtual Person Person { get;  set; }
        public string FormatArt { get; set; }
        public virtual Format Format { get; set; }
        public string KatagorieArt { get; set; }
        public virtual Katagorie Katagorie { get; set; }

        public Person PersonK { get; set; }
        public Format FormatK { get; set; }
        public Category CategoryK { get; set; }

        
        //public override string ToString()
        //{
        //    return "Buch Author :" + Author + "Buch Titel :" + Title +
        //           "Buch EAN :" + EAN + "Buch Publisher :" + Publisher + "Datum :" + Date +
        //            "Price :" + Price;
        //}
    
}


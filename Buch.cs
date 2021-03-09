using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace BookShoppingNeu
{
  // Buch Entity 
    public class Buch
    {
        [SQLite.Unique]
        public int Id { get; set; }
        public string Author { get; set; }
        public string Title { get; set; }
        public string EAN { get; set; }
        public string Publisher { get; set; }
        public string Date { get; set; }
        public string Price { get; set; }

        // das ist Navigation
        //public virtual Person? Person { get;  set; }
        ////public int? PersonId { get; set; }
        //public virtual string FormatArt { get; set; }
        ////[ForeignKey("Format")]

      //  public virtual Format? Format { get; set; }

       public  int? FormatId { get; set; }
        ////[ForeignKey("Katagorie")]
        //public virtual Katagorie? Katagorie { get; set; }
        //public string KatagorieArt { get; internal set; }
        //public string FormatArt { get; internal set; }
        public int? KatagorieId { get; set; }
        //public string KatagorieArt { get; set; }




       public override string ToString()
        {
           return "Buch Author :" + Author + '\n' + "Buch Titel :" + Title + '\n' +
                   "Buch EAN :" + EAN + '\n' + "Buch Publisher :" + Publisher + '\n' + "Datum :" + Date +
                   '\n' + "Price :" + Price;





















        }
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace BookShoppingNeu
{
    // Buch Entity 
    public class Buch
    {
        public int BuchID { get; set; }
        public string Author { get; set; }
        public string Title { get; set; }
        public string EAN { get; set; }
        public string Publisher { get; set; }
        public string Date { get; set; }
        public string Price { get; set; }
        

        public Person PersonK { get; set; }
        public Format FormatK { get; set; }
        public Katagorie KatagorieK { get; set; }
        
        //public override string ToString()
        //{
        //    return "Buch Author :" + Author + "Buch Titel :" + Title +
        //           "Buch EAN :" + EAN + "Buch Publisher :" + Publisher + "Datum :" + Date +
        //            "Price :" + Price;
        //}
    }
}


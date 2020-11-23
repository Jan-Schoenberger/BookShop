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
        


        public virtual Person? Person { get;  set; }
        public string FormatArt { get; set; }
        public virtual Format? Format { get; set; }
        public string KatagorieArt { get; set; }
        public virtual Katagorie? Katagorie { get; set; }
        
        //public override string ToString()
        //{
        //    return "Buch Author :" + Author + "Buch Titel :" + Title +
        //           "Buch EAN :" + EAN + "Buch Publisher :" + Publisher + "Datum :" + Date +
        //            "Price :" + Price;
        //}
    
}


using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace BookShoppingNeu
{
    // Buch Entity 
    public class Book
    {
        public int Id { get; set; }
        public string Author { get; set; }
        public string Title { get; set; }
        public string EAN { get; set; }
        public string Publisher { get; set; }
        public string Date { get; set; }
        public string Price { get; set; }
        


        
       
        public virtual int? FormatId { get; set; }
       public int? CategorieId { get; set; }
        
      public override string ToString()
        {
           return "Buch Author :" + Author + '\t' + "Buch Titel :" + Title + '\t' +
                   "Buch EAN :" + EAN + '\t' + "Buch Publisher :" + Publisher + '\t' + "Datum :" + Date +
                   '\t' + "Price :" + Price;}
    
}
}

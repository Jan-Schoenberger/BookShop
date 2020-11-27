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
           return "Buch Author :" + Author + '\n' + "Buch Titel :" + Title + '\n' +
                   "Buch EAN :" + EAN + '\n' + "Buch Publisher :" + Publisher + '\n' + "Datum :" + Date +
                   '\n' + "Price :" + Price;}
    
}
}

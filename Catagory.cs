﻿namespace BookShoppingNeu
{
    // Catagorie Entity 
    public class Catagory
    {

        public int Id { get; set; }
        public string CategorieType { get; set; }
        //[ForeignKey("BuchID")]
        //public int BuchID { get; set; }

        //public Buch Buch { get; set; }

        //public override string ToString()
        //{
        //    return "Buch Catagorie :" + KatagorieArt;
        //}
    }
}


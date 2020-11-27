namespace BookShoppingNeu
{
    public class History
    {
        public int HistoryID { get; set; }
        public string BuchKaufen { get; set; }
        public string MoechteKaufen { get; set; }
        

        public Book Buch { get; set; }
        public Person Person { get; set; }


    }
}
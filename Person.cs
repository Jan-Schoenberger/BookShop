using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookShoppingNeu
{
    // Person Entity
    
    public class Person
    {
       
       
        public int Id { get; set; }
        public string PersonGender { get; set; }
        public string PersonLastName { get; set; }
        public string PersonFirstname { get; set; }
        public string PersonStreet { get; set; }
        public string PersonPLZ { get; set; }
        public string PersonCity { get; set; }
        public string PersonEmail { get; set; }
        public string PersonUser { get; set; }
        public string PersonPasswort { get; set; }
        public string PersonBirthay { get; set; }
       
    
       
         public List<Book> books { get; set; }



        //public int BuchId { get; set; }

       public override string ToString()
        {
            return  PersonGender + "," + PersonFirstname + 
                    "," + PersonLastName + "," + PersonStreet + "," + PersonPLZ + "," +  PersonCity + "," + PersonEmail +
                 "," + PersonUser + "," + PersonPasswort + "," + PersonBirthay + '\n' ;
        }
    }
}


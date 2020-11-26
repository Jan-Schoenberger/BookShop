using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookShoppingNeu
{
    // Person Entity
    
    public class Person
    {
       
       
        public int PersonId { get; set; }
        public string PersonGender { get; set; }
        public string PersonName { get; set; }
        public string PersonVorname { get; set; }
        public string PersonStraße { get; set; }
        public string PersonPLZ { get; set; }
        public string PersonStadt { get; set; }
        public string PersonEmail { get; set; }
        public string PersonUser { get; set; }
        public string PersonPasswort { get; set; }
        public string PersonBirthay { get; set; }
       
    
       
        public List<Book> Books { get; set; }



        //public int BuchId { get; set; }

        //public override string ToString()
        //{
        //    return "Gender :" + PersonGender + "Name :" + PersonName +
        //            "Vorname :" + PersonVorname + "Adresse : " + PersonPLZ + PersonStadt + PersonStraße +
        //            "Geburtsdatum :" + PersonBirthay + "Zugangsdaten :" + PersonUser + PersonEmail + PersonPasswort;
        //}
    }
}


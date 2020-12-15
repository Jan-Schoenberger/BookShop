using BookShoppingNeu;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.IO;

namespace BookShoppingNeu
{ class Program : Book
    {
        public int pruf;
        public int hilf;
        public int counter;
        public int cnt;
        public int meinKat;
        public int meinFor;
        public string cate;
        public string forma;
        private string author, title, ean, publisher, date, price;
        private string currPersonemail;
        private string currPersonPasswort;
        private static void Main()
        {
            Program prog = new Program();
            prog.DateiAuslesen();
            prog.ShowData();
            prog.DataEingeben();
            // prog.Createdata();



        }
        private void Createdata()
        {



        }
        private void ShowData()
        {
            using (var db = new BookshoppingContext())
            {
                Console.WriteLine("\n----- Anzahl der Mitglieder -----");

                Console.WriteLine($" - {db.Persons.Count()} Mitglieder");
                Console.WriteLine("-----------------------------------");
                Console.WriteLine();
                Console.WriteLine("\n------- Anzahl der Buecher -------");
                Console.WriteLine($" - {db.Books.Count()} Buecher in unsere Bibliothek");
                Console.WriteLine("------------------------------------");
                Console.WriteLine();

                Console.WriteLine("\n------- Anzahl der Category -------");
                Console.WriteLine($" - {db.Categories.Count()} Category in unsere Bibliothek");
                Console.WriteLine("------------------------------------");
                Console.WriteLine();


                /*
                db.Persons.ToArray();
                var query = db.Persons ;
                foreach (var Per in query)
                {
                    Console.WriteLine(Per); // uses blog.ToString()

                    foreach (Buch buch in Per.buecher)
                    {
                        Console.WriteLine("  " + buch); // uses post.ToString()
                    }
                } 
                */
            }
        }
        public void DataEingeben()
        {
            Program prog = new Program();


            Console.WriteLine(" ****Buchladen****");
            Console.WriteLine();
            Console.WriteLine(" Möchten Sie registirieren oder sich anmelden?");
            Console.WriteLine();

            Console.Write(" Registrierung (R), Anmeldung (A)");
            string eing = Console.ReadLine();
            eing.ToLower();
          
            if (eing == "r")
            {
                Console.WriteLine(" -------Registrierung-------");
                Console.Write(" Gender M/F : ");
               string  gender = Console.ReadLine();

                Console.Write(" Vorname : ");
                string vorname = Console.ReadLine();
                

                Console.Write(" Nachname : ");
                string nachname = Console.ReadLine();

                Console.Write(" Straße : ");
                string straße = Console.ReadLine();

                Console.Write(" PLZ : ");
                string plz = Console.ReadLine();

                Console.Write(" Stadt : ");
                string stadt = Console.ReadLine();

                Console.Write(" Email : ");
                string email = Console.ReadLine();

                Console.Write(" Benutzername : ");
                string benutzername = Console.ReadLine();

                Console.Write(" Passwort : ");
                string passwort = Console.ReadLine();

                Console.Write(" Geburtsdatum : ");
                string Gdatum = Console.ReadLine();


                // fügen wir die neue Mitglieder zu unsere Daten bank ein 
              NeuPersonhinzufuegen(gender, nachname, vorname, straße, plz, stadt, email, benutzername, passwort, Gdatum);

            }
            else
            {
                Console.WriteLine();
                Console.WriteLine(" -------Anmeldung-------");
                Console.WriteLine();
                Console.Write(" Email : ");
                string email = Console.ReadLine();
                Console.Write(" Benutzername : ");
                string benutzer = Console.ReadLine();
                Console.Write(" Passwort : ");
                string passwort = Console.ReadLine();
                bool bo = DatenPruefen(email, benutzer, passwort);
                // prüfen wir, ob die Daten richtig sind

            }

        }
        public void DateiAuslesen()
        {

            using (var db = new BookshoppingContext())
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
                Person per = new Person();
                //List<Person> per;
                string line;
                //Format currFormat = null;

                System.IO.StreamReader file = new System.IO.StreamReader(@"C:\tmp\fake-persons.txt");
                System.IO.StreamReader file2 = new System.IO.StreamReader(@"C:\tmp\spiegel-bestseller.txt");
                while ((line = file.ReadLine()) != null)
                {
                    string[] zerlegendeLine = line.Split(",".ToCharArray());
                    //foreach (string s in zerlegendeLine)
                    //{
                    //    Console.WriteLine(s);
                    //}
                    for (int i = 1; i < zerlegendeLine.Length; i++)
                    {
                        db.Add(new Person
                        {
                            // PersonId = i,
                            PersonGender = zerlegendeLine[i],
                            PersonLastName  = zerlegendeLine[i++],
                            PersonFirstname = zerlegendeLine[i++],
                            PersonStreet  = zerlegendeLine[i++],
                            PersonPLZ = zerlegendeLine[i++],
                            PersonCity = zerlegendeLine[i++],
                            PersonEmail = zerlegendeLine[i++],
                            PersonUser = zerlegendeLine[i++],
                            PersonPasswort = zerlegendeLine[i++],
                            PersonBirthay = zerlegendeLine[i++]

                        });
                        db.SaveChanges();
                        
                        // PS soll ich ein andere daten banl tabelle erstellen um (Person, Bucher) daten zu speichern
                    }
                }
                file.Close();

                while ((line = file2.ReadLine()) != null)
                {
                    if ((line != "### titles by category and format"))
                    {

                        string[] zerlegendeLine2 = line.Split(":".ToCharArray());

                        for (int i = 0; i < zerlegendeLine2.Length; i++)
                        {
                            if (zerlegendeLine2[i] == "CATEGORY")
                            {
                                db.Add(new Catagory
                                {
                                   CategorieType = zerlegendeLine2[1]

                                });
                                db.SaveChanges();

                            }
                            else if (zerlegendeLine2[i] == "FORMAT")
                            {
                                //currFormat = new Format
                                //{
                                //    FormatArt = zerlegendeLine2[1]
                                //};
                                //db.Add(currFormat);
                                //db.SaveChanges();
                                db.Add(new Format
                                {
                                    FormatType = zerlegendeLine2[1]

                                });
                                db.SaveChanges();

                            }



                        }

                    }
                    else
                    {

                        BuchTabelleAusfuelen(line);
                        // ich habe hier break gestellt, weil diese Methode sih wiederholt 
                        // ich brauche diese Auslesen nur einmal, um Katagorie und Format auszufüllen
                        break;
                    }






                }

            }



        }

        public bool DatenPruefen(string email, string benutzer, string pass)
        {
            bool gefunden = false;

            using (var db = new BookshoppingContext())
            {

                var per = db.Persons
                     .OrderBy(b => b.Id)
                     .First();

                foreach (Person p in db.Persons)
                {
                    var permail = (from e in db.Persons where e.PersonEmail == email && e.PersonUser == benutzer && e.PersonPasswort == pass select e);
                    if (permail.Any())
                    {
                        gefunden = true;
                    }

                }

                if (gefunden is true)
                {
                    currPersonemail = email;
                    currPersonPasswort = pass;
                    Console.WriteLine();
                    Console.WriteLine(" Wilkommen");
                    Console.Write(" Möchten Sie Buch Kaufen K/ oder Ihre persönliche Profil zeigen Z/ ?");
                    string antwort = Console.ReadLine().ToLower();
                    antwort.ToLower();
                    if (antwort == "k")
                    {
                        // in diesem Schritt zeigen wir die Bibliothek, um Buecher zu kaufen
                        BuchBibliothekZeigen();

                    }
                    else if (antwort == "z")
                    {
                        // in diesem Schritt zeigen wir die persönliche Daten für ein Mitglieder und History, wenn es gibt
                    }
                    else
                    {
                        Console.Write(" Versuchen Sie noch einaml, entweder mit K oder mit Z zu beantworten " + '\v' + "Danke!");
                    }
                }
                else
                {
                    Console.Write(" Versuchen Sie sich noch einmal anzumelden A / Oder sich zu registrieren R");

                    Program prog = new Program();

                    prog.DataEingeben();


                }


                //var permail1 = db.Persons.Find(email);



            }

            return gefunden;

        }
        public void NeuPersonhinzufuegen(string Geschlecht, string Name, string Vorname, string strasse, string plz, string stadt, string email, string benutzername, string pass, string geburtsdatum)
        {

            string path = @"C:\tmp\fake-persons.txt";
            string line1 = " ";
            string leerLine = line1;
            using (var db = new BookshoppingContext())
            {
                db.Add(new Person
                {

                    PersonGender = Geschlecht,
                    PersonLastName = Name,
                    PersonFirstname  = Vorname,
                    PersonStreet = strasse,
                    PersonPLZ = plz,
                    PersonCity = stadt,
                    PersonEmail = email,
                    PersonUser = benutzername,
                    PersonPasswort = pass,
                    PersonBirthay = geburtsdatum
                });
                db.SaveChanges();

                currPersonemail = email;
                currPersonPasswort = pass;

                var per = db.Persons
                    .OrderBy(p => p.Id)
                    .First();


                var per1 = from p in db.Persons
                           where p.PersonEmail.Contains(email) && p.PersonPasswort.Contains(pass)
                           select p;
                foreach (Person p in per1)
                {
                     line1 = p.ToString();
                }
                File.AppendAllText(path, line1);
                

             

            }
           
           
                Console.WriteLine(" Melden Sie sich bitte!");

            Program prog = new Program();

            prog.DataEingeben();
        }
        public int CategoryFormathilf(string cat, string form)
        {

            using (var db = new BookshoppingContext())
            {

                if (cat == "Belletristik" && form == "Hardcover")
                {
                    pruf = 1;
                }
                else if (cat == "Sachbuch" && form == "Hardcover")
                {
                    pruf = 2;
                }
                else if (cat == "Belletristik" && form == "Paperback")
                {
                    pruf = 3;
                }
                else if (cat == "Sachbuch" && form == "Paperback")
                {
                    pruf = 4;
                }
                else if (cat == "Belletristik" && form == "Taschenbuch")
                {
                    pruf = 5;
                }
                else if (cat == "Sachbuch" && form == "Taschenbuch")
                {
                    pruf = 6;
                }
                else if (cat == "Jugendroman" && form == "Buch")
                {
                    pruf = 7;
                }
                else if (cat == "Sachbuch" && form == "Buch")
                {
                    pruf = 8;
                }
                else if (cat == "Bilderbuch" && form == "Buch")
                {
                    pruf = 9;
                }
                else if (cat == "Kinderbücher" && form == "Buch")
                {
                    pruf = 10;
                }
                else if (cat == "Sachbuch" && form == "CD")
                {
                    pruf = 11;
                }
                else if (cat == "Belletristik" && form == "CD")
                {
                    pruf = 12;
                }
                else if (cat == "Kinder & Jugend" && form == "CD")
                {
                    pruf = 13;
                }
                else if (cat == "Spielfilm" && form == "DVD")
                {
                    pruf = 14;
                }
                else if (cat == "TV & Hobby" && form == "DVD")
                {
                    pruf = 15;
                }
                else if (cat == "Leben & Gesundheit" && form == "Buch")
                {
                    pruf = 16;
                }
                else if (cat == "Essen & Trinken" && form == "Buch")
                {
                    pruf = 17;
                }
                else if (cat == "Natur & Garten" && form == "Buch")
                {
                    pruf = 18;
                }
                else if (cat == "Hobby & Kreativität" && form == "Buch")
                {
                    pruf = 19;
                }
                else if (cat == "Wirtschaft" && form == "Buch")
                {
                    pruf = 20;
                }
                hilf = pruf;
                return hilf;

            }

        }
        public void BuchTabelleAusfuelen(string line)// Format currFormat)
        {
            Book buch;

            using (var db = new BookshoppingContext())
            {

                System.IO.StreamReader file2 = new System.IO.StreamReader(@"C:\tmp\spiegel-bestseller.txt");
                while ((line = file2.ReadLine()) != null)
                {

                    if (line == "### titles by category and format")
                    {
                        counter = counter + 1;
                    }
                    if (counter != 0)
                    {
                        string[] zerlegendeLine2 = line.Split(":".ToCharArray());
                        for (int i = 0; i < zerlegendeLine2.Length; i++)
                        {
                            if (zerlegendeLine2[i] == "CATEGORY")
                            {
                                cate = zerlegendeLine2[1];
                            }
                            else if (zerlegendeLine2[i] == "FORMAT")
                            {
                                forma = zerlegendeLine2[1];
                            }
                            else if (zerlegendeLine2[i] == "AUTHOR")
                            {
                                author = zerlegendeLine2[1];
                            }
                            else if (zerlegendeLine2[i] == "TITLE")
                            {
                                title = zerlegendeLine2[1];
                            }
                            else if (zerlegendeLine2[i] == "EAN")
                            {
                                ean = zerlegendeLine2[1];
                            }
                            else if (zerlegendeLine2[i] == "PUBLISHER")
                            {
                                publisher = zerlegendeLine2[1];
                            }
                            else if (zerlegendeLine2[i] == "DATE")
                            {
                                date = zerlegendeLine2[1];
                            }
                            else if (zerlegendeLine2[i] == "PRICE")
                            {
                                price = zerlegendeLine2[1];
                            }

                        }
                        Program pro = new Program();
                        hilf = pro.CategoryFormathilf(cate, forma);
                    }
                    if (price != null)
                    {
                        switch (hilf)
                        {

                            case 1:
                            
                                var b = new Book
                                {
                                    Author = author,
                                    Title = title,
                                    EAN = ean,
                                    Publisher = publisher,
                                    Date = date,
                                    Price = price,
                                    FormatId = 4,
                                    CategorieId = 1


                                }
                               ;
                                //db.Entry<Buch>().State = EntityState.Detached;
                                db.Add(b);
                                db.SaveChanges();
                                cnt = cnt + 1;
                                if (cnt == 1)
                                {
                                    buch = db.Books.First();
                                }
                                else
                                {
                                    // db.Entry<Buch>(b).State = EntityState.Detached;
                                    buch = db.Books.Find(cnt);
                                    //db.Attach<Buch>(b).State = EntityState.Detached;
                                }

                                //KatagorieFormatIDAusfuellen(cate, forma, buch);
                               
                                
                                author = title = ean = publisher = date = price = null;
                                buch = null;
                                break;

                            case 2:

                                 b = new Book
                                {
                                    Author = author,
                                    Title = title,
                                    EAN = ean,
                                    Publisher = publisher,
                                    Date = date,
                                    Price = price,
                                    FormatId = 4,
                                    CategorieId = 10,


                                };

                                db.Add(b);
                                db.SaveChanges();
                                cnt = cnt + 1;
                                if (cnt == 1)
                                {
                                    buch = db.Books.First();
                                }
                                else
                                {
                                    // db.Entry<Buch>(b).State = EntityState.Detached;
                                    buch = db.Books.Find(cnt);
                                   
                                    db.Attach<Book>(b).State = EntityState.Detached;
                                  
                                    
                                }

                                //KatagorieFormatIDAusfuellen(cate, forma, buch);
                                //db.Attach<Buch>(b).State = EntityState.Detached;
                                //db.ChangeTracker.CascadeDeleteTiming = CascadeTiming.OnSaveChanges;
                                //db.ChangeTracker.DeleteOrphansTiming = CascadeTiming.OnSaveChanges;

                                author = title = ean = publisher = date = price = null;
                                buch = null;
                                break;

                            case 3:
                                 b = new Book
                                {
                                    Author = author,
                                    Title = title,
                                    EAN = ean,
                                    Publisher = publisher,
                                    Date = date,
                                    Price = price,
                                    FormatId = 5,
                                    CategorieId = 1,


                                }
                               ;
                                //db.Entry<Buch>().State = EntityState.Detached;
                                db.Add(b);
                                db.SaveChanges();
                                cnt = cnt + 1;
                                if (cnt == 1)
                                {
                                    buch = db.Books.First();
                                }
                                else
                                {
                                    // db.Entry<Buch>(b).State = EntityState.Detached;
                                    buch = db.Books.Find(cnt);
                                    db.Entry<Book>(b).State = EntityState.Detached;
                                }

                                //KatagorieFormatIDAusfuellen(cate, forma, buch);

                                author = title = ean = publisher = date = price = null;
                                buch = null;
                                break;

                            case 4:
                                 b = new Book
                                {
                                    Author = author,
                                    Title = title,
                                    EAN = ean,
                                    Publisher = publisher,
                                    Date = date,
                                    Price = price,
                                    FormatId = 5,
                                    CategorieId= 10


                                }
                               ;
                                //db.Entry<Buch>().State = EntityState.Detached;
                                db.Add(b);
                                db.SaveChanges();
                                cnt = cnt + 1;
                                if (cnt == 1)
                                {
                                    buch = db.Books.First();
                                }
                                else
                                {
                                    // db.Entry<Buch>(b).State = EntityState.Detached;
                                    buch = db.Books.Find(cnt);
                                    db.Entry<Book>(b).State = EntityState.Detached;
                                }

                                //KatagorieFormatIDAusfuellen(cate, forma, buch);

                                author = title = ean = publisher = date = price = null;
                                buch = null;
                                break;
                            case 5:
                                 b = new Book
                                {
                                    Author = author,
                                    Title = title,
                                    EAN = ean,
                                    Publisher = publisher,
                                    Date = date,
                                    Price = price,
                                    FormatId = 6,
                                    CategorieId = 1


                                }
                               ;
                                //db.Entry<Buch>().State = EntityState.Detached;
                                db.Add(b);
                                db.SaveChanges();
                                cnt = cnt + 1;
                                if (cnt == 1)
                                {
                                    buch = db.Books.First();
                                }
                                else
                                {
                                    // db.Entry<Buch>(b).State = EntityState.Detached;
                                    buch = db.Books.Find(cnt);
                                    db.Entry<Book>(b).State = EntityState.Detached;
                                }

                                //KatagorieFormatIDAusfuellen(cate, forma, buch);

                                author = title = ean = publisher = date = price = null;
                                buch = null;
                                break;
                            case 6:
                                 b = new Book
                                {
                                    Author = author,
                                    Title = title,
                                    EAN = ean,
                                    Publisher = publisher,
                                    Date = date,
                                    Price = price,
                                    FormatId = 6,
                                    CategorieId = 10


                                }
                               ;
                                //db.Entry<Buch>().State = EntityState.Detached;
                                db.Add(b);
                                db.SaveChanges();
                                cnt = cnt + 1;
                                if (cnt == 1)
                                {
                                    buch = db.Books.First();
                                }
                                else
                                {
                                    // db.Entry<Buch>(b).State = EntityState.Detached;
                                    buch = db.Books.Find(cnt);
                                    db.Entry<Book>(b).State = EntityState.Detached;
                                }

                              //  KatagorieFormatIDAusfuellen(cate, forma, buch);

                                author = title = ean = publisher = date = price = null;
                                buch = null;
                                break;
                            case 7:
                                b = new Book
                                {
                                    Author = author,
                                    Title = title,
                                    EAN = ean,
                                    Publisher = publisher,
                                    Date = date,
                                    Price = price,
                                    FormatId = 1,
                                    CategorieId = 5

                                }
                               ;
                                //db.Entry<Buch>().State = EntityState.Detached;
                                db.Add(b);
                                db.SaveChanges();
                                cnt = cnt + 1;
                                if (cnt == 1)
                                {
                                    buch = db.Books.First();
                                }
                                else
                                {
                                    // db.Entry<Buch>(b).State = EntityState.Detached;
                                    buch = db.Books.Find(cnt);
                                    db.Entry<Book>(b).State = EntityState.Detached;
                                }

                               // KatagorieFormatIDAusfuellen(cate, forma, buch);

                                author = title = ean = publisher = date = price = null;
                                buch = null;
                                break;
                            case 8:
                                 b = new Book
                                {
                                    Author = author,
                                    Title = title,
                                    EAN = ean,
                                    Publisher = publisher,
                                    Date = date,
                                    Price = price,
                                    FormatId = 1,
                                   CategorieId = 10

                                }
                               ;
                                //db.Entry<Buch>().State = EntityState.Detached;
                                db.Add(b);
                                db.SaveChanges();
                                cnt = cnt + 1;
                                if (cnt == 1)
                                {
                                    buch = db.Books.First();
                                }
                                else
                                {
                                    // db.Entry<Buch>(b).State = EntityState.Detached;
                                    buch = db.Books.Find(cnt);
                                    db.Entry<Book>(b).State = EntityState.Detached;
                                }

                               // KatagorieFormatIDAusfuellen(cate, forma, buch);

                                author = title = ean = publisher = date = price = null;
                                buch = null;
                                break;
                            case 9:
                                 b = new Book
                                {
                                    Author = author,
                                    Title = title,
                                    EAN = ean,
                                    Publisher = publisher,
                                    Date = date,
                                    Price = price,
                                    FormatId = 1,
                                    CategorieId = 2


                                }
                              ;
                                //db.Entry<Buch>().State = EntityState.Detached;
                                db.Add(b);
                                db.SaveChanges();
                                cnt = cnt + 1;
                                if (cnt == 1)
                                {
                                    buch = db.Books.First();
                                }
                                else
                                {
                                    // db.Entry<Buch>(b).State = EntityState.Detached;
                                    buch = db.Books.Find(cnt);
                                    db.Entry<Book>(b).State = EntityState.Detached;
                                }

                               // KatagorieFormatIDAusfuellen(cate, forma, buch);

                                author = title = ean = publisher = date = price = null;
                                buch = null;
                                break;
                            case 10:
                                 b = new Book
                                {
                                    Author = author,
                                    Title = title,
                                    EAN = ean,
                                    Publisher = publisher,
                                    Date = date,
                                    Price = price,
                                    FormatId = 1,
                                    CategorieId = 7


                                }
                              ;
                                //db.Entry<Buch>().State = EntityState.Detached;
                                db.Add(b);
                                db.SaveChanges();
                                cnt = cnt + 1;
                                if (cnt == 1)
                                {
                                    buch = db.Books.First();
                                }
                                else
                                {
                                    // db.Entry<Buch>(b).State = EntityState.Detached;
                                    buch = db.Books.Find(cnt);
                                    db.Entry<Book>(b).State = EntityState.Detached;
                                }

                               // KatagorieFormatIDAusfuellen(cate, forma, buch);

                                author = title = ean = publisher = date = price = null;
                                buch = null;
                                break;
                            case 11:
                                 b = new Book
                                {
                                    Author = author,
                                    Title = title,
                                    EAN = ean,
                                    Publisher = publisher,
                                    Date = date,
                                    Price = price,
                                    FormatId = 2,
                                    CategorieId = 10


                                }
                              ;
                                //db.Entry<Buch>().State = EntityState.Detached;
                                db.Add(b);
                                db.SaveChanges();
                                cnt = cnt + 1;
                                if (cnt == 1)
                                {
                                    buch = db.Books.First();
                                }
                                else
                                {
                                    // db.Entry<Buch>(b).State = EntityState.Detached;
                                    buch = db.Books.Find(cnt);
                                    db.Entry<Book>(b).State = EntityState.Detached;
                                }

                               // KatagorieFormatIDAusfuellen(cate, forma, buch);

                                author = title = ean = publisher = date = price = null;
                                buch = null;
                                break;
                            case 12:
                                 b = new Book
                                {
                                    Author = author,
                                    Title = title,
                                    EAN = ean,
                                    Publisher = publisher,
                                    Date = date,
                                    Price = price,
                                    FormatId = 2,
                                    CategorieId = 6

                                }
                               ;
                                //db.Entry<Buch>().State = EntityState.Detached;
                                db.Add(b);
                                db.SaveChanges();
                                cnt = cnt + 1;
                                if (cnt == 1)
                                {
                                    buch = db.Books.First();
                                }
                                else
                                {
                                    // db.Entry<Buch>(b).State = EntityState.Detached;
                                    buch = db.Books.Find(cnt);
                                    db.Entry<Book>(b).State = EntityState.Detached;
                                }

                              //  KatagorieFormatIDAusfuellen(cate, forma, buch);

                                author = title = ean = publisher = date = price = null;
                                buch = null;
                                break;
                            case 13:
                                 b = new Book
                                {
                                    Author = author,
                                    Title = title,
                                    EAN = ean,
                                    Publisher = publisher,
                                    Date = date,
                                    Price = price,
                                    FormatId = 3,
                                    CategorieId = 11


                                }
                              ;
                                //db.Entry<Buch>().State = EntityState.Detached;
                                db.Add(b);
                                db.SaveChanges();
                                cnt = cnt + 1;
                                if (cnt == 1)
                                {
                                    buch = db.Books.First();
                                }
                                else
                                {
                                    // db.Entry<Buch>(b).State = EntityState.Detached;
                                    buch = db.Books.Find(cnt);
                                    db.Entry<Book>(b).State = EntityState.Detached;
                                }

                              //  KatagorieFormatIDAusfuellen(cate, forma, buch);

                                author = title = ean = publisher = date = price = null;
                                buch = null;
                                break;
                            case 14:
                                 b = new Book
                                {
                                    Author = author,
                                    Title = title,
                                    EAN = ean,
                                    Publisher = publisher,
                                    Date = date,
                                    Price = price,
                                    FormatId = 3,
                                    CategorieId = 12

                                }
                                ;
                                //db.Entry<Buch>().State = EntityState.Detached;
                                db.Add(b);
                                db.SaveChanges();
                                cnt = cnt + 1;
                                if (cnt == 1)
                                {
                                    buch = db.Books.First();
                                }
                                else
                                {
                                    // db.Entry<Buch>(b).State = EntityState.Detached;
                                    buch = db.Books.Find(cnt);
                                    db.Entry<Book>(b).State = EntityState.Detached;
                                }

                               // KatagorieFormatIDAusfuellen(cate, forma, buch);

                                author = title = ean = publisher = date = price = null;
                                buch = null;
                                break;
                            case 15:
                                 b = new Book
                                {
                                    Author = author,
                                    Title = title,
                                    EAN = ean,
                                    Publisher = publisher,
                                    Date = date,
                                    Price = price,
                                    FormatId = 1,
                                    CategorieId = 8

                                }
                               ;
                                //db.Entry<Buch>().State = EntityState.Detached;
                                db.Add(b);
                                db.SaveChanges();
                                cnt = cnt + 1;
                                if (cnt == 1)
                                {
                                    buch = db.Books.First();
                                }
                                else
                                {
                                    // db.Entry<Buch>(b).State = EntityState.Detached;
                                    buch = db.Books.Find(cnt);
                                    db.Entry<Book>(b).State = EntityState.Detached;
                                }

                              //  KatagorieFormatIDAusfuellen(cate, forma, buch);

                                author = title = ean = publisher = date = price = null;
                                buch = null;
                                break;
                            case 16:
                                 b = new Book
                                {
                                    Author = author,
                                    Title = title,
                                    EAN = ean,
                                    Publisher = publisher,
                                    Date = date,
                                    Price = price,
                                    FormatId = 1,
                                    CategorieId = 9

                                }
                              ;
                                //db.Entry<Buch>().State = EntityState.Detached;
                                db.Add(b);
                                db.SaveChanges();
                                cnt = cnt + 1;
                                if (cnt == 1)
                                {
                                    buch = db.Books.First();
                                }
                                else
                                {
                                    // db.Entry<Buch>(b).State = EntityState.Detached;
                                    buch = db.Books.Find(cnt);
                                    db.Entry<Book>(b).State = EntityState.Detached;
                                }

                              //  KatagorieFormatIDAusfuellen(cate, forma, buch);

                                author = title = ean = publisher = date = price = null;
                                buch = null;
                                break;
                            case 17:
                                 b = new Book
                                {
                                    Author = author,
                                    Title = title,
                                    EAN = ean,
                                    Publisher = publisher,
                                    Date = date,
                                    Price = price,
                                    FormatId = 1,
                                   CategorieId = 3

                                }
                               ;
                                //db.Entry<Buch>().State = EntityState.Detached;
                                db.Add(b);
                                db.SaveChanges();
                                cnt = cnt + 1;
                                if (cnt == 1)
                                {
                                    buch = db.Books.First();
                                }
                                else
                                {
                                    // db.Entry<Buch>(b).State = EntityState.Detached;
                                    buch = db.Books.Find(cnt);
                                    db.Entry<Book>(b).State = EntityState.Detached;
                                }

                               // KatagorieFormatIDAusfuellen(cate, forma, buch);

                                author = title = ean = publisher = date = price = null;
                                buch = null;
                                break;
                            case 18:
                                b = new Book
                                {
                                    Author = author,
                                    Title = title,
                                    EAN = ean,
                                    Publisher = publisher,
                                    Date = date,
                                    Price = price,
                                    FormatId = 1,
                                   CategorieId = 6

                                }
                              ;
                                //db.Entry<Buch>().State = EntityState.Detached;
                                db.Add(b);
                                db.SaveChanges();
                                cnt = cnt + 1;
                                if (cnt == 1)
                                {
                                    buch = db.Books.First();
                                }
                                else
                                {
                                    // db.Entry<Buch>(b).State = EntityState.Detached;
                                    buch = db.Books.Find(cnt);
                                    db.Entry<Book>(b).State = EntityState.Detached;
                                }

                                // KatagorieFormatIDAusfuellen(cate, forma, buch);

                                author = title = ean = publisher = date = price = null;
                                buch = null;
                                break;
                            case 19:
                                b = new Book
                                {
                                    Author = author,
                                    Title = title,
                                    EAN = ean,
                                    Publisher = publisher,
                                    Date = date,
                                    Price = price,
                                    FormatId = 1,
                                    CategorieId = 4

                                }
                              ;
                                //db.Entry<Buch>().State = EntityState.Detached;
                                db.Add(b);
                                db.SaveChanges();
                                cnt = cnt + 1;
                                if (cnt == 1)
                                {
                                    buch = db.Books.First();
                                }
                                else
                                {
                                    // db.Entry<Buch>(b).State = EntityState.Detached;
                                    buch = db.Books.Find(cnt);
                                    db.Entry<Book>(b).State = EntityState.Detached;
                                }

                                // KatagorieFormatIDAusfuellen(cate, forma, buch);

                                author = title = ean = publisher = date = price = null;
                                buch = null;
                                break;
                            case 20:
                                b = new Book
                                {
                                    Author = author,
                                    Title = title,
                                    EAN = ean,
                                    Publisher = publisher,
                                    Date = date,
                                    Price = price,
                                    FormatId = 1,
                                    CategorieId = 13

                                }
                              ;
                                //db.Entry<Buch>().State = EntityState.Detached;
                                db.Add(b);
                                db.SaveChanges();
                                cnt = cnt + 1;
                                if (cnt == 1)
                                {
                                    buch = db.Books.First();
                                }
                                else
                                {
                                    // db.Entry<Buch>(b).State = EntityState.Detached;
                                    buch = db.Books.Find(cnt);
                                    db.Entry<Book>(b).State = EntityState.Detached;
                                }

                                // KatagorieFormatIDAusfuellen(cate, forma, buch);

                                author = title = ean = publisher = date = price = null;
                                buch = null;
                                break;
                        }



                    }
                }
            }
        }
        public int BuchBibliothekZeigen()
        {
            string  buchAuthor, buchPrice, buchEan, buchDate, buchPublisher, buchTitle, buchId;
            //int buchId;
            int antwort;
            Console.Write(" Wählen Sie eine Kategorie aus ?");
            int kId = KategorieZeigen();
            Console.Write(" Wählen Sie eine Format aus ?");
            int fId = FormatZeigen();

            using (var db = new BookshoppingContext())
            {

                var buch1 = from b in db.Books
                            where b.CategorieId == kId && b.FormatId == fId
                            select new
                            {
                                Buchnummer = b.Id,
                                Buchauthor = b.Author,
                                Buchprice = b.Price,
                                Buchdatum = b.Date,
                                Buchpublish = b.Publisher,
                                Buchtitle = b.Title,
                                Buchean = b.EAN

                            };
                foreach (var s in buch1)
                {
                    buchId = s.Buchnummer.ToString();
                    buchAuthor = s.Buchauthor;
                    buchPrice = s.Buchprice;
                    buchEan = s.Buchean;
                    buchDate = s.Buchdatum;
                    buchPublisher = s.Buchpublish;
                    buchTitle = s.Buchtitle;

                    Console.WriteLine(" Buchnummer : " + buchId + '\n' + " Autor : " + buchAuthor +'\n'
                                      + " Preis : " + buchPrice + '\n' + " Datum : " + buchDate + '\n' + " Publisher : " + buchPublisher 
                                      + '\n' + " Title : " + buchTitle + '\n' + " EAN : " + buchEan + '\n');
                        
                }

               
                Console.Write(" Wählen Sie ein Buch aus / Nummer des Buchs : ");
                antwort = Convert.ToInt32(Console.ReadLine());
                BuchHinzufuegen(antwort);
                Console.Write(" Möchten Sie andere Buch Kaufen K/ oder möchzen Sie Ihr persönliche Profil ansehen Z/?");
                string stringAntwort = Console.ReadLine().ToLower();
                if(stringAntwort == "k")
                {
                    BuchBibliothekZeigen();
                }
                else
                {
                    ProfilZeigen();
                    // Profil ansehen 
                }
                return antwort;

            }
            //Console.WriteLine("Wählen Sie ein Buch aus / Nummer des Buchs");
            //int Kaufantwort = Convert.ToInt32(Console.ReadLine());
            //Console.WriteLine("Möchten Sie dieses Buch kaufen k/, oder dieses Buch zum Favorit hinzufügen F/");
            //string antwort2 = Console.ReadLine();

            //BuchKaufen(Kaufantwort, antwort2);


        }
        public int FormatZeigen()
        {
            int antwort;


            using (var db = new BookshoppingContext())
            {


                var form = from f in db.Formats
                           select new
                           {
                               Number = f.Id,
                               Format = f.FormatType
                           };
                foreach (var s in form)
                {
                    Console.WriteLine();
                    Console.WriteLine(s.Number + "\t" + s.Format);
                }

            }

            antwort = Convert.ToInt32(Console.ReadLine());

            using (var db = new BookshoppingContext())
            {
                var form = from f in db.Formats
                           where f.Id == antwort
                           select new
                           {
                               fid = f.Id
                           };
                foreach (var s in form)
                {
                    meinFor = s.fid;
                }


            }


            return meinFor;
        }
        public int KategorieZeigen()
        {

            int antwort = 1;
            using (var db = new BookshoppingContext())
            {

                var kat = db.Categories
                     .OrderBy(b => b.Id)
                     .First();


                var kateg = from k in db.Categories
                            select new
                            {
                                Number = k.Id,
                                Categorie = k.CategorieType
                            };
                foreach (var s in kateg)
                {
                    Console.WriteLine();
                    Console.WriteLine(s.Number + "\t" + s.Categorie);
                }

            }
            antwort = Convert.ToInt32(Console.ReadLine());

            using (var db = new BookshoppingContext())
            {
                var kat = from k in db.Categories
                          where k.Id == antwort
                          select new
                          {
                              kid = k.Id
                          };
                foreach (var s in kat)
                {
                    meinKat = s.kid;
                }


            }

            return meinKat;
        }
        //public void BuchKaufen(int Buchnummer, string antwort2)
        //{
        //    using (var db = new BookshoppingContext())
        //    {

        //        var buch = from b in db.Buescher
        //                   where b.Id == Buchnummer
        //                   select new
        //                   {
        //                       BuchTitle = b.Title,
        //                       BuchPrice = b.Price
        //                   };


        //        foreach (var s in buch)
        //        {
        //            if (antwort2 == "k")
        //            {
        //                db.Add(new History
        //                {
        //                    BuchKaufen = s.BuchTitle
        //                });

        //                db.SaveChanges();
        //                Console.WriteLine();
        //                Console.Write(" Der Buch " + s.BuchTitle + "kostet " + s.BuchPrice + "Wird in deinem Warenkrob hinzufügt ");
        //                Console.Write(" Wenn Sie deinen Warenkrob sehen möchten,drücken Sie /wk");
        //                string antwort = Console.ReadLine();
        //                HistoryZeigen(antwort);
        //            }
        //            else if (antwort2 == "f")
        //            {
        //                db.Add(new History
        //                {
        //                    MoechteKaufen = s.BuchTitle

        //                });
        //                db.SaveChanges();
        //                Console.WriteLine();
        //                Console.WriteLine(" Der Buch " + s.BuchTitle + "kostet " + s.BuchPrice + "Wird in deiner Wunschliste hinzufügt ");
        //                Console.Write(" Wenn Sie deine Wunschliste sehen möchten,drücken Sie /wl");
        //                string antwort = Console.ReadLine();
        //                HistoryZeigen(antwort);
        //            }
        //            else
        //            {
        //                Console.WriteLine(" Versuchen Sie noch einmal ");
        //                BuchBibliothekZeigen();

        //            }

        //        }

        //    }

        //}
        //public void HistoryZeigen(string antwort)
        //{
         
        //    using (var db = new BookshoppingContext())
        //    {
        //        if (antwort == "wk")
        //        {
        //            var Wark = from w in db.Histories
        //                       select new
        //                       {
        //                           Title = w.BuchKaufen
        //                       };
        //            foreach (var s in Wark)
        //            {
        //                Console.WriteLine(s);
        //            }
        //        } // hie nur Wuschliste zeigen 
        //    }
        //}
        public void IDAusfüllen()
        {
            using (var db = new BookshoppingContext())
            {
                //var buch = db.Buescher
                //    .OrderBy(b => b.BuchId)
                //    .FirstOrDefault();
                //buch.Person.PersonId = 1;
            }
        }
        //public void KatagorieFormatIDAusfuellen(string kate, string forma, Buch buch)
        //{
            
        //    using (var db = new BookshoppingContext()) {

               
        //        var kat = db.Catags
        //                  .Where(k => k.KatagorieArt == kate)
        //                  .Select(k => new
        //                  {
        //                      kart = k.KatagorieArt,
        //                      kid = k.Id
        //                  });

        //        var form = db.Formats
        //                   .Where(f => f.FormatArt == forma)
        //                   .Select(f => new
        //                   {
        //                       fart = f.FormatArt,
        //                       fid = f.Id
        //                   });

        //      //var  buch = db.Buescher
        //      //            .OrderBy(b => b.BuchId)
        //      //             .FirstOrDefault();

        //        //using (var db1 = new BookshoppingContext())
        //        //{
        //        //    buch = db1.Buescher.First();
        //        //}
        //        foreach (var s in kat)
        //        {
        //            buch.Katagorie = new Katagorie
        //            {
        //                KatagorieArt = s.kart,
        //                Id = s.kid
        //            };
        //        }
        //        foreach (var f in form)
        //        {

        //            buch.Format = new Format
        //            {
        //                FormatArt = f.fart,
        //                Id = f.fid
        //            };

        //        }

        //        //db.Buescher.Add(buch);

           
               
        //        var atta = db.Attach(buch);
        //            atta.State = EntityState.Modified;
        //        db.Update(buch);
        //        db.SaveChanges();


        //        //Entry = null;


        //    }

        //}   
        public void BuchHinzufuegen(int antwort)
        {
            int pid;
            Person person;
            using (var db = new BookshoppingContext())
            {
                Book buch = db.Books.Find(antwort);
                string author = buch.Author;
                string title = buch.Title;
                string price = buch.Price;
                string ean = buch.EAN;
                string publisher = buch.Publisher;
                string date = buch.Date;
                var per1 = db.Persons.OrderBy(p => p.Id).First();
                var per = from p in db.Persons where p.PersonEmail.Contains(currPersonemail) && p.PersonPasswort.Contains(currPersonPasswort) select p;
                foreach (var p in per)
                {
                    pid = p.Id;
                    person = db.Persons.Find(pid);

                    person.books?.Add(new Book()
                    {
                        Author = author,
                        Title = title,
                        Price = price,
                        EAN = ean,
                        Publisher = publisher,
                        Date = date,
                    }) ;

                   
                    var atta = db.Attach(person);
                    atta.State = EntityState.Modified;
                    db.Update(person);
                    db.SaveChanges();

                }

             

             

            }

        }

        public void ProfilZeigen()
        {
            string? FirstName, LastName, Email, User;
                
            using (var db = new BookshoppingContext())
            {
                var per = from p in db.Persons
                          where p.PersonEmail == currPersonemail && p.PersonPasswort == currPersonPasswort
                          select p;
                var per1 = from p in db.Persons
                          where p.PersonEmail == currPersonemail && p.PersonPasswort == currPersonPasswort
                          select p.books;

                Console.WriteLine("Mitglierder Profil");
                Console.WriteLine();
                Console.WriteLine("Ihr persönliche Daten :");
                foreach(var s in per)
                {
                    
                     FirstName = s.PersonFirstname;
                    LastName = s.PersonLastName;
                     Email = s.PersonEmail;
                     User = s.PersonUser;

                    Console.WriteLine("Mitglieder Name :" + LastName + '\n' + "Mitglieder Vorname :" +
                                        FirstName + '\n' + "Mitglieder Email : " + Email + '\n' + "Mitglieder Benutzername : " + User);
                    Console.WriteLine();
                    
                }
                
                foreach (var s in per1) 
                {
                    s.ForEach(i => Console.WriteLine("{0}\n", i));

                }
               
             }
            Console.WriteLine("Möchten Sie andere Buch kaufen k/ oder Beenden B/");
            string stringAntwort = Console.ReadLine().ToLower();
            if(stringAntwort == "k")
            {
                BuchBibliothekZeigen();
            }
            else
            {
                Console.WriteLine("Auf Wiedersehen");
                Console.ReadKey();
            }

        }

        public class DbUpdateException : Exception { }

    }
}

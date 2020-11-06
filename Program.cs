using BookShoppingNeu;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;

namespace BookShoppingNeu
{
    class Program
    {
        public int pruf;
        public int hilf;
        public int counter;
        public string cate;
        public string forma;
        private string author, title, ean, publisher, date, price;
        private static void Main()
        {
            Program prog = new Program();
            prog.DateiAuslesen();
            prog.ShowData();
            prog.DataEingeben();
            // prog.Createdata();
             Console.WriteLine("Test");



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
                Console.WriteLine("--------------------------------");
                Console.WriteLine();
                Console.WriteLine("\n----- Anzahl der Buecher -----");
                Console.WriteLine($" - {db.Buescher.Count()} Buecher in unsere Bibliothek");
                Console.WriteLine("--------------------------------");
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


            Console.WriteLine("****Buchladen****");
            Console.WriteLine();
            Console.WriteLine("Möchten Sie registirieren oder sich anmelden?");
            Console.WriteLine();

            Console.Write("Registrierung (R), Anmeldung (A)");
            string eing = Console.ReadLine();
            eing.ToLower();

            if (eing == "r")
            {
                Console.WriteLine("-------Registrierung-------");
                Console.Write("Gender M/F : ");
                string gender = Console.ReadLine();

                Console.Write("Vorname : ");
                string vorname = Console.ReadLine();

                Console.Write("Nachname : ");
                string nachname = Console.ReadLine();

                Console.Write("Straße : ");
                string straße = Console.ReadLine();

                Console.Write("PLZ : ");
                string plz = Console.ReadLine();

                Console.Write("Stadt : ");
                string stadt = Console.ReadLine();

                Console.Write("Email : ");
                string email = Console.ReadLine();

                Console.Write("Benutzername : ");
                string benutzername = Console.ReadLine();

                Console.Write("Passwort : ");
                string passwort = Console.ReadLine();

                Console.Write("Geburtsdatum : ");
                string Gdatum = Console.ReadLine();


                // fügen wir die neue Mitglieder zu unsere Daten bank ein 
                prog.NeuPersonhinzufuegen(gender, nachname, vorname, straße, plz, stadt, email, benutzername, passwort, Gdatum);

            }
            else
            {
                Console.WriteLine("-------Anmeldung-------");
                Console.WriteLine();
                Console.Write("Email : ");
                string email = Console.ReadLine();
                Console.Write("Benutzername : ");
                string benutzer = Console.ReadLine();
                Console.Write("Passwort : ");
                string passwort = Console.ReadLine();
                prog.DatenPruefen(email, benutzer, passwort);
                // prüfen wir, ob die Daten richtig sind
           

            }

        }
        public void DateiAuslesen()
        {
            string cat, form;
            
            //int hilfer;
            using (var db = new BookshoppingContext())
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
                Person per = new Person();
                //List<Person> per;
                string line;


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
                            PersonName = zerlegendeLine[i++],
                            PersonVorname = zerlegendeLine[i++],
                            PersonStraße = zerlegendeLine[i++],
                            PersonPLZ = zerlegendeLine[i++],
                            PersonStadt = zerlegendeLine[i++],
                            PersonEmail = zerlegendeLine[i++],
                            PersonUser = zerlegendeLine[i++],
                            PersonPasswort = zerlegendeLine[i++],
                            PersonBirthay = zerlegendeLine[i++]

                        });
                        db.SaveChanges();

                        // PS soll ich ein andere daten banl tabelle erstellen um (Person, Bucher) daten zu speichern
                    }
                }
                while ((line = file2.ReadLine()) != null)
                {
                    if (line != "### titles by category and format")
                    {
                        string[] zerlegendeLine2 = line.Split(":".ToCharArray());

                        for (int i = 0; i < zerlegendeLine2.Length; i++)
                        {
                            if (zerlegendeLine2[i] == "CATEGORY")
                            {
                                db.Add(new Katagorie
                                {
                                    KatagorieArt = zerlegendeLine2[1]

                                });
                                db.SaveChanges();
                            }
                            else if (zerlegendeLine2[i] == "FORMAT")
                            {
                                db.Add(new Format
                                {
                                    FormatArt = zerlegendeLine2[1]
                                });
                                db.SaveChanges();

                            }

                           

                        }


                    }
                    else
                    {
                        BuchTabelleAusfuelen(line);
                    }
                    //foreach (string s in zerlegendeLine2)
                    //{
                    //    Console.WriteLine(s);
                    //}

                }

            }



        }
        public bool DatenPruefen(string email, string benutzer, string pass)
        {
            bool gefunden = false;

            using (var db = new BookshoppingContext())
            {

                var per = db.Persons
                     .OrderBy(b => b.PersonId)
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
                    Console.WriteLine("Wilkommen");
                    Console.WriteLine("Möchten Sie Buch Kaufen K/ oder Ihre persönliche Profil zeigen Z/ ?");
                    string antwort = Console.ReadLine();
                    antwort.ToLower();
                    if (antwort == "k")
                    {
                        // in diesem Schritt zeigen wir die Bibliothek, um Buecher zu kaufen

                    }
                    else if(antwort == "z")
                    {
                        // in diesem Schritt zeigen wir die persönliche Daten für ein Mitglieder und History, wenn es gibt
                    }
                    else
                    {
                        Console.WriteLine("Versuchen Sie noch einaml, entweder mit K oder mit Z zu beantworten " + "/t + Danke!");
                    }
                }
                else
                {
                    Console.WriteLine("Versuchen Sie sich noch einmal anzumelden A/ Oder sich zu registrieren R");

                    Program prog = new Program();

                    prog.DataEingeben();


                }


                //var permail1 = db.Persons.Find(email);



            }

            return gefunden;

        }
        public void NeuPersonhinzufuegen(string Geschlecht, string Name, string Vorname, string strasse, string plz, string stadt, string email, string benutzername, string pass, string geburtsdatum)
        {
            using (var db = new BookshoppingContext())
            {
                db.Add(new Person
                {

                    PersonGender = Geschlecht,
                    PersonName = Name,
                    PersonVorname = Vorname,
                    PersonStraße = strasse,
                    PersonPLZ = plz,
                    PersonStadt = stadt,
                    PersonEmail = email,
                    PersonUser = benutzername,
                    PersonPasswort = pass,
                    PersonBirthay = geburtsdatum
                });
                db.SaveChanges();


            }
            Console.WriteLine("Melden Sie sich bitte!");

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
                else if (cat == "Sachbuch" && form == "Taschenbuch")
                {
                    pruf = 5;
                }
                else if (cat == "Sachbuch" && form == "Buch")
                {
                    pruf = 6;
                }
                else if (cat == "Bilderbuch" && form == "Buch")
                {
                    pruf = 7;
                }
                else if (cat == "Kinderbücher" && form == "Buch")
                {
                    pruf = 8;
                }
                else if (cat == "Sachbuch" && form == "CD")
                {
                    pruf = 9;
                }
                else if (cat == "Belletristik" && form == "CD")
                {
                    pruf = 10;
                }
                else if (cat == "Kinder & Jugend" && form == "CD")
                {
                    pruf = 11;
                }
                else if (cat == "Spielfilm" && form == "DVD")
                {
                    pruf = 12;
                }
                else if (cat == "TV & Hobby" && form == "DVD")
                {
                    pruf = 13;
                }
                else if (cat == "Leben & Gesundheit" && form == "Buch")
                {
                    pruf = 14;
                }
                else if (cat == "Essen & Trinken" && form == "Buch")
                {
                    pruf = 15;
                }
                else if (cat == "Natur & Garten" && form == "Buch")
                {
                    pruf = 16;
                }
                else if (cat == "Wirtschaft" && form == "Buch")
                {
                    pruf = 17;
                }
                hilf = pruf;
                return hilf;

            }

        }
        public void BuchTabelleAusfuelen(string line)
        {

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
                                db.Add(new Buch
                                {
                                    Author = author,
                                    Title = title,
                                    EAN = ean,
                                    Publisher = publisher,
                                    Date = date,
                                    Price = price
                                });
                                db.SaveChanges();
                                author = title = ean = publisher = date = price = null;
                                break;
                            case 2:
                                db.Add(new Buch
                                {
                                    Author = author,
                                    Title = title,
                                    EAN = ean,
                                    Publisher = publisher,
                                    Date = date,
                                    Price = price
                                });
                                db.SaveChanges();
                                author = title = ean = publisher = date = price = null;
                                break;
                            case 3:
                                db.Add(new Buch
                                {
                                    Author = author,
                                    Title = title,
                                    EAN = ean,
                                    Publisher = publisher,
                                    Date = date,
                                    Price = price
                                });
                                db.SaveChanges();
                                author = title = ean = publisher = date = price = null;
                                break;
                            case 4:
                                db.Add(new Buch
                                {
                                    Author = author,
                                    Title = title,
                                    EAN = ean,
                                    Publisher = publisher,
                                    Date = date,
                                    Price = price
                                });
                                db.SaveChanges();
                                author = title = ean = publisher = date = price = null;
                                break;
                            case 5:
                                db.Add(new Buch
                                {
                                    Author = author,
                                    Title = title,
                                    EAN = ean,
                                    Publisher = publisher,
                                    Date = date,
                                    Price = price
                                });
                                db.SaveChanges();
                                author = title = ean = publisher = date = price = null;
                                break;
                            case 6:
                                db.Add(new Buch
                                {
                                    Author = author,
                                    Title = title,
                                    EAN = ean,
                                    Publisher = publisher,
                                    Date = date,
                                    Price = price
                                });
                                db.SaveChanges();
                                author = title = ean = publisher = date = price = null;
                                break;
                            case 7:
                                db.Add(new Buch
                                {
                                    Author = author,
                                    Title = title,
                                    EAN = ean,
                                    Publisher = publisher,
                                    Date = date,
                                    Price = price
                                });
                                db.SaveChanges();
                                author = title = ean = publisher = date = price = null;
                                break;
                            case 8:
                                db.Add(new Buch
                                {
                                    Author = author,
                                    Title = title,
                                    EAN = ean,
                                    Publisher = publisher,
                                    Date = date,
                                    Price = price
                                });
                                db.SaveChanges();
                                author = title = ean = publisher = date = price = null;
                                break;
                            case 9:
                                db.Add(new Buch
                                {
                                    Author = author,
                                    Title = title,
                                    EAN = ean,
                                    Publisher = publisher,
                                    Date = date,
                                    Price = price
                                });
                                db.SaveChanges();
                                author = title = ean = publisher = date = price = null;
                                break;
                            case 10:
                                db.Add(new Buch
                                {
                                    Author = author,
                                    Title = title,
                                    EAN = ean,
                                    Publisher = publisher,
                                    Date = date,
                                    Price = price
                                });
                                db.SaveChanges();
                                author = title = ean = publisher = date = price = null;
                                break;
                            case 11:
                                db.Add(new Buch
                                {
                                    Author = author,
                                    Title = title,
                                    EAN = ean,
                                    Publisher = publisher,
                                    Date = date,
                                    Price = price
                                });
                                db.SaveChanges();
                                author = title = ean = publisher = date = price = null;
                                break;
                            case 12:
                                db.Add(new Buch
                                {
                                    Author = author,
                                    Title = title,
                                    EAN = ean,
                                    Publisher = publisher,
                                    Date = date,
                                    Price = price
                                });
                                db.SaveChanges();
                                author = title = ean = publisher = date = price = null;
                                break;
                            case 13:
                                db.Add(new Buch
                                {
                                    Author = author,
                                    Title = title,
                                    EAN = ean,
                                    Publisher = publisher,
                                    Date = date,
                                    Price = price
                                });
                                db.SaveChanges();
                                author = title = ean = publisher = date = price = null;
                                break;
                            case 14:
                                db.Add(new Buch
                                {
                                    Author = author,
                                    Title = title,
                                    EAN = ean,
                                    Publisher = publisher,
                                    Date = date,
                                    Price = price
                                });
                                db.SaveChanges();
                                author = title = ean = publisher = date = price = null;
                                break;
                            case 15:
                                db.Add(new Buch
                                {
                                    Author = author,
                                    Title = title,
                                    EAN = ean,
                                    Publisher = publisher,
                                    Date = date,
                                    Price = price
                                });
                                db.SaveChanges();
                                author = title = ean = publisher = date = price = null;
                                break;
                            case 16:
                                db.Add(new Buch
                                {
                                    Author = author,
                                    Title = title,
                                    EAN = ean,
                                    Publisher = publisher,
                                    Date = date,
                                    Price = price
                                });
                                db.SaveChanges();
                                author = title = ean = publisher = date = price = null;
                                break;
                            case 17:
                                db.Add(new Buch
                                {
                                    Author = author,
                                    Title = title,
                                    EAN = ean,
                                    Publisher = publisher,
                                    Date = date,
                                    Price = price
                                });
                                db.SaveChanges();
                                author = title = ean = publisher = date = price = null;
                                break;
                        }
                    }
                }
            }
        }
                
        
        public class DbUpdateException : Exception { }

    }
}

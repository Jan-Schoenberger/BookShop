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
        public int check;
        public int help;
        public int counter;
        public string cate;
        public string forma;
        private string author, title, ean, publisher, date, price;
        private static void Main()
        {
            Program prog = new Program();
            prog.ReadData();
            prog.ShowData();
            prog.InputData();
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
                Console.WriteLine("--------------------------------");
                Console.WriteLine();
                Console.WriteLine("\n----- Anzahl der Buecher -----");
                Console.WriteLine($" - {db.Books.Count()} Buecher in unsere Bibliothek");
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
        public void InputData()
        {
            Program prog = new Program();


            Console.WriteLine("****Buchladen****");
            Console.WriteLine();
            Console.WriteLine("Möchten Sie registirieren oder sich anmelden?");
            Console.WriteLine();

            Console.Write("Registrierung (R), Anmeldung (A)");
            string input = Console.ReadLine();
            input.ToLower();

            if (input == "R" || input == "r")
            {
                Console.WriteLine("-------Registrierung-------");
                Console.Write("Gender M/F: ");
                string gender = Console.ReadLine();

                Console.Write("Vorname: ");
                string firstname = Console.ReadLine();

                Console.Write("Nachname: ");
                string lastname = Console.ReadLine();

                Console.Write("Straße: ");
                string steet = Console.ReadLine();

                Console.Write("PLZ: ");
                string plz = Console.ReadLine();

                Console.Write("Stadt: ");
                string city = Console.ReadLine();

                Console.Write("Email: ");
                string email = Console.ReadLine();

                Console.Write("Benutzername: ");
                string username = Console.ReadLine();

                Console.Write("Passwort: ");
                string password = Console.ReadLine();

                Console.Write("Geburtsdatum: ");
                string Bday = Console.ReadLine();


                // fügen wir die neue Mitglieder zu unsere Daten bank ein 
                prog.AddNewPerson(gender, lastname, firstname, steet, plz, city, email, username, password, Bday);

            }
            else
            {
                Console.WriteLine("-------Anmeldung-------");
                Console.WriteLine();
                Console.Write("Email: ");
                string email = Console.ReadLine();
                Console.Write("Benutzername: ");
                string user = Console.ReadLine();
                Console.Write("Passwort : ");
                string passwort = Console.ReadLine();
                prog.CheckData(email, user, password);
                // prüfen wir, ob die Daten richtig sind

            }

        }
        public void ReadData()
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
                    string[] splitLine = line.Split(",".ToCharArray());
                    //foreach (string s in zerlegendeLine)
                    //{
                    //    Console.WriteLine(s);
                    //}
                    for (int i = 1; i < splitLine.Length; i++)
                    {
                        db.Add(new Person
                        {
                            // PersonId = i,
                            PersonGender = splitLine[i],
                            PersonName = splitLine[i++],
                            PersonVorname = splitLine[i++],
                            PersonStraße = splitLine[i++],
                            PersonPLZ = splitLine[i++],
                            PersonStadt = splitLine[i++],
                            PersonEmail = splitLine[i++],
                            PersonUser = splitLine[i++],
                            PersonPasswort = splitLine[i++],
                            PersonBirthay = splitLine[i++]

                        });
                        db.SaveChanges();

                        // PS soll ich ein andere daten banl tabelle erstellen um (Person, Bucher) daten zu speichern
                    }
                }
                while ((line = file2.ReadLine()) != null)
                {
                    if (line != "### titles by category and format")
                    {
                        string[] splitLine2 = line.Split(":".ToCharArray());

                        for (int i = 0; i < splitLine2.Length; i++)
                        {
                            if (splitLine2[i] == "CATEGORY")
                            {
                                db.Add(new Category
                                {
                                    KategorieArt = splitLine2[1]

                                });
                                db.SaveChanges();
                            }
                            else if (splitLine2[i] == "FORMAT")
                            {
                                db.Add(new Format
                                {
                                    FormatArt =splitLine2[1]
                                });
                                db.SaveChanges();

                            }

                           

                        }


                    }
                    else
                    {
                        FillBookTable(line);
                    }
                    //foreach (string s in zerlegendeLine2)
                    //{
                    //    Console.WriteLine(s);
                    //}

                }

            }



        }
        public bool CheckData(string email, string user, string pass)
        {
            bool found = false;

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
                        found = true;
                    }

                }

                if (found is true)
                {
                    Console.WriteLine("Wilkommen");
                    Console.WriteLine("Möchten Sie Buch Kaufen K/ oder Ihre persönliche Profil zeigen Z/ ?");
                    string answere = Console.ReadLine();
                    answere.ToLower();
                    if (answere == "k")
                    {
                        // in diesem Schritt zeigen wir die Bibliothek, um Buecher zu kaufen

                    }
                    else if(answere == "z")
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

                    prog.InputData();


                }


                //var permail1 = db.Persons.Find(email);



            }

            return found;

        }
        public void AddNewPerson(string Geschlecht, string Name, string Vorname, string strasse, string plz, string stadt, string email, string benutzername, string pass, string geburtsdatum)
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

            prog.InputData();
        }
        public int CategoryFormatHelp(string cat, string form)
        {

            using (var db = new BookshoppingContext())
            {

                if (cat == "Belletristik" && form == "Hardcover")
                {
                    check = 1;
                }
                else if (cat == "Sachbuch" && form == "Hardcover")
                {
                    check = 2;
                }
                else if (cat == "Belletristik" && form == "Paperback")
                {
                    check = 3;
                }
                else if (cat == "Sachbuch" && form == "Paperback")
                {
                    check = 4;
                }
                else if (cat == "Sachbuch" && form == "Taschenbuch")
                {
                    check = 5;
                }
                else if (cat == "Sachbuch" && form == "Buch")
                {
                    check = 6;
                }
                else if (cat == "Bilderbuch" && form == "Buch")
                {
                    check = 7;
                }
                else if (cat == "Kinderbücher" && form == "Buch")
                {
                    check = 8;
                }
                else if (cat == "Sachbuch" && form == "CD")
                {
                    check = 9;
                }
                else if (cat == "Belletristik" && form == "CD")
                {
                    check = 10;
                }
                else if (cat == "Kinder & Jugend" && form == "CD")
                {
                    check = 11;
                }
                else if (cat == "Spielfilm" && form == "DVD")
                {
                    check = 12;
                }
                else if (cat == "TV & Hobby" && form == "DVD")
                {
                    check = 13;
                }
                else if (cat == "Leben & Gesundheit" && form == "Buch")
                {
                    check = 14;
                }
                else if (cat == "Essen & Trinken" && form == "Buch")
                {
                    check = 15;
                }
                else if (cat == "Natur & Garten" && form == "Buch")
                {
                    check = 16;
                }
                else if (cat == "Wirtschaft" && form == "Buch")
                {
                    check = 17;
                }
                help = check;
                return help;

            }

        }
        public void FillBookTable(string line)
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
                        hilf = pro.CategoryFormatHelp(cate, forma);
                    }
                    if (price != null)
                    {
                        switch (hilf)
                        {
                            case 1:
                                db.Add(new Book
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
                                db.Add(new Book
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
                                db.Add(new Book
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
                                db.Add(new Book
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
                                db.Add(new Book
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
                                db.Add(new Book
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
                                db.Add(new Book
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
                                db.Add(new Book
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
                                db.Add(new Book
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
                                db.Add(new Book
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
                                db.Add(new Book
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
                                db.Add(new Book
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
                                db.Add(new Book
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
                                db.Add(new Book
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
                                db.Add(new Book
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
                                db.Add(new Book
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
                                db.Add(new Book
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

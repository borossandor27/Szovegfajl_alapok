using System;
using System.Text;
using System.IO;
using System.Collections.Generic;

namespace Szovegfajl_alapok
{
    class Program
    {
        class Tanulo
        {
            /*
             * -- logikailag összetartozó adatok tárolására legrugalmasabb adatszerkezet
             */
            private string kod; //-- A tanuló kódja (négyjegyű szám)
            public string Kod
            {
                get
                {
                    return kod;
                }
                set
                {
                    int ertek;
                    if (int.TryParse(value, out ertek) && ertek >= 1000 && ertek <= 9999)
                    {
                        kod = value;
                    }
                    else
                    {
                        kod = "0000";
                    }
                }

            }
            private string nev;
            public string Nev //-- A tanuló neve  (min=3 && max=35)
            {
                get
                {
                    return nev;
                }
                set
                {
                    int hossz = value.Length;
                    if (hossz >= 3 && hossz <= 35)
                    {
                        nev = value;
                    }
                }
            }
            private DateTime szuletett; //-- születési idő (maximum 100 éves lehet)
            public string Szuletett
            {
                get
                {
                    return szuletett.ToString("yyyy.MM.dd");
                }
                set
                {
                    DateTime d = DateTime.Parse(value);

                    if (DateTime.TryParse(value, out d) && d.Year >= DateTime.Today.Year - 100 && d.Year <= DateTime.Today.Year)
                    {
                        szuletett = DateTime.Parse(value);
                    }
                    else
                    {
                        szuletett = new DateTime();
                    }
                }
            }

            public string getTanulo()
            {
                return string.Join(";", this.Kod, this.Nev, this.Szuletett);
            }

            public Tanulo(string a, string b, string c)
            {
                this.Kod = a;
                this.Nev = b;
                this.Szuletett = c;
            }
        }
        static void Main(string[] args)
        {
            /*
             * -- Szövegfájlba írás ----
             */

            List<Tanulo> tanulok = new List<Tanulo>();              //-- adatok a gyakorláshoz
            tanulok.Add(new Tanulo("1000", "Béla", "2018-11-11"));
            tanulok.Add(new Tanulo("1001", "Ferenc", "2009-01-07"));
            tanulok.Add(new Tanulo("1002", "Ágnes", "2008-09-01"));
            tanulok.Add(new Tanulo("1003", "Ibolya", "2008-10-06"));
            tanulok.Add(new Tanulo("1004", "Aladár", "2008-12-15"));
            //-- gyakorló adatok kiiratása ---
            foreach (Tanulo item in tanulok)
            {
                Console.WriteLine("{0,-8} {1,-20} {2,15}", item.Kod, item.Nev, item.Szuletett);
            }
            string fajl = "tanulok.txt"; //-- ha nincs elérési út, akkor az exe állomány mappájában hozza létre

            //-- kiiras fájlba ---
            string TeljesEleresiUt = Directory.GetCurrentDirectory() + @"\" + fajl;
            Console.WriteLine("Adatok kiírása {0} fájlba...", TeljesEleresiUt);
            StreamWriter sw = null;
            try
            {
                using (sw = new StreamWriter(fajl))
                {
                    foreach (Tanulo item in tanulok)
                    {
                        sw.WriteLine(item.getTanulo());
                    }
                    sw.Close();
                }
                Console.WriteLine("Szövegfájl létrehozás vége.");
            }
            catch (Exception e)
            {
                Console.WriteLine("{0} hiba a {1} fájl létrehozása közben!", e.Message, TeljesEleresiUt);
                throw;
            }
            finally
            {
                if (sw != null)
                {
                    sw.Flush();
                    sw.Close();
                    sw.Dispose();
                }
            }
            Console.WriteLine("Program vége");
            Console.ReadKey();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Wpf_Steuerprogramm
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {

            Schraube Bolt = new Schraube();
            Bolt.Gewindeart = rb_Gewindeart();
            Bolt.Durchmesser = Durchmesserauswahl(Bolt.MetrischeTabelle(), Bolt.WhitworthTabelle(), Bolt.Gewindeart);
            Bolt.Gewindelänge = Convert.ToDouble(txtBox_Gewindelänge.Text);
            Bolt.Schaftlänge = Convert.ToDouble(txtBox_Schaftlänge.Text);
            Bolt.Kopf = rb_Kopf();
            Bolt.Material = rb_Material();
            (Bolt.Streckgrenze, Bolt.Zugfestigkeit) = Festigkeiten(Bolt.Material);
            Bolt.Festigkeitsklasse = Festigkeitsklasse();



            if (Bolt.Gewindeart == 1)

            {
                txtBox_Steigung.Text = Convert.ToString(Konsolenprogramm.BerechnungSteigung(Bolt.MetrischeTabelle(), Bolt.Durchmesser));
                Bolt.Steigung = Convert.ToDouble(txtBox_Steigung.Text);
            }
            if (Bolt.Gewindeart == 2)
            {
                Bolt.Steigung = Convert.ToDouble(txtBox_Steigung.Text);
            }
            if (Bolt.Gewindeart == 3)
            {
                (Bolt.Gangzahl, Bolt.Steigung) = Konsolenprogramm.BerechnungWitworthSteigung(Bolt.WhitworthTabelle(), Bolt.Durchmesser);
                txtBox_Steigung.Text = Convert.ToString(Bolt.Steigung);
                Bolt.Steigung = Convert.ToDouble(txtBox_Steigung.Text);

            }

            // Ausgabeparameter
            //double DurchmesserKernloch = Konsolenprogramm.BerechnungKernlochbohrung(Bolt.Durchmesser, Bolt.Steigung, Bolt.MetrischeTabelle(), Bolt.Gewindeart, Bolt.WhitworthTabelle());
            //double Schlüsselweite = Konsolenprogramm.AusgabeSchlüsselweite(Bolt.Kopf, Bolt.Durchmesser, Bolt.MetrischeTabelle());
            //double Kopfdurchmesser = Konsolenprogramm.AusgabeKopfdurchmesser(Bolt.Kopf, Bolt.Durchmesser, Bolt.MetrischeTabelle());
            //double Kopfhöhe = Konsolenprogramm.AusgabeKopfhöhe(Bolt.Kopf, Bolt.Durchmesser, Bolt.MetrischeTabelle());
            //double Durchgangsbohrung = Konsolenprogramm.BerechnungDurchgangsbohrung(Bolt.MetrischeTabelle(), Bolt.Durchmesser);
            //double Senkdurchmesser = Konsolenprogramm.BerechnungSenkdurchmesser(Bolt.MetrischeTabelle(), Bolt.Durchmesser);
            //double DurchmesserKegelsenkung = Konsolenprogramm.BerechnungDurchmesserKegelsenkung(Bolt.MetrischeTabelle(), Bolt.Durchmesser);
            //double MaxBelastung = Konsolenprogramm.BerechnungMaxBelastung(Bolt.Durchmesser, Bolt.Steigung, Bolt.MetrischeTabelle(), Bolt.Streckgrenze, Bolt.Gewindeart, Bolt.WhitworthTabelle());
            //string WhitworthDurchmesser = Konsolenprogramm.AusgabeWitworthdurchmesser(Bolt.WhitworthTabelle(), Bolt.Durchmesser);
            //string WhitworthFlankendurchmesser = Konsolenprogramm.AusgabeWitworthflankendurchmesser(Bolt.WhitworthTabelle(), Bolt.Durchmesser);

            Bolt.Schlüsselweite = Konsolenprogramm.AusgabeSchlüsselweite(Bolt.Kopf, Bolt.Durchmesser, Bolt.MetrischeTabelle());
            Bolt.Kernlochdurchmesser = Konsolenprogramm.BerechnungKernlochbohrung(Bolt.Durchmesser, Bolt.Steigung, Bolt.MetrischeTabelle(), Bolt.Gewindeart, Bolt.WhitworthTabelle());
            Bolt.Kopfhöhe = Konsolenprogramm.AusgabeKopfhöhe(Bolt.Kopf, Bolt.Durchmesser, Bolt.MetrischeTabelle());
            Bolt.Kopfdurchmesser = Konsolenprogramm.AusgabeKopfdurchmesser(Bolt.Kopf, Bolt.Durchmesser, Bolt.MetrischeTabelle());
            Bolt.Durchgangsbohrung = Konsolenprogramm.BerechnungDurchgangsbohrung(Bolt.MetrischeTabelle(), Bolt.Durchmesser);
            Bolt.Senkdurchmesser = Konsolenprogramm.BerechnungSenkdurchmesser(Bolt.MetrischeTabelle(), Bolt.Durchmesser);
            Bolt.DurchmesserKegelsenkung = Konsolenprogramm.BerechnungDurchmesserKegelsenkung(Bolt.MetrischeTabelle(), Bolt.Durchmesser);
            Bolt.MaxBelastung = Konsolenprogramm.BerechnungMaxBelastung(Bolt.Durchmesser, Bolt.Steigung, Bolt.MetrischeTabelle(), Bolt.Streckgrenze, Bolt.Gewindeart, Bolt.WhitworthTabelle());
            Bolt.WhitworthDurchmesser = Konsolenprogramm.AusgabeWitworthdurchmesser(Bolt.WhitworthTabelle(), Bolt.Durchmesser);
            Bolt.WhitworthFlankendurchmesser = Konsolenprogramm.AusgabeWitworthflankendurchmesser(Bolt.WhitworthTabelle(), Bolt.Durchmesser);

            // Ausgabe im Label

            lbl_Ausgabe.Content = "Kernlochdurchmesser: " + Bolt.Kernlochdurchmesser + "     Schlüsselweite: " + Bolt.Schlüsselweite + "    Durchmesser: " + Bolt.Durchmesser + "    Kopf: " + Bolt.Kopf + "    Kopfhöhe: " + Bolt.Kopfhöhe +
                "\nSteigung: " + Bolt.Steigung;
        }


        //Eingabemethoden

        public int rb_Gewindeart()
        {
            int Gewindeart = 0;

            if (rb_MetrischStandard.IsChecked == true)
            {
                Gewindeart = 1;
            }

            if (rb_MetrischFein.IsChecked == true)
            {
                Gewindeart = 2;
            }

            if (rb_Whitworth.IsChecked == true)
            {
                Gewindeart = 3;
            }

            return Gewindeart;
        }


        public int rb_Kopf()
        {
            int Kopf = 0;

            if (rb_Sechskant.IsChecked == true)
            {
                Kopf = 1;
            }

            if (rb_Zylinderkopf.IsChecked == true)
            {
                Kopf = 2;
            }

            if (rb_Senkkopf.IsChecked == true)
            {
                Kopf = 3;
            }

            return Kopf;
        }

        public int rb_Material()
        {
            int Material = 0;

            if (rb_8_8.IsChecked == true)
            {
                Material = 1;
            }

            if (rb_10_9.IsChecked == true)
            {
                Material = 2;
            }

            if (rb_12_9.IsChecked == true)
            {
                Material = 3;
            }

            if (rb_A4_50.IsChecked == true)
            {
                Material = 4;
            }

            return Material;
        }

        public string Festigkeitsklasse()
        {
            string Festigkeitsklasse = "";

            if (rb_8_8.IsChecked == true)
            {
                Festigkeitsklasse = "8.8";
            }

            if (rb_10_9.IsChecked == true)
            {
                Festigkeitsklasse = "10.9";
            }

            if (rb_12_9.IsChecked == true)
            {
                Festigkeitsklasse = "12.9";
            }

            if (rb_A4_50.IsChecked == true)
            {
                Festigkeitsklasse = "A4-50";
            }

            return Festigkeitsklasse;
        }

        public (double, double) Festigkeiten(int Material)
        {
            double Streckgrenze = 0;
            double Zugfestigkeit = 0;

            if (Material == 1)
            {
                Streckgrenze = 640;
                Zugfestigkeit = 800;
            }

            if (Material == 2)
            {
                Streckgrenze = 900;
                Zugfestigkeit = 1000;
            }

            if (Material == 3)
            {
                Streckgrenze = 1080;
                Zugfestigkeit = 1200;
            }

            if (Material == 4)
            {
                Streckgrenze = 210;
                Zugfestigkeit = 500;
            }

            return (Streckgrenze, Zugfestigkeit);

        }


        public void MetrischFeinChecked(object sender, RoutedEventArgs e)
        {
            txtBox_Steigung.IsEnabled = true;
        }

        public void MetrischFeinUnchecked(object sender, RoutedEventArgs e)
        {

            txtBox_Steigung.IsEnabled = false;
        }


        public double Durchmesserauswahl(double[,] MetrischeTabelle, string[,] WhitworthTabelle, int Gewindeart)
        {
            double Durchmesser = 0;

            int de = 0;

            if (Gewindeart == 1 | Gewindeart == 2)
            {
                de = cmbBox_Metrisch.SelectedIndex;
                Durchmesser = MetrischeTabelle[de, 0];
            }

            else
            {
                de = cmbBox_Whitworth.SelectedIndex;
                string ausgabe = WhitworthTabelle[de, 0];
                Durchmesser = Double.Parse(WhitworthTabelle[de, 1]);
            }
            return Durchmesser;
        }

        private void WhitworthChecked(object sender, RoutedEventArgs e)
        {
            cmbBox_Metrisch.Visibility = Visibility.Hidden;
            cmbBox_Whitworth.Visibility = Visibility.Visible;
        }

        private void WhitworthUnchecked(object sender, RoutedEventArgs e)
        {
            cmbBox_Metrisch.Visibility = Visibility.Visible;
            cmbBox_Whitworth.Visibility = Visibility.Hidden;
        }
    }







    //Berechnungmethoden

    class Konsolenprogramm
    {

        static public string SchraubenbezeichnungMX(int Gewindeart, int Kopf, double Durchmesser, double Gesamtlänge, string Festigkeitsklasse)
        {
            string schraubenbezeichnung = "";
            string gewindetyp = "";
            string bezugsnorm = "";
            string schraubenname = "";



            if (Kopf == 1 & Gewindeart == 1)//Sechskant Regelgewinde
            {
                schraubenname = "Sechskantschraube ";
                bezugsnorm = "DIN EN ISO 4014 ";
                gewindetyp = "M";
            }


            if (Kopf == 2 & Gewindeart == 1)//Zylinder Regelgewinde
            {
                schraubenname = "Zylinderschraube ";
                bezugsnorm = "DIN EN ISO 4762 ";
                gewindetyp = "M";
            }


            if (Kopf == 3 & Gewindeart == 1)//Senkschraube Regelgewinde
            {
                schraubenname = "Senkschraube ";
                bezugsnorm = "DIN EN ISO 10642 ";
                gewindetyp = "M";

            }


            schraubenbezeichnung = schraubenname + bezugsnorm + "- " + gewindetyp + Durchmesser + " x " + Gesamtlänge + " - " + Festigkeitsklasse;
            return schraubenbezeichnung;
        }


        static public string SchraubenbezeichnungMF(int Gewindeart, int Kopf, double Durchmesser, double Steigung, double Gesamtlänge, string Festigkeitsklasse)
        {
            string schraubenbezeichnung = "";
            string gewindetyp = "";
            string bezugsnorm = "";
            string schraubenname = "";



            if (Kopf == 1 & Gewindeart == 2)//Sechskant Feingewinde
            {
                schraubenname = "Sechskantschraube ";
                bezugsnorm = "DIN EN ISO 8765 ";
                gewindetyp = "M";

            }



            if (Kopf == 2 & Gewindeart == 2)//Zylinder Feingewinde
            {
                schraubenname = "Zylinderschraube ";
                bezugsnorm = "DIN 34821 ";
                gewindetyp = "M";
            }


            if (Kopf == 3 & Gewindeart == 2)//Senkschraube Feingewinde
            {
                schraubenname = "Senkschraube ";
                gewindetyp = "M";

            }


            schraubenbezeichnung = schraubenname + bezugsnorm + "- " + gewindetyp + Durchmesser + " x " + Steigung + " x " + Gesamtlänge + " - " + Festigkeitsklasse;
            return schraubenbezeichnung;
        }

        static public double PreisberechnungMX(double durchmessereingabe, double laengenausgabegewinde, double laengenausgabeschaft, double dichte)
        {
            double preisMX = 0.08; // z.B. 0,08 €/g
            Console.WriteLine(" Steigung des metrischen Regelgewindes:  " + BerechnungSteigung(Tabellen(), durchmessereingabe) + " mm");

            double gewindevolumenMX = Math.Round(GewindevolumenMX(durchmessereingabe, laengenausgabegewinde), 2);
            //Console.WriteLine("Gewindevolumen: " + gewindevolumenMX + " mm^3");

            double schaftvolumenMX = Math.Round(SchaftvolumenMX(durchmessereingabe, laengenausgabeschaft), 2);
            //Console.WriteLine("Schaftvolumen: " + schaftvolumenMX + " mm^3");

            double gewindemasseMX = Math.Round(dichte * gewindevolumenMX, 2);
            //Console.WriteLine("Gewindemasse im preis: " + gewindemasseMX + " g");

            double schaftmasseMX = Math.Round(dichte * schaftvolumenMX, 2);

            //Console.WriteLine("Schaftmasse: " + schaftmasseMX + " g");

            double gewindepreisMX = Math.Round(preisMX * gewindemasseMX, 2);
            double schaftpreisMX = Math.Round(preisMX * schaftmasseMX, 2);

            double gesamtpreisMX = Math.Round(gewindepreisMX + schaftpreisMX, 2);

            //Console.WriteLine("Gewindepreis: " + gesamtpreisMX + " Euro");

            return gesamtpreisMX;
        }

        static public double PreisberechnungWW(double durchmessereingabe, double laengenausgabegewinde, double laengenausgabeschaft, double dichte)
        {
            double preisWW = 0.4; // z.B. 0,1€/g
            (double Gangzahl, double WitworthSteigung) = BerechnungWitworthSteigung(WitworthTabelle(), durchmessereingabe);
            Console.WriteLine(" Gangzahl des Whitworth-Gewindes:  " + Gangzahl);
            Console.WriteLine(" Steigung des Whitworth-Gewindes:  " + string.Format("{0:0.00}", WitworthSteigung) + " mm");

            double gewindevolumenWW = Math.Round(GewindevolumenWW(durchmessereingabe, laengenausgabegewinde, WitworthSteigung), 2);
            //Console.WriteLine("Gewindevolumen: "+ gewindevolumenWW+" mm^3");

            double schaftvolumenWW = Math.Round(SchaftvolumenWW(durchmessereingabe, laengenausgabeschaft), 2);
            //Console.WriteLine("Schaftvolumen: " + schaftvolumenWW + " mm^3");

            double gewindemasseWW = Math.Round(dichte * gewindevolumenWW, 2);
            //Console.WriteLine("Gewindemasse: "+gewindemasseWW+" g");

            double schaftmasseWW = Math.Round(dichte * schaftvolumenWW, 2);
            //Console.WriteLine("Schaftmasse: " + schaftmasseWW + " g");

            double gewindepreisWW = Math.Round(preisWW * gewindemasseWW, 2);
            double schaftpreisWW = Math.Round(preisWW * schaftmasseWW, 2);

            double gesamtpreisWW = Math.Round(gewindepreisWW + schaftpreisWW, 2);

            //Console.WriteLine("Gewindepreis: " + gesamtpreisWW + " Euro");

            return gesamtpreisWW;

        }

        static public double PreisberechnungSechskant(double schlüsselweiteAusgabe, double kopfhöhe, double dichte)
        {
            double preisSechskant = 0.5;
            double kopfpreis = Sechskantvolumen(schlüsselweiteAusgabe, kopfhöhe) * dichte * preisSechskant;
            return kopfpreis;
        }

        static public double PreisberechnungZylinder(int Schraubenkopfnummer, double durchmessereingabe, double kopfhöhe, double dichte)
        {
            double preisZylinder = 0.2;
            double kopfdurchmesserZylinder = AusgabeKopfdurchmesser(Schraubenkopfnummer, durchmessereingabe, Tabellen());
            Console.WriteLine(" Kopfdurchmesser: " + kopfdurchmesserZylinder + " mm");

            double kopfpreisZylinder = Zylindervolumen(kopfdurchmesserZylinder, kopfhöhe) * dichte * preisZylinder;
            return kopfpreisZylinder;
        }

        static public double PreisberechnungSenkkopf(int Schraubenkopfnummer, double durchmessereingabe, double kopfhöhe, double dichte)
        {
            double preisSenkkopf = 0.6;
            double kopfdurchmesserSenkkopf = AusgabeKopfdurchmesser(Schraubenkopfnummer, durchmessereingabe, Tabellen());
            Console.WriteLine(" Kopfdurchmesser: " + kopfdurchmesserSenkkopf + " mm");

            double kopfpreisSenkkopf = Senkkopfvolumen(kopfdurchmesserSenkkopf, kopfhöhe, durchmessereingabe) * dichte * preisSenkkopf;

            return kopfpreisSenkkopf;
        }

        static public double PreisberechnungMF(double steigung, double durchmessereingabe, double laengenausgabegewinde, double laengenausgabeschaft, double dichte)
        {
            double preisMF = 0.12; // z.B. 0,12€/g
            Console.WriteLine(" Ausgewählte Steigung: " + steigung + " mm");

            double gewindevolumenMF = Math.Round(GewindevolumenMF(durchmessereingabe, laengenausgabegewinde, steigung), 2);
            //Console.WriteLine("Gewindevolumen: " + gewindevolumenMF + " mm^3");

            double schaftvolumenMF = Math.Round(SchaftvolumenMF(durchmessereingabe, laengenausgabeschaft), 2);
            //Console.WriteLine("Schaftvolumen: " + schaftvolumenMF + " mm^3");

            double gewindemasseMF = Math.Round(dichte * gewindevolumenMF, 2);
            //Console.WriteLine("Gewindemasse: " + gewindemasseMF + " g");

            double schaftmasseMF = Math.Round(dichte * schaftvolumenMF, 2);
            //Console.WriteLine("Schaftmasse: " + schaftmasseMF + " g");

            double gewindepreisMF = Math.Round(preisMF * gewindemasseMF, 2);
            double schaftpreisMF = Math.Round(preisMF * schaftmasseMF, 2);

            double gesamtpreisMF = Math.Round(gewindepreisMF + schaftpreisMF, 2);

            //Console.WriteLine("Gewindepreis: " + gesamtpreisMF + " Euro");

            return gesamtpreisMF;
        }

        static public double GewindevolumenMX(double durchmessereingabe, double laengenausgabegewinde)
        {
            double flankendurchmessre = durchmessereingabe - (0.6495 * (BerechnungSteigung(Tabellen(), durchmessereingabe)));
            double gewindevolumenMX = (Math.PI / 4) * (Math.Pow(flankendurchmessre, 2)) * laengenausgabegewinde;
            //Console.WriteLine("Flankendurchmesser: " + Math.Round(flankendurchmessre, 2) + " mm");
            return gewindevolumenMX;
        }

        static public double GewindemasseMX(double durchmessereingabe, double laengenausgabegewinde, double dichte)
        {
            double gewindemasseMX = Math.Round(GewindevolumenMX(durchmessereingabe, laengenausgabegewinde), 2) * dichte;
            return gewindemasseMX;
        }

        static public double GewindevolumenMF(double durchmessereingabe, double laengenausgabegewinde, double steigung)
        {
            double flankendurchmessre = durchmessereingabe - (0.6495 * steigung);
            double gewindevolumenMF = (Math.PI / 4) * (Math.Pow(flankendurchmessre, 2)) * laengenausgabegewinde;
            //Console.WriteLine("Flankendurchmesser: " + Math.Round(flankendurchmessre, 2) + " mm");
            return gewindevolumenMF;
        }

        static public double GewindevolumenWW(double durchmessereingabe, double laengenausgabegewinde, double WitworthSteigung)
        {
            string flankendurchmessre = AusgabeWitworthflankendurchmesser(WitworthTabelle(), durchmessereingabe);
            double flankendurchemesser = Convert.ToDouble(flankendurchmessre);
            //double flankendurchmessre = durchmessereingabe - (0.64 * WitworthSteigung);
            double gewindevolumenWW = (Math.PI / 4) * (Math.Pow(flankendurchemesser, 2)) * laengenausgabegewinde;
            //Console.WriteLine("Flankendurchmesser: "+Math.Round(flankendurchemesser,2)+" mm");
            return gewindevolumenWW;
        }


        static public double SchaftvolumenMX(double durchmessereingabe, double laengenausgabeschaft)
        {
            double schaftvolumenMX = (Math.PI / 4) * (Math.Pow(durchmessereingabe, 2)) * laengenausgabeschaft;

            return schaftvolumenMX;
        }

        static public double SchaftvolumenMF(double durchmessereingabe, double laengenausgabeschaft)
        {
            double schaftvolumenMF = (Math.PI / 4) * (Math.Pow(durchmessereingabe, 2)) * laengenausgabeschaft;

            return schaftvolumenMF;
        }

        static public double SchaftvolumenWW(double durchmessereingabe, double laengenausgabeschaft)
        {
            double schaftvolumenWW = (Math.PI / 4) * (Math.Pow(durchmessereingabe, 2)) * laengenausgabeschaft;

            return schaftvolumenWW;
        }

        static public double Sechskantvolumen(double schlüsselweiteAusgabe, double kopfhöhe)
        {
            double kopfvolumenSechsKant = Math.Round(kopfhöhe * 3 / 2 * schlüsselweiteAusgabe / 2 / 0.8660254038 * schlüsselweiteAusgabe, 2);

            return kopfvolumenSechsKant;
        }

        static public double Zylindervolumen(double kopfdurchmesserZylinder, double kopfhöhe)
        {
            double kopfvolumenZylinder = Math.Round(kopfhöhe * Math.PI / 4 * kopfdurchmesserZylinder * kopfdurchmesserZylinder, 2);

            return kopfvolumenZylinder;
        }

        static public double Senkkopfvolumen(double kopfdurchmesserSenkkopf, double kopfhöhe, double durchmessereingabe)
        {
            double kopfvolumenSenkKopf = Math.PI * kopfhöhe / 12 * (kopfdurchmesserSenkkopf * kopfdurchmesserSenkkopf + durchmessereingabe * durchmessereingabe + kopfdurchmesserSenkkopf * durchmessereingabe);
            //double kopfvolumenSenkKopf = (kopfdurchmesserSenkkopf+durchmessereingabe)/2*kopfhöhe;

            return kopfvolumenSenkKopf;
        }



















        static public double AusgabeSchlüsselweite(int Kopf, double Durchmesser, double[,] Tabelle)
        {
            double Schlüsselweite = 0;
            int jj = 0; // Variable die zum hochzählen verwendet werden soll
            int M = 0; // double der in der Tabelle steht in einen int umwandeln

            if (Kopf == 1)
            {
                for (jj = 0; jj <= 8; jj++) // durchsuchen der Tabelle nach dem richtigen Durchmesser
                {
                    M = Convert.ToInt32(Tabelle[jj, 0]); //umwandeln der Strings in der Tabelle in int

                    if (Durchmesser == M) // Vergleich ob in dem Tabellenfeld der gleiche Wert steht wie in der Eingabe
                    {
                        Schlüsselweite = Tabelle[jj, 7]; // Wert aus der Tabelle wird Durchgangsbohrung übergeben     
                    }
                }
            }


            if (Kopf == 2)
            {
                for (jj = 0; jj <= 8; jj++) // durchsuchen der Tabelle nach dem richtigen Durchmesser
                {
                    M = Convert.ToInt32(Tabelle[jj, 0]); //umwandeln der Strings in der Tabelle in int

                    if (Durchmesser == M) // Vergleich ob in dem Tabellenfeld der gleiche Wert steht wie in der Eingabe
                    {
                        Schlüsselweite = Tabelle[jj, 9]; // Wert aus der Tabelle wird Durchgangsbohrung übergeben     
                    }
                }
            }


            if (Kopf == 3)
            {
                for (jj = 0; jj <= 8; jj++) // durchsuchen der Tabelle nach dem richtigen Durchmesser
                {
                    M = Convert.ToInt32(Tabelle[jj, 0]); //umwandeln der Strings in der Tabelle in int

                    if (Durchmesser == M) // Vergleich ob in dem Tabellenfeld der gleiche Wert steht wie in der Eingabe
                    {
                        Schlüsselweite = Tabelle[jj, 11]; // Wert aus der Tabelle wird Durchgangsbohrung übergeben     
                    }
                }
            }

            return Schlüsselweite;
        }


        static public double AusgabeKopfdurchmesser(int Kopf, double Durchmesser, double[,] Tabelle)
        {
            double kopfdurchmesser = 0;
            int jj = 0; // Variable die zum hochzählen verwendet werden soll
            int M = 0; // double der in der Tabelle steht in einen int umwandeln



            if (Kopf == 2)//Zylinder
            {
                for (jj = 0; jj <= 8; jj++) // durchsuchen der Tabelle nach dem richtigen Durchmesser
                {
                    M = Convert.ToInt32(Tabelle[jj, 0]); //umwandeln der Strings in der Tabelle in int

                    if (Durchmesser == M) // Vergleich ob in dem Tabellenfeld der gleiche Wert steht wie in der Eingabe
                    {
                        kopfdurchmesser = Tabelle[jj, 8]; // Wert aus der Tabelle wird Durchgangsbohrung übergeben     
                    }
                }
            }


            if (Kopf == 3) //Senkkopf
            {
                for (jj = 0; jj <= 8; jj++) // durchsuchen der Tabelle nach dem richtigen Durchmesser
                {
                    M = Convert.ToInt32(Tabelle[jj, 0]); //umwandeln der Strings in der Tabelle in int

                    if (Durchmesser == M) // Vergleich ob in dem Tabellenfeld der gleiche Wert steht wie in der Eingabe
                    {
                        kopfdurchmesser = Tabelle[jj, 12]; // Wert aus der Tabelle wird Durchgangsbohrung übergeben     
                    }
                }
            }

            return kopfdurchmesser;
        }


        static public double AusgabeKopfhöhe(int Kopf, double Durchmesser, double[,] Tabelle)
        {
            double Kopfhöhe = 0;
            int jj = 0; // Variable die zum hochzählen verwendet werden soll
            int M = 0; // double der in der Tabelle steht in einen int umwandeln



            if (Kopf == 1) //Sechskantkopf
            {
                for (jj = 0; jj <= 8; jj++) // durchsuchen der Tabelle nach dem richtigen Durchmesser
                {
                    M = Convert.ToInt32(Tabelle[jj, 0]); //umwandeln der Strings in der Tabelle in int

                    if (Durchmesser == M) // Vergleich ob in dem Tabellenfeld der gleiche Wert steht wie in der Eingabe
                    {
                        Kopfhöhe = Tabelle[jj, 6]; // Wert aus der Tabelle wird Durchgangsbohrung übergeben     
                    }
                }
            }


            if (Kopf == 2) //Zylinderkopf
            {
                for (jj = 0; jj <= 8; jj++) // durchsuchen der Tabelle nach dem richtigen Durchmesser
                {
                    M = Convert.ToInt32(Tabelle[jj, 0]); //umwandeln der Strings in der Tabelle in int

                    if (Durchmesser == M) // Vergleich ob in dem Tabellenfeld der gleiche Wert steht wie in der Eingabe
                    {
                        Kopfhöhe = Tabelle[jj, 0]; // Wert aus der Tabelle wird Durchgangsbohrung übergeben     
                    }
                }
            }


            if (Kopf == 3) //Senkkopf
            {
                for (jj = 0; jj <= 8; jj++) // durchsuchen der Tabelle nach dem richtigen Durchmesser
                {
                    M = Convert.ToInt32(Tabelle[jj, 0]); //umwandeln der Strings in der Tabelle in int

                    if (Durchmesser == M) // Vergleich ob in dem Tabellenfeld der gleiche Wert steht wie in der Eingabe
                    {
                        Kopfhöhe = Tabelle[jj, 10]; // Wert aus der Tabelle wird Durchgangsbohrung übergeben     
                    }
                }
            }

            return Kopfhöhe;
        }




        static public double BerechnungDurchgangsbohrung(double[,] Tabelle, double Durchmesser)
        {

            //Duchgangbohrung Durchmesser
            double Durchgangsbohrung = 0; // Wert der am Ende ausgegeben werden soll
            int jj = 0; // Variable die zum hochzählen verwendet werden soll
            int M = 0; // double der in der Tabelle steht in einen int umwandeln
            for (jj = 0; jj <= 8; jj++) // durchsuchen der Tabelle nach dem richtigen Durchmesser
            {
                M = Convert.ToInt32(Tabelle[jj, 0]); //umwandeln der Strings in der Tabelle in int

                if (Durchmesser == M) // Vergleich ob in dem Tabellenfeld der gleiche Wert steht wie in der Eingabe
                {
                    Durchgangsbohrung = Tabelle[jj, 1]; // Wert aus der Tabelle wird Durchgangsbohrung übergeben     
                }
            }
            return Durchgangsbohrung;

            // Wenn keine Übereinstimmung gefunden wurde sollte noch eine Meldung ausgegeben werden  
        }


        static public double BerechnungSteigung(double[,] Tabelle, double Durchmesser) // 
        {

            //Duchgangbohrung Durchmesser
            double Steigung = 0; // Wert der am Ende ausgegeben werden soll
            int jj = 0; // Variable die zum hochzählen verwendet werden soll
            int M = 0; // double der in der Tabelle steht in einen int umwandeln
            for (jj = 0; jj <= 8; jj++) // durchsuchen der Tabelle nach dem richtigen Durchmesser
            {
                M = Convert.ToInt32(Tabelle[jj, 0]); //umwandeln der Strings in der Tabelle in int

                if (Durchmesser == M) // Vergleich ob in dem Tabellenfeld der gleiche Wert steht wie in der Eingabe
                {
                    Steigung = Tabelle[jj, 5]; // Wert aus der Tabelle wird übergeben     
                }
            }

            return Steigung;

            // Wenn keine Übereinstimmung gefunden wurde sollte noch eine Meldung ausgegeben werden  
        }


        static public double BerechnungSenkdurchmesser(double[,] Tabelle, double Durchmesser) // 
        {

            //Duchgangbohrung Durchmesser
            double Senkdurchmesser = 0; // Wert der am Ende ausgegeben werden soll
            int jj = 0; // Variable die zum hochzählen verwendet werden soll
            int M = 0; // double der in der Tabelle steht in einen int umwandeln
            for (jj = 0; jj <= 8; jj++) // durchsuchen der Tabelle nach dem richtigen Durchmesser
            {
                M = Convert.ToInt32(Tabelle[jj, 0]); //umwandeln der Strings in der Tabelle in int

                if (Durchmesser == M) // Vergleich ob in dem Tabellenfeld der gleiche Wert steht wie in der Eingabe
                {
                    Senkdurchmesser = Tabelle[jj, 2]; // Wert aus der Tabelle wird übergeben
                }
            }
            return Senkdurchmesser;

            // Wenn keine Übereinstimmung gefunden wurde sollte noch eine Meldung ausgegeben werden  
        }


        static public double BerechnungSenktiefe(double[,] Tabelle, double Durchmesser) // 
        {

            //Duchgangbohrung Durchmesser
            double Senktiefe = 0; // Wert der am Ende ausgegeben werden soll
            int jj = 0; // Variable die zum hochzählen verwendet werden soll
            int M = 0; // double der in der Tabelle steht in einen int umwandeln
            for (jj = 0; jj <= 8; jj++) // durchsuchen der Tabelle nach dem richtigen Durchmesser
            {
                M = Convert.ToInt32(Tabelle[jj, 0]); //umwandeln der Strings in der Tabelle in int

                if (Durchmesser == M) // Vergleich ob in dem Tabellenfeld der gleiche Wert steht wie in der Eingabe
                {
                    Senktiefe = Tabelle[jj, 3]; // Wert aus der Tabelle wird übergeben
                }
            }
            return Senktiefe;

            // Wenn keine Übereinstimmung gefunden wurde sollte noch eine Meldung ausgegeben werden  
        }


        static public double BerechnungDurchmesserKegelsenkung(double[,] Tabelle, double Durchmesser) // 
        {

            //Duchgangbohrung Durchmesser
            double DurchmesserKegelsenkung = 0; // Wert der am Ende ausgegeben werden soll
            int jj = 0; // Variable die zum hochzählen verwendet werden soll
            int M = 0; // double der in der Tabelle steht in einen int umwandeln
            for (jj = 0; jj <= 8; jj++) // durchsuchen der Tabelle nach dem richtigen Durchmesser
            {
                M = Convert.ToInt32(Tabelle[jj, 0]); //umwandeln der Strings in der Tabelle in int

                if (Durchmesser == M) // Vergleich ob in dem Tabellenfeld der gleiche Wert steht wie in der Eingabe
                {
                    DurchmesserKegelsenkung = Tabelle[jj, 4]; // Wert aus der Tabelle wird übergeben
                }
            }
            return DurchmesserKegelsenkung;

            // Wenn keine Übereinstimmung gefunden wurde sollte noch eine Meldung ausgegeben werden  
        }



        static public double BerechnungKernlochbohrung(double Durchmesser, double Steigung, double[,] Tabelle, int Gewindeauswahl, string[,] Witworth)
        {
            int jj = 0;
            double M = 0;
            double Kerndurchmesser = 0;

            //Für metrische Gewinde
            if (Gewindeauswahl == 1 | Gewindeauswahl == 2)
            {
                if (Steigung == 0)
                {
                    for (jj = 0; jj <= 8; jj++) // durchsuchen der Tabelle nach dem richtigen Durchmesser
                    {
                        M = Convert.ToDouble(Tabelle[jj, 0]); //umwandeln der Strings in der Tabelle in int

                        if (Durchmesser == M) // Vergleich ob in dem Tabellenfeld der gleiche Wert steht wie in der Eingabe
                        {
                            Steigung = Tabelle[jj, 5]; // Wert aus der Tabelle wird übergeben
                        }
                    }
                }


                Kerndurchmesser = Durchmesser - Steigung;
            }

            //Für Whitworth Gewinde
            else
            {
                for (jj = 0; jj <= 7; jj++) // durchsuchen der Tabelle nach dem richtigen Durchmesser
                {
                    M = Convert.ToDouble(Witworth[jj, 1]); //umwandeln der Strings in der Tabelle in int

                    if (Durchmesser == M) // Vergleich ob in dem Tabellenfeld der gleiche Wert steht wie in der Eingabe
                    {
                        Kerndurchmesser = Convert.ToDouble(Witworth[jj, 4]); // Wert aus der Tabelle wird übergeben
                    }
                }
            }
            return Kerndurchmesser;
        }



        static public double BerechnungMaxBelastung(double Durchmesser, double Steigung, double[,] Tabelle, double Streckgrenze, int Gewindeart, string[,] Witworth)
        {
            int jj = 0;
            double M = 0;
            double MaxBelastung = 0;
            if (Gewindeart == 1)
            {

                for (jj = 0; jj <= 8; jj++) // durchsuchen der Tabelle nach dem richtigen Durchmesser
                {
                    M = Convert.ToDouble(Tabelle[jj, 0]); //umwandeln der Strings in der Tabelle in int

                    if (Durchmesser == M) // Vergleich ob in dem Tabellenfeld der gleiche Wert steht wie in der Eingabe
                    {
                        Steigung = Tabelle[jj, 5]; // Wert aus der Tabelle wird übergeben
                    }
                }

                MaxBelastung = (Math.Pow((((Durchmesser - 0.6495 * Steigung) + (Durchmesser - 1.2269 * Steigung)) / 2), 2)) * Math.PI * 0.25 * Streckgrenze;

            }



            if (Gewindeart == 2)
            {

                MaxBelastung = (Math.Pow((((Durchmesser - 0.6495 * Steigung) + (Durchmesser - 1.2269 * Steigung)) / 2), 2)) * Math.PI * 0.25 * Streckgrenze;
            }

            if (Gewindeart == 3)
            {
                double Spannungsquerschnitt = 0;

                for (jj = 0; jj <= 7; jj++) // durchsuchen der Tabelle nach dem richtigen Durchmesser
                {
                    M = Convert.ToDouble(Witworth[jj, 1]); //umwandeln der Strings in der Tabelle in int

                    if (Durchmesser == M) // Vergleich ob in dem Tabellenfeld der gleiche Wert steht wie in der Eingabe
                    {
                        Spannungsquerschnitt = Convert.ToDouble(Witworth[jj, 3]);
                    }
                }

                MaxBelastung = Streckgrenze * Spannungsquerschnitt;
            }

            return MaxBelastung;
        }




        //Steigung des Witworth Gewindes als Gangzahl und in mm

        static public (double, double) BerechnungWitworthSteigung(string[,] Witworth, double Durchmesser)
        {
            double Gangzahl = 0;

            for (int jj = 0; jj <= 7; jj++) // durchsuchen der Tabelle nach dem richtigen Durchmesser
            {
                double M = Convert.ToDouble(Witworth[jj, 1]); //umwandeln der Strings in der Tabelle in double

                if (Durchmesser == M) // Vergleich ob in dem Tabellenfeld der gleiche Wert steht wie in der Eingabe
                {
                    Gangzahl = Convert.ToDouble(Witworth[jj, 2]); // Wert aus der Tabelle wird Gangzahl übergeben
                }
            }

            double Steigung = 25.4 / Gangzahl;

            return (Gangzahl, Steigung);
        }


        static public string AusgabeWitworthdurchmesser(string[,] Witworth, double Durchmesser)
        {
            string durchmesserWW = "0";

            for (int jj = 0; jj <= 7; jj++) // durchsuchen der Tabelle nach dem richtigen Durchmesser
            {
                double M = Convert.ToDouble(Witworth[jj, 1]); //umwandeln der Strings in der Tabelle in double

                if (Durchmesser == M) // Vergleich ob in dem Tabellenfeld der gleiche Wert steht wie in der Eingabe
                {
                    durchmesserWW = Witworth[jj, 0]; // Wert aus der Tabelle wird Gangzahl übergeben
                }
            }


            return durchmesserWW;
        }


        static public string AusgabeWitworthflankendurchmesser(string[,] Witworth, double Durchmesser)
        {
            string durchmesserWW = "0";

            for (int jj = 0; jj <= 7; jj++) // durchsuchen der Tabelle nach dem richtigen Durchmesser
            {
                double M = Convert.ToDouble(Witworth[jj, 1]); //umwandeln der Strings in der Tabelle in double

                if (Durchmesser == M) // Vergleich ob in dem Tabellenfeld der gleiche Wert steht wie in der Eingabe
                {
                    durchmesserWW = Witworth[jj, 5]; // Wert aus der Tabelle wird Gangzahl übergeben
                }
            }


            return durchmesserWW;
        }


        static public double[,] Tabellen() // Funktion die ein Array zurückgibt
        {
            // die Werte können nicht mit Formeln errechnet werden, sondern sind auf diese Tabellenwerte genormt
            // deswegen haben wir die als Tabelle hinterlegt um sie bei den Berechnungen bzw. Ausgaben zu verwenden

            double[,] Tabelle = new double[9, 13];

            //Gewinde Nenndurchmesser
            Tabelle[0, 0] = 3;
            Tabelle[1, 0] = 4;
            Tabelle[2, 0] = 5;
            Tabelle[3, 0] = 6;
            Tabelle[4, 0] = 8;
            Tabelle[5, 0] = 10;
            Tabelle[6, 0] = 12;
            Tabelle[7, 0] = 16;
            Tabelle[8, 0] = 20;

            //Durchgangsloch Durchmesser
            Tabelle[0, 1] = 3.4;
            Tabelle[1, 1] = 4.5;
            Tabelle[2, 1] = 5.5;
            Tabelle[3, 1] = 6.6;
            Tabelle[4, 1] = 9;
            Tabelle[5, 1] = 11;
            Tabelle[6, 1] = 13.5;
            Tabelle[7, 1] = 17.5;
            Tabelle[8, 1] = 22;

            //ISO 4762 Senkdurchmesser Zylinderkopfschraube
            Tabelle[0, 2] = 6.5;
            Tabelle[1, 2] = 8;
            Tabelle[2, 2] = 10;
            Tabelle[3, 2] = 11;
            Tabelle[4, 2] = 15;
            Tabelle[5, 2] = 18;
            Tabelle[6, 2] = 20;
            Tabelle[7, 2] = 26;
            Tabelle[8, 2] = 33;

            //ISO 4762 Senktiefe Zylinderkopfschraube
            Tabelle[0, 3] = 3.4;
            Tabelle[1, 3] = 4.4;
            Tabelle[2, 3] = 5.4;
            Tabelle[3, 3] = 6.4;
            Tabelle[4, 3] = 8.6;
            Tabelle[5, 3] = 10.6;
            Tabelle[6, 3] = 12.6;
            Tabelle[7, 3] = 16.6;
            Tabelle[8, 3] = 20.6;

            // Durchmesser Kegelsenkung
            Tabelle[0, 4] = 6.9;
            Tabelle[1, 4] = 9.2;
            Tabelle[2, 4] = 11.5;
            Tabelle[3, 4] = 13.7;
            Tabelle[4, 4] = 18.3;
            Tabelle[5, 4] = 22.7;
            Tabelle[6, 4] = 27.2;
            Tabelle[7, 4] = 34;
            Tabelle[8, 4] = 40.7;

            //Steigung metrisches Regelgewinde
            Tabelle[0, 5] = 0.5;
            Tabelle[1, 5] = 0.7;
            Tabelle[2, 5] = 0.8;
            Tabelle[3, 5] = 1;
            Tabelle[4, 5] = 1.25;
            Tabelle[5, 5] = 1.5;
            Tabelle[6, 5] = 1.75;
            Tabelle[7, 5] = 2;
            Tabelle[8, 5] = 2.5;

            //Kopfhöhe Sechskantschraube
            Tabelle[0, 6] = 2;
            Tabelle[1, 6] = 2.8;
            Tabelle[2, 6] = 3.5;
            Tabelle[3, 6] = 4;
            Tabelle[4, 6] = 5.3;
            Tabelle[5, 6] = 6.4;
            Tabelle[6, 6] = 7.5;
            Tabelle[7, 6] = 10;
            Tabelle[8, 6] = 12.5;

            //Schlüsselweite Sechskantschraube
            Tabelle[0, 7] = 5.5;
            Tabelle[1, 7] = 7;
            Tabelle[2, 7] = 8;
            Tabelle[3, 7] = 10;
            Tabelle[4, 7] = 13;
            Tabelle[5, 7] = 16;
            Tabelle[6, 7] = 18;
            Tabelle[7, 7] = 24;
            Tabelle[8, 7] = 30;

            //Kopfdurchmesser Zylinderkopfschraube
            Tabelle[0, 8] = 5.5;
            Tabelle[1, 8] = 7;
            Tabelle[2, 8] = 8.5;
            Tabelle[3, 8] = 10;
            Tabelle[4, 8] = 13;
            Tabelle[5, 8] = 16;
            Tabelle[6, 8] = 18;
            Tabelle[7, 8] = 24;
            Tabelle[8, 8] = 30;

            //Kopfhöhe Zylinderkopfschraube = Nenndurchmesser

            //Schlüsselweite des Innensechskants bei Zylinderkopfschrauben
            Tabelle[0, 9] = 2.5;
            Tabelle[1, 9] = 3;
            Tabelle[2, 9] = 4;
            Tabelle[3, 9] = 5;
            Tabelle[4, 9] = 6;
            Tabelle[5, 9] = 8;
            Tabelle[6, 9] = 10;
            Tabelle[7, 9] = 14;
            Tabelle[8, 9] = 17;

            //Kopfhöhe Senkschrauben
            Tabelle[0, 10] = 1.9;
            Tabelle[1, 10] = 2.5;
            Tabelle[2, 10] = 3.1;
            Tabelle[3, 10] = 3.7;
            Tabelle[4, 10] = 5;
            Tabelle[5, 10] = 6.2;
            Tabelle[6, 10] = 7.4;
            Tabelle[7, 10] = 8.8;
            Tabelle[8, 10] = 10.2;

            //Schlüsselweite des Innensechskants bei Senkschrauben
            Tabelle[0, 11] = 2;
            Tabelle[1, 11] = 2.5;
            Tabelle[2, 11] = 3;
            Tabelle[3, 11] = 4;
            Tabelle[4, 11] = 5;
            Tabelle[5, 11] = 6;
            Tabelle[6, 11] = 8;
            Tabelle[7, 11] = 10;
            Tabelle[8, 11] = 12;

            //Kopfdurchmesser bei Senkschrauben
            Tabelle[0, 12] = 5.5;
            Tabelle[1, 12] = 7.5;
            Tabelle[2, 12] = 9.4;
            Tabelle[3, 12] = 11.3;
            Tabelle[4, 12] = 15.2;
            Tabelle[5, 12] = 19.2;
            Tabelle[6, 12] = 23.1;
            Tabelle[7, 12] = 29;
            Tabelle[8, 12] = 36;

            return Tabelle;
        }


        static public string[,] WitworthTabelle() // Funktion die ein Array zurückgibt
        {
            // die Werte können nicht mit Formeln errechnet werden, sondern sind auf diese Tabellenwerte genormt
            // deswegen haben wir die als Tabelle hinterlegt um sie bei den Berechnungen bzw. Ausgaben zu verwenden

            string[,] Witworth = new string[8, 6];

            //Gewinde Nenndurchmesser
            Witworth[0, 0] = "1/4";
            Witworth[1, 0] = "3/8";
            Witworth[2, 0] = "1/2";
            Witworth[3, 0] = "3/4";
            Witworth[4, 0] = "1";
            Witworth[5, 0] = "1 1/4";
            Witworth[6, 0] = "1 1/2";
            Witworth[7, 0] = "2";

            //Außendurchmesser
            Witworth[0, 1] = "6,35";
            Witworth[1, 1] = "9,53";
            Witworth[2, 1] = "12,7";
            Witworth[3, 1] = "19,05";
            Witworth[4, 1] = "25,4";
            Witworth[5, 1] = "31,75";
            Witworth[6, 1] = "38,1";
            Witworth[7, 1] = "50,8";

            //Gangzahl
            Witworth[0, 2] = "20";
            Witworth[1, 2] = "16";
            Witworth[2, 2] = "12";
            Witworth[3, 2] = "10";
            Witworth[4, 2] = "8";
            Witworth[5, 2] = "7";
            Witworth[6, 2] = "6";
            Witworth[7, 2] = "4,5";

            //Spannungsquerschnitt
            Witworth[0, 3] = "17,5";
            Witworth[1, 3] = "44,1";
            Witworth[2, 3] = "78,4";
            Witworth[3, 3] = "196";
            Witworth[4, 3] = "358";
            Witworth[5, 3] = "577";
            Witworth[6, 3] = "839";
            Witworth[7, 3] = "1491";

            //Kernlochdurchmesser
            Witworth[0, 4] = "4,72";
            Witworth[1, 4] = "7,49";
            Witworth[2, 4] = "9,99";
            Witworth[3, 4] = "15,8";
            Witworth[4, 4] = "21,34";
            Witworth[5, 4] = "27,10";
            Witworth[6, 4] = "32,68";
            Witworth[7, 4] = "43,57";

            //Flankendurchmesser
            Witworth[0, 5] = "5,54";
            Witworth[1, 5] = "8,51";
            Witworth[2, 5] = "11,35";
            Witworth[3, 5] = "17,42";
            Witworth[4, 5] = "23,37";
            Witworth[5, 5] = "29,43";
            Witworth[6, 5] = "35,39";
            Witworth[7, 5] = "47,19";

            return Witworth;

        }
    }

    class Schraube
    {
        public double Durchmesser { get; set; }
        public double Gewindelänge { get; set; }
        public double Schaftlänge { get; set; }
        public double Steigung { get; set; }
        public int Kopf { get; set; }
        public int Gewindeart { get; set; }
        public int Material { get; set; }
        public double Streckgrenze { get; set; }
        public double Zugfestigkeit { get; set; }
        public double Gangzahl { get; set; }
        public object Festigkeitsklasse { get; internal set; }
        public double Schlüsselweite { get; internal set; }
        public double Kernlochdurchmesser { get; internal set; }
        public double Kopfhöhe { get; internal set; }
        public double Kopfdurchmesser { get; internal set; }
        public double Durchgangsbohrung { get; internal set; }
        public double Senkdurchmesser { get; internal set; }
        public double DurchmesserKegelsenkung { get; internal set; }
        public double MaxBelastung { get; internal set; }
        public string WhitworthDurchmesser { get; internal set; }
        public string WhitworthFlankendurchmesser { get; internal set; }

        public double[,] MetrischeTabelle()
        {
            return Konsolenprogramm.Tabellen();
        }

        public string[,] WhitworthTabelle()
        {
            return Konsolenprogramm.WitworthTabelle();
        }


    }
}
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
using System.Text.RegularExpressions;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using System.IO;

namespace algaGenetyczna
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Warstwa start;
        public bool pobrane = false;
        public List<int> warstwy = new List<int>();
        public List<double> x = new List<double>();
        public List<double> y = new List<double>();

        public MainWindow()
        {
            InitializeComponent();
            finish.Text = "Przy tworzeniu wiekszych populacji po kilku generacjach populacja czesto \nosiaga stan w ktorym juz sie nie poprawia (czasem jest inaczej). Nie jestem \npewien czy to znaczy ze jest dobrze, czy nie.\n W tym zadaniu wiekszej instrukcji nie trzeba- kazde zadanie jest pod innym \nguzikiem, do zad2 trzeba wybrac najpierw plik z danymi (np sinusik.txt od \nPana Nowaka) oddzielonymi spacja i enterem tak jak we wczesniej \nwymienionym pliku. Minimalne wartosci inputow ustawione zgodnie z \nwymogami instrukcji.";

        }
        private void zad3Click(object sender, RoutedEventArgs e)
        {
            start = new Warstwa();
            warstwy = new List<int>();
            int LBnCh;
            String tmp = inputLBnCh.Text;
            if (!int.TryParse(tmp, out LBnCh)) { }
            Regex rgx = new Regex(@"(?<=\s|^)[-+]?\d+(?=\s|$)");
            if (rgx.IsMatch(tmp) != true || LBnCh < 4)
            {
                MessageBox.Show("LBnCh- Podawaj tylko liczbe typu int i wieksza badz rowna 4!");
                inputLBnCh.Text = "4";
                return;
            }
            int ZDMin;
            tmp = inputZDMin.Text;
            if (!int.TryParse(tmp, out ZDMin)) { }
            if (rgx.IsMatch(tmp) != true)
            {
                MessageBox.Show("ZDMin- Podawaj tylko liczbe typu int!");
                inputZDMin.Text = "-10";
                return;
            }
            int ZDMax;
            tmp = inputZDMax.Text;
            if (!int.TryParse(tmp, out ZDMax)) { }
            if (rgx.IsMatch(tmp) != true)
            {
                MessageBox.Show("ZDMax- Podawaj tylko liczbe typu int!");
                inputZDMax.Text = "10";
                return;
            }
            int ilosc;
            tmp = inputPopulacja.Text;
            if (!int.TryParse(tmp, out ilosc)) { }
            if (rgx.IsMatch(tmp) != true || ilosc < 13 || (ilosc % 2) != 1 || ((ilosc-1)%6)!=0)
            {
                MessageBox.Show("Populacja- Podawaj tylko liczbe typu int, nieparzysta, populacja-1 podzielna przez 6 i wieksza badz rowna 13!");
                inputPopulacja.Text = "13";
                return;
            }
            int iteracje;
            tmp = inputIteracje.Text;
            if (!int.TryParse(tmp, out iteracje)) { }
            if (rgx.IsMatch(tmp) != true || iteracje < 100)
            {
                MessageBox.Show("Iteracje- Podawaj tylko liczbe typu int i wieksze lub rowne 100!");
                inputIteracje.Text = "100";
                return;
            }
            finish.Text = "";
            List<Osobnik> populacja = new List<Osobnik>();
            populacja = tworzOsobniki(ilosc, LBnCh, 9, populacja);
            warstwy.Add(2);
            warstwy.Add(2);
            warstwy.Add(1);
            zad3Licz(populacja, LBnCh, ZDMin, ZDMax);
            zad3Rek(LBnCh, ZDMin, ZDMax,iteracje, 0, populacja);
        }
        private void zad2Click(object sender, RoutedEventArgs e)
        {
            if (!pobrane) { 
                MessageBox.Show("Najpierw dodaj plik z danymi (sinusik)");
            return; }
            int LBnCh;
            String tmp = inputLBnCh.Text;
            if (!int.TryParse(tmp, out LBnCh)) { }
            Regex rgx = new Regex(@"(?<=\s|^)[-+]?\d+(?=\s|$)");
            if (rgx.IsMatch(tmp) != true || LBnCh < 4)
            {
                MessageBox.Show("LBnCh- Podawaj tylko liczbe typu int i wieksza badz rowna 4!");
                inputLBnCh.Text = "4";
                return;
            }
            int ZDMin;
            tmp = inputZDMin.Text;
            if (!int.TryParse(tmp, out ZDMin)) { }
            if (rgx.IsMatch(tmp) != true)
            {
                MessageBox.Show("ZDMin- Podawaj tylko liczbe typu int!");
                inputZDMin.Text = "-10";
                return;
            }
            int ZDMax;
            tmp = inputZDMax.Text;
            if (!int.TryParse(tmp, out ZDMax)) { }
            if (rgx.IsMatch(tmp) != true)
            {
                MessageBox.Show("ZDMax- Podawaj tylko liczbe typu int!");
                inputZDMax.Text = "10";
                return;
            }
            int ilosc;
            tmp = inputPopulacja.Text;
            if (!int.TryParse(tmp, out ilosc)) { }
            if (rgx.IsMatch(tmp) != true || ilosc < 13 || (ilosc % 2) != 1 || ((ilosc - 1) % 6) != 0)
            {
                MessageBox.Show("Populacja- Podawaj tylko liczbe typu int, nieparzysta, populacja -1 podzielna przez 6 i wieksza badz rowna 13!");
                inputPopulacja.Text = "13";
                return;
            }
            int iteracje;
            tmp = inputIteracje.Text;
            if (!int.TryParse(tmp, out iteracje)) { }
            if (rgx.IsMatch(tmp) != true || iteracje < 100)
            {
                MessageBox.Show("Iteracje- Podawaj tylko liczbe typu int i wieksze lub rowne 100!");
                inputIteracje.Text = "100";
                return;
            }
            finish.Text = "";
            List<Osobnik> populacja = new List<Osobnik>();
            populacja = tworzOsobniki(ilosc, LBnCh, 3, populacja);           
            zad2Licz(populacja, LBnCh, ZDMin, ZDMax);
            zad2Rek(LBnCh, ZDMin, ZDMax, iteracje, 0, populacja);
        }
        public void zad2Rek(int LBnCh, int ZDMin, int ZDMax, int iteracje, int obecna, List<Osobnik> populacja)
        {

            List<Osobnik> newpop = new List<Osobnik>();
            for (int j = 0; j < (populacja.Count-1)/3; j++)
            {
                Osobnik os = selekcjaTurniej2(3, populacja);
                newpop.Add(mutacja2(LBnCh, os, 3));
            }
            for (int j = 0; j < (populacja.Count - 1) / 6; j++)
            {
                Osobnik p1 = selekcjaTurniej2(3, populacja);
                Osobnik p2 = selekcjaTurniej2(3, populacja);
                List<Osobnik> krzyz = krzyzowanie(p1, p2, LBnCh, 3);
                foreach (Osobnik osobnik in krzyz)
                    newpop.Add(osobnik);
            }
            for (int j = 0; j < (populacja.Count - 1) / 6; j++)
            {
                Osobnik p1 = selekcjaTurniej2(3, populacja);
                Osobnik p2 = selekcjaTurniej2(3, populacja);
                List<Osobnik> krzyz = krzyzowanie(p1, p2, LBnCh, 3);
                foreach (Osobnik osobnik in krzyz)
                    newpop.Add(mutacja2(LBnCh, osobnik, 3));
            }

            Osobnik najlepszy = selekcjaTurniej2(populacja.Count, populacja);
            newpop.Add(najlepszy);
            zad2Licz(newpop, LBnCh, ZDMin, ZDMax);
            double suma = 0;
            foreach (Osobnik osobnik in populacja)
            {
                suma += osobnik.Ocena;
            }
            finish.Text += "Najnizsza ocena: " + najlepszy.Ocena + " Srednia ocena: " + (suma / populacja.Count) + "\n";
            if (obecna < iteracje - 1)
                zad2Rek(LBnCh, ZDMin, ZDMax, iteracje, obecna + 1, newpop);

        }
        public void zad3Rek(int LBnCh, int ZDMin, int ZDMax, int iteracje, int obecna, List<Osobnik> populacja)
        {

            List<Osobnik> newpop = new List<Osobnik>();
            for (int j = 0; j < (populacja.Count - 1) / 3; j++)
            {
                Osobnik os = selekcjaTurniej2(3, populacja);               
                newpop.Add(mutacja2(LBnCh, os, 9));
            }
            for (int j = 0; j < (populacja.Count - 1) / 6; j++)
            {
                Osobnik p1 = selekcjaTurniej2(3, populacja);
                Osobnik p2 = selekcjaTurniej2(3, populacja);
                List<Osobnik> krzyz = krzyzowanie(p1, p2, LBnCh, 9);
                foreach (Osobnik osobnik in krzyz)
                    newpop.Add(osobnik);
            }
            for (int j = 0; j < (populacja.Count - 1) / 6; j++)
            {               
                Osobnik p1 = selekcjaTurniej2(3, populacja);
                Osobnik p2 = selekcjaTurniej2(3, populacja);
                List<Osobnik> krzyz = krzyzowanie(p1, p2, LBnCh, 9);
                foreach (Osobnik osobnik in krzyz)                    
                    newpop.Add(mutacja2(LBnCh, osobnik, 9));
            }

            Osobnik najlepszy = selekcjaTurniej2(populacja.Count, populacja);
            newpop.Add(najlepszy);
            zad3Licz(newpop, LBnCh, ZDMin, ZDMax);
            double suma = 0;
            foreach (Osobnik osobnik in populacja)
            {
                suma += osobnik.Ocena;
            }
            finish.Text += "Najnizsza ocena: " + najlepszy.Ocena + " Srednia ocena: " + (suma / populacja.Count) + "\n";
            if (obecna < iteracje - 1)
                zad3Rek(LBnCh, ZDMin, ZDMax, iteracje, obecna + 1, newpop);

        }
        private void zad2Licz(List<Osobnik> populacja, int LBnCh, int ZDMin, int ZDMax)
        {
            foreach (Osobnik osobnik in populacja)
            {               
                osobnik.Ocena = 0;              
                List<int> chromosomy = new List<int>();
                for (int k=0; k < LBnCh; k++)
                    chromosomy.Add(osobnik.Chromosomy[k]);
                osobnik.PA = dekoduj(chromosomy, LBnCh, ZDMin, ZDMax);
                chromosomy = new List<int>();
                for (int k=LBnCh; k < LBnCh*2; k++)
                    chromosomy.Add(osobnik.Chromosomy[k]);
                osobnik.PB = dekoduj(chromosomy, LBnCh, ZDMin, ZDMax);
                chromosomy = new List<int>();
                for (int k=LBnCh*2; k < LBnCh*3; k++)
                    chromosomy.Add(osobnik.Chromosomy[k]);
                osobnik.PC = dekoduj(chromosomy, LBnCh, ZDMin, ZDMax);
                for (int i = 0; i < x.Count; i++)
                    osobnik.Ocena += Math.Pow(y[i] - osobnik.PA * Math.Sin(osobnik.PB * x[i] + osobnik.PC),2);               
            }
        }
        private void zad3Licz(List<Osobnik> populacja, int LBnCh, int ZDMin, int ZDMax)
        {
            foreach (Osobnik osobnik in populacja)
            {
                osobnik.Ocena = 0;
                start = new Warstwa();
                for (int i = 0; i < warstwy[0]; i++)
                {
                    start.Neutrony.Add(1);
                }
                generuj(start, 0, LBnCh, osobnik, ZDMin, ZDMax);
                robWarstwy(start, warstwy, 1, LBnCh, osobnik, ZDMin, ZDMax);
                liczWyniki(start, 0);

                List<List<int>> probki = new List<List<int>>();
                probki = tworzProbki();
                foreach (List<int> probka in probki)
                {
                    for (int i = 0; i < warstwy[0]; i++)
                        start.Neutrony[i]=Convert.ToDouble(probka[i]);
                    int d = probka[0];
                    for (int i = 1; i < probka.Count; i++)
                        d ^= probka[i];

                    liczWyniki2(start, 0);
                    Warstwa warstwa = start;
                    for (int k = 0; k < warstwy.Count - 1; k++)
                        warstwa = warstwa.Dzieci[0];
                    osobnik.Ocena += Math.Pow((d - warstwa.Neutrony[0]), 2);
                   }
            }
        }

        private void robWarstwy(Warstwa warstwa, List<int> warstwy, int skip, int LBnCh, Osobnik osobnik, int ZDMin, int ZDMax)
        {
            Warstwa tmp = new Warstwa();
            if (warstwy.Count - 1 != skip)
                generuj(tmp, skip, LBnCh, osobnik, ZDMin, ZDMax);

            warstwa.Dzieci.Add(tmp);
            if (warstwy.Count > skip + 1)
                robWarstwy(tmp, warstwy, skip + 1, LBnCh, osobnik, ZDMin, ZDMax);
        }
        private void generuj(Warstwa warstwa, int i, int LBnCh, Osobnik osobnik, double ZDMin, double ZDMax)
        {
            
            for (int j = 0; j < (warstwy[i] + 1) * warstwy[i + 1]; j++)
            {
                List<int> chromosomy = new List<int>();
                if(i==0)
                for (int k = j*LBnCh; k < LBnCh*(j+1); k++)
                    chromosomy.Add(osobnik.Chromosomy[k]);
                else 
                    for (int k = j * LBnCh +i* (warstwy[i-1] + 1) * warstwy[i]*LBnCh; k < LBnCh * (j + 1) + i * (warstwy[i - 1] + 1) * warstwy[i] * LBnCh; k++)
                        chromosomy.Add(osobnik.Chromosomy[k]);
                warstwa.Wagi.Add(dekoduj(chromosomy, LBnCh, ZDMin, ZDMax));
            }

        }
        private List<List<int>> tworzProbki()
        {
            List<List<int>> probki = new List<List<int>>();
            for (int i = 0; i < Math.Pow(2, warstwy[0]); i++)
            {
                probki.Add(new List<int>());
                for (int j = 0; j < warstwy[0]; j++)
                {
                    int p = i >> j;
                    probki[i].Add(p & 1);
                }
            }
            return probki;
        }
        void liczWyniki(Warstwa warstwa, int skip, double beta = 1)
        {
            int w = 0;
            for (int j = 0; j < warstwy[skip + 1]; j++)
            {
                double s = 0;
                for (int i = -1; i < warstwy[skip]; w++, i++)
                {
                    if (i == -1)
                        s += 1 * warstwa.Wagi[w];
                    else
                        s += warstwa.Neutrony[i] * warstwa.Wagi[w];
                }
                double wynik = 1 / (1 + Math.Pow(Math.E, -beta * s));
                warstwa.Dzieci[0].Neutrony.Add(wynik);
            }
            if (warstwy.Count > skip + 2)
                liczWyniki(warstwa.Dzieci[0], skip + 1);
        }
        void liczWyniki2(Warstwa warstwa, int skip, double beta = 1)
        {
            int w = 0;
            for (int j = 0; j < warstwy[skip + 1]; j++)
            {
                double s = 0;
                for (int i = -1; i < warstwy[skip]; w++, i++)
                {
                    if (i == -1)
                        s += 1 * warstwa.Wagi[w];
                    else
                        s += warstwa.Neutrony[i] * warstwa.Wagi[w];
                }
                double wynik = 1 / (1 + Math.Pow(Math.E, -beta * s));
                warstwa.Dzieci[0].Neutrony[j] = wynik;
            }
            if (warstwy.Count > skip + 2)
                liczWyniki2(warstwa.Dzieci[0], skip + 1);
        }
        public List<Osobnik> tworzOsobniki(int ilosc, int LBnCh, int lParam, List<Osobnik> populacja)
        {
            Random rand = new Random();
            for(int i =0; i<ilosc; i++)
            {
                Osobnik osobnik = new Osobnik();
                for (int j = 0; j < LBnCh * lParam; j++)
                    osobnik.Chromosomy.Add(rand.Next(0, 2));
                populacja.Add(osobnik);
            }
            return populacja;
        }
        public double dekoduj(List<int> chromosom, int LBnCh, double ZDMin, double ZDMax)
        {
            double ctmp = 0;
            double ZD = ZDMax - ZDMin;
            for (int i = LBnCh - 1; i >= 0; i--)
                ctmp += chromosom[i] * Math.Pow(2, LBnCh-i-1);
            double pm = ZDMin + ctmp / (Math.Pow(2, LBnCh) - 1) * ZD;
            return pm;
        }
        public Osobnik mutacja2(int LBnCh, Osobnik os, int lParam)
        {
            Random rand = new Random();
            Osobnik nowy = new Osobnik();
            int bPunkt = rand.Next(0, LBnCh * 2);
            for (int i = 0; i < LBnCh * lParam; i++)
                if (bPunkt == i)
                    nowy.Chromosomy.Add(os.Chromosomy[i] ^ 1);
                else
                    nowy.Chromosomy.Add(os.Chromosomy[i]);
            return nowy;
        }
        public Osobnik selekcjaTurniej2(int TurRozm, List<Osobnik> populacja)
        {
            Random rand = new Random();
            Osobnik win = new Osobnik();
            List<int> possible = Enumerable.Range(0, populacja.Count).ToList();
            List<int> listNumbers = new List<int>();
            for (int i = 0; i < TurRozm; i++)
            {
                int index = rand.Next(0, possible.Count);
                listNumbers.Add(possible[index]);
                possible.RemoveAt(index);
            }
            double min = populacja[0].Ocena;
            foreach (int nr in listNumbers)
                if (populacja[nr].Ocena < min)
                    min = populacja[nr].Ocena;
            foreach (Osobnik osob in populacja)
                if (osob.Ocena == min)
                    foreach (int chrom in osob.Chromosomy)
                        win.Chromosomy.Add(chrom);
            return win;
        }
        public Osobnik selekcjaTurniej(int TurRozm, List<Osobnik> populacja)
        {
            Random rand = new Random();
            Osobnik win = new Osobnik();
            List<int> possible = Enumerable.Range(0, populacja.Count).ToList();
            List<int> listNumbers = new List<int>();
            for (int i = 0; i < TurRozm; i++)
            {
                int index = rand.Next(0, possible.Count);
                listNumbers.Add(possible[index]);
                possible.RemoveAt(index);
            }
            double max = populacja[0].Ocena;
            foreach(int nr in listNumbers)
                if (populacja[nr].Ocena > max)
                    max = populacja[nr].Ocena;
            foreach (Osobnik osob in populacja)
                if (osob.Ocena == max)
                    win=osob;
            return win;
        }
        public List<Osobnik> krzyzowanie(Osobnik p1, Osobnik p2, int LBnCh, int lParam)
        {
            Random rand = new Random();           
            Osobnik nowy = new Osobnik();
            List<Osobnik> nowList = new List<Osobnik>();
            int bPunkt = rand.Next(0, LBnCh * 2);
            for (int i = 0; i < LBnCh * lParam; i++)
                if (bPunkt >=i)
                    nowy.Chromosomy.Add(p1.Chromosomy[i]);
                else
                    nowy.Chromosomy.Add(p2.Chromosomy[i]);
            nowList.Add(nowy);
            nowy = new Osobnik();
            for (int i = 0; i < LBnCh * lParam; i++)
                if (bPunkt < i)
                    nowy.Chromosomy.Add(p1.Chromosomy[i]);
                else
                    nowy.Chromosomy.Add(p2.Chromosomy[i]);
            nowList.Add(nowy);
            return nowList;
        }
        public void mutacja(int LBnCh, List<Osobnik> newpop, List<Osobnik> populacja, int lParam, int TurRozm=0)
        {
            Random rand = new Random();
            if (TurRozm==0)
                TurRozm = rand.Next(2, Convert.ToInt32(populacja.Count * 0.2));
            Osobnik najlepszy= selekcjaTurniej(TurRozm, populacja);
            Osobnik nowy = new Osobnik();
            int bPunkt = rand.Next(0, LBnCh*lParam);
            for (int i = 0; i < LBnCh*lParam; i++)
                if (bPunkt == i)
                    nowy.Chromosomy.Add(najlepszy.Chromosomy[i] ^ 1);
                else
                    nowy.Chromosomy.Add(najlepszy.Chromosomy[i]);
            newpop.Add(nowy);
        }
        public void zad1Licz(int LBnCh, int ZDMin, int ZDMax, List<Osobnik> populacja)
        {
            foreach (Osobnik osobnik in populacja)
            {
                List<int> chromosomy = new List<int>();
                for (int i = 0; i < LBnCh; i++)
                    chromosomy.Add(osobnik.Chromosomy[i]);
                osobnik.X1 = dekoduj(chromosomy, LBnCh, ZDMin, ZDMax);
                chromosomy = new List<int>();
                for (int i = LBnCh; i < 2 * LBnCh; i++)
                    chromosomy.Add(osobnik.Chromosomy[i]);
                osobnik.X2 = dekoduj(chromosomy, LBnCh, ZDMin, ZDMax);
                osobnik.Ocena = Math.Sin(osobnik.X1 * 0.05) + Math.Sin(osobnik.X2 * 0.05) + 0.4 * Math.Sin(osobnik.X1 * 0.15) * Math.Sin(osobnik.X2 * 0.15);
            }
        }
        public void zad1Rek(int LBnCh, int ZDMin, int ZDMax, int iteracje, int obecna, int lParam, List<Osobnik> populacja)
        {
            
            List<Osobnik> newpop = new List<Osobnik>();
            for (int j = 0; j < populacja.Count - 1; j++)
                mutacja(LBnCh, newpop, populacja, lParam);            
            Osobnik najlepszy = selekcjaTurniej(populacja.Count, populacja);         
            newpop.Add(najlepszy);
            zad1Licz(LBnCh, ZDMin, ZDMax, newpop);
            double suma = 0;
            foreach (Osobnik osobnik in populacja)
            {                
                suma += osobnik.Ocena;
            }     
                finish.Text += "Najwyzsza ocena: " + najlepszy.Ocena +" Srednia ocena: " + (suma/populacja.Count)+"\n";
            if (obecna < iteracje - 1)
                zad1Rek(LBnCh, ZDMin, ZDMax, iteracje, obecna + 1, lParam, newpop);       

        }

        private void Zad1(object sender, RoutedEventArgs e)
        {
            int LBnCh;
            String tmp = inputLBnCh.Text;
            if (!int.TryParse(tmp, out LBnCh)) { }
            Regex rgx = new Regex(@"(?<=\s|^)[-+]?\d+(?=\s|$)");
            if (rgx.IsMatch(tmp) != true || LBnCh < 3)
            {
                MessageBox.Show("LBnCh- Podawaj tylko liczbe typu int i wieksza badz rowna 3!");
                inputLBnCh.Text = "3";
                return;
            }
            int ZDMin;
            tmp = inputZDMin.Text;
            if (!int.TryParse(tmp, out ZDMin)) { }
            if (rgx.IsMatch(tmp) != true)
            {
                MessageBox.Show("ZDMin- Podawaj tylko liczbe typu int!");
                inputZDMin.Text = "0";
                return;
            }
            int ZDMax;
            tmp = inputZDMax.Text;
            if (!int.TryParse(tmp, out ZDMax)) { }
            if (rgx.IsMatch(tmp) != true)
            {
                MessageBox.Show("ZDMax- Podawaj tylko liczbe typu int!");
                inputZDMax.Text = "100";
                return;
            }
            int ilosc;
            tmp = inputPopulacja.Text;
            if (!int.TryParse(tmp, out ilosc)) { }
            if (rgx.IsMatch(tmp) != true || ilosc < 9 || (ilosc % 2) != 1)
            {
                MessageBox.Show("Populacja- Podawaj tylko liczbe typu int, nieparzysta i wieksza badz rowna 9!");
                inputPopulacja.Text = "9";
                return;
            }
            int iteracje;
            tmp = inputIteracje.Text;
            if (!int.TryParse(tmp, out iteracje)) { }
            if (rgx.IsMatch(tmp) != true|| iteracje<20)
            {
                MessageBox.Show("Iteracje- Podawaj tylko liczbe typu int i wieksze lub rowne 20!");
                inputIteracje.Text = "20";
                return;
            }
            finish.Text = "";
            List<Osobnik> populacja = new List<Osobnik>();
            populacja=tworzOsobniki(ilosc, LBnCh, 2, populacja);
            zad1Licz(LBnCh, ZDMin, ZDMax, populacja);
            zad1Rek(LBnCh, ZDMin, ZDMax, iteracje, 0, 2, populacja);       


        }

        private void fileClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog plik;
            plik = new OpenFileDialog();
            plik.Filter = "PlikTekstowy|*.txt";
            plik.DefaultExt = ".txt";
            Nullable<bool> plikOK = plik.ShowDialog();
            if (plikOK == true)
            {
                pobrane = true;
                string[] linie = File.ReadAllLines(plik.FileName);
                for (int i = 0; i < linie.Length; i++)
                {
                    x = new List<double>();
                    y = new List<double>();
                    var linia = linie[i].Trim();
                    var liczby = linia.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);            
                    x.Add(double.Parse(liczby[0].Trim()));
                    y.Add(double.Parse(liczby[1].Trim()));
                }
            }
        }
    }
}

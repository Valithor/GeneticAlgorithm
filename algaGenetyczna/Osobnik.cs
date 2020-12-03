using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;


namespace algaGenetyczna
{
    public class Osobnik
    {
        public Osobnik()
        {
            Chromosomy = new List<int>();
            X1 = 0;
            X2 = 0;
            PA = 0;
            PB = 0;
            PC = 0;
            Ocena = 0;
        }      
        public List<int> Chromosomy { get; set; }
        public double X1 { get; set; }
        public double PA { get; set; }
        public double PB { get; set; }
        public double PC { get; set; }
        public double X2 { get; set; }
        public double Ocena { get; set; }


    }
}
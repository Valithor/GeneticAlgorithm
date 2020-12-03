using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace algaGenetyczna
{
    public class Warstwa
    {
        public Warstwa()
        {
            Neutrony = new List<double>();
            Wagi = new List<double>();
            Dzieci = new List<Warstwa>();
            Id = Interlocked.Increment(ref GlobalId);
            Beta = 1;
        }
        public double Beta { get; set; }
        public int Id { get; private set; }
        public static int GlobalId;
        public List<Warstwa> Dzieci { get; set; }
        public List<double> Neutrony { get; set; }
        public List<double> Wagi { get; set; }


    }
}
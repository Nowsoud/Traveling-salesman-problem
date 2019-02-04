using System;
using System.Collections.Generic;
using System.Linq;

namespace TspModel
{

    [Serializable]
    public class Tour
    {

        // Member variables
        public List<City> cities { get; private set; }
        public double distance { get; private set; }
        public double fitness { get; private set; }

        // ctor
        public Tour(List<City> l)
        {
            this.cities = l;
            this.distance = this.calcDist();
            this.fitness = this.calcFit();
        }

        // Functionality
        public static Tour random(int n)
        {
            List<City> points = new List<City>();

            for (int i = 0; i < n; ++i)
                points.Add( City.random() );

            return new Tour(points);
        }

        public Tour shuffle()
        {
            List<City> tmp = new List<City>(this.cities);
            int n = tmp.Count;

            while (n > 1)
            {
                n--;
                int k = Settings.Random.Next(n + 1);
                City v = tmp[k];
                tmp[k] = tmp[n];
                tmp[n] = v;
            }

            return new Tour(tmp);
        }

        public Tour crossover(Tour m)
        {
            int i = Settings.Random.Next(0, m.cities.Count);
            int j = Settings.Random.Next(i, m.cities.Count);
            List<City> s = this.cities.GetRange(i, j - i + 1);
            List<City> ms = m.cities.Except(s).ToList();
            List<City> c = ms.Take(i)
                             .Concat(s)
                             .Concat( ms.Skip(i) )
                             .ToList();
            return new Tour(c);
        }

        public Tour mutate()
        {
            List<City> tmp = new List<City>(this.cities);

            if (Settings.Random.NextDouble() < Settings.MutRate)
            {
                int i = Settings.Random.Next(0, this.cities.Count);
                int j = Settings.Random.Next(0, this.cities.Count);
                City v = tmp[i];
                tmp[i] = tmp[j];
                tmp[j] = v;
            }

            return new Tour(tmp);
        }

        private double calcDist()
        {
            double total = 0;
            for (int i = 0; i < this.cities.Count; ++i)
                total += this.cities[i].distanceTo( this.cities[ (i + 1) % this.cities.Count ] );

            return total;

            // Execution time is doubled by using linq in this case
            //return this.t.Sum( c => c.distanceTo(this.t[ (this.t.IndexOf(c) + 1) % this.t.Count] ) );
        }

        private double calcFit()
        {
            return 1 / this.distance;
        }

    }
}


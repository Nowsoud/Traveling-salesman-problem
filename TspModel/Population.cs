using System;
using System.Collections.Generic;
using System.Linq;

namespace TspModel
{

    [Serializable]
    public class Population
    {
        public Random random = new Random();
        public List<Tour> tours { get; private set; }
        public double maxFit { get; private set; }
        
        public Population(List<Tour> l)
        {
            this.tours = l;
            this.maxFit = this.calcMaxFit();
        }
        
        public static Population Randomized(Tour t, int n)
        {
            List<Tour> tmp = new List<Tour>();

            for (int i = 0; i < n; ++i)
                tmp.Add( t.shuffle() );

            return new Population(tmp);
        }

        private double calcMaxFit()
        {
            return this.tours.Max( t => t.fitness );
        }

        public Tour select()
        {
            while (true)
            {
                int i = random.Next(0, Settings.PopSize);

                if (random.NextDouble() < this.tours[i].fitness / this.maxFit)
                    return new Tour(this.tours[i].cities);
            }
        }

        public Population genNewPop(int n)
        {
            List<Tour> p = new List<Tour>();

            for (int i = 0; i < n; ++i)
            {
                Tour t = this.select().crossover( this.select() );

                foreach (City c in t.cities)
                    t = t.mutate();

                p.Add(t);
            }

            return new Population(p);
        }

        public Population elite(int n)
        {
            List<Tour> best = new List<Tour>();
            Population tmp = new Population(tours);

            for (int i = 0; i < n; ++i)
            {
                best.Add( tmp.findBest() );
                tmp = new Population( tmp.tours.Except(best).ToList() );
            }

            return new Population(best);
        }

        public Tour findBest()
        {
            foreach (var t in this.tours)
                if (t.fitness == this.maxFit)
                    return t;
            return null;
        }

        public Population Evolve()
        {
            Population best = this.elite(Settings.Elitism);
            Population np = this.genNewPop(Settings.PopSize - Settings.Elitism);
            return new Population( best.tours.Concat(np.tours).ToList() );
        }
    }
}


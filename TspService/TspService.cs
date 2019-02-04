using TspModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TspService
{
    public class TspService : MarshalByRefObject, ITspService
    {
        public volatile Population population;
        public volatile int generation = 0;

        static object locked = new object();
        public TspService()
        {
            this.population = Population.Randomized(Tour.random(Settings.NumCities), Settings.PopSize);
            Console.WriteLine("cities: " + Settings.NumCities);
            Console.WriteLine("population size: " + Settings.PopSize);
            Console.WriteLine("elitism: " + Settings.Elitism);
            Console.WriteLine("mutation rate: " + Settings.MutRate);
        }
        public Population GetBestPopulation() {
                return population;
        }
        

        public void SetBestPopuation(Population population, string name)
        {
            lock (locked)
            {
                if (this.population.maxFit < population.maxFit)
                    this.population = population;
                generation++;

                Console.WriteLine(population.findBest().distance + ", Generation " + generation + " Agent: " + name + " fit: " + population.maxFit);
            }
        }
    }
}

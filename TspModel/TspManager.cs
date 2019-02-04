using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TspModel
{
    public class Settings
    {
        private static Random random = new Random();
        private static double mutRate = 0.04;
        private static int popSize = 60;
        private static int elitism = PopSize / 2;
        private static int numCities = 40;

        public static Random Random { get => random; set => random = value; }
        public static double MutRate { get => mutRate; set => mutRate = value; }
        public static int PopSize { get => popSize; set => popSize = value; }
        public static int Elitism { get => elitism; set => elitism = value; }
        public static int NumCities { get => numCities; set => numCities = value; }
    }
    static class TspExtentions {
        public static void LoadFrom(string path) {

        }
    }
}


using TspModel;

namespace TspService
{
    public interface ITspService
    {
        Population GetBestPopulation();
        void SetBestPopuation(Population population, string name);
    }
}
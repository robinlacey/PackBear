using System.Collections.Generic;

namespace PackBear.UseCases.GetStartingStats.Interface
{
    public interface IGetStartingStats
    {
        Dictionary<string, int> Execute();
    }
}
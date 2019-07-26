using Microsoft.Extensions.Configuration;
using PackBear.Player.Interface;

namespace PackBear.UseCases.GetStartingStats.Interface
{
    public interface IGetStartingStats
    {
        IStartingStats Execute(IConfiguration configuration);
    }
}
using System;
using System.Collections.Generic;
using DealerBear.Player;
using Microsoft.Extensions.Configuration;
using PackBear.Player.Interface;

namespace PackBear.Player
{
    public class StartingStats:IStartingStats
    {
      
        public Dictionary<string, IStat> Stats { get; set; } = new Dictionary<string, IStat>();
        
        public StartingStats(IConfiguration configuration)
        {
           
            SetStartingStats(configuration);

        }
        private void SetStartingStats(IConfiguration configuration)
        {
            foreach (IConfigurationSection configurationSection in configuration.GetChildren())
            {
                if (configurationSection.Key == "CardStats")
                {
                    foreach (IConfigurationSection cartStat in configurationSection.GetChildren())
                    {
                       Stats.Add(cartStat.Key, new Stat());
                        foreach (IConfigurationSection statSubValue in cartStat.GetChildren())
                        {
                            if (statSubValue.Key.Trim().ToLower().Contains("starting"))
                            {
                                Stats[cartStat.Key].Current = Int32.Parse(statSubValue.Value);
                            }
                            if (statSubValue.Key.Trim().ToLower().Contains("minimum"))
                            {
                               Stats[cartStat.Key].Minimum = Int32.Parse(statSubValue.Value);
                            }
                            if (statSubValue.Key.Trim().ToLower().Contains("maximum"))
                            {
                                Stats[cartStat.Key].Maximum = Int32.Parse(statSubValue.Value);
                            }
                        }
                    }
                }
            }
        }
      
    }
}
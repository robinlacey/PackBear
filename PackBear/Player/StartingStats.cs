using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using PackBear.Player.Interface;

namespace PackBear.Player
{
    public class StartingStats : IStartingStats
    {
        public int OptionsCount { get; set; }
        public Dictionary<string, float> Weights { get; }
        public Dictionary<string, IStartingStat> Stats { get; set; } = new Dictionary<string, IStartingStat>();

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
                        Stats.Add(cartStat.Key, new StartingStat());
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quantum
{
  unsafe public class ConfigAssetsHelper
  {
    public static TurnConfig GetTurnConfig(Frame f, TurnType type)
    {
      var configAssets = f.FindAsset<ConfigAssets>(f.RuntimeConfig.ConfigAssets.Id);
      TurnConfig config = null;
      switch (type)
      {
        case TurnType.Countdown:
          config = f.FindAsset<TurnConfig>(configAssets.CountdownTurnConfig.Id);
          break;
        case TurnType.Play:
          config = f.FindAsset<TurnConfig>(configAssets.PlayTurnConfig.Id);
          break;
        default:
          break;
      }
      return config;
    }

    public static GameConfig GetGameConfig(Frame f) {
      var configAssets = f.FindAsset<ConfigAssets>(f.RuntimeConfig.ConfigAssets.Id);
      return f.FindAsset<GameConfig>(configAssets.GameConfig.Id);
    }
  }
}

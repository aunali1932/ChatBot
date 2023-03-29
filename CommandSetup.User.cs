using System;
using System.Collections.Generic;
using Photon.Deterministic;

namespace Quantum {
  public static partial class DeterministicCommandSetup {
    static partial void AddCommandFactoriesUser(ICollection<IDeterministicCommandFactory> factories, RuntimeConfig gameConfig, SimulationConfig simulationConfig) {
      // user commands go here
      factories.Add(new InspectCellCommand());
      factories.Add(new PlaceUnitCommand());
      factories.Add(new RemoveUnitCommand());
      factories.Add(new BuyUnitCommand());
      factories.Add(new BuyExperienceCommand());
      factories.Add(new SellUnitCommand());
      factories.Add(new RefreshStoreCommand());
      factories.Add(new LockStoreCommand());
    }
  }
}

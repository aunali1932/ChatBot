using Photon.Deterministic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Quantum {
  public static class SystemSetup {
    public static SystemBase[] CreateSystems(RuntimeConfig gameConfig, SimulationConfig simulationConfig) {
      return new SystemBase[] {
        // pre-defined core systems
        new Core.CullingSystem2D(), 
        new Core.CullingSystem3D(),
        
        new Core.PhysicsSystem2D(),
        new Core.PhysicsSystem3D(),
        
        new Core.NavigationSystem(),
        new Core.EntityPrototypeSystem(),

        // user systems go here

        //TurnBasedFramework
        new TurnTimerSystem(),
        new CommandSystem(),

        //AutoChess Commands
        new BuyExperienceCommandSystem(),
        new BuyUnitCommandSystem(),
        new PlaceUnitCommandSystem(),
        new RemoveUnitCommandSystem(),
        new SellUnitCommandSystem(),
        new RefreshStoreCommandSystem(),
        new LockStoreCommandSystem(),

        // user systems go here
        new TurnSystem(),
        new GridSystem(),
        new PlayerSystem(),
        new PlayerLevelSystem(),
        new PlayerCurrencySystem(),
        new StoreSystem(),
        new UnitSystem(),
        new ProjectileSystem(),
        new BattleSystem(),
        new BotPlayerSystem(),  
      };
    }
  }
}
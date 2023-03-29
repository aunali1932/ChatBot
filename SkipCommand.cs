using System;
using Photon.Deterministic;

namespace Quantum
{
  [Serializable]
  public struct SkipCommandData
  {
    // game-specific command data here
  }

  public class SkipCommand : DeterministicCommand
  {
    public SkipCommandData Data;

    public override void Serialize(BitStream stream)
    {
      // serialize command data here
    }
  }
}

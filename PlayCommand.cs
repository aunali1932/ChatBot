using System;
using Photon.Deterministic;

namespace Quantum
{
  [Serializable]
  public struct PlayCommandData
  {
    public FP Force;
    public FPVector3 Direction;
    public FPVector2 Spin;
  }

  public class PlayCommand : DeterministicCommand
  {
    public PlayCommandData Data;

    public override void Serialize(BitStream stream)
    {
      // serialize command data here
      stream.Serialize(ref Data.Force);
      stream.Serialize(ref Data.Direction);
      stream.Serialize(ref Data.Spin);
    }
  }
}
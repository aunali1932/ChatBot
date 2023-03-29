using Photon.Deterministic;
using System;

namespace Quantum
{
  partial class RuntimeConfig
  {
    public AssetRefConfigAssets ConfigAssets;
    public AssetRefBallPoolSpec BallPoolSpec;

    partial void SerializeUserData(BitStream stream)
    {
      stream.Serialize(ref ConfigAssets.Id);
      stream.Serialize(ref BallPoolSpec.Id);
    }
  }
}
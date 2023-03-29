using Photon.Deterministic;
using System;

namespace Quantum {
  partial class RuntimeConfig {
    public AssetRefTurnConfig ActiveTurnConfig;
    public AssetRefBoardSpec BoardSpec;
    public AssetRefMatchSpec MatchSpec;

    partial void SerializeUserData(BitStream stream)
    {
      stream.Serialize(ref ActiveTurnConfig.Id);
      stream.Serialize(ref BoardSpec.Id);
      stream.Serialize(ref MatchSpec.Id);
    }
  }
}
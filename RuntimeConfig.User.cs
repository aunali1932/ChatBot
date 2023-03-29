using Photon.Deterministic;
using System;

namespace Quantum {
  partial class RuntimeConfig {
    public AssetRefConfigAssets ConfigAssets;
    public AssetRefGridsSpec GridSpec;
    public AssetRefPlayerSpec PlayerSpec;
    public AssetRefStoreSpec StoreSpec;

    partial void SerializeUserData(BitStream stream)
    {
      stream.Serialize(ref ConfigAssets.Id);
      stream.Serialize(ref GridSpec.Id);
      stream.Serialize(ref PlayerSpec.Id);
      stream.Serialize(ref StoreSpec.Id);
    }
  }
}
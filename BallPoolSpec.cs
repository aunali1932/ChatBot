using System;
using Photon.Deterministic;

namespace Quantum
{
  unsafe partial class BallPoolSpec
  {
    public FP SpinMultiplier;
    public FP EndOfMovementVelocityThreshold;
    public Int32 EndOfMovementWaitingInTicks;

    public Int32 Layer { get; set; }
  }
}

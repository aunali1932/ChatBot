using Photon.Deterministic;

namespace Quantum
{
  public unsafe class TurnTimerSystem : SystemMainThread
  {
    public override void Update(Frame f)
    {
      f.Global->CurrentTurn.Update(f);
    }
  }
}
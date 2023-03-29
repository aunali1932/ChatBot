using System;
using Photon.Deterministic;

namespace Quantum
{
  partial struct TurnData
  {
    public void Update(Frame f)
    {
      var config = f.FindAsset<TurnConfig>(ConfigRef.Id);
      if (config == null || !config.UsesTimer || Status != TurnStatus.Active)
      {
        return;
      }
      Ticks++;
      if (Ticks >= config.TurnDurationInTicks)
      {
        f.Signals.OnTurnEnded(this, TurnEndReason.Time);
      }
    }

    public void AccumulateStats(TurnData from)
    {
      Ticks += from.Ticks;
      Number++;
    }

    public void SetType(TurnType newType, Frame f = null)
    {
      if (Type == newType)
      {
        return;
      }
      var previousType = Type;
      Type = newType;
      f?.Events.TurnTypeChanged(this, previousType);
    }

    public void SetStatus(TurnStatus newStatus, Frame f = null)
    {
      if (Status == newStatus)
      {
        return;
      }
      var previousStatus = Status;
      Status = newStatus;
      f?.Events.TurnStatusChanged(this, previousStatus);
      if (Status == TurnStatus.Active)
      {
        f?.Events.TurnActivated(this);
      }
    }

    // frame is only necessary if caller wants to raise events
    public void ResetTicks(Frame f = null)
    {
      ResetData(Type, Status, Entity, Player, ConfigRef, f);
    }

    public void Reset(TurnConfig config, TurnType type, TurnStatus status, Frame f = null)
    {
      ResetData(type, status, Entity, Player, config, f);
    }

    public void Reset(EntityRef entity, PlayerRef owner, Frame f = null)
    {
      ResetData(Type, Status, entity, owner, ConfigRef, f);
    }

    public void Reset(TurnConfig config, TurnType type, TurnStatus status, EntityRef entity, PlayerRef owner, Frame f = null)
    {
      ResetData(type, status, entity, owner, config, f);
    }

    private void ResetData(TurnType type, TurnStatus status, EntityRef entity, PlayerRef owner, AssetRefTurnConfig config, Frame f = null)
    {
      if (entity != EntityRef.None)
      {
        Entity = entity;
      }
      if (owner != PlayerRef.None)
      {
        Player = owner;
      }

      if (config != null)
      {
        ConfigRef = config;
      }

      var previousType = Type;
      Type = type;
      var previousStatus = Status;
      Status = status;

      Ticks = 0;

      if (Type != previousType)
      {
        f?.Events.TurnTypeChanged(this, previousType);
      }

      if (Status != previousStatus)
      {
        f?.Events.TurnStatusChanged(this, previousStatus);
        if (Status == TurnStatus.Active)
        {
          f?.Events.TurnActivated(this);
        }
      }

      f?.Events.TurnTimerReset(this);
    }
  }
}

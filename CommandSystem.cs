using System;

namespace Quantum
{
  public unsafe class CommandSystem : SystemMainThread
  {
    public override void Update(Frame f)
    {
      var currentTurn = f.Global->CurrentTurn;
      if (currentTurn.Status != TurnStatus.Active)
      {
        return;
      }


      var currentPlayer = f.Global->CurrentTurn.Player;

      switch (f.GetPlayerCommand(currentPlayer))
      {
        case PlayCommand playCommand:
          if (currentTurn.Type != TurnType.Play)
          {
            return;
          }
          f.Signals.OnPlayCommandReceived(currentPlayer, playCommand.Data);
          f.Events.PlayCommandReceived(currentPlayer, playCommand.Data);
          break;

        case SkipCommand skipCommand:
          var config = f.FindAsset<TurnConfig>(currentTurn.ConfigRef.Id);
          if (!config.IsSkippable)
          {
            return;
          }
          f.Signals.OnSkipCommandReceived(currentPlayer, skipCommand.Data);
          f.Events.SkipCommandReceived(currentPlayer, skipCommand.Data);
          break;
      }
    }
  }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Quantum;

public class CameraBehavior : QuantumCallbacks
{
  private bool _isInverted = false;

  public override void OnUpdateView(QuantumGame game)
  {
    var player = game.Session.LocalPlayerIndices[0];
    if (player != 0 && !_isInverted)
    {
      transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y +180, transform.eulerAngles.z);

      for (int i = 0; i < ChessViewUpdater.Instance.Pieces.Length; i++)
      {
        var p = ChessViewUpdater.Instance.Pieces[i];
        p.transform.eulerAngles = new Vector3(p.transform.eulerAngles.x, p.transform.eulerAngles.y, p.transform.eulerAngles.z + 180);
      }
      _isInverted = true;
    }
  }
}

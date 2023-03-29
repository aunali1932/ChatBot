using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Quantum;

public class CheckIndicatorView : QuantumCallbacks {

  public GameObject Indicator;
  public PieceView Piece;

	void Start () {
    Indicator.SetActive(false);
    QuantumEvent.Subscribe<EventPlayerInCheck>(this, SetCheckIndicator);
    QuantumEvent.Subscribe<EventTurnEnded>(this, ResetCheckIndicator);
  }

  private void SetCheckIndicator(EventPlayerInCheck e)
  {
    if (e.Color == Piece.Color)
    {
      Indicator.SetActive(true);
    }
  }

  private void ResetCheckIndicator(EventTurnEnded e)
  {
    Indicator.SetActive(false);
  }

  protected override void OnDisable()
  {
    QuantumEvent.UnsubscribeListener(this);
  }
}

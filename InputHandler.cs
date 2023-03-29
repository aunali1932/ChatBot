using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Quantum;
using Photon.Deterministic;

public unsafe class InputHandler : MonoBehaviour
{

  public int InitialIndex;
  public int TargetIndex;
  public bool HasInitial;
  public bool DebugMoves = false;
  public MovesIndicatorManager MovesIndicator;
  public GameObject SelectedPieceIndicator;

  public UnityEngine.LayerMask BoardsRaycastMask;

  private void Update()
  {
    HandleClick();

    if (HasInitial)
    {
      ChessViewUpdater.Instance.SetObjectByIndex(SelectedPieceIndicator, InitialIndex);
    }
    else {
      SelectedPieceIndicator.SetActive(false);
    }
  }

  public void HandleClick()
  {
    if (UnityEngine.Input.GetMouseButtonDown(0))
    {
      // Perform the Unity raycast
      Ray ray = Camera.main.ScreenPointToRay(UnityEngine.Input.mousePosition);
      RaycastHit hit;
      Physics.Raycast(ray, out hit, 100, BoardsRaycastMask);

      // If the raycast hit the Board...
      if (hit.collider != null)
      {
        var position = new FPVector2(FP.FromFloat_UNSAFE(hit.point.x), FP.FromFloat_UNSAFE(hit.point.z));
        if (HasInitial == false || DebugMoves)
        {
          InitialIndex = BoardHelper.GetIndexByPosition(position);
          HasInitial = true;
          MovesIndicator.UpdatePossibleMovements(InitialIndex);
        }
        else
        {
          TargetIndex = BoardHelper.GetIndexByPosition(position);
          Frame f = QuantumRunner.Default.Game.Frames.Verified;
          if (MoveValidatorHelper.IsValidMove(ref f.Global->Board, InitialIndex, TargetIndex, true) == false)
          {
            InitialIndex = TargetIndex;
            MovesIndicator.UpdatePossibleMovements(InitialIndex);
          }
          else
          {
            HasInitial = false;
            //sendcommand
            var c = new MoveCommand();
            c.Data.InitialIndex = InitialIndex;
            c.Data.TargetIndex = TargetIndex;
            QuantumRunner.Default.Game.SendCommand(c);
            MovesIndicator.ResetPrefabs();
          }
        }
      }
    }
  }
}

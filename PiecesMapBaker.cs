using UnityEngine;
using Quantum;
using Photon.Deterministic;
#if UNITY_EDITOR
using UnityEditor;
#endif

//When a class implements MapDataBakerCallback, it can handle the activation of the "OnBake" event.
//This event is called when you hit the "Bake Data" at the Unity scene
//So this is useful whenever the developer needs to be into the bake process
public class PiecesMapBaker : MapDataBakerCallback
{
  public override void OnBake(MapData data)
  {
    PieceView[] pieces = GameObject.FindObjectsOfType<PieceView>();

    var boardSpec = UnityDB.FindAsset<BoardSpecAsset>(data.Asset.Settings.UserAsset.Id);
    boardSpec.Settings.Pieces = new BoardSpec.PieceMap[pieces.Length];

    FillBoardInformation(boardSpec.Settings, pieces);
#if UNITY_EDITOR
    EditorUtility.SetDirty(boardSpec.Settings.GetUnityAsset());
#endif

  }

  public override void OnBeforeBake(MapData data)
  {
  }

  private void FillBoardInformation(BoardSpec targetSpec, PieceView[] pieces)
  {
    for (int i = 0; i < targetSpec.Pieces.Length; i++)
    {
      targetSpec.Pieces[i].InitialIndex = -1;

      targetSpec.Pieces[i].Type = PieceType.None;
      targetSpec.Pieces[i].Color = PieceColor.None;
    }

    for (int i = 0; i < pieces.Length; i++)
    {
      FP positionX = FP.FromFloat_UNSAFE(pieces[i].transform.position.x);
      FP positionY = FP.FromFloat_UNSAFE(pieces[i].transform.position.z);
      var index = BoardHelper.GetIndexByPosition(new FPVector2(positionX, positionY));
      if (index >= 0)
      {
        targetSpec.Pieces[i].InitialIndex = index;
        pieces[i].IndexOnBoard = index;
        targetSpec.Pieces[i].Type = pieces[i].Type;
        targetSpec.Pieces[i].Color = pieces[i].Color;
      }
      else
      {
        pieces[i].IndexOnBoard = -1;
      }
    }
  }
}

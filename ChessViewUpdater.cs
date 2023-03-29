using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Quantum;

public unsafe class ChessViewUpdater : QuantumCallbacks
{
  public static ChessViewUpdater Instance;

  public PieceView[] Pieces = new PieceView[32];

  public Vector3 PieceOffset = new Vector3(.5f, 0, .5f);

  public GameObject LastMoveIdicatorInitial;
  public GameObject LastMoveIdicatorTarget;

  public Sprite WhiteQueenSprite;
  public Sprite WhiteRookSprite;
  public Sprite WhiteKnightSprite;
  public Sprite WhiteBishopSprite;
  //
  public Sprite BlackQueenSprite;
  public Sprite BlackRookSprite;
  public Sprite BlackKnightSprite;
  public Sprite BlackBishopSprite;

  public DeadPiecesManager DeadPieces;

  private bool _initialized = false;


  public void Start()
  {
    if (Instance == null)
    {
      Instance = this;
    }

    QuantumEvent.Subscribe<EventChangePiecePosition>(this, UpdatePiece);
    QuantumEvent.Subscribe<EventRemovePiece>(this, RemovePiece);
    QuantumEvent.Subscribe<EventPiecePromotion>(this, PiecePromotion);
  }

  public override void OnUpdateView(QuantumGame game)
  {
    if (!_initialized)
    {
      _initialized = true;
      var f = game.Frames.Verified;
      for (int i = 0; i < f.Global->Board.Cells.Length; i++)
      {
        int index = GetPieceIndex(i);
        if (index != -1)
        {
          var p = Pieces[index];
          var position = BoardHelper.GetCordinatesByIndex(p.IndexOnBoard);
          p.SetTargetPosition(new Vector3((float)position.X + PieceOffset.x, PieceOffset.x, (float)position.Y + PieceOffset.z));
        }
      }
    }
  }

  public void PiecePromotion(EventPiecePromotion e)
  {
    var index = GetPieceIndex(e.Index);
    Pieces[index].Type = e.NewType;
    switch (e.NewType)
    {
      case PieceType.Bishop:
        if (Pieces[index].Color == PieceColor.White)
          Pieces[index].GetComponent<SpriteRenderer>().sprite = WhiteBishopSprite;
        else
          Pieces[index].GetComponent<SpriteRenderer>().sprite = BlackBishopSprite;
        break;
      case PieceType.Knight:
        if (Pieces[index].Color == PieceColor.White)
          Pieces[index].GetComponent<SpriteRenderer>().sprite = WhiteKnightSprite;
        else
          Pieces[index].GetComponent<SpriteRenderer>().sprite = BlackKnightSprite;
        break;
      case PieceType.Queen:
        if (Pieces[index].Color == PieceColor.White)
          Pieces[index].GetComponent<SpriteRenderer>().sprite = WhiteQueenSprite;
        else
          Pieces[index].GetComponent<SpriteRenderer>().sprite = BlackQueenSprite;
        break;
      case PieceType.Rook:
        if (Pieces[index].Color == PieceColor.White)
          Pieces[index].GetComponent<SpriteRenderer>().sprite = WhiteRookSprite;
        else
          Pieces[index].GetComponent<SpriteRenderer>().sprite = BlackRookSprite;
        break;
    }
  }

  public void UpdatePiece(EventChangePiecePosition e)
  {
    var origin = (int)e.Index.X;
    var target = (int)e.Index.Y;

    SetObjectByIndex(LastMoveIdicatorInitial, origin);
    SetObjectByIndex(LastMoveIdicatorTarget, target);

    int index = GetPieceIndex(origin);
    if (index != -1)
    {
      var p = Pieces[index];
      p.IndexOnBoard = target;
      var position = BoardHelper.GetCordinatesByIndex(p.IndexOnBoard);
      p.SetTargetPosition(new Vector3((float)position.X + PieceOffset.x, PieceOffset.x, (float)position.Y + PieceOffset.z));
    }
  }

  public void SetObjectByIndex(GameObject go, int index)
  {
    var position = BoardHelper.GetCordinatesByIndex(index);
    go.transform.position = new Vector3((float)position.X + PieceOffset.x, PieceOffset.y, (float)position.Y + PieceOffset.z);
    go.SetActive(true);
  }

  public void RemovePiece(EventRemovePiece e)
  {
    var index = GetPieceIndex(e.Index);
    DeadPieces.StoreDeadPiece(Pieces[index], e.Color);
    Pieces[index].IndexOnBoard = -1;
  }

  public int GetPieceIndex(int index)
  {
    for (int i = 0; i < Pieces.Length; i++)
    {
      if (Pieces[i] == null)
      {
        continue;
      }
      if (Pieces[i].IndexOnBoard == index)
      {
        return i;
      }
    }
    return -1;
  }

  public void OnDisable()
  {
    QuantumEvent.UnsubscribeListener(this);
  }
}

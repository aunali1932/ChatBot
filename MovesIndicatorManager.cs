using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Quantum;

public unsafe class MovesIndicatorManager : QuantumCallbacks
{

  public GameObject HighlightPrefab;

  private List<GameObject> _prefabs = new List<GameObject>();

  public void UpdatePossibleMovements(int index)
  {
    var f = QuantumRunner.Default.Game.Frames.Verified;
    ResetPrefabs();
    for (int i = 0; i < f.Global->Board.Cells.Length; i++)
    {
      if (MoveValidatorHelper.IsValidMove(ref f.Global->Board, index, i, true))
      {
        var FPPosition = BoardHelper.GetCordinatesByIndex(i);
        Vector3 position = new Vector3((float)FPPosition.X + .5f, .5f, (float)FPPosition.Y + .5f);
        var prefab = GetPrefab();
        if (prefab == null)
        {
          var go = Instantiate(HighlightPrefab, position, Quaternion.identity, transform);
          go.SetActive(true);
          _prefabs.Add(go);
        }
        else
        {
          prefab.SetActive(true);
          prefab.transform.position = position;
        }
      }
    }
  }
  private GameObject GetPrefab()
  {
    for (int i = 0; i < _prefabs.Count; i++)
    {
      if (_prefabs[i] != null && _prefabs[i].activeSelf == false)
      {
        return _prefabs[i];
      }
    }
    return null;
  }

  public void ResetPrefabs()
  {
    for (int i = 0; i < _prefabs.Count; i++)
    {
      if (_prefabs[i] != null)
      {
        _prefabs[i].SetActive(false);
      }
    }
  }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Quantum;

public class PieceView : MonoBehaviour {
  public PieceType Type;
  public PieceColor Color;
  public int IndexOnBoard = -1;
  public bool Initialized = false;

  [SerializeField]
  private Vector3 _targetPosition;

  public void SetTargetPosition(Vector3 target)
  {
    Initialized = true;
    _targetPosition = target;
  }

  void Update()
  {
    if (Vector3.Distance(transform.position, _targetPosition) > 0.01f && Initialized) {
      transform.position = Vector3.Lerp(transform.position, _targetPosition, Time.deltaTime * 10);
    }
  }

}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TankDetection : MonoBehaviour
{
  [SerializeField, Tooltip("Радиус обнаружения")]
  private float _detectionRadius;

  [SerializeField, Tooltip("Найденная цель")]
  private Transform _target;

  [SerializeField, Tooltip("")]
  private LayerMask _playerLayerMask;

  //=======================================

  /// <summary>
  /// True, если цель обнаружена
  /// </summary>
  public bool TargetDetected { get; private set; }

  /// <summary>
  /// Найденная цель
  /// </summary>
  public Transform Target
  {
    get => _target;
    set
    {
      _target = value;

    }
  }

  //=======================================

  private void Update()
  {
    CheckTargetDetected();
    //_target = GetTargetDetected();
  }

  //=======================================
  
  private void CheckTargetDetected()
  {
    _target = null;

    var target = Physics2D.OverlapCircle(transform.position, _detectionRadius, _playerLayerMask);

    if (target != null)
    {
      _target = target.transform;
    }

    //return target != null && target.GetComponent<TankController>() ? target.transform : null;
  }

  //=======================================

  private void OnDrawGizmos()
  {
    Gizmos.color = Color.red;
    Gizmos.DrawWireSphere(transform.position, _detectionRadius);
  }

  //=======================================
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTankController : TankControllerBehaviour
{
  [SerializeField, Tooltip("True, если это турель")]
  private bool _isTurret;

  //---------------------------------------

  private TankDetection tankDetection;

  private EnemyTankMovement enemyTankMovement;

  //=======================================

  /// <summary>
  /// True, если это турель
  /// </summary>
  public bool IsTurret => _isTurret;

  //=======================================

  protected override void Awake()
  {
    base.Awake();

    tankDetection = GetComponentInChildren<TankDetection>();

    enemyTankMovement = GetComponentInChildren<EnemyTankMovement>();
  }

  private void Start()
  {
    levelManager.NumberEnemiesLevel++;
  }

  private void OnEnable()
  {
    health.OnDie.AddListener(levelManager.LevelCompleted);
  }

  private void OnDisable()
  {
    health.OnDie.RemoveListener(levelManager.LevelCompleted);
  }

  private void Update()
  {
    if (!_isTurret)
    {
      if (tankDetection.Target == null)
      {
        tankMovement.Move(enemyTankMovement.MovementDirection);
        return;
      }

      tankMovement.Move(Vector2.zero);
    }

    if (tankDetection.Target == null)
      return;

    towerRotation.TowerAim(tankDetection.Target.position);

    if (!towerRotation.GetEnemyTargetDetected())
      return;

    tankBehaviour.TowerController.Shoot();
  }

  //=======================================
}
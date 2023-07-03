using UnityEngine;

public abstract class TankControllerBehaviour : MonoBehaviour
{
  protected TankBehaviour tankBehaviour;
  protected TankMovement tankMovement;
  protected TowerRotation towerRotation;

  protected Health health;

  protected Rigidbody2D rigidbody2D;

  protected LevelManager levelManager;

  //=======================================

  protected virtual void Awake()
  {
    tankBehaviour = GetComponent<TankBehaviour>();
    tankMovement = GetComponentInChildren<TankMovement>();
    towerRotation = GetComponentInChildren<TowerRotation>();

    health = GetComponent<Health>();

    rigidbody2D = GetComponent<Rigidbody2D>();

    levelManager = LevelManager.Instance;
  }

  //=======================================
}
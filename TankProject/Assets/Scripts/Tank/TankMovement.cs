using UnityEngine;

public class TankMovement : MonoBehaviour
{
  [SerializeField, Tooltip("Данные танка")]
  private TankData _tankData;

  //---------------------------------------

  private Rigidbody2D rigidbody2D;

  private Vector2 movement;
  /// <summary>
  /// Текущая скорость
  /// </summary>
  private float currentSpeed = 0;
  /// <summary>
  /// Текущее направление движения
  /// </summary>
  private float currentForewardDirection = 1;

  //=======================================

  /// <summary>
  /// Данные танка
  /// </summary>
  public TankData TankData { get => _tankData; private set => _tankData = value; }

  //=======================================

  private void Start()
  {
    rigidbody2D = GetComponentInParent<Rigidbody2D>();
  }

  private void FixedUpdate()
  {
    rigidbody2D.velocity = (Vector2)transform.up * currentSpeed * currentForewardDirection * Time.fixedDeltaTime;
  }

  //=======================================

  /// <summary>
  /// Движение танка
  /// </summary>
  public void Move(Vector2 parMovement)
  {
    movement = parMovement;
    CalculateSpeed(parMovement);

    if (movement.y > 0)
    {
      if (currentForewardDirection == -1)
        currentSpeed = 0;
      currentForewardDirection = 1;
    }
    else if (movement.y < 0)
    {
      if (currentForewardDirection == 1)
        currentSpeed = 0;
      currentForewardDirection = -1;
    }
  }

  private void CalculateSpeed(Vector2 movementVector)
  {
    if (Mathf.Abs(movementVector.y) > 0) {
      currentSpeed += TankData.Acceleration * Time.deltaTime;
    }
    else {
      currentSpeed -= TankData.Deacceleration * Time.deltaTime;
    }

    currentSpeed = Mathf.Clamp(currentSpeed, 0, TankData.Speed);
  }

  public void Rotation()
  {
    if (rigidbody2D != null)
      rigidbody2D.MoveRotation(transform.rotation * Quaternion.Euler(0, 0, -movement.x * TankData.RotationSpeed * Time.fixedDeltaTime));
  }

  //=======================================
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTankMovement : MonoBehaviour
{
  private EnemyTankController enemyTankController;

  /// <summary>
  /// True, если поворот
  /// </summary>
  private bool isTurning = false;
  /// <summary>
  /// Целевой угол поворота
  /// </summary>
  private Quaternion targetRotation;
  /// <summary>
  /// Длительность поворота
  /// </summary>
  private float rotationDuration = 0.5f;

  //=======================================

  /// <summary>
  /// Направление движения
  /// </summary>
  public Vector2 MovementDirection { get; private set; } = Vector2.zero;

  //=======================================

  private void Awake()
  {
    enemyTankController = GetComponent<EnemyTankController>();
  }

  private void Start()
  {
    if (enemyTankController.IsTurret)
      return;

    isTurning = true;

    Invoke(nameof(SmoothTurn), rotationDuration);
  }

  private void Update()
  {
    if (enemyTankController.IsTurret)
      return;

    RandomDirection();
  }

  //=======================================

  private void RandomDirection()
  {
    float rayLength = 1f;

    var enemyTank = enemyTankController.transform;
    RaycastHit2D hitLeft = Physics2D.Raycast(enemyTank.position - enemyTank.right * 0.34f, enemyTank.up, rayLength, ~LayerMask.GetMask("Enemy", "Bullet"));
    Debug.DrawLine(enemyTank.position - enemyTank.right * 0.34f, enemyTank.position - enemyTank.right * 0.34f + enemyTank.up * rayLength, Color.yellow);
    //RaycastHit2D hitCenter = Physics2D.Raycast(enemyTank.position, enemyTank.up, rayLength, ~LayerMask.GetMask("Enemy", "Bullet"));
    //Debug.DrawLine(enemyTank.position, enemyTank.position + enemyTank.up * rayLength, Color.yellow);
    RaycastHit2D hitRight = Physics2D.Raycast(enemyTank.position + enemyTank.right * 0.34f, enemyTank.up, rayLength, ~LayerMask.GetMask("Enemy", "Bullet"));
    Debug.DrawLine(enemyTank.position + enemyTank.right * 0.34f, enemyTank.position + enemyTank.right * 0.34f + enemyTank.up * rayLength, Color.yellow);

    if ((hitLeft.collider != null || hitRight.collider != null) && !isTurning)
    {
      MovementDirection = Vector2.zero;
      isTurning = true;

      Invoke(nameof(SmoothTurn), rotationDuration);
    }
  }

  /// <summary>
  /// Плавный поворот
  /// </summary>
  private void SmoothTurn()
  {
    float randomRotation = Random.Range(0, 360);
    targetRotation = Quaternion.Euler(0f, 0f, randomRotation);

    StartCoroutine(TurnCoroutine());
  }

  private IEnumerator TurnCoroutine()
  {
    Quaternion startRotation = enemyTankController.transform.rotation;
    float elapsedTime = 0f;

    while (elapsedTime < rotationDuration)
    {
      float t = elapsedTime / rotationDuration;
      enemyTankController.transform.rotation = Quaternion.Lerp(startRotation, targetRotation, t);
      elapsedTime += Time.deltaTime;
      yield return null;
    }

    isTurning = false;
    MovementDirection = Vector2.one;
  }

  //=======================================
}
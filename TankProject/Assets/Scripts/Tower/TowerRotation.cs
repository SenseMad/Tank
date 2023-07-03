using UnityEngine;

public class TowerRotation : MonoBehaviour
{
  private TankBehaviour tankBehaviour;

  //=======================================

  private void Awake()
  {
    tankBehaviour = GetComponentInParent<TankBehaviour>();
  }

  //=======================================

  /// <summary>
  /// Цель башни
  /// </summary>
  public void TowerAim(Vector2 parTowerPosision)
  {
    var towerDirection = (Vector3)parTowerPosision - transform.position;

    var angle = Mathf.Atan2(towerDirection.y, towerDirection.x) * Mathf.Rad2Deg;

    var rotationStep = tankBehaviour.TowerController.TowerData.RotationSpeedTower * Time.deltaTime;

    transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, angle - 90), rotationStep);
  }

  /// <summary>
  /// Сделать выстрел если башня направлена прямо на игрока
  /// </summary>
  public bool GetEnemyTargetDetected()
  {
    Ray2D ray = new Ray2D(transform.position, transform.up);

    float rayLength = 10f;

    Debug.DrawLine(ray.origin, ray.origin + ray.direction * rayLength, Color.blue);

    RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, rayLength, LayerMask.GetMask("Tank"));

    if (hit.collider == null)
      return false;

    return true;
    /*RaycastHit2D[] hits = Physics2D.RaycastAll(ray.origin, ray.direction, rayLength);

    if (hits.Length <= 0 || hits.Length == 1)
      return false;

    if (hits[1].collider.GetComponent<Health>())
      return true;

    if (!hits[1].collider.GetComponent<TankController>())
      return false;*/
  }

  //======================================= 
}
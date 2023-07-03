using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TowerController : MonoBehaviour
{
  [SerializeField, Tooltip("Данные башни")]
  private TowerData _towerData;

  [SerializeField, Tooltip("Точки выстрела")]
  private List<Transform> _shotPoints;

  //---------------------------------------

  private TankBehaviour tankBehaviour;

  /// <summary>
  /// True, если можно стрелять
  /// </summary>
  private bool canShoot = true;

  /// <summary>
  /// Текущая задержка перезарядки
  /// </summary>
  private float currentReloadDelay = 0;

  //=======================================

  /// <summary>
  /// Данные башни
  /// </summary>
  public TowerData TowerData => _towerData;

  //=======================================

  /// <summary>
  /// Событие: Стрельба
  /// </summary>
  public CustomUnityEvent OnShoot { get; } = new CustomUnityEvent();

  //=======================================

  private void Update()
  {
    Reload();
  }

  //=======================================

  /// <summary>
  /// Инициализация
  /// </summary>
  public void Initialize(TankBehaviour parTankBehaviour)
  {
    tankBehaviour = parTankBehaviour;
  }

  /// <summary>
  /// Перезарядка
  /// </summary>
  private void Reload()
  {
    if (!canShoot)
    {
      currentReloadDelay += Time.deltaTime;
      if (currentReloadDelay >= TowerData.ReloadDelay)
      {
        canShoot = true;
      }
    }
  }

  /// <summary>
  /// Выстрел
  /// </summary>
  public bool Shoot()
  {
    if (!canShoot)
      return false;

    canShoot = false;
    currentReloadDelay = 0;

    foreach (var shotPoint in _shotPoints)
    {
      var bullet = Instantiate(tankBehaviour.BulletController);
      bullet.transform.position = shotPoint.position;
      bullet.transform.localRotation = shotPoint.rotation;

      bullet.Initialize(tankBehaviour.BulletController.BulletData);
    }

    return true;
  }

  //=======================================
}
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

/// <summary>
/// Переключение пуль
/// </summary>
public class SwitchingBullet : MonoBehaviour
{
  /// <summary>
  /// Текущая пуля
  /// </summary>
  private BulletController currentBulletController;

  private TankBehaviour tankBehaviour;

  /// <summary>
  /// Текущий индекс пули
  /// </summary>
  private int currentIndexBullet = 0;

  //=======================================

  /// <summary>
  /// Событие: Сменить пулю
  /// </summary>
  public CustomUnityEvent<BulletController> ChangeBullet { get; } = new CustomUnityEvent<BulletController>();

  //=======================================

  private void Awake()
  {
    tankBehaviour = GetComponent<TankBehaviour>();
  }

  private void Start()
  {
    CreatingBullet();
  }

  //=======================================

  /// <summary>
  /// Создание пули
  /// </summary>
  private void CreatingBullet()
  {
    if (tankBehaviour.ListPrefabsBullets.Count == 0)
      return;

    currentIndexBullet = 0;

    currentBulletController = tankBehaviour.ListPrefabsBullets[currentIndexBullet];

    ChangeBullet?.Invoke(currentBulletController);
  }

  //=======================================

  /// <summary>
  /// Сменить пулю
  /// </summary>
  public void ChangeBullet_performed(InputAction.CallbackContext obj)
  {
    if (obj.ReadValue<Vector2>().x > 0)
      currentIndexBullet++;
    else
      currentIndexBullet--;

    currentIndexBullet = Mathf.Clamp(currentIndexBullet, 0, tankBehaviour.ListPrefabsBullets.Count - 1);

    currentBulletController = tankBehaviour.ListPrefabsBullets[currentIndexBullet];

    ChangeBullet?.Invoke(currentBulletController);
  }

  //=======================================
}
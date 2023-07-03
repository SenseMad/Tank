using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

/// <summary>
/// Переключение башен
/// </summary>
public class SwitchingTowers : MonoBehaviour
{
  [SerializeField, Tooltip("Родительский объект башни")]
  private Transform _transformTowerParent;

  //---------------------------------------

  /// <summary>
  /// Текущая башня
  /// </summary>
  private TowerController currentTowerController;

  private TankBehaviour tankBehaviour;

  /// <summary>
  /// Список созданных башен
  /// </summary>
  private List<TowerController> listTowersCreated = new List<TowerController>();

  /// <summary>
  /// Текущий индекс башни
  /// </summary>
  private int currentIndexTower = 0;

  //=======================================

  /// <summary>
  /// Событие: Сменить башню
  /// </summary>
  public CustomUnityEvent<TowerController> ChangeTower { get; } = new CustomUnityEvent<TowerController>();

  //=======================================

  private void Awake()
  {
    tankBehaviour = GetComponent<TankBehaviour>();
  }

  private void Start()
  {
    CreatingTowers();
  }

  //=======================================

  /// <summary>
  /// Создание башен
  /// </summary>
  private void CreatingTowers()
  {
    if (tankBehaviour.ListPrefabsTowers.Count == 0)
      return;

    currentIndexTower = 0;

    if (_transformTowerParent != null)
    {
      foreach (var item in tankBehaviour.ListPrefabsTowers)
      {
        var newObject = Instantiate(item);

        newObject.Initialize(tankBehaviour);

        newObject.transform.parent = _transformTowerParent;
        newObject.transform.SetLocalPositionAndRotation(new Vector3(0, 0.25f, 0), Quaternion.identity);
        newObject.transform.localScale = Vector3.one;
        newObject.gameObject.SetActive(false);

        listTowersCreated.Add(newObject);
      }

      listTowersCreated[currentIndexTower].gameObject.SetActive(true);

      currentTowerController = listTowersCreated[currentIndexTower];
    }
    else {
      currentTowerController = tankBehaviour.ListPrefabsTowers[currentIndexTower];
      currentTowerController.Initialize(tankBehaviour);
    }

    ChangeTower?.Invoke(currentTowerController);
  }

  //=======================================

  /// <summary>
  /// Сменить башню
  /// </summary>
  public void ChangeTower_performed(InputAction.CallbackContext obj)
  {
    listTowersCreated[currentIndexTower].gameObject.SetActive(false);

    if (obj.ReadValue<Vector2>().x > 0)
      currentIndexTower++;
    else
      currentIndexTower--;

    currentIndexTower = Mathf.Clamp(currentIndexTower, 0, listTowersCreated.Count - 1);

    listTowersCreated[currentIndexTower].gameObject.SetActive(true);

    currentTowerController = listTowersCreated[currentIndexTower];

    ChangeTower?.Invoke(currentTowerController);
  }

  //=======================================
}
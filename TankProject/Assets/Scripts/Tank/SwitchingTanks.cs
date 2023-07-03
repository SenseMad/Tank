using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class SwitchingTanks : MonoBehaviour
{
  [SerializeField, Tooltip("Родительский объект танка")]
  private Transform _transformTankBodyParent;

  //---------------------------------------

  /// <summary>
  /// Текущий активный танк
  /// </summary>
  private TankMovement currentTankMovement;

  private TankBehaviour tankBehaviour;

  /// <summary>
  /// Список созданных танков
  /// </summary>
  private List<TankMovement> listTankCreated = new List<TankMovement>();
  
  /// <summary>
  /// Текущий активный индекс танка
  /// </summary>
  private int currentIndexTank = 0;

  //=======================================

  /// <summary>
  /// Событие: Сменить танк
  /// </summary>
  public CustomUnityEvent<TankMovement> ChangeTank { get; } = new CustomUnityEvent<TankMovement>();

  //=======================================

  private void Awake()
  {
    tankBehaviour = GetComponent<TankBehaviour>();

    CreatingTanks();
  }

  //=======================================

  /// <summary>
  /// Создание танков
  /// </summary>
  private void CreatingTanks()
  {
    if (tankBehaviour.ListPrefabsTowers.Count == 0)
      return;

    currentIndexTank = 0;

    if (_transformTankBodyParent != null)
    {
      foreach (var item in tankBehaviour.ListPrefabsTanks)
      {
        var newObject = Instantiate(item);

        newObject.transform.parent = _transformTankBodyParent;
        newObject.transform.SetLocalPositionAndRotation(new Vector3(0, 0, 0), Quaternion.identity);
        newObject.transform.localScale = Vector3.one;
        newObject.gameObject.SetActive(false);

        listTankCreated.Add(newObject);
      }

      listTankCreated[currentIndexTank].gameObject.SetActive(true);

      currentTankMovement = listTankCreated[currentIndexTank];
    }
    else
    {
      currentTankMovement = tankBehaviour.ListPrefabsTanks[currentIndexTank];
    }

    ChangeTank?.Invoke(currentTankMovement);
  }

  //=======================================

  /// <summary>
  /// Сменить танк
  /// </summary>
  public void ChangeTank_performed(InputAction.CallbackContext obj)
  {
    listTankCreated[currentIndexTank].gameObject.SetActive(false);

    if (obj.ReadValue<Vector2>().x > 0)
      currentIndexTank++;
    else
      currentIndexTank--;

    currentIndexTank = Mathf.Clamp(currentIndexTank, 0, listTankCreated.Count - 1);

    listTankCreated[currentIndexTank].gameObject.SetActive(true);

    currentTankMovement = listTankCreated[currentIndexTank];

    ChangeTank?.Invoke(currentTankMovement);
  }

  //=======================================
}
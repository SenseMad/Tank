using System.Collections.Generic;
using UnityEngine;

public class PickUpController : MonoBehaviour
{
  [SerializeField, Tooltip("Список предметов которые можно создать")]
  private List<PickUp> _pickUpList;

  //---------------------------------------

  private Health health;

  //=======================================

  private void Awake()
  {
    health = GetComponent<Health>();
  }

  private void OnEnable()
  {
    health.OnDie.AddListener(CreatePickUp);
  }

  private void OnDisable()
  {
    health.OnDie.RemoveListener(CreatePickUp);
  }

  //=======================================

  private void CreatePickUp()
  {
    if (_pickUpList.Count == 0)
      return;

    int randomNumber = Random.Range(-1, _pickUpList.Count);
    if (randomNumber < 0)
      return;

    Instantiate(_pickUpList[randomNumber], transform.position, Quaternion.identity);
  }

  //=======================================
}
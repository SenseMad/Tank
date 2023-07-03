using UnityEngine;

public class HealthPickUp : PickUp
{
  /// <summary>
  /// Количество здоровья
  /// </summary>
  private int amountHealth;

  //=======================================

  private void Awake()
  {
    amountHealth = Random.Range(5, 20);
  }

  //=======================================

  protected override void OnPicked(Collider2D parCollider2D)
  {    
    if (parCollider2D.TryGetComponent<Health>(out var parHealth))
    {
      parHealth.AddHealth(amountHealth);
      Destroy(gameObject);
    }
  }

  //=======================================
}
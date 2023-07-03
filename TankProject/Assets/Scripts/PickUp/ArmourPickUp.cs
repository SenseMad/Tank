using UnityEngine;

/// <summary>
/// Подбор бонуса брони
/// </summary>
public class ArmourPickUp : PickUp
{
  /// <summary>
  /// Количество брони
  /// </summary>
  private int amountArmour;

  //=======================================

  private void Awake()
  {
    amountArmour = Random.Range(5, 20);
  }

  //=======================================

  protected override void OnPicked(Collider2D parCollider2D)
  {
    if (parCollider2D.TryGetComponent<Health>(out var parArmour))
    {
      parArmour.AddArmour(amountArmour);
      Destroy(gameObject);
    }
  }

  //=======================================
}
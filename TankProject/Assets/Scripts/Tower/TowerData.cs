using UnityEngine;

[CreateAssetMenu(fileName = "NewTowerData", menuName = "Data/TowerData")]
public class TowerData : ScriptableObject
{
  [SerializeField, Header("Скорость поворота башни")]
  private float _rotationSpeedTower;

  [SerializeField, Header("Зажержка перезарядки")]
  private float _reloadDelay;

  //=======================================

  /// <summary>
  /// Скорость поворота башни
  /// </summary>
  public float RotationSpeedTower { get => _rotationSpeedTower; set => _rotationSpeedTower = value; }

  /// <summary>
  /// Зажержка перезарядки
  /// </summary>
  public float ReloadDelay { get => _reloadDelay; set => _reloadDelay = value; }

  //=======================================
}
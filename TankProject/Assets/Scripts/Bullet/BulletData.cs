using UnityEngine;

[CreateAssetMenu(fileName = "NewBulletData", menuName = "Data/BulletData")]
public class BulletData : ScriptableObject
{
  [SerializeField, Tooltip("Название пули")]
  private string _nameBullet;

  [SerializeField, Header("Скорость пули")]
  private float _speedBullet;

  [SerializeField, Header("Урон пули")]
  private int _damageBullet;

  [SerializeField, Header("Максимальная дистанция полета пули")]
  private float _maxDistance;

  [SerializeField, Header("Радиус взрыва"), Range(0, 10)]
  private float _explosionRadius;

  //=======================================

  /// <summary>
  /// Название пули
  /// </summary>
  public string NameBullet { get => _nameBullet; private set => _nameBullet = value; }

  /// <summary>
  /// Скорость пули
  /// </summary>
  public float SpeedBullet { get => _speedBullet; private set => _speedBullet = value; }

  /// <summary>
  /// Урон пули
  /// </summary>
  public int DamageBullet { get => _damageBullet; private set => _damageBullet = value; }

  /// <summary>
  /// Максимальная дистанция полета пули
  /// </summary>
  public float MaxDistance { get => _maxDistance; private set => _maxDistance = value; }

  /// <summary>
  /// Радиус взрыва
  /// </summary>
  public float ExplosionRadius { get => _explosionRadius; private set => _explosionRadius = value; }

  //=======================================
}
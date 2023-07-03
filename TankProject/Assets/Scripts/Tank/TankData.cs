using UnityEngine;

[CreateAssetMenu(fileName = "NewTankData", menuName = "Data/TankData")]
public class TankData : ScriptableObject
{
  [SerializeField, Tooltip("Название танка")]
  private string _nameTank;

  [SerializeField, Header("Скорость танка")]
  private float _speed;

  [SerializeField, Header("Скорость поворота танка")]
  private float _rotationSpeed;

  [SerializeField, Header("Ускорение танка")]
  private float _acceleration;

  [SerializeField, Header("Замедление танка")]
  private float _deacceleration;

  [Header("ЗДОРОВЬЕ / БРОНЯ")]
  [SerializeField, Tooltip("Максимальное здоровье")]
  private int _maxHealth;
  [SerializeField, Tooltip("Максимальная броня")]
  private int _maxArmour;

  //=======================================

  /// <summary>
  /// Название танка
  /// </summary>
  public string NameTank { get => _nameTank; private set => _nameTank = value; }

  /// <summary>
  /// Скорость танка
  /// </summary>
  public float Speed { get => _speed; private set => _speed = value; }

  /// <summary>
  /// Скорость поворота танка
  /// </summary>
  public float RotationSpeed { get => _rotationSpeed; private set => _rotationSpeed = value; }

  /// <summary>
  /// Ускорение танка
  /// </summary>
  public float Acceleration { get => _acceleration; private set => _acceleration = value; }

  /// <summary>
  /// Замедление танка
  /// </summary>
  public float Deacceleration { get => _deacceleration; private set => _deacceleration = value; }

  /// <summary>
  /// Максимальное здоровье
  /// </summary>
  public int MaxHealth { get => _maxHealth; private set => _maxHealth = value; }
  /// <summary>
  /// Максимальная броня
  /// </summary>
  public int MaxArmour { get => _maxArmour; private set => _maxArmour = value; }

  //=======================================
}
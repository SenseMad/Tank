using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
  [SerializeField, Tooltip("Максимальное здоровье")]
  private int _maxHealth;

  [SerializeField, Tooltip("Максимальная броня")]
  private int _maxArmour;

  [SerializeField, Tooltip("Эффект смерти")]
  private GameObject _effectDie;

  //---------------------------------------

  private int currentHealth;
  private int currentArmour;

  /// <summary>
  /// Задержка перед пополнением брони
  /// </summary>
  private readonly float delayBeforeArmour = 3.0f;
  /// <summary>
  /// Текущее время пополнения брони
  /// </summary>
  private float currentTimeArmour = 0;

  //=======================================

  /// <summary>
  /// Максимальное здоровье
  /// </summary>
  public int MaxHealth
  {
    get => _maxHealth;
    set
    {
      _maxHealth = value;
      CurrentHealth = value;
    }
  }
  /// <summary>
  /// Максимальная броня
  /// </summary>
  public int MaxArmour
  {
    get => _maxArmour;
    set
    {
      _maxArmour = value;
      CurrentArmour = value;
    }
  }

  /// <summary>
  /// Текущее здоровье
  /// </summary>
  public int CurrentHealth
  {
    get => currentHealth;
    set
    {
      currentHealth = Mathf.Clamp(value, 0, _maxHealth);
      ChangeHealth?.Invoke(currentHealth);
    }
  }
  /// <summary>
  /// Текущая броня
  /// </summary>
  public int CurrentArmour
  {
    get => currentArmour;
    set
    {
      currentArmour = Mathf.Clamp(value, 0, _maxArmour);
      ChangeArmour?.Invoke(currentArmour);
    }
  }

  //=======================================

  /// <summary>
  /// Событие: Изменение здоровья
  /// </summary>
  public CustomUnityEvent<float> ChangeHealth { get; } = new CustomUnityEvent<float>();

  /// <summary>
  /// Событие: Изменение брони
  /// </summary>
  public CustomUnityEvent<float> ChangeArmour { get; } = new CustomUnityEvent<float>();

  /// <summary>
  /// Событие: Смерть
  /// </summary>
  public CustomUnityEvent OnDie { get; } = new CustomUnityEvent();

  //=======================================

  private void Start()
  {
    currentHealth = _maxHealth;
    currentArmour = _maxArmour;
  }

  private void Update()
  {
    if (CurrentArmour == _maxArmour)
      return;

    currentTimeArmour += Time.deltaTime;
    if (currentTimeArmour >= delayBeforeArmour)
    {
      CurrentArmour += (int)(_maxArmour / 5);
      currentTimeArmour = 0;
    }
  }

  //=======================================

  /// <summary>
  /// Добавить здоровье
  /// </summary>
  public void AddHealth(int parHealth)
  {
    CurrentHealth += parHealth;
  }

  /// <summary>
  /// Добавить броню
  /// </summary>
  public void AddArmour(int parArmour)
  {
    CurrentArmour += parArmour;
  }

  /// <summary>
  /// Получение урона
  /// </summary>
  public void TakeDamage(int parDamage)
  {
    if (CurrentArmour <= _maxArmour && CurrentArmour != 0)
    {
      int armourBefore = CurrentArmour;
      CurrentArmour -= parDamage;

      int damageAmount = armourBefore - CurrentArmour;
      if (damageAmount < 0)
        CurrentHealth -= damageAmount;
    }
    else
    {
      CurrentHealth -= parDamage;
    }

    currentTimeArmour = 0;
    
    if (CurrentHealth == 0)
      Die();
  }
  
  /// <summary>
  /// Смерть
  /// </summary>
  private void Die()
  {
    OnDie?.Invoke();
    gameObject.SetActive(false);

    if (_effectDie == null)
      return;

    var effectObject = Instantiate(_effectDie);
    effectObject.transform.SetPositionAndRotation(transform.position, Quaternion.Euler(0, 0, Random.Range(0, 360)));
    Destroy(effectObject, 1.0f);
  }

  //=======================================
}
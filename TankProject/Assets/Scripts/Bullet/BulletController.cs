using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
  [SerializeField, Tooltip("Данные пули")]
  private BulletData _bulletData;

  [SerializeField, Tooltip("Объект эффекта попадания")]
  private GameObject _objectEffectHitShot;

  //---------------------------------------

  private Rigidbody2D rigidbody2D;

  /// <summary>
  /// Начальная позиция пули
  /// </summary>
  private Vector2 startBulletPosition;
  /// <summary>
  /// Пройденное пулей расстояние
  /// </summary>
  private float bulletDistanceTraveled = 0;

  /// <summary>
  /// Данные пули
  /// </summary>
  public BulletData BulletData => _bulletData;

  //=======================================

  private void Awake()
  {
    rigidbody2D = GetComponent<Rigidbody2D>();
  }

  private void Update()
  {
    bulletDistanceTraveled = Vector2.Distance(transform.position, startBulletPosition);
    if (bulletDistanceTraveled >= _bulletData.MaxDistance)
    {
      DeleteBulletObject();
    }
  }

  //=======================================

  /// <summary>
  /// Инициализация пули
  /// </summary>
  public void Initialize(BulletData parBulletData)
  {
    _bulletData = parBulletData;
    startBulletPosition = transform.position;
    rigidbody2D.velocity = transform.up * _bulletData.SpeedBullet;
  }

  /// <summary>
  /// Получить все объекты со здоровьем в радиусе
  /// </summary>
  private List<Health> GetObjectsRadius()
  {
    var retHealth = new List<Health>();
    var colliders = Physics2D.OverlapCircleAll(transform.position, _bulletData.ExplosionRadius);

    foreach (var collider in colliders)
    {
      if (!collider.TryGetComponent(out Health parHealth))
        continue;

      retHealth.Add(parHealth);
    }

    return retHealth;
  }

  /// <summary>
  /// Удалить объект пули
  /// </summary>
  private void DeleteBulletObject()
  {
    rigidbody2D.velocity = Vector2.zero;
    gameObject.SetActive(false);

    Destroy(gameObject, 1.0f);
  }

  //=======================================

  private void OnTriggerEnter2D(Collider2D collision)
  {
    var effectObject = Instantiate(_objectEffectHitShot);
    effectObject.transform.SetPositionAndRotation(transform.position, Quaternion.Euler(0, 0, Random.Range(0, 360)));
    Destroy(effectObject, 1.0f);

    var listHealth = GetObjectsRadius();

    if (collision.TryGetComponent(out Health parHealth))
    {
      parHealth.TakeDamage(_bulletData.DamageBullet);
      listHealth.Remove(parHealth);
    }

    // Урон в радиусе
    foreach (var health in listHealth)
    {
      health.TakeDamage(_bulletData.DamageBullet);
    }

    DeleteBulletObject();
  }

  //=======================================
}
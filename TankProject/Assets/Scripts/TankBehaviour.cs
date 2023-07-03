using System.Collections.Generic;
using UnityEngine;

public abstract class TankBehaviour : MonoBehaviour
{
  [SerializeField, Tooltip("Список префабов танков")]
  private List<TankMovement> _listPrefabsTanks;

  [SerializeField, Tooltip("Список префабов башен")]
  private List<TowerController> _listPrefabsTowers;

  [SerializeField, Tooltip("Список префабов пуль")]
  private List<BulletController> _listPrefabsBullets;

  //=======================================

  protected InputHandler inputHandler;

  protected SwitchingTowers switchingTowers;

  //=======================================

  public SwitchingTanks SwitchingTanks { get; set; }
  public SwitchingBullet SwitchingBullet { get; set; }

  /// <summary>
  /// Список префабов танков
  /// </summary>
  public List<TankMovement> ListPrefabsTanks => _listPrefabsTanks;

  /// <summary>
  /// Список префабов башен
  /// </summary>
  public List<TowerController> ListPrefabsTowers => _listPrefabsTowers;

  /// <summary>
  /// Список префабов пуль
  /// </summary>
  public List<BulletController> ListPrefabsBullets => _listPrefabsBullets;

  //---------------------------------------

  /// <summary>
  /// Текущий активный танк
  /// </summary>
  public TankMovement TankMovement { get; private set; }

  /// <summary>
  /// Текущая активная башня
  /// </summary>
  public TowerController TowerController { get; private set; }

  /// <summary>
  /// Текущая активная пуля
  /// </summary>
  public BulletController BulletController { get; private set; }

  /// <summary>
  /// Здоровье танка
  /// </summary>
  public Health Health { get; private set; }

  //=======================================

  protected virtual void Awake()
  {
    inputHandler = InputHandler.Instance;

    TankMovement = GetComponentInChildren<TankMovement>();

    SwitchingTanks = GetComponent<SwitchingTanks>();
    switchingTowers = GetComponent<SwitchingTowers>();
    SwitchingBullet = GetComponent<SwitchingBullet>();

    Health = GetComponent<Health>();
  }

  protected virtual void Start()
  {    
    Health.MaxHealth = TankMovement.TankData.MaxHealth;
    Health.MaxArmour = TankMovement.TankData.MaxArmour;
  }

  protected virtual void OnEnable()
  {
    SwitchingTanks.ChangeTank.AddListener(GetTank);

    switchingTowers.ChangeTower.AddListener(GetTower);

    SwitchingBullet.ChangeBullet.AddListener(GetBullet);
  }

  protected virtual void OnDisable()
  {
    SwitchingTanks.ChangeTank.RemoveListener(GetTank);

    switchingTowers.ChangeTower.RemoveListener(GetTower);

    SwitchingBullet.ChangeBullet.RemoveListener(GetBullet);
  }

  //=======================================

  /// <summary>
  /// Получить танк
  /// </summary>
  protected virtual void GetTank(TankMovement parTankMovement)
  {
    TankMovement = parTankMovement;

    Health.MaxHealth = TankMovement.TankData.MaxHealth;
    Health.MaxArmour = TankMovement.TankData.MaxArmour;
  }

  /// <summary>
  /// Получить башню
  /// </summary>
  protected virtual void GetTower(TowerController parTowerController)
  {
    TowerController = parTowerController;
  }

  /// <summary>
  /// Получить пулю
  /// </summary>
  protected virtual void GetBullet(BulletController parBulletController)
  {
    BulletController = parBulletController;
  }

  //=======================================
}
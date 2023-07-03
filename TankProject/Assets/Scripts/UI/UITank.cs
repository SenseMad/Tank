using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UITank : MonoBehaviour
{
  [SerializeField, Tooltip("Слайдер здоровья")]
  private Slider _healthBarSlider;
  [SerializeField, Tooltip("Слайдер брони")]
  private Slider _armourBarSlider;

  [Space(10)]
  [SerializeField, Tooltip("Текст названия танка")]
  private TextMeshProUGUI _textTank;
  [SerializeField, Tooltip("Текст названия снаряда")]
  private TextMeshProUGUI _textBullet;

  //---------------------------------------

  private Tank tank;

  //=======================================

  private void Awake()
  {
    tank = Tank.Instance;
  }

  private void Start()
  {
    UpdateTextTank(tank.TankMovement);
    UpdateTextBullet(tank.BulletController);
  }

  private void OnEnable()
  {
    tank.SwitchingTanks.ChangeTank.AddListener(UpdateTextTank);
    tank.SwitchingBullet.ChangeBullet.AddListener(UpdateTextBullet);

    tank.Health.ChangeHealth.AddListener(UpdateSliderHealth);
    tank.Health.ChangeArmour.AddListener(UpdateSliderArmour);
  }

  private void OnDisable()
  {
    tank.SwitchingTanks.ChangeTank.RemoveListener(UpdateTextTank);
    tank.SwitchingBullet.ChangeBullet.RemoveListener(UpdateTextBullet);

    tank.Health.ChangeHealth.RemoveListener(UpdateSliderHealth);
    tank.Health.ChangeArmour.RemoveListener(UpdateSliderArmour);
  }

  //=======================================

  /// <summary>
  /// Обновить текст танка
  /// </summary>
  private void UpdateTextTank(TankMovement parTankMovement)
  {
    _textTank.text = $"{parTankMovement.TankData.NameTank}";
  }

  /// <summary>
  /// Обновить текст снаряда
  /// </summary>
  private void UpdateTextBullet(BulletController parBulletController)
  {
    _textBullet.text = $"{parBulletController.BulletData.NameBullet}";
  }

  /// <summary>
  /// Обновить слайдер здоровья
  /// </summary>
  private void UpdateSliderHealth(float parValue)
  {
    _healthBarSlider.value = (float)parValue / tank.Health.MaxHealth;
  }
  /// <summary>
  /// Обновить слайдер брони
  /// </summary>
  private void UpdateSliderArmour(float parValue)
  {
    int maxArmour = tank.Health.MaxArmour;
    _armourBarSlider.value = (float)parValue / (maxArmour = maxArmour == 0 ? 1 : tank.Health.MaxArmour);
  }

  //=======================================
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
  [SerializeField, Tooltip("Слайдер здоровья")]
  private Slider _healthBarSlider;

  [SerializeField, Tooltip("Слайдер брони")]
  private Slider _armourBarSlider;

  //---------------------------------------

  private Health health;

  //=======================================

  private void Awake()
  {
    health = GetComponentInParent<Health>();
  }

  private void OnEnable()
  {
    health.ChangeHealth.AddListener(ChangeHealthBarSlider);
    health.ChangeArmour.AddListener(ChangeArmourBarSlider);
  }

  private void OnDisable()
  {
    health.ChangeHealth.RemoveListener(ChangeHealthBarSlider);
    health.ChangeArmour.RemoveListener(ChangeArmourBarSlider);
  }

  //=======================================

  private void ChangeHealthBarSlider(float parValue)
  {
    if (_healthBarSlider == null)
      return;

    if (health.CurrentHealth >= health.MaxHealth) {
      _healthBarSlider.gameObject.SetActive(false);
    }
    else {
      _healthBarSlider.gameObject.SetActive(true);
    }

    _healthBarSlider.value = (float)health.CurrentHealth / health.MaxHealth;
    //_healthBarSlider.value = parValue;
  }
  
  private void ChangeArmourBarSlider(float parValue)
  {
    if (_armourBarSlider == null)
      return;

    if (health.CurrentArmour >= health.MaxArmour) {
      _armourBarSlider.gameObject.SetActive(false);
    }
    else {
      _armourBarSlider.gameObject.SetActive(true);
      _healthBarSlider.gameObject.SetActive(true);
    }

    _armourBarSlider.value = (float)health.CurrentArmour / health.MaxArmour;
    //_armourBarSlider.value = parValue;
  }

  //=======================================
}
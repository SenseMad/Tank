using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : TankBehaviour
{
  private static Tank _instance;

  //=======================================

  public static Tank Instance
  {
    get
    {
      if (_instance == null) { _instance = FindObjectOfType<Tank>(); }
      return _instance;
    }
  }

  //=======================================

  protected override void Awake()
  {
    base.Awake();
    
    if (_instance != null && _instance != this)
    {
      Destroy(this);
      return;
    }
    _instance = this;
  }

  protected override void OnEnable()
  {
    base.OnEnable();

    inputHandler.AI_Player.Player.ChangeTank.performed += SwitchingTanks.ChangeTank_performed;

    inputHandler.AI_Player.Player.ChangeTower.performed += switchingTowers.ChangeTower_performed;

    inputHandler.AI_Player.Player.ChangeBullet.performed += SwitchingBullet.ChangeBullet_performed;
  }

  protected override void OnDisable()
  {
    base.OnDisable();

    inputHandler.AI_Player.Player.ChangeTank.performed -= SwitchingTanks.ChangeTank_performed;

    inputHandler.AI_Player.Player.ChangeTower.performed -= switchingTowers.ChangeTower_performed;

    inputHandler.AI_Player.Player.ChangeBullet.performed -= SwitchingBullet.ChangeBullet_performed;
  }

  //=======================================

  protected override void GetTank(TankMovement parTankMovement)
  {
    base.GetTank(parTankMovement);

    Health.MaxHealth = TankMovement.TankData.MaxHealth;
    Health.MaxArmour = TankMovement.TankData.MaxArmour;
  }

  //=======================================
}
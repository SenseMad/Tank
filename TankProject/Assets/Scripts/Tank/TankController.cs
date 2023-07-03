using UnityEngine;
using UnityEngine.InputSystem;

public class TankController : TankControllerBehaviour
{
  private InputHandler inputHandler;

  private Camera mainCamera;

  //=======================================

  protected override void Awake()
  {
    base.Awake();

    inputHandler = InputHandler.Instance;

    mainCamera = Camera.main;
  }

  private void OnEnable()
  {
    inputHandler.AI_Player.Player.Shoot.performed += Shoot_performed;

    health.OnDie.AddListener(levelManager.Defeat);
  }

  private void OnDisable()
  {
    inputHandler.AI_Player.Player.Shoot.performed -= Shoot_performed;

    health.OnDie.RemoveListener(levelManager.Defeat);
  }

  private void Update()
  {
    // Движение танка
    Vector2 move = inputHandler.GetInputMovement();
    tankBehaviour.TankMovement.Move(move.normalized);

    tankBehaviour.TankMovement.Rotation();

    // Поворот башни
    towerRotation.TowerAim(inputHandler.GetMousePosition(mainCamera));
  }

  //=======================================

  /// <summary>
  /// Кнопка выстрела
  /// </summary>
  private void Shoot_performed(InputAction.CallbackContext obj)
  {
    if (!inputHandler.GetInputShoot())
      return;

    if (tankBehaviour.TowerController.Shoot())
      CameraShake.Shake(0.2f, 0.1f);
  }

  //=======================================
}
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
  private static InputHandler instance;

  //=======================================

  public AI_Player AI_Player {  get; private set; }

  //=======================================

  public static InputHandler Instance
  {
    get
    {
      if (instance == null)
      {
        var singleton = new GameObject("InputHandler");
        instance = singleton.AddComponent<InputHandler>();
        DontDestroyOnLoad(singleton);
      }

      return instance;
    }
  }

  //=======================================

  private void Awake()
  {
    AI_Player = new AI_Player();

    if (instance == null)
    {
      instance = this;
      DontDestroyOnLoad(gameObject);
    }
    else
    {
      Destroy(gameObject);
      return;
    }
  }

  private void OnEnable()
  {
    AI_Player.Enable();
  }

  private void OnDisable()
  {
    AI_Player.Disable();
  }

  //=======================================

  /// <summary>
  /// True, если можно использовать управление
  /// </summary>
  public bool CanInput()
  {
    return AI_Player != null;
  }

  //=======================================

  /// <summary>
  /// Получить кнопки управления
  /// </summary>
  public Vector2 GetInputMovement()
  {
    return CanInput() ? AI_Player.Player.Movement.ReadValue<Vector2>() : default;
  }

  /// <summary>
  /// Получить кнопку стрельбы
  /// </summary>
  public bool GetInputShoot()
  {
    return CanInput();
  }

  /// <summary>
  /// Получить мировую позицию мыши
  /// </summary>
  public Vector2 GetMousePosition(Camera parCamera)
  {
    Vector2 mousePosition = Mouse.current.position.ReadValue();
    Vector3 worldPosition = parCamera.ScreenToWorldPoint(mousePosition);

    return worldPosition;
  }

  //=======================================
}
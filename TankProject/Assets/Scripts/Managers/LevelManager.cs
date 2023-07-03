using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
  private static LevelManager _instance;

  //=======================================

  private InputHandler inputHandler;

  //=======================================

  public static LevelManager Instance
  {
    get
    {
      if (_instance == null) { _instance = FindObjectOfType<LevelManager>(); }
      return _instance;
    }
  }

  //=======================================

  /// <summary>
  /// Количество врагов на уровне
  /// </summary>
  public int NumberEnemiesLevel { get; set; }

  //=======================================

  private void Awake()
  {
    if (_instance != null && _instance != this)
    {
      Destroy(this);
      return;
    }
    _instance = this;

    inputHandler = InputHandler.Instance;
  }

  private void OnEnable()
  {
    inputHandler.AI_Player.Player.Reload.performed += Reload_performed;
  }

  private void OnDisable()
  {
    inputHandler.AI_Player.Player.Reload.performed -= Reload_performed;
  }

  //=======================================

  /// <summary>
  /// Уровень завершен
  /// </summary>
  public void LevelCompleted()
  {
    NumberEnemiesLevel--;

    if (NumberEnemiesLevel > 0)
      return;

    Debug.Log("Уровень пройден");
    CameraShake.Shake(0, 0);
    Time.timeScale = 0;
  }

  //=======================================

  private void Reload_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
  {
    if (Time.timeScale == 0)
      Time.timeScale = 1;

    int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    SceneManager.LoadScene(currentSceneIndex);

    Debug.Log("Уровень перезагружен");
  }

  //=======================================

  /// <summary>
  /// Уровень проигран
  /// </summary>
  public void Defeat()
  {
    Debug.Log("Уровень проигран");
    StartCoroutine(RestartScene());
  }

  /// <summary>
  /// Перезупустить сцену
  /// </summary>
  private IEnumerator RestartScene()
  {
    yield return new WaitForSeconds(2);

    int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    SceneManager.LoadScene(currentSceneIndex);
  }

  //=======================================
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
  /*public static ChangeScene instance;

  private void Awake()
  {
    DontDestroyOnLoad(gameObject);
    if(instance == null)
    {
      instance = this;
    }
    else if(instance != this)
    {
      Destroy(gameObject);
    }
  }*/

  public void LoadScene(string sceneName) {

    SceneManager.LoadScene(sceneName);
  }

}

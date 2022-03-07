using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Manager/GameSettings")]
public class GameSettings : ScriptableObject
{
  [SerializeField]
  private string _gameVersion = "0.0.0";
  public string GameVersion {get {return _gameVersion;}}

[SerializeField]
private string _screenName = "Player";
public string ScreenName{ get {
    int rndm = Random.Range(0, 99);
    return _screenName + rndm.ToString();
     }}

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreElement : MonoBehaviour
{
    public TMP_Text usernameText;
    public TMP_Text winsText;
    public TMP_Text lossesText;
    public TMP_Text tiesText;

    public void NewScoreElement (string _username, int _wins, int _losses, int _ties)
    {
        usernameText.text = _username;
        winsText.text = _wins.ToString();
        lossesText.text = _losses.ToString();
        tiesText.text = _ties.ToString();
    }
}

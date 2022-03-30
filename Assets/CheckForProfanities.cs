using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class CheckForProfanities : MonoBehaviour
{
    public Text create;
    public GameObject Popuperror;
    public Text popuperrortext;

      public bool CheckforProfanities(){
     
        if (create.text.ToLower().Contains("fuck") ||
            create.text.ToLower().Contains("anal") ||
            create.text.ToLower().Contains("anus") ||
            create.text.ToLower().Contains("arse") ||
            create.text.ToLower().Contains("ass") ||
            create.text.ToLower().Contains("b1tch") ||
            create.text.ToLower().Contains("ballsack") ||
            create.text.ToLower().Contains("bastard") ||
            create.text.ToLower().Contains("bitch") ||
            create.text.ToLower().Contains("biatch") ||
            create.text.ToLower().Contains("blowjob") ||
            create.text.ToLower().Contains("bollock") ||
            create.text.ToLower().Contains("bollok") ||
            create.text.ToLower().Contains("boner") ||
            create.text.ToLower().Contains("boob") ||
            create.text.ToLower().Contains("buttplug") ||
            create.text.ToLower().Contains("clit") ||
            create.text.ToLower().Contains("cock") ||
            create.text.ToLower().Contains("cum") ||
            create.text.ToLower().Contains("cunt") ||
            create.text.ToLower().Contains("dick") ||
            create.text.ToLower().Contains("dildo") ||
            create.text.ToLower().Contains("dyke") ||
            create.text.ToLower().Contains("erection") ||
            create.text.ToLower().Contains("fag") ||
            create.text.ToLower().Contains("feck") ||
            create.text.ToLower().Contains("fellate") ||
            create.text.ToLower().Contains("fellatio") ||
            create.text.ToLower().Contains("felching") ||
            create.text.ToLower().Contains("fudgepacker") ||
            create.text.ToLower().Contains("genital") ||
            create.text.ToLower().Contains("jizz") ||
            create.text.ToLower().Contains("knobend") ||
            create.text.ToLower().Contains("labia") ||
            create.text.ToLower().Contains("masturbate") ||
            create.text.ToLower().Contains("masturbation") ||
            create.text.ToLower().Contains("nigger") ||
            create.text.ToLower().Contains("nigga") ||
            create.text.ToLower().Contains("penis") ||
            create.text.ToLower().Contains("piss") ||
            create.text.ToLower() == "pussy" ||
            create.text.ToLower().Contains("scrotum") ||
            create.text.ToLower().Contains("sex") ||
            create.text.ToLower().Contains("shit") ||
            create.text.ToLower().Contains("sh1t") ||
            create.text.ToLower().Contains("slut") ||
            create.text.ToLower().Contains("smegma") ||
            create.text.ToLower().Contains("tit") ||
            create.text.ToLower().Contains("tranny") ||
            create.text.ToLower().Contains("trannies") ||
            create.text.ToLower().Contains("twat") ||
            create.text.ToLower().Contains("vagina") ||
            create.text.ToLower().Contains("wank") ||
            create.text.ToLower().Contains("whore") ||
            create.text.ToLower().Contains("asshole") ||
            create.text.ToLower().Contains("fvck") ||
            create.text.ToLower().Contains("asshat") ||
            create.text.ToLower().Contains("pu55y") ||
            create.text.ToLower().Contains("pen1s")) {
            return true;
        }
     
     return false;
    
    }

   public IEnumerator Popup(){
       
       Popuperror.SetActive(true);
       popuperrortext.GetComponent<Text>().text = "You cannot use profanities in your room name!";
       yield return new WaitForSeconds(3);
       Popuperror.SetActive(false);

   }
}

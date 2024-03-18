using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //UI »ç¿ëÇÏ±â À§ÇÔ
using UnityEngine.SceneManagement; // SceneManager »ç¿ëÇÏ±â À§ÇÔ
//Æ¯¼ö¹®ÀÚ Á¦ÇÑÀ» µÎ±âÀ§ÇÔ
using System.IO;
using System.Text.RegularExpressions;


public class BtnEvent : MonoBehaviour
{
    [SerializeField] private InputField nickNameField;
    [SerializeField] private Text canIdTxt;
    //[serializeField] private Panel SettingPanel;

    private string nickName;
    private bool isSetting;

    void Awake()
    {
        nickName = "";
        isSetting = false; 
    }

    void Start()
    {
        nickNameField.characterLimit = 10;
    }

    bool CheckNickName()
    {
        nickName = nickNameField.text;
        string Check = Regex.Replace(nickName, @"[^a-zA-Z0-9°¡-ÆR]", "", RegexOptions.Singleline);
        Check = Regex.Replace(nickName, @"[^\w\.@-]", "", RegexOptions.Singleline);

        //´Ð³×ÀÓ¿¡ Æ¯¼ö ¹®ÀÚ°¡ Æ÷ÇÔµÇ¸é
        if (!nickName.Equals(Check))
        {
            canIdTxt.text = "Nickname cannot contain special characters";
            return false;
        }

        //´Ð³×ÀÓÀÇ ±æÀÌ°¡ 16±ÛÀÚ ÃÊ°ú½Ã
        if (nickName.Length > 8)
        {
            canIdTxt.text = "Nickname must be less than 8 characters";
            return false;
        }

        // ´Ð³×ÀÓÀ» ÀÔ·Â ¾ÈÇßÀ» ¶§
        if (nickName == "")
        {
            canIdTxt.text = "Please enter your nickname..";
            return false;
        }

        // ´Ð³×ÀÓÀ» ¹Ù¸£°Ô ÀÔ·ÂÇßÀ» ¶§
        if (nickName != null)
        {
            canIdTxt.text = "Welcome " + nickName + "!";
            return true;
        }

        return false;
    }


    // ¹öÆ°ÀÌ ´­¸®¸é ¹ß»ýÇÒ ¸Þ¼Òµå
    public void SendBtn()
    {
        if (CheckNickName())
        {
            PlayerPrefs.SetString("NickName", nickName);
            SceneManager.LoadScene("Main");
        }
    }
    
    // ¼³Á¤ ¹öÆ° ´©¸£¸é ¹ß»ýÇÒ ¸Þ¼Òµå
    public void SettingBtn()
    {
        //isSetting != isSetting;
        if (isSetting)
        {
          //  SettingPanel.SetVisble(true);
        }
        else
        {
           // SettingPanel.SetVisible(false);
        }
    }
}

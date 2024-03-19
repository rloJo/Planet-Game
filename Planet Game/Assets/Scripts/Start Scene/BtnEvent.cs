using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //UI 사용하기 위함
using UnityEngine.SceneManagement; // SceneManager 사용하기 위함
//특수문자 제한을 두기위함
using System.IO;
using System.Text.RegularExpressions;


public class BtnEvent : MonoBehaviour
{
    [SerializeField] private InputField nickNameField;
    [SerializeField] private Button JoinBtn;
    [SerializeField] private Text canIdTxt;

    private string nickName;

    void Awake()
    {
        //사용자 로컬에 저장된 nickName 불러오기
        nickName = PlayerPrefs.GetString("NickName", nickName);
    }

    void Start()
    {
        //로컬에 저장된 닉네임이 없으면
        if(nickName != null)
        {
            PlayerPrefs.SetFloat("BGMVolume", 1.0f);
            PlayerPrefs.SetFloat("EffectsVolume", 1.0f);
            nickName = "";
            JoinBtn.gameObject.SetActive(true);
            nickNameField.gameObject.SetActive(true);  
            nickNameField.characterLimit = 10;
        }
        //있을 경우
        else
        {
            StartCoroutine(WaitForSec());
            LoadingSceneController.LoadScene("Main");
        }
    }

    void Update()
    {
     
    }

    bool CheckNickName()
    {
        nickName = nickNameField.text;
        string Check = Regex.Replace(nickName, @"[^a-zA-Z0-9가-힣]", "", RegexOptions.Singleline);
        Check = Regex.Replace(nickName, @"[^\w\.@-]", "", RegexOptions.Singleline);

        //닉네임에 특수 문자가 포함되면
        if (!nickName.Equals(Check))
        {
            canIdTxt.text = "Nickname cannot contain special characters";
            return false;
        }

        //닉네임의 길이가 16글자 초과시
        if (nickName.Length > 8)
        {
            canIdTxt.text = "Nickname must be less than 8 characters";
            return false;
        }

        // 닉네임을 입력 안했을 때
        if (nickName == "")
        {
            canIdTxt.text = "Please enter your nickname..";
            return false;
        }

        // 닉네임을 바르게 입력했을 때
        if (nickName != null)
        {
            canIdTxt.text = "Welcome " + nickName + "!";
            return true;
        }

        return false;
    }


    // 버튼이 눌리면 발생할 메소드
    public void SendBtn()
    {
        if (CheckNickName())
        {
            PlayerPrefs.SetString("NickName", nickName);
            SceneManager.LoadScene("Main");
        }
    }
    
    // 설정 버튼 누르면 발생할 메소드
    public void SettingBtn()
    {
        
    }

    IEnumerator WaitForSec()
    {
        yield return new WaitForSeconds(2.0f);
    }

}

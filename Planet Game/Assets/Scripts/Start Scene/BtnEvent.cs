using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //UI ����ϱ� ����
using UnityEngine.SceneManagement; // SceneManager ����ϱ� ����
//Ư������ ������ �α�����
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
        //����� ���ÿ� ����� nickName �ҷ�����
        nickName = PlayerPrefs.GetString("NickName", nickName);
    }

    void Start()
    {
        //���ÿ� ����� �г����� ������
        if(nickName != null)
        {
            PlayerPrefs.SetFloat("BGMVolume", 1.0f);
            PlayerPrefs.SetFloat("EffectsVolume", 1.0f);
            nickName = "";
            JoinBtn.gameObject.SetActive(true);
            nickNameField.gameObject.SetActive(true);  
            nickNameField.characterLimit = 10;
        }
        //���� ���
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
        string Check = Regex.Replace(nickName, @"[^a-zA-Z0-9��-�R]", "", RegexOptions.Singleline);
        Check = Regex.Replace(nickName, @"[^\w\.@-]", "", RegexOptions.Singleline);

        //�г��ӿ� Ư�� ���ڰ� ���ԵǸ�
        if (!nickName.Equals(Check))
        {
            canIdTxt.text = "Nickname cannot contain special characters";
            return false;
        }

        //�г����� ���̰� 16���� �ʰ���
        if (nickName.Length > 8)
        {
            canIdTxt.text = "Nickname must be less than 8 characters";
            return false;
        }

        // �г����� �Է� ������ ��
        if (nickName == "")
        {
            canIdTxt.text = "Please enter your nickname..";
            return false;
        }

        // �г����� �ٸ��� �Է����� ��
        if (nickName != null)
        {
            canIdTxt.text = "Welcome " + nickName + "!";
            return true;
        }

        return false;
    }


    // ��ư�� ������ �߻��� �޼ҵ�
    public void SendBtn()
    {
        if (CheckNickName())
        {
            PlayerPrefs.SetString("NickName", nickName);
            SceneManager.LoadScene("Main");
        }
    }
    
    // ���� ��ư ������ �߻��� �޼ҵ�
    public void SettingBtn()
    {
        
    }

    IEnumerator WaitForSec()
    {
        yield return new WaitForSeconds(2.0f);
    }

}

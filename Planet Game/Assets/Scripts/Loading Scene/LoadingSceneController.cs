using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingSceneController : MonoBehaviour
{
    [SerializeField] private Image progressBar;
    [SerializeField] private Text gameTipText;
    
    //���� �� �ۼ�
    private string[] gameTips =
    {
        "Pluto was removed from the solar system in 2006",
        "Saturn has a ring of hydrogen gas",
        "The moon is a satellite of the earth"
    };
    
    static string nextScene;

    public static void LoadScene(string sceneName)
    {
        nextScene = sceneName;
        SceneManager.LoadScene("Loading");
    }

    //������ ���� ����
    void Awake()
    {
        int Tip = Random.Range(0, 3);
        gameTipText.text = gameTips[Tip];
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadSceneProcess()); //�ڷ�ƾ ���
    }

    IEnumerator LoadSceneProcess()
    {
        //LoadScene�� ���������� ���ҷ����� ���� �ٸ��� �� �� ���� 
        //�񵿱� ������� ���� �ҷ��� -> ���߿� �ٸ� �۾� ����
        //�� �ҷ����� �����Ȳ�� AsyncOperation ���·� ��ȯ ����
        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
        //���� �ٺҷ����� �ڵ����� ���� ������ �Ѿ ������ ����
        //false ������ ���� 90������ load�ѵ� true�� �ٲ� �� ���� ��ٸ�
        op.allowSceneActivation = false;

        float timer = 0f;
        while(!op.isDone)
        {
            //�Ʒ� ������ ���� ������ ����Ƽ ������ ������� �ѱ��� �ʾ� �ٸ� �۾� �Ұ���
            yield return null;

            if(op.progress<0.9f) // ���൵�� 90%�� �� ������ ���α׷����ٸ� ���� �ӵ���� ä���
            {
                progressBar.fillAmount = op.progress;
            }
            else //90% �̻��� �Ǹ� fake�� ä�� (fake�ε� game�� ���� �ְų� ���丮 �˷��ٶ� ���)
            {
                timer += Time.unscaledDeltaTime;
                progressBar.fillAmount = Mathf.Lerp(0.9f,1f,timer/2); //2�ʿ� ���ļ� ä��
                if(progressBar.fillAmount >= 1f) // ���α׷����ٸ� ��ä���
                {
                    op.allowSceneActivation = true; //���� �� �ε�
                    yield break;
                }
            }
        }
    }
}

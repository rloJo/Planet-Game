using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingSceneController : MonoBehaviour
{
    [SerializeField] private Image progressBar;
    [SerializeField] private Text gameTipText;
    
    //게임 팁 작성
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

    //게임팁 랜덤 설정
    void Awake()
    {
        int Tip = Random.Range(0, 3);
        gameTipText.text = gameTips[Tip];
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadSceneProcess()); //코루틴 등록
    }

    IEnumerator LoadSceneProcess()
    {
        //LoadScene는 동기방식으로 씬불러오는 도중 다른거 할 수 없음 
        //비동기 방식으로 씬을 불러옴 -> 도중에 다른 작업 가능
        //씬 불러오는 진행상황은 AsyncOperation 형태로 반환 해줌
        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
        //씬을 다불러오면 자동으로 다음 씬으로 넘어갈 것인지 설정
        //false 설정시 씬을 90까지만 load한뒤 true로 바뀔 때 까지 기다림
        op.allowSceneActivation = false;

        float timer = 0f;
        while(!op.isDone)
        {
            //아래 선언을 하지 않으면 유니티 엔진에 제어권을 넘기지 않아 다른 작업 불가능
            yield return null;

            if(op.progress<0.9f) // 진행도가 90%일 때 까지는 프로그래스바를 원래 속도대로 채운다
            {
                progressBar.fillAmount = op.progress;
            }
            else //90% 이상이 되면 fake로 채움 (fake로딩 game의 팁을 주거나 스토리 알려줄때 사용)
            {
                timer += Time.unscaledDeltaTime;
                progressBar.fillAmount = Mathf.Lerp(0.9f,1f,timer/2); //2초에 걸쳐서 채움
                if(progressBar.fillAmount >= 1f) // 프로그래스바를 다채우면
                {
                    op.allowSceneActivation = true; //다음 씬 로딩
                    yield break;
                }
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMPlayer : MonoBehaviour
{
    private AudioSource audioSource;
    private GameObject[] musics;

    private void Awake()
    {

        musics = GameObject.FindGameObjectsWithTag("Music");
        if(musics.Length >= 2) //BGM�� ���������� ���ϼ� ����
        {
            Destroy(this.gameObject);
        }

        //���̵��� �ı����� ����
        DontDestroyOnLoad(transform.gameObject);
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayMusic()
    {
        if(audioSource.isPlaying) { return; }
        audioSource.Play();
    }

    public void StopMusic() 
    { 
        audioSource.Stop();
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

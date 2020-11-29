using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioSerial : MonoBehaviour
{

    /*
     * 소리를 구현하게 되는 소스 resource에있는 음원파일들을 가져와 하나씩 출력이 가능해졌음. 
     */


    AudioSource audioSource;
    int i;
    float time;
    string wavs;

    string[] music;

    // Start is called before the first frame update
    void Start()
    {
        i = 0;
        audioSource = GameObject.Find("Main Camera").GetComponent<AudioSource>();
        music = GameObject.Find("Main Camera").GetComponent<SongInfo>().wavs;

    }

    public void getAudio(int n)
    {
        string[] st = music[n].Split('.');
        audioSource.clip = Resources.Load<AudioClip>("sound/" + st[0]);
        audioSource.Play();
    }


    // Update is called once per frame
    void Update()
    {
        /*
        time += Time.deltaTime;

        if(time > 2)
        {
            time = 0;
           
            this.wavs = GameObject.Find("Main Camera").GetComponent<SongInfo>().wavs[i++];
            //Debug.Log(wavs);
            string[] st = wavs.Split('.');
            audioSource.clip = Resources.Load<AudioClip>("sound/" + st[0]);
            //audioSource.Play();

        }*/
    }
}

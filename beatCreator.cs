using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;



//특정 비트에 대해서 라인과 음악을 정해서 beat(num)으로 보내주는 코드줄.
public class beatCreator : MonoBehaviour
{
    int currentMadi;
    float madiPersec;
    string[] k1, k2, k3, k4, k5, k6, k7;
    int totalMadi;
    float MadiTimer;
    float beatTimer;
    float TPN_k1; // 노트별 타이밍을 재주는 변수
    int[] beatsk1;
    bool startSong;
    int k1_i;


    public GameObject beat;
    AudioClip audioClip;

    void getKeyScript()
    {
        k1 = GameObject.Find("Main Camera").GetComponent<SongInfo>().key1;
        k2 = GameObject.Find("Main Camera").GetComponent<SongInfo>().key2;
        k3 = GameObject.Find("Main Camera").GetComponent<SongInfo>().key3;
        k4 = GameObject.Find("Main Camera").GetComponent<SongInfo>().key4;
        k5 = GameObject.Find("Main Camera").GetComponent<SongInfo>().key5;
        k6 = GameObject.Find("Main Camera").GetComponent<SongInfo>().key6;
        k7 = GameObject.Find("Main Camera").GetComponent<SongInfo>().key7;
        totalMadi = GameObject.Find("Main Camera").GetComponent<SongInfo>().currentMadi;
        //Debug.Log(k1[1]);
        //getBeat(k1[1]);
        //확인용
        //Debug.Log(madiPersec);
        //Debug.Log(totalMadi);
    }

    int[] getBeat(string note)
    {

        int j = 0;
        string []s = new string [note.Length/2];
        int[] beats = new int[note.Length/2];
        for(int i = 0; i < note.Length; i= i+2)
        {
            s[j] += note[i];
            s[j] += note[i+1];
            j++;
        }
        for(int i = 0; i < s.Length; i++)
        {
            beats[i] = Convert.ToInt32(s[i] , 16);
            Debug.Log(beats[i]);
        }
        TPN_k1 = madiPersec / (note.Length / 2);
        Debug.Log(TPN_k1);

        return beats;
    }



    // Start is called before the first frame update
    void Start()
    {
        startSong = false;
        currentMadi = 0;
        madiPersec = GameObject.Find("Main Camera").GetComponent<SongInfo>().bpm;
        madiPersec = 60 / (madiPersec / 4);
        k1_i = 0;
        

    }

    // Update is called once per frame
    void Update()
    {
        MadiTimer += Time.deltaTime;
        beatTimer += Time.deltaTime;


        if (Input.GetKeyDown(KeyCode.Space))
        {

            currentMadi = 1;
            MadiTimer = 0;
            beatTimer = 0;
            getKeyScript();
            beatsk1 = getBeat(k1[currentMadi]);
            startSong = true;
            
        }



        if (MadiTimer > madiPersec)
        { 
            MadiTimer = 0;
        }
        else // timer < madiPersec
        {
            if (startSong)
            {
                if (beatTimer >= TPN_k1)
                {
                    if(k1_i < beatsk1.Length)
                    {
                        Debug.Log(beatsk1[k1_i]);
                        GameObject.Find("Main Camera").GetComponent<audioSerial>().getAudio(beatsk1[k1_i]);
                        //k1.play
                        beatTimer = 0;
                        k1_i++;
                    }
                    
                }
            }
            



        }


    }

}

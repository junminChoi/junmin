using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;


/*
 * bpm관련. 
 * 
 * beat per minute = 128;
 * 1분당 4분음표를 128번
 * 즉 1분이면 32마디를 친다.
 * 즉 1마디는 60/32초
 * bpm의 마디당 초는 60 / (bpm/4) 
 * 
 */


/// <summary>
/// 곡을 받아오고, 전체적인 가공을 시켜줄 SongInfo class입니다.
/// </summary>
public class SongInfo : MonoBehaviour
{
    public AudioSource audioSource;
    public string[] key1, key2, key3, key4, key5, key6, key7;
    public int bpm;
    public string song;
    public int currentMadi;

    private string songname;
    private FileInfo fileName;//BMS파일 열기 위한 변수
    protected StreamReader reader;//파일 스트림을 일기 위한 streamreader
    //private string []tempSplit;

    //private AudioSource[] audio;
    private bool isWave;
    private bool isData;


    public string[] wavs;      //wav들을 저장하는 배열 wavs;
    private int wavsIndex;      //wav에 접근하기 위한 wavsIndex
    private string[,] madiscript;


    private string player;
    private int madi;
    private int inputKeyType;

    

    string StrText;


    private string[] madiscript01;

    

    // Start is called before the first frame update
    void Start()
    {

        //초기화
        //tempSplit = null;
        wavs = null;
        wavsIndex = 1;
        StrText = "";
        isWave = false;
        player = "";
        isData = false;
        wavsIndex = 0;
        wavs = new string[300];
        bpm = 0;
        currentMadi = 0;
        madiscript = new string[200, 20];
        madiscript01 = new string[20];
        key1 = new string[200];
        key2 = new string[200];
        key3 = new string[200];
        key4 = new string[200];
        key5 = new string[200];
        key6 = new string[200];
        key7 = new string[200];
        audioSource = this.gameObject.GetComponent<AudioSource>();

        songname = "3amba_7LB";
        fileName = new FileInfo("Assets/songs/" + songname + ".bme");





        if (fileName != null)
        {
            reader = fileName.OpenText();
        }
        else
        {
            Debug.Log("parse Error In Cs_SongInfo");
        }


        readFile();
    } 

   void readWaves(string title)
    {
        wavs[wavsIndex] = title;
        wavsIndex++;
    }

    void readFile()
    {
        StrText = reader.ReadLine();
        Debug.Log(StrText);
        while (true)
        {

            StrText = reader.ReadLine();
            Debug.Log(StrText);
            string[] st = StrText.Split(' ');

            if (st[0] == "#PLAYER")
            {
                player = st[1];
                Debug.Log("player : " + player);
            }
            if (st[0] == "#BPM")
            {
                bpm = int.Parse(st[1]);
                Debug.Log("bpm : " + st[1]);
            }
            if (st[0] == "#WAV01")
            {
                isWave = true;
            }

            if (isWave)
            {
                if(st[1] != null)
                {
                    readWaves(st[1]);
                }
                //Convert.ToInt32(st[0].Substring(4, 2), 16)); // 16진수인 wav넘버를 정수화시키는코드
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        
        if(StrText != null)
        {


            StrText = reader.ReadLine();
            
            
            if(StrText == "*---------------------- MAIN DATA FIELD")
            {
                isData = true;
            }

            if (isData)
            {
                string[] st = StrText.Split(':');
                if(st.Length > 1)
                {
                    //마디를 계산하는부분
                    madi = int.Parse(st[0].Substring(1, 3));
                    if (madi != currentMadi)
                    {
                        currentMadi++;
                        Debug.Log("현재 마디 : " + currentMadi);
                    }
                    Debug.Log(st[0]);

                    //입력 키를 분류하는 부분
                    inputKeyType = int.Parse(st[0].Substring(4, 2));
                    if (inputKeyType == 11)
                    {
                        key1[currentMadi] = st[1];
                    }
                    else if (inputKeyType == 12)
                    {
                        key2[currentMadi] = st[1];
                    }
                    else if (inputKeyType == 13)
                    {
                        key3[currentMadi] = st[1];
                    }
                    else if (inputKeyType == 14)
                    {
                        key4[currentMadi] = st[1];
                    }
                    else if (inputKeyType == 15)
                    {
                        key5[currentMadi] = st[1];

                    }
                    else if (inputKeyType == 16)
                    {
                        key6[currentMadi] = st[1];

                    }
                    else if (inputKeyType == 17)
                    {
                        key7[currentMadi] = st[1];
                    }


                }
                
            }
            



            
        }


            /*
    

            //data파일 >> 비트의 위치, 갯수 등을 가공하는 if문들

            if(StrText == "*---------------------- MAIN DATA FIELD")
            {
                isWave = false;
                isData = true;
            }
            if (isData)
            {
                if(st.Length > 1)
                {
                    
                    madi = int.Parse(st[0].Substring(1,3));
                    //마디를 알아내는 부분.
                    if(madi != currentMadi) // 다음 번째 마디로 넘어가면
                    {
                        currentMadi++;
                    }

                    //current마디의 inputkey 의 소스
                    inputKeyType = int.Parse(st[0].Substring(4, 2));
                    if(inputKeyType != 1)
                    {
                        if(inputKeyType == 11)
                        {
                            key1[currentMadi] = st[1];
                            //Debug.Log(key1[currentMadi]);
                        }
                        else if(inputKeyType == 12)
                        {
                            key2[currentMadi] = st[1];


                        }
                        else if(inputKeyType == 13)
                        {
                            key3[currentMadi] = st[1];
                        }
                        else if(inputKeyType == 14)
                        {
                            key4[currentMadi] = st[1];
                        }
                        else if (inputKeyType == 15)
                        {
                            key5[currentMadi] = st[1];

                        }
                        else if (inputKeyType == 16)
                        {
                            key6[currentMadi] = st[1];

                        }
                        else if (inputKeyType == 17)
                        {
                            key7[currentMadi] = st[1];
                        }


                        madiscript[currentMadi,inputKeyType] = st[1]; // 노트의 위치 소스를 가지는 madiscript
                        //Debug.Log("1번키 아닐때, "+currentMadi+"번마디의 " +inputKeyType + "번키의 데이터값은" +  madiscript[currentMadi,inputKeyType]);
                    }else if(inputKeyType == 1){
                        //01키 (오토입력)에 대해 따로 만들어둘 필요성이 있을지도.
                       
                        madiscript01[0] = st[1];
                    }


                    
                }
            }

            */
            


    }






}

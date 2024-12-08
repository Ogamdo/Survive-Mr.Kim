using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    [Header("빌런세팅")]
    private GameTimer gameTimer;
    [SerializeField]public float startT=15;
    [SerializeField]public float repeatT=15;
    [SerializeField]private List<GameObject> villains= new List<GameObject>();

    public List<GameObject> villains = new List<GameObject>();
    
    [Header("클리어 문구와 사망 문구 세팅")]
    public GameObject youSurvivedText; // "You Survived" 텍스트 오브젝트
    public GameObject youDiedText; // "You Died" 텍스트 오브젝트
    public GameObject ClearImage;
    
        

    public static GameManager Instance { get; private set; } // GameManager의 싱글톤 인스턴스
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this; // 싱글톤 인스턴스 설정
            DontDestroyOnLoad(gameObject); // 씬 전환 시에도 파괴되지 않도록 설정
        }
        else
        {
            Destroy(gameObject); // 중복된 인스턴스 파괴
        }
    }

    void Start()
    {
        youDiedText.SetActive(false); // "You Died" 텍스트 비활성화
        youSurvivedText.SetActive(false); // "You Survived" 텍스트 비활성화
        ClearImage.SetActive(false); //Clear 이미지 비활성화

        villains[0].SetActive(false);
        villains[1].SetActive(false);
        villains[2].SetActive(false);
        villains[3].SetActive(false);
        for (int i = 0;i<`)


        InvokeRepeating(nameof(VillainsSpwan),startT,repeatT);
    }

    public void EndGame(bool clear)
    {  
       if(clear) 
       {
            youSurvivedText.SetActive(true);
            ClearImage.SetActive(true);
       }
       else
       {
            youDiedText.SetActive(true) ;
       }
      ; // "You Died" 텍스트 활성화
    }

    public void ClearGame()
    {
        youSurvivedText.SetActive(true); // "You Survived" 텍스트 활성화
    }

    public void VillainsSpwan()
    {
          int count =0;
        if(villains[count] !=null)
        {
         villains[count].SetActive(true);   

         Debug.Log(count+"번 빌런 등장!");
        }
          count++;
           
    }
}

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleUIManager : MonoBehaviour
{
     //게임 메뉴얼, 세 개의 버튼과 불러올 씬을 선언
   public GameObject gameManual;
   public GameObject credit;
   public Button Btn_play;
   public Button Btn_manual;
   public Button Btn_credit;
   public GameObject Btn_back;
   public string play1; // 전환할 씬 이름

   public void Start(){
        gameManual.SetActive(false);
        credit.SetActive(false);
        Btn_back.SetActive(false);
      
   }
   public void OnClickBtn_play(){
     SceneManager.LoadScene(play1);
   }
   public void OnClickBtn_manual(){
     gameManual.SetActive(true);
     Btn_back.SetActive(true);

   }
   public void OnClickBtn_Credit(){
     credit.SetActive(true);
     Btn_back.SetActive(true);
   }

   public void OnClickBtn_back(){
   
     Btn_back.SetActive(false);
      gameManual.SetActive(false);
      credit.SetActive(false);
   }
}

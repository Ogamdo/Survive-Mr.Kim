using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleUIManager : MonoBehaviour
{
     //���� �޴���, �� ���� ��ư�� �ҷ��� ���� ����
   public GameObject gameManual;
   public GameObject credit;
   public Button Btn_play;
   public Button Btn_manual;
   public Button Btn_credit;
   public GameObject Btn_back;
   public string play1; // ��ȯ�� �� �̸�

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

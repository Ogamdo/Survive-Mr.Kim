using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class TitleUIManager : MonoBehaviour
{
    public GameObject gameManual;
    public GameObject credits;
    public Button btnPlay;
    public Button btnManual;
    //public Button btnCredit;
    public Button btnBack;
    public string playScene;

    public float subSp = 2f; // 이동 속도
    public float subDis = 500f; // 이동 거리
    public Transform TitleSubway;
    private Vector3 posIni; // 시작 위치
    private Vector3 targetPosition; // 목표 위치
    private bool isSubwayMoving = false; // 지하철 이동 상태 확인

    private void Start()
    {
        // TitleSubway의 시작 위치 저장 및 목표 위치 계산
        if (TitleSubway != null)
        {
            posIni = TitleSubway.position;
            targetPosition = posIni + new Vector3(subDis, 0, 0); // 오른쪽으로 이동
        }

        // 초기 UI 설정
        SetUIActive(gameManual, false);
        //SetUIActive(credits, false);
        SetUIActive(btnBack.gameObject, false);

        // 버튼 이벤트 리스너 설정
        btnPlay.onClick.AddListener(() =>
        {
            StartCoroutine(ExecuteAfterGameStart());
        });

        btnManual.onClick.AddListener(() =>
        {
            ShowPanel(gameManual);
        });

     /*   btnCredit.onClick.AddListener(() =>
        {
            ShowPanel(credits);
        }); */

        btnBack.onClick.AddListener(() => HideAllPanels());
    }

    private void SetUIActive(GameObject uiElement, bool isActive)
    {
        if (uiElement != null)
        {
            uiElement.SetActive(isActive);
        }
    }

    private void ShowPanel(GameObject panel)
    {
        // 모든 패널 숨기고 특정 패널만 활성화
        HideAllPanels();
        SetUIActive(panel, true);
        SetUIActive(btnBack.gameObject, true);
    }

    public void HideAllPanels()
    {
        SetUIActive(gameManual, false);
        SetUIActive(credits, false);
        SetUIActive(btnBack.gameObject, false);
    }

    public IEnumerator GameStart()
    {
        // GameStart 시 지하철 이동 시작
        isSubwayMoving = true;
        yield return StartCoroutine(MoveSubway());
        Debug.Log("GameStart 실행 완료");
    }

    private IEnumerator MoveSubway()
    {
        // 지하철이 목표 위치로 이동
        while (isSubwayMoving && TitleSubway != null)
        {
            // 현재 위치에서 목표 위치로 이동
            TitleSubway.position = Vector3.MoveTowards(TitleSubway.position, targetPosition, subSp * Time.deltaTime);

            // 목표 위치에 도달하면 이동 중지
            if (Vector3.Distance(TitleSubway.position, targetPosition) < 0.1f)
            {
                isSubwayMoving = false;
                Debug.Log("목표 위치 도달");
            }

            yield return null; // 다음 프레임까지 대기
        }
    }

    private IEnumerator ExecuteAfterGameStart()
    {
        // GameStart 실행
        yield return StartCoroutine(GameStart());

        // 1초 대기 후 씬 전환
        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(playScene);
        Debug.Log("씬 로드 완료");
    }
}

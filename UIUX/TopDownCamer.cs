using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class TopDownCamera : MonoBehaviour
{
    public Transform target; // 카메라가 따라갈 캐릭터
    public float height = 10f; // 카메라의 높이
    public float distance = 5f; // 캐릭터와의 거리
    public Vector3 rotationOffset = new Vector3(90f, 0f, 0f); // 카메라의 회전 오프셋

    private AudioSource audioSource; // 오디오 소스 컴포넌트

    void Start()
    {
        // 오디오 소스 컴포넌트 가져오기
        audioSource = GetComponent<AudioSource>();
    }

    void LateUpdate()
    {
        if (target != null)
        {
            // 카메라의 Z값을 캐릭터의 Z값에 따라 조정
            Vector3 newPosition = new Vector3(target.position.x, height, target.position.z - distance);
            transform.position = newPosition;

            // 인스펙터에서 설정한 회전값으로 카메라 회전
            transform.rotation = Quaternion.Euler(rotationOffset);
        }
    }

    // 오디오 클립을 설정하는 메서드
    public void SetAudioClip(AudioClip clip)
    {
        audioSource.clip = clip;
    }

    // 오디오 클립 재생 메서드
    public void PlayAudio()
    {
        if (audioSource.clip != null)
        {
            audioSource.Play();
        }
    }
}
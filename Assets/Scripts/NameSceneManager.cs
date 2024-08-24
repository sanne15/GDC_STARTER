using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Video;
using System.Text.RegularExpressions;

public class NameSceneManager : MonoBehaviour
{
    public TMP_InputField surnameInputField; // 성 입력 필드
    public TMP_InputField nameInputField;    // 이름 입력 필드
    public TMP_InputField nicknameInputField; // 별명 입력 필드
    public Button submitButton; // 제출 버튼
    public CanvasGroup panelCanvasGroup; // 패널의 CanvasGroup 참조
    public VideoPlayer videoPlayer; // 비디오 플레이어 참조
    public Material blurMaterial; // 블러 효과를 적용할 Material
    public TextMeshProUGUI warningText;       // 경고 문구 TextMeshProUGUI
    public CanvasGroup warningCanvasGroup;    // 경고 텍스트의 CanvasGroup
    public TextMeshProUGUI loadingText; // 로딩텍스트

    private Coroutine warningCoroutine;

    void Start()
    {
        // 버튼 클릭 시 이름과 별명을 처리하는 메서드를 호출
        submitButton.onClick.AddListener(OnSubmit);

        // 비디오 플레이어 설정
        videoPlayer.targetMaterialRenderer = GetComponent<Renderer>(); // 비디오를 재생할 타겟을 설정
        videoPlayer.targetMaterialProperty = "_MainTex"; // 사용할 Material의 텍스처 프로퍼티를 설정

        videoPlayer.loopPointReached += OnVideoEnd;

        // 블러 효과를 적용
        // videoPlayer.targetMaterialRenderer.material = blurMaterial;

        warningCanvasGroup.alpha = 0f;
    }

    void OnSubmit()
    {
        string surname = surnameInputField.text;
        string name = nameInputField.text;
        string nickname = nicknameInputField.text;

        // 유효성 검사
        if (!IsValidInput(surname) || !IsValidInput(name) || !IsValidInput(nickname))
        {
            if (warningCoroutine != null)
            {
                StopCoroutine(warningCoroutine);
            }

            // 새 경고 코루틴 실행
            warningCoroutine = StartCoroutine(ShowWarning("이름, 성 또는 별명은 10글자 이내여야 하며 특수문자를 포함할 수 없습니다."));
            return;
        }

        // NamePasser 인스턴스에 변수 전달
        NamePasser.Instance.SetPlayerInfo(name, surname, nickname);

        // 페이드 아웃과 다음 씬으로의 전환
        StartCoroutine(FadeOutAndLoadNextScene());
    }

    IEnumerator ShowWarning(string message)
    {
        // 경고 문구 설정 및 알파값 1로 변경
        warningText.text = message;
        warningCanvasGroup.alpha = 1f;

        // 1초간 경고 문구 표시
        yield return new WaitForSeconds(1f);

        // 자연스러운 페이드 아웃
        float fadeDuration = 1f; // 페이드 아웃 지속 시간
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            warningCanvasGroup.alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            yield return null;
        }

        // 알파값이 완전히 0이 되었는지 확인
        warningCanvasGroup.alpha = 0f;
    }

    bool IsValidInput(string input)
    {
        // 입력값이 비어 있지 않고, 10글자 이내이며, 특수 문자가 없는지 확인
        return !string.IsNullOrWhiteSpace(input) && input.Length <= 10 && Regex.IsMatch(input, @"^[a-zA-Z0-9가-힣]*$");
    }

    IEnumerator FadeOutAndLoadNextScene()
    {
        float duration = 1f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            panelCanvasGroup.alpha = Mathf.Lerp(0f, 1f, elapsed / duration);
            yield return null;
        }

        panelCanvasGroup.alpha = 1f;

        // 페이드 아웃 후 3초 대기
        yield return new WaitForSeconds(4f);

        loadingText.text = "Complete!";

        // 다음 씬으로 전환
        SceneManager.LoadScene("Opening");
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        // 비디오를 다시 처음부터 재생
        vp.Stop();
        vp.Play();
    }
}

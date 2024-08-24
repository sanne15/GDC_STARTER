using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Video;
using System.Text.RegularExpressions;

public class NameSceneManager : MonoBehaviour
{
    public TMP_InputField surnameInputField; // �� �Է� �ʵ�
    public TMP_InputField nameInputField;    // �̸� �Է� �ʵ�
    public TMP_InputField nicknameInputField; // ���� �Է� �ʵ�
    public Button submitButton; // ���� ��ư
    public CanvasGroup panelCanvasGroup; // �г��� CanvasGroup ����
    public VideoPlayer videoPlayer; // ���� �÷��̾� ����
    public Material blurMaterial; // �� ȿ���� ������ Material
    public TextMeshProUGUI warningText;       // ��� ���� TextMeshProUGUI
    public CanvasGroup warningCanvasGroup;    // ��� �ؽ�Ʈ�� CanvasGroup
    public TextMeshProUGUI loadingText; // �ε��ؽ�Ʈ

    private Coroutine warningCoroutine;

    void Start()
    {
        // ��ư Ŭ�� �� �̸��� ������ ó���ϴ� �޼��带 ȣ��
        submitButton.onClick.AddListener(OnSubmit);

        // ���� �÷��̾� ����
        videoPlayer.targetMaterialRenderer = GetComponent<Renderer>(); // ������ ����� Ÿ���� ����
        videoPlayer.targetMaterialProperty = "_MainTex"; // ����� Material�� �ؽ�ó ������Ƽ�� ����

        videoPlayer.loopPointReached += OnVideoEnd;

        // �� ȿ���� ����
        // videoPlayer.targetMaterialRenderer.material = blurMaterial;

        warningCanvasGroup.alpha = 0f;
    }

    void OnSubmit()
    {
        string surname = surnameInputField.text;
        string name = nameInputField.text;
        string nickname = nicknameInputField.text;

        // ��ȿ�� �˻�
        if (!IsValidInput(surname) || !IsValidInput(name) || !IsValidInput(nickname))
        {
            if (warningCoroutine != null)
            {
                StopCoroutine(warningCoroutine);
            }

            // �� ��� �ڷ�ƾ ����
            warningCoroutine = StartCoroutine(ShowWarning("�̸�, �� �Ǵ� ������ 10���� �̳����� �ϸ� Ư�����ڸ� ������ �� �����ϴ�."));
            return;
        }

        // NamePasser �ν��Ͻ��� ���� ����
        NamePasser.Instance.SetPlayerInfo(name, surname, nickname);

        // ���̵� �ƿ��� ���� �������� ��ȯ
        StartCoroutine(FadeOutAndLoadNextScene());
    }

    IEnumerator ShowWarning(string message)
    {
        // ��� ���� ���� �� ���İ� 1�� ����
        warningText.text = message;
        warningCanvasGroup.alpha = 1f;

        // 1�ʰ� ��� ���� ǥ��
        yield return new WaitForSeconds(1f);

        // �ڿ������� ���̵� �ƿ�
        float fadeDuration = 1f; // ���̵� �ƿ� ���� �ð�
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            warningCanvasGroup.alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            yield return null;
        }

        // ���İ��� ������ 0�� �Ǿ����� Ȯ��
        warningCanvasGroup.alpha = 0f;
    }

    bool IsValidInput(string input)
    {
        // �Է°��� ��� ���� �ʰ�, 10���� �̳��̸�, Ư�� ���ڰ� ������ Ȯ��
        return !string.IsNullOrWhiteSpace(input) && input.Length <= 10 && Regex.IsMatch(input, @"^[a-zA-Z0-9��-�R]*$");
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

        // ���̵� �ƿ� �� 3�� ���
        yield return new WaitForSeconds(4f);

        loadingText.text = "Complete!";

        // ���� ������ ��ȯ
        SceneManager.LoadScene("Opening");
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        // ������ �ٽ� ó������ ���
        vp.Stop();
        vp.Play();
    }
}

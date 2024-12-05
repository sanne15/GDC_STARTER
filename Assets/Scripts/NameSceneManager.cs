using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Video;
using System.Text.RegularExpressions;

public class NameSceneManager : MonoBehaviour
{
    public TMP_InputField surnameInputField; // ¼º ÀÔ·Â ÇÊµå
    public TMP_InputField nameInputField;    // ÀÌ¸§ ÀÔ·Â ÇÊµå
    public TMP_InputField nicknameInputField; // º°¸í ÀÔ·Â ÇÊµå
    public Button submitButton; // Á¦Ãâ ¹öÆ°
    public CanvasGroup panelCanvasGroup; // ÆĞ³ÎÀÇ CanvasGroup ÂüÁ¶
    public VideoPlayer videoPlayer; // ºñµğ¿À ÇÃ·¹ÀÌ¾î ÂüÁ¶
    public Material blurMaterial; // ºí·¯ È¿°ú¸¦ Àû¿ëÇÒ Material
    public TextMeshProUGUI warningText;       // °æ°í ¹®±¸ TextMeshProUGUI
    public CanvasGroup warningCanvasGroup;    // °æ°í ÅØ½ºÆ®ÀÇ CanvasGroup
    public TextMeshProUGUI loadingText; // ·ÎµùÅØ½ºÆ®

    private Coroutine warningCoroutine;

    void Start()
    {
        // ¹öÆ° Å¬¸¯ ½Ã ÀÌ¸§°ú º°¸íÀ» Ã³¸®ÇÏ´Â ¸Ş¼­µå¸¦ È£Ãâ
        submitButton.onClick.AddListener(OnSubmit);

        // ºñµğ¿À ÇÃ·¹ÀÌ¾î ¼³Á¤
        videoPlayer.targetMaterialRenderer = GetComponent<Renderer>(); // ºñµğ¿À¸¦ Àç»ıÇÒ Å¸°ÙÀ» ¼³Á¤
        videoPlayer.targetMaterialProperty = "_MainTex"; // »ç¿ëÇÒ MaterialÀÇ ÅØ½ºÃ³ ÇÁ·ÎÆÛÆ¼¸¦ ¼³Á¤

        videoPlayer.loopPointReached += OnVideoEnd;

        // ºí·¯ È¿°ú¸¦ Àû¿ë
        // videoPlayer.targetMaterialRenderer.material = blurMaterial;

        warningCanvasGroup.alpha = 0f;
    }

    void OnSubmit()
    {
        string surname = surnameInputField.text;
        string name = nameInputField.text;
        string nickname = nicknameInputField.text;

        // À¯È¿¼º °Ë»ç
        if (!IsValidInput(surname) || !IsValidInput(name) || !IsValidInput(nickname))
        {
            if (warningCoroutine != null)
            {
                StopCoroutine(warningCoroutine);
            }

            // »õ °æ°í ÄÚ·çÆ¾ ½ÇÇà
            warningCoroutine = StartCoroutine(ShowWarning("ÀÌ¸§, ¼º ¶Ç´Â º°¸íÀº 10±ÛÀÚ ÀÌ³»¿©¾ß ÇÏ¸ç Æ¯¼ö¹®ÀÚ¸¦ Æ÷ÇÔÇÒ ¼ö ¾ø½À´Ï´Ù."));
            return;
        }

        // NamePasser ÀÎ½ºÅÏ½º¿¡ º¯¼ö Àü´Ş
        NamePasser.Instance.SetPlayerInfo(name, surname, nickname);

        // ÆäÀÌµå ¾Æ¿ô°ú ´ÙÀ½ ¾ÀÀ¸·ÎÀÇ ÀüÈ¯
        StartCoroutine(FadeOutAndLoadNextScene());
    }

    IEnumerator ShowWarning(string message)
    {
        // °æ°í ¹®±¸ ¼³Á¤ ¹× ¾ËÆÄ°ª 1·Î º¯°æ
        warningText.text = message;
        warningCanvasGroup.alpha = 1f;

        // 1ÃÊ°£ °æ°í ¹®±¸ Ç¥½Ã
        yield return new WaitForSeconds(1f);

        // ÀÚ¿¬½º·¯¿î ÆäÀÌµå ¾Æ¿ô
        float fadeDuration = 1f; // ÆäÀÌµå ¾Æ¿ô Áö¼Ó ½Ã°£
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            warningCanvasGroup.alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            yield return null;
        }

        // ¾ËÆÄ°ªÀÌ ¿ÏÀüÈ÷ 0ÀÌ µÇ¾ú´ÂÁö È®ÀÎ
        warningCanvasGroup.alpha = 0f;
    }

    bool IsValidInput(string input)
    {
        // ÀÔ·Â°ªÀÌ ºñ¾î ÀÖÁö ¾Ê°í, 10±ÛÀÚ ÀÌ³»ÀÌ¸ç, Æ¯¼ö ¹®ÀÚ°¡ ¾ø´ÂÁö È®ÀÎ
        return !string.IsNullOrWhiteSpace(input) && input.Length <= 10 && Regex.IsMatch(input, @"^[a-zA-Z0-9°¡-ÆR]*$");
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

        // ÆäÀÌµå ¾Æ¿ô ÈÄ 3ÃÊ ´ë±â
        yield return new WaitForSeconds(4f);

        loadingText.text = "Complete!";

        // ´ÙÀ½ ¾ÀÀ¸·Î ÀüÈ¯
        SceneManager.LoadScene("Opening");
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        // ºñµğ¿À¸¦ ´Ù½Ã Ã³À½ºÎÅÍ Àç»ı
        vp.Stop();
        vp.Play();
    }
}

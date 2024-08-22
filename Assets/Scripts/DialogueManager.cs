using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    public RectTransform bubbleRectTransform; // 말풍선의 RectTransform을 참조
    public CanvasGroup bubbleCanvasGroup; // 말풍선의 CanvasGroup 참조
    public Image arrowImage; // 화살표 이미지 참조
    public ARPanelController arPanelController; // ARPanelController를 참조

    public TextMeshProUGUI fastForwardText; // "빨리감기" 버튼의 TextMeshProUGUI 참조
    public Color normalColor;
    public Color activeColor = Color.white;
    public float colorTransitionDuration = 0.1f;

    private Queue<string> sentences;
    private bool canProceed; // 다음 문장으로 진행할 수 있는지 여부를 나타냄

    private System.Action onDialogueComplete; // Conversation Ending Callback

    private ControlManager controlManager;
    private bool isColorTransitioning = false;


    void Start()
    {
        sentences = new Queue<string>();
        canProceed = false;
        bubbleCanvasGroup.alpha = 0; // 초기에는 말풍선을 투명하게 설정

        controlManager = FindObjectOfType<ControlManager>();
        normalColor = fastForwardText.fontMaterial.GetColor("_FaceColor"); // 기본 색상 설정
    }

    public void StartDialogue(Dialogue dialogue, System.Action onComplete)
    {
        nameText.text = dialogue.characterName;
        sentences.Clear();

        StartCoroutine(StartDialogueWithFadeIn(dialogue, onComplete)); // 대화 시작 시 말풍선 페이드 인
    }

    void Update()
    {
        if (arPanelController != null && arPanelController.isPanelVisible)
        {
            return;
        }

        // 마우스 클릭 또는 스페이스 키를 감지
        if (canProceed && (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)))
        {
            StartCoroutine(AnimateArrow());
            DisplayNextSentence();
        }

        if (controlManager.isCtrlPressed)
        {
            if (!isColorTransitioning)
            {
                StartCoroutine(ChangeTextColor(activeColor));
            }
        }
        else
        {
            if (!isColorTransitioning)
            {
                StartCoroutine(ChangeTextColor(normalColor));
            }
        }
    }

    public void DisplayNextSentence()
    {
        DayManager dayManager = FindObjectOfType<DayManager>();

        if (dayManager.currentState == DayManager.GameState.Settlement)
        {
            return;
        }

        if (sentences.Count == 0)
        {
            StartCoroutine(FadeOutBubble());
            EndDialogue();
            dialogueText.text = "";
            AdjustBubbleSize();
            return;
        }

        string sentence = sentences.Dequeue();
        // StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        canProceed = false; // 타이핑 중에는 진행할 수 없도록 설정
        float typingSpeed = 0.05f; // 각 글자 사이의 지연 시간 (초 단위)

        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            AdjustBubbleSize();

            if (!Input.GetKey(KeyCode.LeftControl) && !Input.GetKey(KeyCode.RightControl))
            {
                yield return new WaitForSeconds(typingSpeed); // Ctrl 키가 눌리지 않았을 때만 대기
            }
            else
            {
                yield return new WaitForSeconds(0.01f); // Ctrl 키가 눌린 상태에서는 대기하지 않고 바로 다음 글자로 넘어감
            }
        }

        if (!Input.GetKey(KeyCode.LeftControl) && !Input.GetKey(KeyCode.RightControl))
        {
            yield return new WaitForSeconds(0.5f);
        }

        canProceed = true;

    }

    void AdjustBubbleSize()
    {
        // TextMeshPro의 텍스트가 차지하는 공간 계산
        Vector2 textSize = dialogueText.GetPreferredValues(dialogueText.text);

        // 말풍선에 여백을 추가하여 텍스트가 너무 꽉 차지 않도록 함
        float paddingX = 30f; // 가로 여백
        float paddingY = 20f; // 세로 여백

        // 말풍선의 크기를 텍스트 크기에 맞게 조정 (여기서는 약간의 여백을 더함)
        bubbleRectTransform.sizeDelta = new Vector2(textSize.x + paddingX, textSize.y + paddingY);
    }

    void EndDialogue()
    {
        Debug.Log("Dialogue ended.");

        if (onDialogueComplete != null)
        {
            onDialogueComplete.Invoke();
        }
    }

    private IEnumerator StartDialogueWithFadeIn(Dialogue dialogue, System.Action onComplete)
    {
        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        yield return StartCoroutine(FadeInBubble()); // 대화 시작 시 말풍선 페이드 인

        onDialogueComplete = onComplete;
        DisplayNextSentence();
    }

    IEnumerator FadeInBubble()
    {
        float duration = 0.3f; // 페이드 인/아웃 시간
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            bubbleCanvasGroup.alpha = Mathf.Lerp(0, 1, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        bubbleCanvasGroup.alpha = 1;
    }

    IEnumerator FadeOutBubble()
    {
        float duration = 0.3f;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            bubbleCanvasGroup.alpha = Mathf.Lerp(1, 0, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        bubbleCanvasGroup.alpha = 0;
    }

    IEnumerator AnimateArrow()
    {
        Color originalColor = arrowImage.color;
        Color fadedColor = new Color(originalColor.r, originalColor.g, originalColor.b, 0.3f);

        // 색을 바래게 만듦
        arrowImage.color = fadedColor;

        // 잠시 대기
        yield return new WaitForSeconds(0.3f);

        // 원래 색으로 복구
        arrowImage.color = originalColor;
    }

    IEnumerator ChangeTextColor(Color targetColor)
    {
        isColorTransitioning = true;

        Color currentColor = fastForwardText.fontMaterial.GetColor("_FaceColor");
        float elapsed = 0f;

        while (elapsed < colorTransitionDuration)
        {
            Color newColor = Color.Lerp(currentColor, targetColor, elapsed / colorTransitionDuration);
            fastForwardText.fontMaterial.SetColor("_FaceColor", newColor);
            elapsed += Time.deltaTime;
            yield return null;
        }

        fastForwardText.fontMaterial.SetColor("_FaceColor", targetColor);
        isColorTransitioning = false;
    }
}


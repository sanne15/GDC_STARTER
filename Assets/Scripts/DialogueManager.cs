using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    public RectTransform bubbleRectTransform; // ��ǳ���� RectTransform�� ����
    public CanvasGroup bubbleCanvasGroup; // ��ǳ���� CanvasGroup ����
    public Image arrowImage; // ȭ��ǥ �̹��� ����
    public ARPanelController arPanelController; // ARPanelController�� ����

    public TextMeshProUGUI fastForwardText; // "��������" ��ư�� TextMeshProUGUI ����
    public Color normalColor;
    public Color activeColor = Color.white;
    public float colorTransitionDuration = 0.1f;

    private Queue<string> sentences;
    private bool canProceed; // ���� �������� ������ �� �ִ��� ���θ� ��Ÿ��

    private System.Action onDialogueComplete; // Conversation Ending Callback

    private ControlManager controlManager;
    private bool isColorTransitioning = false;


    void Start()
    {
        sentences = new Queue<string>();
        canProceed = false;
        bubbleCanvasGroup.alpha = 0; // �ʱ⿡�� ��ǳ���� �����ϰ� ����

        controlManager = FindObjectOfType<ControlManager>();
        normalColor = fastForwardText.fontMaterial.GetColor("_FaceColor"); // �⺻ ���� ����
    }

    public void StartDialogue(Dialogue dialogue, System.Action onComplete)
    {
        nameText.text = dialogue.characterName;
        sentences.Clear();

        StartCoroutine(StartDialogueWithFadeIn(dialogue, onComplete)); // ��ȭ ���� �� ��ǳ�� ���̵� ��
    }

    void Update()
    {
        if (arPanelController != null && arPanelController.isPanelVisible)
        {
            return;
        }

        // ���콺 Ŭ�� �Ǵ� �����̽� Ű�� ����
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
        canProceed = false; // Ÿ���� �߿��� ������ �� ������ ����
        float typingSpeed = 0.05f; // �� ���� ������ ���� �ð� (�� ����)

        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            AdjustBubbleSize();

            if (!Input.GetKey(KeyCode.LeftControl) && !Input.GetKey(KeyCode.RightControl))
            {
                yield return new WaitForSeconds(typingSpeed); // Ctrl Ű�� ������ �ʾ��� ���� ���
            }
            else
            {
                yield return new WaitForSeconds(0.01f); // Ctrl Ű�� ���� ���¿����� ������� �ʰ� �ٷ� ���� ���ڷ� �Ѿ
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
        // TextMeshPro�� �ؽ�Ʈ�� �����ϴ� ���� ���
        Vector2 textSize = dialogueText.GetPreferredValues(dialogueText.text);

        // ��ǳ���� ������ �߰��Ͽ� �ؽ�Ʈ�� �ʹ� �� ���� �ʵ��� ��
        float paddingX = 30f; // ���� ����
        float paddingY = 20f; // ���� ����

        // ��ǳ���� ũ�⸦ �ؽ�Ʈ ũ�⿡ �°� ���� (���⼭�� �ణ�� ������ ����)
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

        yield return StartCoroutine(FadeInBubble()); // ��ȭ ���� �� ��ǳ�� ���̵� ��

        onDialogueComplete = onComplete;
        DisplayNextSentence();
    }

    IEnumerator FadeInBubble()
    {
        float duration = 0.3f; // ���̵� ��/�ƿ� �ð�
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

        // ���� �ٷ��� ����
        arrowImage.color = fadedColor;

        // ��� ���
        yield return new WaitForSeconds(0.3f);

        // ���� ������ ����
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


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
    public TextMeshProUGUI LogText; // "��ȭ�α�" ��ư�� TextMeshProUGUI ����
    public Color normalColor = new Color(0.3f, 0.3f, 0.3f, 1f);
    public Color activeColor = Color.white;
    public float colorTransitionDuration = 0.1f;

    private Queue<SentenceData> sentences;
    private bool canProceed; // ���� �������� ������ �� �ִ��� ���θ� ��Ÿ��

    private System.Action onDialogueComplete; // Conversation Ending Callback
    private bool Colorcount = false;

    private ControlManager controlManager;
    private bool isColorTransitioning = false;
    private bool isColorTransitioning2 = false;

    private string character_name_dialogue;
    private string subcharacter_name_dialogue;

    private Dictionary<string, string> dict_alias;
    public string playername;
    public string shopname;

    public Customer currentCustomer;


    void Start()
    {

        dict_alias = new Dictionary<string, string>
        {
            { "�÷��̾�", playername },
            { "�����̸�", shopname },
            { "br", "\n" }
        };

        sentences = new Queue<SentenceData>();
        canProceed = false;
        bubbleCanvasGroup.alpha = 0; // �ʱ⿡�� ��ǳ���� �����ϰ� ����

        controlManager = FindObjectOfType<ControlManager>();
        normalColor = fastForwardText.fontMaterial.GetColor("_FaceColor"); // �⺻ ���� ����
    }

    public void StartDialogue(Dialogue dialogue, System.Action onComplete)
    {
        foreach (var sentence in dialogue.sentences)
        {
            // �ؽ�Ʈ ġȯ ����
            sentence.text = ReplaceVariablesInText(sentence.text);
        }

        nameText.text = dialogue.characterName;
        character_name_dialogue = nameText.text;
        subcharacter_name_dialogue = dialogue.characterName;
        sentences.Clear();

        StartCoroutine(StartDialogueWithFadeIn(dialogue, onComplete)); // ��ȭ ���� �� ��ǳ�� ���̵� ��
    }

    private string ReplaceVariablesInText(string text)
    {
        foreach (var entry in dict_alias)
        {
            string key = "(" + entry.Key + ")"; // (�÷��̾�) ���·� Ű�� ã��
            text = text.Replace(key, entry.Value); // �ؽ�Ʈ ������ ġȯ
        }
        return text;
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

        if (controlManager.isLPressed)
        {
            if (!isColorTransitioning2)
            {
                StartCoroutine(ChangeTextColor2());
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

        SentenceData sentenceData = sentences.Dequeue();
        // StopAllCoroutines();
        StartCoroutine(TypeSentence(sentenceData));
    }

    IEnumerator TypeSentence(SentenceData sentence)
    {

        if (sentence == null || string.IsNullOrEmpty(sentence.text))
        {
            Debug.LogError("SentenceData or its text is null or empty.");
            yield break;
        }

        dialogueText.text = "";
        canProceed = false; // Ÿ���� �߿��� ������ �� ������ ����
        float typingSpeed = 0.05f; // �� ���� ������ ���� �ð� (�� ����)

        // speaker : 0�� ���� 1�� ������ 2�� �� 3�� 3�� �� 4��
        switch (sentence.speaker)
        {
            case 0:
                nameText.text = character_name_dialogue;
                currentCustomer.ChangeExpression(sentence.emotion);
                break;

            case 1:
                nameText.text = playername;
                break;

            case 2:
                nameText.text = subcharacter_name_dialogue;
                break;

            default:
                Debug.Log($"Dialogue format invaild : speaker = {sentence.speaker}");
                break;
        }

        foreach (char letter in sentence.text.ToCharArray())
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
        // ���ϴ� ��ǳ���� �ִ� ���� �����մϴ�.
        float maxWidth = 700f; // ���ϴ� �ִ� ���� ����

        // TextMeshPro�� �ش� ���� ���� �ؽ�Ʈ�� �����ϵ��� �����մϴ�.
        dialogueText.enableWordWrapping = true;
        dialogueText.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, maxWidth);

        // TextMeshPro�� �ؽ�Ʈ�� �����ϴ� ������ ����մϴ�.
        Vector2 textSize = dialogueText.GetPreferredValues(dialogueText.text, maxWidth, 0);

        // ��ǳ���� ũ�⸦ �ؽ�Ʈ ũ�⿡ �°� ���� (���⼭�� �ణ�� ������ ����)
        float paddingX = 10f; // ���� ����
        float paddingY = 20f; // ���� ����

        bubbleRectTransform.sizeDelta = new Vector2(Mathf.Min(textSize.x + paddingX, maxWidth + paddingX), textSize.y + paddingY);
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
        foreach(SentenceData sentence in dialogue.sentences)
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

    IEnumerator ChangeTextColor2()
    {
        Color targetColor;
        isColorTransitioning2 = true;
        Colorcount = !Colorcount;

        if (Colorcount)
        {
            targetColor = activeColor;
        }
        else
        {
            targetColor = normalColor;
        }

        Color currentColor = LogText.fontMaterial.GetColor("_FaceColor");
        float elapsed = 0f;

        while (elapsed < colorTransitionDuration)
        {
            Color newColor = Color.Lerp(currentColor, targetColor, elapsed / colorTransitionDuration);
            LogText.fontMaterial.SetColor("_FaceColor", newColor);
            elapsed += Time.deltaTime;
            yield return null;
        }

        LogText.fontMaterial.SetColor("_FaceColor", targetColor);
        isColorTransitioning2 = false;
    }
}


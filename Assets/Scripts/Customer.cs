using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{
    public Dialogue dialogue;
    public SpriteRenderer expressionSpriteRenderer; // ǥ�� ���� ���� SpriteRenderer
    public Sprite[] expressionSprites; // ǥ���̳� �߰� ��ҿ� ����� ��������Ʈ �迭

    public enum Emotion
    {
        neutral,
        delight,
        angry,
        exhausted,
        sad,
        bored,
        frightened,
        despair,
        laughter
        // �ʿ��� �ٸ� ������ �߰�
    }

    public Dictionary<Emotion, int> emotionToSpriteIndex;

    // Unity Editor���� Dictionary�� ������ �� �ֵ��� Serializable Ŭ���� ����
    [System.Serializable]
    public class EmotionSpriteMapping
    {
        public Emotion emotion;
        public int spriteIndex; // �ش� ������ ���ε� ��������Ʈ �ε���
    }

    public EmotionSpriteMapping[] emotionSpriteMappings;

    void Awake()
    {
        // Dictionary �ʱ�ȭ �� EmotionSpriteMapping �迭 ������� ä���
        emotionToSpriteIndex = new Dictionary<Emotion, int>();
        foreach (var mapping in emotionSpriteMappings)
        {
            if (!emotionToSpriteIndex.ContainsKey(mapping.emotion))
            {
                emotionToSpriteIndex.Add(mapping.emotion, mapping.spriteIndex);
            }
        }
    }

    void Start()
    {
        // �ʱ�ȭ �� alpha ���� 0���� �����Ͽ� ������ �ʵ��� ��
        SetAlpha(0f);

        // Dialogue�� �Ҵ���� ���� ��� �⺻�� ���� (�ʿ��� ���)
        if (dialogue == null)
        {
            // Debug.LogWarning("Dialogue is not assigned on start. Please check the assignment.");
            dialogue = ScriptableObject.CreateInstance<Dialogue>();
            dialogue.characterName = "Default";
            dialogue.sentences = new List<SentenceData>
            {
                new SentenceData
                {
                    text = "No dialogue available.",
                    speaker = 0, // �⺻������ ������ ��ȭ�ڷ� ����
                    emotion = "neutral" // �⺻ ���� ����
                }
            };
        }
    }

    public void EnterShop(System.Action onCustomerSeated)
    {
        StartCoroutine(EnterShopRoutine(onCustomerSeated));
    }

    private IEnumerator EnterShopRoutine(System.Action onCustomerSeated)
    {
        SetAlpha(1.0f);

        StartCoroutine(ApplyDarkTint());

        // 1�� ��ٸ�
        yield return new WaitForSeconds(1f);

        if (expressionSprites.Length > 0)
        {
            expressionSpriteRenderer.sprite = expressionSprites[0]; // �⺻ ǥ������ ����
        }

        if (dialogue != null)
        {
            if (!string.IsNullOrEmpty(dialogue.characterName))
            {
                Debug.Log("Customer entered the shop: " + dialogue.characterName);
            }
            else
            {
                Debug.LogWarning("Dialogue characterName is null or empty for this customer.");
            }
        }
        onCustomerSeated?.Invoke();
    }

    public IEnumerator ExitShop(bool exitToLeft)
    {
        // �ڷ�ƾ ���� ������ �̹� �ı��� ��� �ٷ� ����
        if (expressionSpriteRenderer == null)
        {
            yield break;
        }

        expressionSpriteRenderer.color = new Color(0.1f, 0.1f, 0.1f, 0.7f);

        // ���� �� ��ο� ���� ȿ�� ���� �� ĳ���� ����
        yield return StartCoroutine(ExitWithEasing(exitToLeft));

        // ĳ������ ���� ������Ʈ�� ����
        if (expressionSpriteRenderer != null)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator ApplyDarkTint()
    {
        // ���� ���� ����
        Color expressionOriginalColor = expressionSpriteRenderer.color;

        // ��ο� ���� ���� (������ ����� ���� ��ó��)
        Color darkColor = new Color(0.1f, 0.1f, 0.1f, 0.7f); // ������, �ణ �����ϰ� ����

        // �̹����� ������ ��Ӱ� ����
        expressionSpriteRenderer.color = darkColor;

        // ��� ��� (0.3�� ����)
        yield return new WaitForSeconds(0.3f);

        // ���� �������� ����
        expressionSpriteRenderer.color = expressionOriginalColor;
    }

    IEnumerator ExitWithEasing(bool exitToLeft)
    {
        float duration = 1.0f; // �̵� �ð�
        float elapsedTime = 0f;

        Vector3 startPosition = transform.position;
        Vector3 endPosition;

        if (exitToLeft)
        {
            endPosition = startPosition + new Vector3(-15f, 0, 0); // �������� �̵�
        }
        else
        {
            endPosition = startPosition + new Vector3(15f, 0, 0); // ���������� �̵�
        }

        while (elapsedTime < duration)
        {
            // �ӵ� ������ ���� �ð� ���� ��� (ó�� ������, �߰��� ������, ���� ������)
            float t = elapsedTime / duration;
            t = t * t * (3f - 2f * t); // SmoothStep (Ease-in-out ���)

            // ���ο� ��ġ ���
            transform.position = Vector3.Lerp(startPosition, endPosition, t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // ���� ��ġ ����
        transform.position = endPosition;
    }

    public void ChangeExpression(string emotion)
    {
        if (expressionSprites == null || expressionSprites.Length == 0)
        {
            Debug.LogWarning("No expression sprites assigned to this customer.");
            return;
        }

        // �Էµ� emotion�� enum���� ��ȯ �õ�
        if (System.Enum.TryParse(emotion, true, out Emotion parsedEmotion))
        {
            // emotionToSpriteIndex���� �ش� ������ �ε����� ������
            if (emotionToSpriteIndex.TryGetValue(parsedEmotion, out int expressionIndex))
            {
                if (expressionIndex >= 0 && expressionIndex < expressionSprites.Length)
                {
                    expressionSpriteRenderer.sprite = expressionSprites[expressionIndex];
                }
                else
                {
                    Debug.LogWarning($"Sprite index for emotion '{emotion}' is out of bounds for this customer.");
                }
            }
            else
            {
                Debug.LogWarning($"Emotion '{emotion}' not mapped to a sprite for this customer.");
            }
        }
        else
        {
            Debug.LogWarning($"Emotion '{emotion}' could not be parsed to an enum.");
        }
    }


    void SetAlpha(float alpha)
    {
        Color expressionColor = expressionSpriteRenderer.color;
        expressionColor.a = alpha;
        expressionSpriteRenderer.color = expressionColor;
    }
}


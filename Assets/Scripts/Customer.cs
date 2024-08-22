using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{
    public Dialogue dialogue;
    public SpriteRenderer expressionSpriteRenderer; // 표정 등을 위한 SpriteRenderer
    public Sprite[] expressionSprites; // 표정이나 추가 요소에 사용할 스프라이트 배열

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
        // 필요한 다른 감정들 추가
    }

    public Dictionary<Emotion, int> emotionToSpriteIndex;

    // Unity Editor에서 Dictionary를 설정할 수 있도록 Serializable 클래스 정의
    [System.Serializable]
    public class EmotionSpriteMapping
    {
        public Emotion emotion;
        public int spriteIndex; // 해당 감정에 매핑된 스프라이트 인덱스
    }

    public EmotionSpriteMapping[] emotionSpriteMappings;

    void Awake()
    {
        // Dictionary 초기화 및 EmotionSpriteMapping 배열 기반으로 채우기
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
        // 초기화 시 alpha 값을 0으로 설정하여 보이지 않도록 함
        SetAlpha(0f);

        // Dialogue가 할당되지 않은 경우 기본값 설정 (필요한 경우)
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
                    speaker = 0, // 기본값으로 상대방이 발화자로 설정
                    emotion = "neutral" // 기본 감정 설정
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

        // 1초 기다림
        yield return new WaitForSeconds(1f);

        if (expressionSprites.Length > 0)
        {
            expressionSpriteRenderer.sprite = expressionSprites[0]; // 기본 표정으로 설정
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
        // 코루틴 시작 시점에 이미 파괴된 경우 바로 종료
        if (expressionSpriteRenderer == null)
        {
            yield break;
        }

        expressionSpriteRenderer.color = new Color(0.1f, 0.1f, 0.1f, 0.7f);

        // 퇴장 시 어두운 색상 효과 적용 및 캐릭터 삭제
        yield return StartCoroutine(ExitWithEasing(exitToLeft));

        // 캐릭터의 게임 오브젝트를 삭제
        if (expressionSpriteRenderer != null)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator ApplyDarkTint()
    {
        // 원래 색상 저장
        Color expressionOriginalColor = expressionSpriteRenderer.color;

        // 어두운 색상 설정 (검정색 비닐을 씌운 것처럼)
        Color darkColor = new Color(0.1f, 0.1f, 0.1f, 0.7f); // 검정색, 약간 투명하게 설정

        // 이미지의 색상을 어둡게 변경
        expressionSpriteRenderer.color = darkColor;

        // 잠시 대기 (0.3초 정도)
        yield return new WaitForSeconds(0.3f);

        // 원래 색상으로 복구
        expressionSpriteRenderer.color = expressionOriginalColor;
    }

    IEnumerator ExitWithEasing(bool exitToLeft)
    {
        float duration = 1.0f; // 이동 시간
        float elapsedTime = 0f;

        Vector3 startPosition = transform.position;
        Vector3 endPosition;

        if (exitToLeft)
        {
            endPosition = startPosition + new Vector3(-15f, 0, 0); // 왼쪽으로 이동
        }
        else
        {
            endPosition = startPosition + new Vector3(15f, 0, 0); // 오른쪽으로 이동
        }

        while (elapsedTime < duration)
        {
            // 속도 조절을 위한 시간 비율 계산 (처음 느리게, 중간에 빠르게, 끝에 느리게)
            float t = elapsedTime / duration;
            t = t * t * (3f - 2f * t); // SmoothStep (Ease-in-out 방식)

            // 새로운 위치 계산
            transform.position = Vector3.Lerp(startPosition, endPosition, t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 최종 위치 설정
        transform.position = endPosition;
    }

    public void ChangeExpression(string emotion)
    {
        if (expressionSprites == null || expressionSprites.Length == 0)
        {
            Debug.LogWarning("No expression sprites assigned to this customer.");
            return;
        }

        // 입력된 emotion을 enum으로 변환 시도
        if (System.Enum.TryParse(emotion, true, out Emotion parsedEmotion))
        {
            // emotionToSpriteIndex에서 해당 감정의 인덱스를 가져옴
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


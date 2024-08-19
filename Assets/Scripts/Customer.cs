using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{
    public Dialogue dialogue;
    public SpriteRenderer expressionSpriteRenderer; // 표정 등을 위한 SpriteRenderer
    public Sprite[] expressionSprites; // 표정이나 추가 요소에 사용할 스프라이트 배열


    void Start()
    {
        // 초기화 시 alpha 값을 0으로 설정하여 보이지 않도록 함
        SetAlpha(0f);
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

        Debug.Log("Customer entered the shop: " + dialogue.namae);
        onCustomerSeated?.Invoke();
    }

    public IEnumerator ExitShop(bool exitToLeft)
    {
        expressionSpriteRenderer.color = new Color(0.1f, 0.1f, 0.1f, 0.7f);

        // 퇴장 시 어두운 색상 효과 적용 및 캐릭터 삭제
        yield return StartCoroutine(ExitWithEasing(exitToLeft));

        // 캐릭터의 게임 오브젝트를 삭제
        Destroy(gameObject);
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

    public void ChangeExpression(int index)
    {
        if (index >= 0 && index < expressionSprites.Length)
        {
            expressionSpriteRenderer.sprite = expressionSprites[index];
        }
    }

    void SetAlpha(float alpha)
    {
        Color expressionColor = expressionSpriteRenderer.color;
        expressionColor.a = alpha;
        expressionSpriteRenderer.color = expressionColor;
    }
}


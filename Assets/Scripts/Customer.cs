using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{
    public Dialogue dialogue;
    public SpriteRenderer expressionSpriteRenderer; // ǥ�� ���� ���� SpriteRenderer
    public Sprite[] expressionSprites; // ǥ���̳� �߰� ��ҿ� ����� ��������Ʈ �迭


    void Start()
    {
        // �ʱ�ȭ �� alpha ���� 0���� �����Ͽ� ������ �ʵ��� ��
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

        // 1�� ��ٸ�
        yield return new WaitForSeconds(1f);

        if (expressionSprites.Length > 0)
        {
            expressionSpriteRenderer.sprite = expressionSprites[0]; // �⺻ ǥ������ ����
        }

        Debug.Log("Customer entered the shop: " + dialogue.namae);
        onCustomerSeated?.Invoke();
    }

    public IEnumerator ExitShop(bool exitToLeft)
    {
        expressionSpriteRenderer.color = new Color(0.1f, 0.1f, 0.1f, 0.7f);

        // ���� �� ��ο� ���� ȿ�� ���� �� ĳ���� ����
        yield return StartCoroutine(ExitWithEasing(exitToLeft));

        // ĳ������ ���� ������Ʈ�� ����
        Destroy(gameObject);
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


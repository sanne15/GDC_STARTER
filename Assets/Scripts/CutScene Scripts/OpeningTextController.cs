using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using TMPro;

public class OpeningTextController : MonoBehaviour
{
	private CanvasGroup _canvasGroup;
	private TextMeshProUGUI _openingText;

	public float textSpeed;
	private string playerName;

	private string cutSceneText1;
	private string cutSceneText2;
	private string cutSceneText3;
	private string cutSceneText4;
	private string cutSceneText5;

	private void Awake()
	{
		_canvasGroup = gameObject.GetComponent<CanvasGroup>();
		_openingText = GetComponent<TextMeshProUGUI>();
		_openingText.text = string.Empty;

		playerName = NamePasser.Instance.playeraltername;
        cutSceneText1 = $"- {playerName}~ �ۿ��� �׸� ��� �� �����!\n- ��~";
		cutSceneText2 = "�Ŀ�.. ���� ��� ���� �ʳ�. �θ���� �̷� �� ��� �Ͻ� ����..";
		cutSceneText3 = "����ÿ��� �������? ������ �ΰ��ϰڴٰ�?\n� �� ������ �׷��� ���� �����ڳİ�";
		cutSceneText4 = "������! ���� ��� �ѱ׸�!";
		cutSceneText5 = "�� �����ϴ�~ �մ�, ���� �Ϸ�� ��̳���?";
	}

	public void ResetText()
	{
		_openingText.text = string.Empty;
		_canvasGroup.alpha = 1f;
	}

	private IEnumerator TypingEffect(string text)
	{
		StringBuilder openingTextBuilder = new StringBuilder();
		for (int i = 0; i < text.Length; i++)
		{
			if (char.IsWhiteSpace(text[i]))
			{
				openingTextBuilder.Append(text[i]);
				_openingText.text = openingTextBuilder.ToString();
				i++;
			}
			openingTextBuilder.Append(text[i]);
			_openingText.text = openingTextBuilder.ToString();
			yield return new WaitForSeconds(textSpeed);
		}
	}

	public IEnumerator DisplayText1()
	{
		yield return StartCoroutine(TypingEffect(cutSceneText1));
	}

	public IEnumerator DisplayText2()
	{
		yield return StartCoroutine(TypingEffect(cutSceneText2));
	}

	public IEnumerator DisplayText3()
	{
		yield return StartCoroutine(TypingEffect(cutSceneText3));
	}

	public IEnumerator DisplayText4()
	{
		yield return StartCoroutine(TypingEffect(cutSceneText4));
	}

	public IEnumerator DisplayText5()
	{
		yield return StartCoroutine(TypingEffect(cutSceneText5));
	}
}

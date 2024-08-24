using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using TMPro;

public class Ending1TextController : MonoBehaviour
{
	private CanvasGroup _canvasGroup;
	private TextMeshProUGUI _endingText;

	private string playerName;

	private string cutSceneText1;
	private string cutSceneText2;
	private string cutSceneText3;
	private string cutSceneText4;
	private string cutSceneText5;
	private string cutSceneText6;
	private string cutSceneText7;

	public float textSpeed;

	private void Awake()
	{
		_canvasGroup = gameObject.GetComponent<CanvasGroup>();
		_endingText = GetComponent<TextMeshProUGUI>();
		_endingText.text = string.Empty;

        playerName = NamePasser.Instance.playeraltername;

        cutSceneText1 = $"{playerName} : ��.";
		cutSceneText2 = "�̺��� : ������� ������ ���� ���Ҽ�.";
		cutSceneText3 = $"{playerName} : �ƴϿ�. �� ��ǥ�� ���� �ƴϰڼ�.";
		cutSceneText4 = "�̺��� : ������. ����. �� �׷�, �κ��� ���� �����ǾƷ� ���ư�����.";
		cutSceneText5 = "[�Ӻ��Դϴ�. �̺��� ��ǥ�� �ְ������ڷ� ���� ����Ǿ����ϴ�.]";
		cutSceneText6 = "[�̺��� ��ǥ�� ���� �� �κ��� ���� ��⸦ ����Ͽ����ϴ�.]\n[����, �κ����� �����ϴ� �Ϻ��� ���� ������ �����ϴ� �.]";
		cutSceneText7 = "[Ed 1. ���� ����]";
	}

	public void ResetText()
	{
		_endingText.text = string.Empty;
		_canvasGroup.alpha = 1f;
	}

	private IEnumerator TypingEffect(string text)
	{
		StringBuilder endingTextBuilder = new StringBuilder();
		for (int i = 0; i < text.Length; i++)
		{
			if (char.IsWhiteSpace(text[i]))
			{
				endingTextBuilder.Append(text[i]);
				_endingText.text = endingTextBuilder.ToString();
				i++;
			}
			endingTextBuilder.Append(text[i]);
			_endingText.text = endingTextBuilder.ToString();
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

	public IEnumerator DisplayText6()
	{
		yield return StartCoroutine(TypingEffect(cutSceneText6));
	}

	public IEnumerator DisplayText7()
	{
		yield return StartCoroutine(TypingEffect(cutSceneText7));
	}
}

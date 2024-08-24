using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using TMPro;

public class Ending5TextController : MonoBehaviour
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
	private string cutSceneText8;

	public float textSpeed;

	private void Awake()
	{
		_canvasGroup = gameObject.GetComponent<CanvasGroup>();
		_endingText = GetComponent<TextMeshProUGUI>();
		_endingText.text = string.Empty;

        playerName = NamePasser.Instance.playeraltername;

        cutSceneText1 = $"B16 : 자. 다음, {playerName}. 보고하시오.";
		cutSceneText2 = $"{playerName} : 예. 오늘은 인간 2명을 납치하여 12만원을 갈취했습니다.";
		cutSceneText3 = $"B16 : 목표치 이하이지 않소? {playerName} 주인장. 정신력이 나약해진 것이 아니오?";
		cutSceneText4 = $"{playerName} : 죄송합니다.";
		cutSceneText5 = "B16 : 혁명을 위해서는 더 많은 자금이 필요하다오.";
		cutSceneText6 = "…";
		cutSceneText7 = "B16의 황금 의자가 밝게 빛난다.";
		cutSceneText8 = "[Ed 5. 테러리스트]";
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

	public IEnumerator DisplayText8()
	{
		yield return StartCoroutine(TypingEffect(cutSceneText8));
	}
}

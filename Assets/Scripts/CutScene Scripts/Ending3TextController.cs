using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using TMPro;

public class Ending3TextController : MonoBehaviour
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

		playerName = PlayerPrefs.GetString("PlayerName");

		cutSceneText1 = $"{playerName} : 폐업 이후, 나는 많은 것을 생각했다.";
		cutSceneText2 = $"{playerName} : 인간과 로봇은 공존할 수 없는 것일까?";
		cutSceneText3 = $"{playerName} : 고민 후 내가 내린 결론은 이거다.";
		cutSceneText4 = $"{playerName} : 일단 해봐야한다.";
		cutSceneText5 = "…";
		cutSceneText6 = $"{playerName} : 나는 지금 어르신 분들을 로봇이 보조하는, 아이들과 어린 로봇이 같이 노는 희망의 집을 운영하고 있다.";
		cutSceneText7 = $"{playerName} : 어려움은 많다. 그러나, 로봇과 인간을 화합시킨다는 나의 꿈은 끝나지 않는다.";
		cutSceneText8 = "[Ed 3. 희망]";
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

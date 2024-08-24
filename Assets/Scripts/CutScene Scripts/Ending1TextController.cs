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

        cutSceneText1 = $"{playerName} : ….";
		cutSceneText2 = "이병도 : 여기까지 오느라 수고 많았소.";
		cutSceneText3 = $"{playerName} : 아니오. 다 대표님 덕분 아니겠소.";
		cutSceneText4 = "이병도 : 하하하. 고맙소. 자 그럼, 로봇이 없는 유토피아로 나아가봅세.";
		cutSceneText5 = "[속보입니다. 이병도 대표가 최고지도자로 오늘 선출되었습니다.]";
		cutSceneText6 = "[이병도 대표는 취임 후 로봇의 전면 폐기를 명령하였습니다.]\n[또한, 로봇권을 보장하는 일본을 상대로 전쟁을 선포하는 등….]";
		cutSceneText7 = "[Ed 1. 나의 투쟁]";
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

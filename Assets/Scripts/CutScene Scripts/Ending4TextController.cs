using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using TMPro;

public class Ending4TextController : MonoBehaviour
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
	private string cutSceneText9;
	private string cutSceneText10;

	public float textSpeed;

	private void Awake()
	{
		_canvasGroup = gameObject.GetComponent<CanvasGroup>();
		_endingText = GetComponent<TextMeshProUGUI>();
		_endingText.text = string.Empty;

        playerName = NamePasser.Instance.playeraltername;

        cutSceneText1 = $"{playerName} : 폐업 이후, 나는 많은 것을 생각했다.";
		cutSceneText2 = $"{playerName} : 인간과 로봇은 공존할 수 없는 것일까?";
		cutSceneText3 = $"{playerName} : 고민 후 내가 내린 결론은 이거다.";
		cutSceneText4 = $"{playerName} : 그럴 수 없다는 것이다.";
		cutSceneText5 = "…";
		cutSceneText6 = $"{playerName} : 나는 지금 로봇 인권 단체의 회장으로 활동 중이다.";
		cutSceneText7 = $"{playerName} : 로봇이 자유로워지는 건 당연한 것이다.";
		cutSceneText8 = $"{playerName} : 인간이 피해를 보는 것보다 로봇이 자유로워지는 것이 우선이다.";
		cutSceneText9 = $"{playerName} : 사람들은 나를 미치광이라고 부르지만, 내가 옳다는 것을 세상이 아는 날은 올 것이다.";
		cutSceneText10 = "[Ed 4. 사이보그]";
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

	public IEnumerator DisplayText9()
	{
		yield return StartCoroutine(TypingEffect(cutSceneText9));
	}

	public IEnumerator DisplayText10()
	{
		yield return StartCoroutine(TypingEffect(cutSceneText10));
	}
}

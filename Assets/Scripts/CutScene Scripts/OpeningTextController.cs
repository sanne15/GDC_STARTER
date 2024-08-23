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
        cutSceneText1 = $"- {playerName}~ 밖에서 그만 놀고 좀 도우렴!\n- 네~";
		cutSceneText2 = "후우.. 가게 운영이 쉽지 않네. 부모님은 이런 걸 어떻게 하신 건지..";
		cutSceneText3 = "상락시에서 나가라고? 벌금을 부과하겠다고?\n흥… 내 고향을 그렇게 쉽게 버리겠냐고…";
		cutSceneText4 = "주인장! 여기 라면 한그릇!";
		cutSceneText5 = "네 나갑니다~ 손님, 오늘 하루는 어떠셨나요?";
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

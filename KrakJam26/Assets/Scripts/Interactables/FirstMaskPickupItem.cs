using TMPro;
using UnityEngine;

public class FirstMaskPickupItem : MaskPickupItem
{

	[SerializeField] private float delayBetweenEachLine;
	[SerializeField] private string[] dialogueLines;
	[SerializeField] private TextMeshProUGUI dialogueText;

	private int lastShownLineCount = 0;
	private float _timeSinceStart;

	private void Update()
	{
		_timeSinceStart += Time.deltaTime;

		int linesToShow = Mathf.FloorToInt(_timeSinceStart / delayBetweenEachLine);
		linesToShow = Mathf.Clamp(linesToShow, 0, dialogueLines.Length);
		if (linesToShow != lastShownLineCount)
		{
			lastShownLineCount = linesToShow;
			
			dialogueText.text = "";
			for (int i = 0; i < linesToShow; i++)
			{
				dialogueText.text += dialogueLines[i] + "\n";
			}
		}
	}

	public override string GetPromptText()
	{
		return "";
	}

	public override void Interact()
	{
		if (_timeSinceStart < delayBetweenEachLine * dialogueLines.Length)
		{
			return;
		}
		dialogueText.text = "";
		base.Interact();
	}
}

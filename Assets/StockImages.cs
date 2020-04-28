using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using KModkit;

public class StockImages : MonoBehaviour
{
    public KMBombInfo Bomb;
    public KMAudio Audio;
    public KMSelectable[] Buttons;
    public Material[] Pictures;
    private List<int> weed = new List<int> {0,1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,26,27,28,29,30,31,32,33,34,35,36,37,38,39};
    public GameObject[] Buttons2;
	
	//Logging
    static int moduleIdCounter = 1;
    int moduleId;
    private bool moduleSolved;
	
    int dick = 0;
    int dickier = 0;
    int dickiest = 0;
	
	void Awake()
	{
		moduleId = moduleIdCounter++;
		foreach (KMSelectable Button in Buttons)
		{
		  Button.OnInteract += delegate () { ButtonPress(Button); return false; };
		}
    }
	
    void Start()
	{
		weed.Shuffle();
		dick = weed[0];
		dickier = weed[1];
		dickiest = weed[2];
		Buttons2[0].GetComponent<MeshRenderer>().material = Pictures[dick];
		Buttons2[1].GetComponent<MeshRenderer>().material = Pictures[dickier];
		Buttons2[2].GetComponent<MeshRenderer>().material = Pictures[dickiest];
		
		if (dick >= dickier && dick >= dickiest)
		{
			Debug.LogFormat("[Stock Images #{0}] The TL image has the highest value with {1}", moduleId, dick);
		}
		
		else if (dickier >= dick && dickier >= dickiest)
		{
			Debug.LogFormat("[Stock Images #{0}] The BL image has the highest value with {1}", moduleId, dickier);
		}
		
		else if (dickiest >= dick && dickiest >= dickier)
		{
			Debug.LogFormat("[Stock Images #{0}] The BR image has the highest value with {1}", moduleId, dickiest);
		}
    }
	
    void ButtonPress(KMSelectable Button)
	{
		Button.AddInteractionPunch();
		GetComponent<KMAudio>().PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, transform);
		if (moduleSolved == false)
		{
			if (Button == Buttons[0])
			{
				if (dick >= dickier && dick >= dickiest)
				{
					GetComponent<KMBombModule>().HandlePass();
					Debug.LogFormat("[Stock Images #{0}] You pressed TR. You pressed an image with a value of {1}. Module disarmed.", moduleId, dick);
					moduleSolved = true;
				}
					  
				else
				{
					GetComponent<KMBombModule>().HandleStrike();
					Debug.LogFormat("[Stock Images #{0}] You pressed TL. However, there is an image of higher value than the image you selected. Strike.", moduleId);
				}
			}
					
			else if (Button == Buttons[1])
			{
				if (dickier >= dick && dickier >= dickiest)
				{
					GetComponent<KMBombModule>().HandlePass();
					Debug.LogFormat("[Stock Images #{0}] You pressed BL. You pressed an image with a value of {1}. Module disarmed.", moduleId, dickier);
					moduleSolved = true;
				}
						  
				else
				{
					GetComponent<KMBombModule>().HandleStrike();
					Debug.LogFormat("[Stock Images #{0}] You pressed BL. However, there is an image of higher value than the image you selected. Strike.", moduleId);
				}
			}
					
			else if (Button == Buttons[2])
			{
				if (dickiest >= dick && dickiest >= dickier)
				{
					GetComponent<KMBombModule>().HandlePass();
					Debug.LogFormat("[Stock Images #{0}] You pressed BR. You pressed an image with a value of {1}. Module disarmed.", moduleId, dickiest);
					moduleSolved = true;
				}

				else
				{
					GetComponent<KMBombModule>().HandleStrike();
					Debug.LogFormat("[Stock Images #{0}] You pressed BR. However, there is an image of higher value than the image you selected. Strike.", moduleId);
				}
			}
					
			else
			{
				Debug.Log("Fuck");
			}
		}
    }
	
	//twitch plays
    #pragma warning disable 414
    private readonly string TwitchHelpMessage = @"!{0} [TL/BL/BR]| The command presses the respective image in the given position";
    #pragma warning restore 414
	
	IEnumerator ProcessTwitchCommand(string command)
	{
		if (Regex.IsMatch(command, @"^\s*TL\s*$", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant))
		{
			yield return null;
			Buttons[0].OnInteract();
		}
		
		else if (Regex.IsMatch(command, @"^\s*BL\s*$", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant))
		{
			yield return null;
			Buttons[1].OnInteract();
		}
		
		else if (Regex.IsMatch(command, @"^\s*BR\s*$", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant))
		{
			yield return null;
			Buttons[2].OnInteract();
		}
	}
}

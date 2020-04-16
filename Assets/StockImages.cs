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
	
    public GameObject[] Buttons2;
	
    //Logging
    static int moduleIdCounter = 1;
    int moduleId;
    private bool ModuleSolved;
	
    int dick = 0;
    int dickier = 0;
    int dickiest = 0;
    int douchebag = 0;
	
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
		GenerateFuckingNumbers();
		Buttons2[0].GetComponent<MeshRenderer>().material = Pictures[dick];
		Buttons2[1].GetComponent<MeshRenderer>().material = Pictures[dickier];
		Buttons2[2].GetComponent<MeshRenderer>().material = Pictures[dickiest];
    }
	
    void ButtonPress(KMSelectable Button)
	{
		Button.AddInteractionPunch();
		GetComponent<KMAudio>().PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, transform);
		if (!ModuleSolved)
		{
			if (Button == Buttons[0])
			{
				if (dick >= dickier && dick >= dickiest)
				{
					GetComponent<KMBombModule>().HandlePass();
					Debug.LogFormat("[Stock Images #{0}] You pressed an image with a value of {1}. Module disarmed.", moduleId, (dick + 1).ToString());
					ModuleSolved = true;
				}
				
				else
				{
					GetComponent<KMBombModule>().HandleStrike(); Debug.LogFormat("[Stock Images #{0}] There is an image of higher value than the image you selected. Strike.", moduleId);
				}
			}
			
			else if (Button == Buttons[1])
			{
				if (dickier >= dick && dickier >= dickiest)
				{
					GetComponent<KMBombModule>().HandlePass();
					Debug.LogFormat("[Stock Images #{0}] You pressed an image with a value of {1}. Module disarmed.", moduleId, (dickier + 1).ToString());
					ModuleSolved = true;
				}
				
				else
				{
					GetComponent<KMBombModule>().HandleStrike();
					Debug.LogFormat("[Stock Images #{0}] There is an image of higher value than the image you selected. Strike.", moduleId);
				}
			}
			
			else if (Button == Buttons[2])
			{
				if (dickiest >= dick && dickiest >= dickier)
				{
					GetComponent<KMBombModule>().HandlePass();
					Debug.LogFormat("[Stock Images #{0}] You pressed an image with a value of {1}. Module disarmed.", moduleId, (dickiest + 1).ToString());
					ModuleSolved = true;
				}
				
				else
				{
					GetComponent<KMBombModule>().HandleStrike(); Debug.LogFormat("[Stock Images #{0}] There is an image of higher value than the image you selected. Strike.", moduleId);
				}
			}
			
			else
			{
				Debug.Log("Fuck");
			}
		}
    }
	
	void GenerateFuckingNumbers()
	{
		dick = UnityEngine.Random.Range(0,Pictures.Count());
		dickier = UnityEngine.Random.Range(0,Pictures.Count());
		dickiest = UnityEngine.Random.Range(0,Pictures.Count());
		
		if (dick == dickier || dick == dickiest || dickiest == dickier)
		{
			GenerateFuckingNumbers();
		}
		
		if (dick > dickier || dick > dickiest)
		{
			Debug.LogFormat("[Stock Images #{0}] The top-left image contains the highest value with {1}.", moduleId, (dick + 1).ToString());
		}
		
		else if (dickier > dick || dickier > dickiest)
		{
			Debug.LogFormat("[Stock Images #{0}] The bottom-left image contains the highest value with {1}.", moduleId, (dickier + 1).ToString());
		}
		
		else if (dickiest > dickier || dickiest > dick)
		{
			Debug.LogFormat("[Stock Images #{0}] The bottom-right image contains the highest value with {1}.", moduleId, (dickiest + 1).ToString());
		}
	}
	
	//twitch plays
    #pragma warning disable 414
    private readonly string TwitchHelpMessage = @"Select an image in the module by using !{0} select TL/BR/BL.";
    #pragma warning restore 414
	
	IEnumerator ProcessTwitchCommand(string command)
	{
		string[] parameters = command.Split(' ');
		if (Regex.IsMatch(parameters[0], @"^\s*select\s*$", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant))
		{
			yield return null;
			if (Regex.IsMatch(parameters[1], @"^\s*TL\s*$", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant))
			{
				Buttons[0].OnInteract();
			}
			
			else if (Regex.IsMatch(parameters[1], @"^\s*BR\s*$", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant))
			{
				Buttons[1].OnInteract();
			}
			
			else if (Regex.IsMatch(parameters[1], @"^\s*BL\s*$", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant))
			{
				Buttons[2].OnInteract();
			}
			
			else
			{
				yield return "sendtochaterror The command is invalid.";
				yield break;
			}
		}
	}
}

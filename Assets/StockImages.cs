using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StockImages : MonoBehaviour {
   public KMBombInfo Bomb;
   public KMAudio Audio;
   public KMSelectable[] Buttons;
   public Material[] Pictures;
   public GameObject[] Buttons2;

   private readonly List<int> Images = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39 };
   int Answer;

   readonly string[] Pos = { "TL", "BL", "BR" };

   //Logging
   static int moduleIdCounter = 1;
   int moduleId;
   private bool moduleSolved;

   void Awake () {
      moduleId = moduleIdCounter++;
      foreach (KMSelectable Button in Buttons) {
         Button.OnInteract += delegate () { ButtonPress(Button); return false; };
      }
   }

   void Start () {
      Images.Shuffle();
      for (int i = 0; i < 3; i++) {
         Buttons2[i].GetComponent<MeshRenderer>().material = Pictures[Images[i]];
      }
      if (Images[0] > Images[1] && Images[0] > Images[2]) {
         Debug.LogFormat("[Stock Images #{0}] The TL image has the highest value with {1}", moduleId, Images[0] + 1);
         Answer = 0;
      }
      else if (Images[1] > Images[0] && Images[1] > Images[2]) {
         Debug.LogFormat("[Stock Images #{0}] The BL image has the highest value with {1}", moduleId, Images[1] + 1);
         Answer = 1;
      }
      else if (Images[2] > Images[0] && Images[2] > Images[1]) {
         Debug.LogFormat("[Stock Images #{0}] The BR image has the highest value with {1}", moduleId, Images[2] + 1);
         Answer = 2;
      }
   }

   void ButtonPress (KMSelectable Button) {
      Button.AddInteractionPunch();
      GetComponent<KMAudio>().PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, transform);
      if (!moduleSolved) {
         if (Button == Buttons[Answer]) {
            GetComponent<KMBombModule>().HandlePass();
            Debug.LogFormat("[Stock Images #{0}] You pressed the correct image! Module disarmed.", moduleId);
            moduleSolved = true;
         }
         else {
            for (int i = 0; i < 3; i++) {
               if (Button == Buttons[i]) {
                  GetComponent<KMBombModule>().HandleStrike();
                  Debug.LogFormat("[Stock Images #{0}] You pressed image {1}. That is incorrect.", moduleId, Pos[i]);
               }
            }
         }
      }
   }

#pragma warning disable 414
   private readonly string TwitchHelpMessage = @"!{0} [TL/BL/BR]| The command presses the respective image in the given position";
#pragma warning restore 414

   IEnumerator ProcessTwitchCommand (string Command) {
      Command = Command.ToUpperInvariant().Trim();
      if (Command.EqualsAny("TL", "BR", "BL")) {
         yield return null;
         Buttons[Array.IndexOf(Pos, Command)].OnInteract();
      }
   }

   IEnumerator TwitchHandleForcedSolve () {
      yield return ProcessTwitchCommand(Pos[Answer]);
   }
}
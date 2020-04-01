using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using KModkit;

public class StockImages : MonoBehaviour {
    public KMBombInfo Bomb;
    public KMAudio Audio;
    public KMSelectable[] Buttons;
    public Material[] Pictures;
    private List<int> weed = new List<int> {0,1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,26,27,28,29,30,31,32,33,34,35,36,37,38,39};
    public GameObject[] Buttons2;
    static int moduleIdCounter = 1;
    int moduleId;
    private bool moduleSolved;
    int dick = 0;
    int dickier = 0;
    int dickiest = 0;
    int douchebag = 0;
    int retard1 = 0;
       void Awake () {
        moduleId = moduleIdCounter++;
        foreach (KMSelectable Button in Buttons) {
          Button.OnInteract += delegate () { ButtonPress(Button); return false; };
                }
    }
    void Start () {
      dick = UnityEngine.Random.Range(0,weed.Count());
      dickier = UnityEngine.Random.Range(0,weed.Count());
      dickiest = UnityEngine.Random.Range(0,weed.Count());
      Buttons2[0].GetComponent<MeshRenderer>().material = Pictures[dick];
      Buttons2[1].GetComponent<MeshRenderer>().material = Pictures[dickier];
      Buttons2[2].GetComponent<MeshRenderer>().material = Pictures[dickiest];
      }
      void ButtonPress(KMSelectable Button){
        Button.AddInteractionPunch();
        GetComponent<KMAudio>().PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, transform);
        if (Button == Buttons[0]) {
          if (dick >= dickier && dick >= dickiest) {
            GetComponent<KMBombModule>().HandlePass(); Debug.LogFormat("[Stock Images #{0}] You pressed an image with a value of {1}. Module disarmed.", moduleId,weed[dick]);
          }
          else {
            GetComponent<KMBombModule>().HandleStrike(); Debug.LogFormat("[Stock Images #{0}] There is an image of higher value than the image you selected. Strike.", moduleId);
          }
        }
        else if (Button == Buttons[1]) {
          if (dickier >= dick && dickier >= dickiest) {
            GetComponent<KMBombModule>().HandlePass(); Debug.LogFormat("[Stock Images #{0}] You pressed an image with a value of {1}. Module disarmed.", moduleId,weed[dickier]);
          }
          else {
            GetComponent<KMBombModule>().HandleStrike(); Debug.LogFormat("[Stock Images #{0}] There is an image of higher value than the image you selected. Strike.", moduleId);
          }
        }
        else if (Button == Buttons[2]) {
          if (dickiest >= dick && dickiest >= dickier) {
            GetComponent<KMBombModule>().HandlePass(); Debug.LogFormat("[Stock Images #{0}] You pressed an image with a value of {1}. Module disarmed.", moduleId,weed[dickiest]);
          }
          else {
            GetComponent<KMBombModule>().HandleStrike(); Debug.LogFormat("[Stock Images #{0}] There is an image of higher value than the image you selected. Strike.", moduleId);
          }
        }
        else {
          Debug.Log("Fuck");
        }
      }
}

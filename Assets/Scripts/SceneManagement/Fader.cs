using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.SceneManagement
{
    public class Fader : MonoBehaviour
    {
        CanvasGroup fadeCanvasGroup;


        void Start()
        {
            fadeCanvasGroup = GetComponent<CanvasGroup>();
           
            StartCoroutine(FadeInThenOut());
          //  StartCoroutine(FadeOut(3));
        }

        public IEnumerator FadeInThenOut()
        { 
            yield return FadeOut(3);
            yield return FadeIn(1);
        }

        public IEnumerator FadeOut(float fadeTime)
        {
            //  float alphaIncrease = Time.deltaTime / fadeTime;

            while (fadeCanvasGroup.alpha != 1)
            {
                fadeCanvasGroup.alpha += Time.deltaTime / fadeTime;
                yield return null;
            }
        }
        public IEnumerator FadeIn(float fadeTime)
        {
            //  float alphaIncrease = Time.deltaTime / fadeTime;

            while (fadeCanvasGroup.alpha != 0)
            {
                fadeCanvasGroup.alpha -= Time.deltaTime / fadeTime;
                yield return null;
            }
        }
    }
}

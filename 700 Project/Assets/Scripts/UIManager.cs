using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class UIManager : MonoBehaviour
{

    public TMP_Text arrowText;
    public TMP_Text lastClickedText;

    private void Start()
    {
        lastClickedText.alpha = 0; 
    }

    public void SetArrowText(string text, Color color)
    {
        arrowText.text = text;
        arrowText.color = color;
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.W))
        {
            lastClickedText.text = "W";
            StartCoroutine(FadeTextToZeroAlpha(1));
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            lastClickedText.text = "S";
            StartCoroutine(FadeTextToZeroAlpha(1));
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            lastClickedText.text = "A";
            StartCoroutine(FadeTextToZeroAlpha(1));
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            lastClickedText.text = "D";
            StartCoroutine(FadeTextToZeroAlpha(1));
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            lastClickedText.text = "F";
            StartCoroutine(FadeTextToZeroAlpha(1));
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            lastClickedText.text = "E";
            StartCoroutine(FadeTextToZeroAlpha(1));
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Debug.Log("Tab");
            lastClickedText.text = "TAB";
            StartCoroutine(FadeTextToZeroAlpha(1));
        }


        if (Input.GetKeyDown(KeyCode.Space))
        {
            lastClickedText.text = "Space";
            StartCoroutine(FadeTextToZeroAlpha(1));
        }


        if (Input.GetMouseButtonDown(0))
        {
            lastClickedText.text = "Mouse Down";
            StartCoroutine(FadeTextToZeroAlpha(1));
        }

        if (Input.GetMouseButtonUp(0))
        {
            lastClickedText.text = "Mouse Released";
            StartCoroutine(FadeTextToZeroAlpha(1));
        }
    }

    //public IEnumerator TextFade()
    //{
    //    lastClickedText.alpha = 1; 
        
    //}

    public IEnumerator FadeTextToZeroAlpha(float t)
    {
        lastClickedText.color = new Color(lastClickedText.color.r, lastClickedText.color.g, lastClickedText.color.b, 1);
        while (lastClickedText.color.a > 0.0f)
        {
            lastClickedText.color = new Color(lastClickedText.color.r, lastClickedText.color.g, lastClickedText.color.b, lastClickedText.color.a - (Time.deltaTime / t));
            yield return null;
        }
    }
}

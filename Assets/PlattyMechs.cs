using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlattyMechs : MonoBehaviour
{
    public float fourDStart;
    public float fourDEnd;
    private float playerPos4D;
    public Playah4D bigP;
    private Color tmpColor;
    float alpha = 1f;
    Material material;
    //public Gradientgradient;
    void Start()
    {
        /*
        gradient = new Gradient();
        GradientColorKey[] colorKeys = new GradientColorKey[2];
        colorKeys[0].color = posToRgb(fourDStart);
        colorKeys[1].color = posToRgb(fourDEnd);
        colorKeys[0].time = 0f;
        colorKeys[1].time = 1f;

        GradientAlphaKey[] alphaKeys = new GradientAlphaKey[2];
        alphaKeys[0].alpha = 1f;
        alphaKeys[1].alpha = 1f;
        alphaKeys[0].time = 0f;
        alphaKeys[1].time = 1f;

        gradient.colorKeys = colorKeys;
        gradient.alphaKeys = alphaKeys;*/
        Color startCol = posToRgb(fourDStart);
        Color endCol = posToRgb(fourDEnd);
        if (material == null) material = GetComponent<Renderer>().material;
        material.SetColor("_Color1", startCol);
        material.SetColor("_Color2", endCol);
        

    }
    void Update()
    {
        playerPos4D = bigP.playah4dPos;
        if (fourDStart <= playerPos4D && fourDEnd >= playerPos4D)
        {
            GetComponent<Collider>().isTrigger = false;
            alpha= 1f;
        }
        else
        {
            GetComponent<Collider>().isTrigger = true;
            
            if(playerPos4D < fourDStart)
            {
                alpha = 0.8f - ((fourDStart - playerPos4D)) * .012f;
                alpha = Mathf.Clamp(alpha, 0, 1);
            }
            else {
                alpha = 0.8f - (playerPos4D - fourDEnd) * .012f;
                alpha = Mathf.Clamp(alpha, 0, 1);
            }
        }
        material.SetFloat("_Transparency", alpha);
        /*
        tmpColor = posToRgb();
        tmpColor.a = alpha;
        gameObject.GetComponent<MeshRenderer>().material.color = tmpColor;
        Debug.Log(alpha);*/
    }

    public static Color posToRgb(float fourDPos)
    {
        float wl = 550f + (fourDPos * 1.5f);
        float r, g, b, factor;

        if ((wl >= 380) && (wl < 440))
        {
            r = -(float)(wl - 440) / (440f - 380);
            g = 0.0f;
            b = 1.0f;
        }
        else if ((wl >= 440) && (wl < 490))
        {
            r = 0.0f;
            g = (float)(wl - 440) / (490f - 440);
            b = 1.0f;
        }
        else if ((wl >= 490) && (wl < 510))
        {
            r = 0.0f;
            g = 1.0f;
            b = -(float)(wl - 510) / (510f - 490f);
        }
        else if ((wl >= 510) && (wl < 580))
        {
            r = (float)(wl - 510) / (580f - 510f);
            g = 1.0f;
            b = 0.0f;
        }
        else if ((wl >= 580) && (wl < 645))
        {
            r = 1.0f;
            g = -1f * ((float)(wl - 645) / (645f - 580f));
            b = 0.0f;
        }
        else if ((wl >= 645) && (wl < 781))
        {
            r = 1.0f;
            g = 0.0f;
            b = 0.0f;
        }
        else
        {
            r = 0.0f;
            g = 0.0f;
            b = 0.0f;
        }

        if ((wl >= 380) && (wl < 420))
        {
            factor = 0.3f + (float)(0.7 * (wl - 380f) / (420f - 380f));
        }
        else if ((wl >= 420) && (wl < 701))
        {
            factor = 1.0f;
        }
        else if ((wl >= 701) && (wl < 781))
        {
            factor = 0.3f + (float)(0.7 * (780f - wl) / (780f - 700f));
        }
        else
        {
            factor = 0f;
        }


        //Color output = new Color();

        // Don't want 0^x = 1 for x <> 0
        r = r == 0f ? 0f : 1f * Mathf.Pow(r * factor, 0.80f);
        g = g == 0f ? 0f : 1f * Mathf.Pow(g * factor, 0.80f);
        b = b == 0f ? 0f : 1f * Mathf.Pow(b * factor, 0.80f);

        //Debug.Log($"Red: {r}  Green: {g}  Blue: {b}  WaveLength: {wl}");
        return new Color(r, g, b);

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// Creates bar showing how far along the user is in the tutorial video.
/// Red indicates the current %, and the rest of the video remaining is gray.
/// </summary>
// [RequireComponent(typeof(Camera))]
public class AddColorBar : MonoBehaviour
{
    // public Transform startPoint;
    // public Transform endPoint;
    // private float progressBarWidth;

    /*
    Creating variables to find how far along the user is in the video
    */
    private float point;
    private float totTime;
    private float time;
    
    /// <summary>
    /// Create texture and sprite for the video player progress bar.
    /// </summary>
    void Start()
    {
    //   progressBarWidth = Vector3.Distance(startPoint.position, endPoint.position);
        time = 5f;
        totTime = 10f;
        point = time/totTime;
        //  Debug.Log(gameObject.GetComponent<SpriteRenderer> ().sprite.texture.width);
        
        // copies default grey rectangle sprite?
        Texture2D characterTexture2D = CopyTexture2D(gameObject.GetComponent<SpriteRenderer>().sprite.texture,point,Color.red);
        /*
        Get your SpriteRenderer, get the name of the old sprite, 
        create a new sprite, name the sprite the old name, and then update the material.
        If you have multiple sprites, you will want to do this in a loop- which I will post later in another post.
        */
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        string tempName = "color Bar";
        sr.sprite = Sprite.Create (characterTexture2D, sr.sprite.rect, new Vector2(0.5f,0.5f));
        sr.sprite.name = tempName;
        sr.material.mainTexture = characterTexture2D;
        // sr.material.shader = Shader.Find ("Sprites/Transparent Unlit");

    }

    /// <summary>
    /// Makes texture grey everywhere except near x_p (0 - 1) where it will be red.
    /// </summary>
    /// <returns>Resulting texture based on parameters</returns>
    public Texture2D CopyTexture2D(Texture2D copiedTexture, float x_p, Color cl)
    {
        //Create a new Texture2D, which will be the copy.

        Texture2D texture = new Texture2D(copiedTexture.width, copiedTexture.height);
        //Choose your filtermode and wrapmode here.
        texture.filterMode = FilterMode.Point;
        texture.wrapMode = TextureWrapMode.Clamp;

        // int change = int x_p;
        // int change= x_p*texture.width;
        float change = x_p * texture.width;
        Debug.Log(change);
        int y = 0;
        while (y < texture.height)
        {
            int x = 0;
            while (x < texture.width )
            {
                /* Make the pixel red if that part of video is watched */
                if(x > change -3 && x < change +3)
                
                {
                    //This line of code and if statement, turn Green pixels into Red pixels.
                    texture.SetPixel(x, y, cl);
                }
                // else if(x ==100)
                // {
                //     texture.SetPixel(x,y,Color.blue);
                // }

                /* Otherwise, make the pixel Grey */
                else{
                    texture.SetPixel(x,y,Color.grey);
                }
                // else
                // {
                //                //This line of code is REQUIRED. Do NOT delete it. This is what copies the image as it was, without any change.
                // texture.SetPixel(x, y, copiedTexture.GetPixel(x,y));
                // }
                ++x;
            }
            ++y;
        }
        // Name the texture, if you want.
        // texture.name = (Species+Gender+"_SpriteSheet");
 
        /* This finalizes it. If you want to edit it still, do it before you finish with .Apply(). Do NOT expect to edit the image after you have applied. It did NOT work for me to edit it after this function. */
        texture.Apply();
 
        /* Return the variable, so you have it to assign to a permanent variable and so you can use it. */
        return texture;
    }
}

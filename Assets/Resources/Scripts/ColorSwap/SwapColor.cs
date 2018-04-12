using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapColor : MonoBehaviour {

    Texture2D mColorSwapTex;
    Color[] mSpriteColors;
    int HairCounter,EyeCounter,BodyCounter,ShoesCounter,PantsCounter,ShirtCounter;
    SpriteRenderer mSpriteRenderer;

    void Awake()
    {
        mSpriteRenderer = GetComponent<SpriteRenderer>();
        InitColorSwapTex();
        HairCounter = 0; EyeCounter = 0; BodyCounter = 0; ShoesCounter = 0; PantsCounter = 0; ShirtCounter = 0;

       
    }
    public void InitColorSwapTex()
    {
        Texture2D colorSwapTex = new Texture2D(256, 1, TextureFormat.RGBA32, false, false);
        colorSwapTex.filterMode = FilterMode.Point;

        for (int i = 0; i < colorSwapTex.width; ++i)
            colorSwapTex.SetPixel(i, 0, new Color(0.0f, 0.0f, 0.0f, 0.0f));

        colorSwapTex.Apply();

        mSpriteRenderer.material.SetTexture("_SwapTex", colorSwapTex);

        mSpriteColors = new Color[colorSwapTex.width];
        mColorSwapTex = colorSwapTex;
    }
    public void SwapColors(SwapIndex index, Color color)
    {
        mSpriteColors[(int)index] = color;
        mColorSwapTex.SetPixel((int)index, 0, color);
    }

    public void HairColorButtonRight()
    {
     
        if (HairCounter < 10)
        {
            HairCounter++;
        }
        else if (HairCounter == 10)
        {
            HairCounter = 0;
        }

        SwapHairColor(HairCounter);
    }

    public void HairColorButtonLeft()
    {

        if (HairCounter > 0)
        {
            HairCounter--;
        }
        else if (HairCounter == 0)
        {
            HairCounter = 10;
        }

        SwapHairColor(HairCounter);
    }

    public void EyeColorButtonRight()
    {

        if (EyeCounter < 5)
        {
            EyeCounter++;
        }
        else if (EyeCounter == 5)
        {
            EyeCounter = 0;
        }

        SwapEyesColor(EyeCounter);
    }

    public void EyeColorButtonLeft()
    {

        if (EyeCounter > 0)
        {
            EyeCounter--;
        }
        else if (EyeCounter == 0)
        {
            EyeCounter = 5;
        }

        SwapEyesColor(EyeCounter);
    }

    public void SwapHairColor(int Counter)
    {
        
        int R_hair_light = 23;
        int G_hair_light = 29;
        int B_hair_light = 28;
        int R_hair_dark = -16;
        int G_hair_dark = -10;
        int B_hair_dark = -7;
        int R_hair_outline = -36;
        int G_hair_outline = -25;
        int B_hair_outline = -21;
        int R_hair_original = 86;
        int G_hair_original = 49;
        int B_hair_original = 42;

        switch (Counter)
        {
            case 0:
                break;
            case 1:
                R_hair_original = 9;
                G_hair_original = 8;
                B_hair_original = 6;
                break;
            case 2:
                R_hair_original = 44;
                G_hair_original = 34;
                B_hair_original = 43;
                break;
            case 3:
                R_hair_original = 230;
                G_hair_original = 206;
                B_hair_original = 168;
                break;
            case 4:
                R_hair_original = 145;
                G_hair_original = 85;
                B_hair_original = 61;
                break;
            case 5:
                R_hair_original = 181;
                G_hair_original = 82;
                B_hair_original = 57;
                break;
            case 6:
                R_hair_original = 165;
                G_hair_original = 107;
                B_hair_original = 70;
                break;
            case 7:
                R_hair_original = 222;
                G_hair_original = 188;
                B_hair_original = 153;
                break;
            case 8:
                R_hair_original = 59;
                G_hair_original = 48;
                B_hair_original = 36;
                break;
            case 9:
                R_hair_original = 106;
                G_hair_original = 78;
                B_hair_original = 66;
                break;
            case 10:
                R_hair_original = 167;
                G_hair_original = 133;
                B_hair_original = 106;
                break;

        }
        PlayerPrefs.SetInt("HairColor", Counter);
        
        SwapColors(SwapIndex.Hair_main, ColorFromIntRGB(R_hair_original, G_hair_original, B_hair_original));
        SwapColors(SwapIndex.Hair_lighter, ColorFromIntRGB(R_hair_original + R_hair_light, G_hair_original + G_hair_light, B_hair_original + B_hair_light));
        SwapColors(SwapIndex.Hair_darker, ColorFromIntRGB(R_hair_original + R_hair_dark, G_hair_original + G_hair_dark, B_hair_original + B_hair_dark));
        SwapColors(SwapIndex.Hair_outline, ColorFromIntRGB(R_hair_original + R_hair_outline, G_hair_original + G_hair_outline, B_hair_original + B_hair_outline));
        mColorSwapTex.Apply();
    }

    public void SwapEyesColor(int Counter)
    {

        int R_Eye_shadow = -39;
        int G_Eye_shadow = -13;
        int B_Eye_shadow = -8;
        int R_Eye_color = 134;
        int G_Eye_color = 82;
        int B_Eye_color = 73;

        switch (Counter)
        {
            case 0:
                break;
            case 1:
                R_Eye_color = 75;
                G_Eye_color = 105;
                B_Eye_color = 47;
                break;
            case 2:
                R_Eye_color = 11; 
                G_Eye_color = 0;
                B_Eye_color = 217;
                break;
            case 3:
                R_Eye_color = 64;
                G_Eye_color = 132;
                B_Eye_color = 220;
                break;
            case 4:
                R_Eye_color = 63;
                G_Eye_color = 52;
                B_Eye_color = 4;
                break;
            case 5:
                R_Eye_color = 119;
                G_Eye_color = 101;
                B_Eye_color = 54;
                break;
        }
        PlayerPrefs.SetInt("EyesColor", Counter);
        SwapColors(SwapIndex.Eyes_main, ColorFromIntRGB(R_Eye_color, G_Eye_color, B_Eye_color));
        SwapColors(SwapIndex.Eyes_shadow, ColorFromIntRGB(R_Eye_color + R_Eye_shadow, G_Eye_color + G_Eye_shadow, B_Eye_color + B_Eye_shadow));
        mColorSwapTex.Apply();
    }

    public void BodyColorButtonRight()
    {

        if (BodyCounter < 7)
        {
            BodyCounter++;
        }
        else if (BodyCounter == 7)
        {
            BodyCounter = 0;
        }

        SwapBodyColor(BodyCounter);
    }

    public void BodyColorButtonLeft()
    {

        if (BodyCounter > 0)
        {
            BodyCounter--;
        }
        else if (BodyCounter == 0)
        {
            BodyCounter = 7;
        }

        SwapBodyColor(BodyCounter);
    }

    public void SwapBodyColor(int Counter)
    {
        
        int R_body_outline = -70;
        int G_body_outline = -60;
        int B_body_outline = -42;
        int R_body_shadow = -37;
        int G_body_shadow = -35;
        int B_body_shadow = -31;
        int R_body_secondary = -30;
        int G_body_secondary = -24;
        int B_body_secondary = -16;
        int R_body_original = 249;
        int G_body_original = 174;
        int B_body_original = 137;

        switch (Counter)
        {
            case 0:
                break;
            case 1:
                R_body_original = 92;
                G_body_original = 56;
                B_body_original = 54;
                break;
            case 2:
                R_body_original = 255;
                G_body_original = 223;
                B_body_original = 196;
                break;
            case 3:
                R_body_original = 238;
                G_body_original = 206;
                B_body_original = 179;
                break;
            case 4:
                R_body_original = 231;
                G_body_original = 158;
                B_body_original = 109;
                break;
            case 5:
                R_body_original = 240;
                G_body_original = 200;
                B_body_original = 201;
                break;
            case 6:
                R_body_original = 185;
                G_body_original = 124;
                B_body_original = 109;
                break;
            case 7:
                R_body_original = 189;
                G_body_original = 114;
                B_body_original = 60;
                break;
        }
        PlayerPrefs.SetInt("BodyColor", Counter);
        
        SwapColors(SwapIndex.Body_main, ColorFromIntRGB(R_body_original, G_body_original, B_body_original));
        SwapColors(SwapIndex.Body_outline, ColorFromIntRGB(R_body_original + R_body_outline, G_body_original + G_body_outline, B_body_original + B_body_outline));
        SwapColors(SwapIndex.Body_shadow, ColorFromIntRGB(R_body_original + R_body_shadow, G_body_original + G_body_shadow, B_body_original + B_body_shadow));
        SwapColors(SwapIndex.Body_secondary, ColorFromIntRGB(R_body_original + R_body_secondary, G_body_original + G_body_secondary, B_body_original + B_body_secondary));
        mColorSwapTex.Apply();
    }

    public void ShoesColorButtonRight()
    {

        if (ShoesCounter < 19)
        {
            ShoesCounter++;
        }
        else if (ShoesCounter == 19)
        {
            ShoesCounter = 0;
        }

        SwapShoesColor(ShoesCounter);
    }

    public void ShoesColorButtonLeft()
    {

        if (ShoesCounter > 0)
        {
            ShoesCounter--;
        }
        else if (ShoesCounter == 0)
        {
            ShoesCounter = 19;
        }

        SwapShoesColor(ShoesCounter);
    }

    public void SwapShoesColor(int Counter)
    {

        int R_shoes_outline = -26;
        int G_shoes_outline = -23;
        int B_shoes_outline = -23;
        int R_shoes_shadow = 22;
        int G_shoes_shadow = 20;
        int B_shoes_shadow = 20;
        int R_shoes_original = 56;
        int G_shoes_original = 51;
        int B_shoes_original = 51;

        switch (Counter)
        {
            case 0:                         //Black
                break;
            case 1:
                R_shoes_original = 192;     //Silver
                G_shoes_original = 186;
                B_shoes_original = 186;
                break;
            case 2:
                R_shoes_original = 62;     //Light Blue
                G_shoes_original = 154;
                B_shoes_original = 218;
                break;
            case 3:
                R_shoes_original = 253;     //Yellow
                G_shoes_original = 206;
                B_shoes_original = 21;
                break;
            case 4:
                R_shoes_original = 235;     //Kinda white
                G_shoes_original = 235;
                B_shoes_original = 217;
                break;
            case 5:
                R_shoes_original = 47;      //Deep Green
                G_shoes_original = 215;
                B_shoes_original = 34;
                break;
            case 6:
                R_shoes_original = 241;     //Red
                G_shoes_original = 46;
                B_shoes_original = 45;
                break;
            case 7:
                R_shoes_original = 54;      //Blue
                G_shoes_original = 101;
                B_shoes_original = 233;
                break;
            case 8:
                R_shoes_original = 43;      //Dark Blue
                G_shoes_original = 28;
                B_shoes_original = 177;
                break;
            case 9:
                R_shoes_original = 93;      //Green-ish
                G_shoes_original = 169;
                B_shoes_original = 34;
                break;
            case 10:
                R_shoes_original = 233;     //Burned yellow
                G_shoes_original = 169;
                B_shoes_original = 35;
                break;
            case 11:
                R_shoes_original = 177;     //Oranged Brown
                G_shoes_original = 94;
                B_shoes_original = 31;
                break;
            case 12:
                R_shoes_original = 166;     //Violet
                G_shoes_original = 132;
                B_shoes_original = 201;
                break;
            case 13:
                R_shoes_original = 105;     //Blue Grey
                G_shoes_original = 125;
                B_shoes_original = 164;
                break;
            case 14:
                R_shoes_original = 255;     //Orange
                G_shoes_original = 165;
                B_shoes_original = 0;
                break;
            case 15:
                R_shoes_original = 255;     //Coral (Kinda red/orange)
                G_shoes_original = 127;
                B_shoes_original = 80;
                break;
            case 16:
                R_shoes_original = 0;      //Pure Green 
                G_shoes_original = 128;
                B_shoes_original = 0;
                break;
            case 17:
                R_shoes_original = 50;      //Lime Green
                G_shoes_original = 205;
                B_shoes_original = 50;
                break;
            case 18:
                R_shoes_original = 128;     //Maroon
                G_shoes_original = 0;
                B_shoes_original = 0;
                break;
            case 19:
                R_shoes_original = 255;        //Papaya yellow
                G_shoes_original = 239; 
                B_shoes_original = 213;
                break;
        }
        PlayerPrefs.SetInt("ShoesColor", Counter);
        SwapColors(SwapIndex.Shoes_main, ColorFromIntRGB(R_shoes_original, G_shoes_original, B_shoes_original));
        SwapColors(SwapIndex.Shoes_outline, ColorFromIntRGB(R_shoes_original + R_shoes_outline, G_shoes_original + G_shoes_outline, B_shoes_original + B_shoes_outline));
        SwapColors(SwapIndex.Shoes_shadow, ColorFromIntRGB(R_shoes_original + R_shoes_shadow, G_shoes_original + G_shoes_shadow, B_shoes_original + B_shoes_shadow));
        mColorSwapTex.Apply();
    }

    public void PantsColorButtonRight()
    {

        if (PantsCounter < 19)
        {
            PantsCounter++;
        }
        else if (PantsCounter == 19)
        {
            PantsCounter = 0;
        }

        SwapPantsColor(PantsCounter);
    }

    public void PantsColorButtonLeft()
    {

        if (PantsCounter > 0)
        {
            PantsCounter--;
        }
        else if (ShoesCounter == 0)
        {
            PantsCounter = 19;
        }

        SwapPantsColor(PantsCounter);
    }

    public void SwapPantsColor(int Counter)
    {

        int R_pants_outline = -30;
        int G_pants_outline = -35;
        int B_pants_outline = -65;
        int R_pants_shadow = -7;
        int G_pants_shadow = -11;
        int B_pants_shadow = -39;
        int R_pants_original = 67;
        int G_pants_original = 83;
        int B_pants_original = 182;

        switch (Counter)
        {
            case 0:                         //Black
                break;
            
            case 1:
                R_pants_original = 253;     //Yellow
                G_pants_original = 206;
                B_pants_original = 21;
                break;
            
            case 2:
                R_pants_original = 47;      //Deep Green
                G_pants_original = 215;
                B_pants_original = 34;
                break;
            case 3:
                R_pants_original = 43;      //Dark Blue
                G_pants_original = 28;
                B_pants_original = 177;
                break;
            case 4:
                R_pants_original = 241;     //Red
                G_pants_original = 46;
                B_pants_original = 45;
                break;
            case 5:
                R_pants_original = 54;      //Blue
                G_pants_original = 101;
                B_pants_original = 233;
                break;
            
            case 6:
                R_pants_original = 93;      //Green-ish
                G_pants_original = 169;
                B_pants_original = 34;
                break;
            case 7:
                R_pants_original = 233;     //Burned yellow
                G_pants_original = 169;
                B_pants_original = 35;
                break;
            case 8:
                R_pants_original = 192;     //Silver
                G_pants_original = 186;
                B_pants_original = 186;
                break;
            case 9:
                R_pants_original = 62;     //Light Blue
                G_pants_original = 154;
                B_pants_original = 218;
                break;
            case 10:
                R_pants_original = 177;     //Oranged Brown
                G_pants_original = 94;
                B_pants_original = 31;
                break;
            case 11:
                R_pants_original = 166;     //Violet
                G_pants_original = 132;
                B_pants_original = 201;
                break;
            case 12:
                R_pants_original = 105;     //Blue Grey
                G_pants_original = 125;
                B_pants_original = 164;
                break;
            case 13:
                R_pants_original = 50;      //Lime Green
                G_pants_original = 205;
                B_pants_original = 50;
                break;
            case 14:
                R_pants_original = 255;     //Orange
                G_pants_original = 165;
                B_pants_original = 0;
                break;
            case 15:
                R_pants_original = 235;     //Kinda white
                G_pants_original = 235;
                B_pants_original = 217;
                break;
            case 16:
                R_pants_original = 255;     //Coral (Kinda red/orange)
                G_pants_original = 127;
                B_pants_original = 80;
                break;
            case 17:
                R_pants_original = 0;      //Pure Green 
                G_pants_original = 128;
                B_pants_original = 0;
                break;
            case 18:
                R_pants_original = 128;     //Maroon
                G_pants_original = 0;
                B_pants_original = 0;
                break;
            case 19:
                R_pants_original = 255;        //Papaya yellow
                G_pants_original = 239;
                B_pants_original = 213;
                break;
        }
        PlayerPrefs.SetInt("PantsColor", Counter);
        SwapColors(SwapIndex.Pants_main, ColorFromIntRGB(R_pants_original, G_pants_original, B_pants_original));
        SwapColors(SwapIndex.Pants_outline, ColorFromIntRGB(R_pants_original + R_pants_outline, G_pants_original + G_pants_outline, B_pants_original + B_pants_outline));
        SwapColors(SwapIndex.Pants_shadow, ColorFromIntRGB(R_pants_original + R_pants_shadow, G_pants_original + G_pants_shadow, B_pants_original + B_pants_shadow));
        mColorSwapTex.Apply();
    }

    public void ShirtColorButtonRight()
    {

        if (ShirtCounter < 19)
        {
            ShirtCounter++;
        }
        else if (ShirtCounter == 19)
        {
            ShirtCounter = 0;
        }

        SwapShirtColor(ShirtCounter);
    }

    public void ShirtColorButtonLeft()
    {

        if (ShirtCounter > 0)
        {
            ShirtCounter--;
        }
        else if (ShirtCounter == 0)
        {
            ShirtCounter = 19;
        }

        SwapShirtColor(ShirtCounter);
    }

    public void SwapShirtColor(int Counter)
    {

        int R_shirt_outline = -52;
        int G_shirt_outline = -51;
        int B_shirt_outline = -51;
        int R_shirt_shadow = -30;
        int G_shirt_shadow = -20;
        int B_shirt_shadow = -20;
        int R_shirt_original = 173;
        int G_shirt_original = 55;
        int B_shirt_original = 55;

        switch (Counter)
        {
            case 0:                         //Black
                break;
            case 1:
                R_shirt_original = 47;      //Deep Green
                G_shirt_original = 215;
                B_shirt_original = 34;
                break;
            case 2:
                R_shirt_original = 241;     //Red
                G_shirt_original = 46;
                B_shirt_original = 45;
                break;
            case 3:
                R_shirt_original = 233;     //Burned yellow
                G_shirt_original = 169;
                B_shirt_original = 35;
                break;
            case 4:
                R_shirt_original = 128;     //Maroon
                G_shirt_original = 0;
                B_shirt_original = 0;
                break;
            case 5:
                R_shirt_original = 192;     //Silver
                G_shirt_original = 186;
                B_shirt_original = 186;
                break;
            case 6:
                R_shirt_original = 43;      //Dark Blue
                G_shirt_original = 28;
                B_shirt_original = 177;
                break;
            case 7:
                R_shirt_original = 62;     //Light Blue
                G_shirt_original = 154;
                B_shirt_original = 218;
                break;
            case 8:
                R_shirt_original = 177;     //Oranged Brown
                G_shirt_original = 94;
                B_shirt_original = 31;
                break;
            case 9:
                R_shirt_original = 235;     //Kinda white
                G_shirt_original = 235;
                B_shirt_original = 217;
                break;
            case 10:
                R_shirt_original = 166;     //Violet
                G_shirt_original = 132;
                B_shirt_original = 201;
                break;
            case 11:
                R_shirt_original = 253;     //Yellow
                G_shirt_original = 206;
                B_shirt_original = 21;
                break;
            case 12:
                R_shirt_original = 105;     //Blue Grey
                G_shirt_original = 125;
                B_shirt_original = 164;
                break;
            case 13:
                R_shirt_original = 50;      //Lime Green
                G_shirt_original = 205;
                B_shirt_original = 50;
                break;
            case 14:
                R_shirt_original = 54;      //Blue
                G_shirt_original = 101;
                B_shirt_original = 233;
                break;
            case 15:
                R_shirt_original = 255;     //Orange
                G_shirt_original = 165;
                B_shirt_original = 0;
                break;
            case 16:
                R_shirt_original = 255;        //Papaya yellow
                G_shirt_original = 239;
                B_shirt_original = 213;
                break;
            case 17:
                R_shirt_original = 255;     //Coral (Kinda red/orange)
                G_shirt_original = 127;
                B_shirt_original = 80;
                break;
            case 18:
                R_shirt_original = 93;      //Green-ish
                G_shirt_original = 169;
                B_shirt_original = 34;
                break;
            case 19:
                R_shirt_original = 0;      //Pure Green 
                G_shirt_original = 128;
                B_shirt_original = 0;
                break;
            
        }
        PlayerPrefs.SetInt("ShirtColor", Counter);
        SwapColors(SwapIndex.Shirt_main, ColorFromIntRGB(R_shirt_original, G_shirt_original, B_shirt_original));
        SwapColors(SwapIndex.Shirt_outline, ColorFromIntRGB(R_shirt_original + R_shirt_outline, G_shirt_original + G_shirt_outline, B_shirt_original + B_shirt_outline));
        SwapColors(SwapIndex.Shirt_shadow, ColorFromIntRGB(R_shirt_original + R_shirt_shadow, G_shirt_original + G_shirt_shadow, B_shirt_original + B_shirt_shadow));
        mColorSwapTex.Apply();
    }

    public static Color ColorFromInt(int c, float alpha = 1.0f)
    {
        int r = (c >> 16) & 0x000000FF;
        int g = (c >> 8) & 0x000000FF;
        int b = c & 0x000000FF;

        Color ret = ColorFromIntRGB(r, g, b);
        ret.a = alpha;

        return ret;
    }

    public static Color ColorFromIntRGB(int r, int g, int b)
    {
        return new Color((float)r / 255.0f, (float)g / 255.0f, (float)b / 255.0f, 1.0f);
    }
}

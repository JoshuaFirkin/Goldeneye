﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoUI : MonoBehaviour {

    public CanvasGroup ammoGroup;
    public Text ammoClip;
    public Text ammoReserve;
    public Image Bullet;


    public void UpdateClip(int clip)
    {
        ammoClip.text = clip.ToString();
    }


    public void UpdateInventory(int inv)
    {
        ammoReserve.text = inv.ToString();
    }


    public void UpdateClipAndInventory(int clip, int inv)
    {
        ammoClip.text = clip.ToString();
        ammoReserve.text = inv.ToString();
        ammoGroup.alpha = 1;
    }


    public void UpdateImage(Sprite image)
    {
        Bullet.sprite = image;
        ammoGroup.alpha = 1;
    }
}
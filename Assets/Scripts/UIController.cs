﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// need to do this for UI!!!
using UnityEngine.UI;
public class UIController : MonoBehaviour
{
    public static UIController instance;

    public Slider healthSlider;
    public Text healthText;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

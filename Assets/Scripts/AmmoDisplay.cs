using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AmmoDisplay : MonoBehaviour
{
    // Start is called before the first frame update
    public BallControls ballControls;
    public TMP_Text ammoText;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (ballControls != null) {
            ammoText.text = "Ammo: " + ballControls.currentAmmo.ToString();
        }
        
    }
}

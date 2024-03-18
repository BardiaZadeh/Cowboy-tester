using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using TMPro; 
public class Score : MonoBehaviour
{
    public Transform player;
    public TextMeshProUGUI scoreText;
    private float speed;
    private Vector3 previousPosition;
    // Start is called before the first frame update
    void Start()
    {
        previousPosition = player.position;
    }

    // Update is called once per frame
    void Update()
    {   
        //float traveled = Vector3.Distance(player.position, previousPosition);
        //speed = traveled/ Time.deltaTime;
        //previousPosition = player.position;
        float score = (player.position.z + 84);
        scoreText.text = score.ToString("0");
    }
}

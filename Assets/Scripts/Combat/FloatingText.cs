using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FloatingText : MonoBehaviour
{
    public float totalTime;
    private float timer;
    private TextMeshProUGUI text;

    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        timer = totalTime;
    }

    private void Update()
    {
        text.alpha -= Time.deltaTime / totalTime;
        transform.localPosition += new Vector3(0, 0.3f, 0);

        timer -= Time.deltaTime;

        if (timer <= 0)
            Destroy(gameObject);
    }
}

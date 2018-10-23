using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnIndicator : MonoBehaviour
{
    private bool shouldGoUp = true;

    private void Update()
    {
        transform.position += (shouldGoUp ? new Vector3(0, 0.5f, 0) : new Vector3(0, -0.5f, 0)) * Time.deltaTime;

        if (transform.localPosition.y <= 0.45)
            shouldGoUp = true;
        else if (transform.localPosition.y >= .60f)
            shouldGoUp = false;
    }
}

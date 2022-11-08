using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using static UnityEngine.GraphicsBuffer;

public class CircularPointer : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 MousePosition = Input.mousePosition;
        Vector3 PlayerPosition = Camera.main.WorldToScreenPoint(transform.position);
        MousePosition.x = MousePosition.x - PlayerPosition.x;
        MousePosition.y = MousePosition.y - PlayerPosition.y;

        // Calculates the angle between the Circle Pointer and the Mouse
        float Angle = Mathf.Atan2(MousePosition.y, MousePosition.x) * Mathf.Rad2Deg;

        // Applies the rotation
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, Angle));
    }
}

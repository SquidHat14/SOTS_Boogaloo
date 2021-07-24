using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCamera2D : MonoBehaviour
{
    //public float delay = 0.15f;
    [Range(1f,5f)]
    public float speed = 2f;
    public Transform target;

    [Range(0f,3f)]
    public float yOffset = 0;

    public float minX, maxX, minY, maxY;
    public Vector3 bottomRightOfLevel;
    public Vector3 topLeftLevel;

    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();

        var vertExtent = Camera.main.GetComponent<Camera>().orthographicSize;    
        var horzExtent = vertExtent * Screen.width / Screen.height;

        print("vertExtent " + vertExtent);
        print("horzExtent " + horzExtent);

        minX = topLeftLevel.x + horzExtent;
        maxX = bottomRightOfLevel.x - horzExtent;
        minY =  bottomRightOfLevel.y + vertExtent;
        maxY = topLeftLevel.y - vertExtent;
       }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(target)
        {
            Vector3 newPosition = target.position;
            newPosition.y += yOffset;
            newPosition.z = -10;

            Vector3 temp;
            temp = Vector3.Slerp(transform.position, newPosition, speed * Time.deltaTime);
            
            transform.position = temp;
            
            /*
            transform.position = new Vector3(
            Mathf.Clamp(temp.x, minX, maxX),
            Mathf.Clamp(temp.y, minY, maxY),
            temp.z);
            */
        }
    }

    void OnDrawGizmosSelected()
    {
        // Draw a semitransparent blue cube at the transforms position
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        
        Gizmos.DrawCube(bottomRightOfLevel, new Vector3(1, 1, 1));

        Gizmos.color = new Color(0, 0, 1, 0.5f);
        Gizmos.DrawCube(topLeftLevel, new Vector3(1, 1, 1));
    }
}

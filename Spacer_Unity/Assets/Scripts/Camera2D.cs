using UnityEngine;
using System.Collections;

public class Camera2D : MonoBehaviour {

    float orthoSize;
    public Transform target; //hier die zu beobachtende person
    public int zOffset; // abstand Kamera
    public int minimumHeight = 0;

    float currentY;
    Vector3 position;
    // Use this for initialization
    void Start()
    {
        orthoSize = GetComponent<Camera>().orthographicSize;
    }
	// Update is called once per frame
	void LateUpdate () {
        position = target.position;
        position.z -= zOffset;
        
        // bis hier Zentral
        currentY = target.position.y;

        if (currentY > minimumHeight + orthoSize - 1)
        {
            position.y = currentY - orthoSize + 1;
        }
        else
        {
            position.y = minimumHeight;
        }
        transform.position = position; //Anwendendung
    }
}

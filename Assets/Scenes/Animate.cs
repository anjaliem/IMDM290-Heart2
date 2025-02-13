using UnityEngine;

public class Animate : MonoBehaviour
{
    GameObject[] spheres;
    static int numSpheres = 100;
    Vector3[] initPos;
    float[] initSat;
    int time = 0;

    void Start()
    {
        spheres = new GameObject[numSpheres];
        initPos = new Vector3[numSpheres];
        initSat = new float[numSpheres];

        // Let there be spheres..
        for (int i = 0; i < numSpheres; i++){
            spheres[i] = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            float t = (((float)i / numSpheres) * 6f) - 3f;
            float xPos = Mathf.Sqrt(2) * Mathf.Pow(Mathf.Sin(t), 3f);
            float yPos = 1.5f - Mathf.Pow(Mathf.Cos(t), 3f) - Mathf.Pow(Mathf.Cos(t), 2f) + (2 * Mathf.Cos(t));
            initPos[i] = new Vector3(xPos, yPos, -5f);
            spheres[i].transform.position = initPos[i];
            spheres[i].transform.localScale += new Vector3(-0.8f, -0.8f, -0.8f);

            Renderer sphereRenderer = spheres[i].GetComponent<Renderer>();
            float saturation = Mathf.Abs((float)i - (numSpheres/2f)) / (numSpheres/2f); // Saturation ranges from 0 - 1
            if (saturation < 0.2f){
                saturation = 0.2f;
            }
            initSat[i] = saturation;
            Color color = Color.HSVToRGB(1f, saturation, 1f); // Red hue and full brightness
            sphereRenderer.material.color = color;
        }

    }

    // Update is called once per frame
    void Update()
    {
        // "BEATING HEART" EFFECT
        //              |----growing---|---shrinking--|----still-----|
        // timePeriod : 0    1    2    3    4    5    6    7    8    9
        // growScale  : 1   1.1  1.2  1.3  1.2  1.1   1    1    1    1
        // colorFade  : +0 +.1f +.2f +.3f +.2f +.1f  +0   +0   +0   +0

        time++;
        int timePeriod = time % 10;

        float growScale = 1f;
        float satScale = 1f;

        if (timePeriod == 1 || timePeriod == 5){
            growScale = 1.1f;
            satScale = 1.5f;
        } else if (timePeriod == 2 || timePeriod == 4){
            growScale = 1.2f;
            satScale = 2f;
        } else if (timePeriod == 3){
            growScale = 1.3f;
            satScale = 2.5f;
        }

        for (int i = 0; i < numSpheres; i++){
            // change position
            float xPos = initPos[i].x * growScale;
            float yPos = initPos[i].y * growScale;
            spheres[i].transform.position = new Vector3(xPos, yPos, -5f);
            
            // change color
            Renderer sphereRenderer = spheres[i].GetComponent<Renderer>();
            float newSaturation = initSat[i] * satScale;
            Color color = Color.HSVToRGB(1f, newSaturation, 1f);
            sphereRenderer.material.color = color;
        }
    }
}

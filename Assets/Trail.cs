using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Trail : MonoBehaviour
{
    [SerializeField] TrailRenderer trailPrefab;
    private TrailRenderer Swipe;

    public RenderTexture rTex;

    void Start()
    {
        Swipe = GetComponent<TrailRenderer>(); // gets the trail renderer so that it can be used
    }

    // Update is called once per frame
    void Update()
    {

        if (((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved) || Input.GetMouseButton(0))) // If there is more than 0 touches registered and if they are moving or if the mouse is clicked then code executes. This ensures that this code will work for both desktop and mobile devices.
        {
            Plane objPlane = new Plane(Camera.main.transform.forward * -1, this.transform.position); // Create plane if screen is touched/clicked. Plane is in position of the touched/clicked object facing the camera.

            Ray mRay = Camera.main.ScreenPointToRay(Input.mousePosition); // Create ray in position of the mouse position (this code works for touch too).
            float rayDistance; //Plane gets hit at this float.
            if (objPlane.Raycast(mRay, out rayDistance)) //Here the float is used to hit the plane with a raycast at the float that was instantiated above.
                if (Swipe == null) // checks if the line exists
                {
                    Swipe = Instantiate(trailPrefab, mRay.GetPoint(rayDistance), Quaternion.identity); // if it doesn't exist, it instantiates a prefab
                }

                else
                {
                    Swipe.transform.position = mRay.GetPoint(rayDistance); //Update the position of the line to that position.
                }
        }
        else
            if (Swipe != null)
        {
            Swipe = null;
        }
    }


    public void Save() // Starts when the button is pressed.
    {
        StartCoroutine(cSave()); // Coroutine gets called.
    }

    private IEnumerator cSave()
    {
        yield return new WaitForEndOfFrame(); //Waits until the end of the frame after all cameras and UI is rendered, just before displaying the frame on screen.

        RenderTexture.active = rTex;

        var texture2D = new Texture2D(rTex.width, rTex.height); //Copy the render texture information...
        texture2D.ReadPixels(new Rect(0, 0, rTex.width, rTex.height), 0, 0); // ...to texture2d
        texture2D.Apply();

        var data = texture2D.EncodeToPNG(); //Output to png file format.

        File.WriteAllBytes(Application.dataPath + "/savedImage.png", data); // Save the image file to the path. Date and time added.
    }

    // Button UI:

    public void redSwitch() // Starts when the button is pressed.
    {
        trailPrefab.startColor = Color.red; // The trail renderer works in a way that it by default enables a gradient. If we want a solid color, we need to change both the start and...
        trailPrefab.endColor = Color.red; //... the end of the color.
        //StartCoroutine(rSwitch());
    }

    public void greenSwitch() // Starts when the button is pressed.
    {
        trailPrefab.startColor = Color.green;
        trailPrefab.endColor = Color.green;
    }

    public void blueSwitch() // Starts when the button is pressed.
    {
        trailPrefab.startColor = Color.blue;
        trailPrefab.endColor = Color.blue;
    }

    public void yellowSwitch() // Starts when the button is pressed.
    {
        trailPrefab.startColor = Color.yellow;
        trailPrefab.endColor = Color.yellow;
    }
    public void blackSwitch() // Starts when the button is pressed.
    {
        trailPrefab.startColor = Color.black;
        trailPrefab.endColor = Color.black;
    }

    public void thinSize() // Make width thin.
    {
        trailPrefab.startWidth = 0.1f;
        trailPrefab.endWidth = 0.1f;
    }
    public void midSize() // Make width medium.
    {
        trailPrefab.startWidth = 0.2f;
        trailPrefab.endWidth = 0.2f;
    }
    public void thickSize() // Make width thick.
    {
        trailPrefab.startWidth = 0.5f;
        trailPrefab.endWidth = 0.5f;
    }

}

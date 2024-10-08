using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CameraShakeColor : MonoBehaviour
{
    public GameObject flash;
    public float duration;
    public float magnitude;

    private bool didShake;
    private Image flashImage;
    private Vector3 originalPos;

    private void Start()
    {
        didShake = false;
        flashImage = flash.GetComponent<Image>();
        originalPos = transform.localPosition; // Cache the original position at start
    }

    private void Update()
    {
        // Start shaking camera if player is dead and hasn't shaken yet
        if (!Player_color.isAlive && !didShake)
        {
            StartCoroutine(Shake(duration, magnitude));
            didShake = true;
        }
    }

    // Coroutine to shake the camera
    private IEnumerator Shake(float duration, float magnitude)
    {
        float elapsed = 0f;
        flash.SetActive(true);

        // Loop to shake camera every frame
        while (elapsed < duration)
        {
            // Handle screen flash
            FlashScreen();

            // Shake camera by adjusting position randomly
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;
            transform.localPosition = new Vector3(originalPos.x + x, originalPos.y + y, originalPos.z);

            // Decrease magnitude over time, ensuring it doesn't go negative
            magnitude = Mathf.Max(magnitude - (1.5f * Time.deltaTime), 0f);
            elapsed += Time.deltaTime;

            yield return null;
        }

        // Reset camera position after shaking
        transform.localPosition = originalPos;
    }

    // Helper method to handle the flash screen effect
    private void FlashScreen()
    {
        Color color = flashImage.color;
        if (color.a > 0)
        {
            color.a = Mathf.Max(color.a - (2f * Time.deltaTime), 0f); // Smooth alpha reduction
            flashImage.color = color;
        }
        else
        {
            flash.SetActive(false);
        }
    }
}

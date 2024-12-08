using UnityEngine;

public class ClickToOpen : MonoBehaviour
{
    private Vector3 originalScale;
    private bool isOpen = false;

    void Start()
    {
        originalScale = transform.localScale;
    }

    void OnMouseDown()
    {
        if (!isOpen)
        {
            transform.localScale = originalScale * 1.5f;
            isOpen = true;

            Debug.Log("XYS");
            StartCoroutine(ShowTextOnScreen("XYS"));
        }
    }

    private System.Collections.IEnumerator ShowTextOnScreen(string text)
    {
        // Create the text object
        GameObject textObject = new GameObject("TextDisplay");
        var textMesh = textObject.AddComponent<TextMesh>();

        // Set text properties
        textMesh.text = text;
        textMesh.color = Color.white; // Ensure visible text color
        textMesh.fontSize = 64; // Larger font size for visibility
        textMesh.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
        textObject.transform.localScale = Vector3.one * 0.2f; // Adjust for size

        // Position the text in front of the camera
        Camera mainCamera = Camera.main;
        if (mainCamera != null)
        {
            textObject.transform.position = mainCamera.transform.position + mainCamera.transform.forward * 2f; // Place it 2 units in front
            textObject.transform.position += Vector3.up * 0.5f; // Slightly above the center
        }
        else
        {
            Debug.LogWarning("Main Camera not found!");
        }

        // Rotate to face the camera
        textObject.transform.rotation = Quaternion.LookRotation(textObject.transform.position - mainCamera.transform.position);

        // Apply a proper material to avoid the black rectangle issue
        var renderer = textObject.GetComponent<MeshRenderer>();
        renderer.material = new Material(Shader.Find("Standard"));
        renderer.material.color = Color.white; // Match the text color
        renderer.material.SetOverrideTag("RenderType", "Transparent");
        renderer.material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        renderer.material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        renderer.material.SetInt("_ZWrite", 0);
        renderer.material.DisableKeyword("_ALPHATEST_ON");
        renderer.material.EnableKeyword("_ALPHABLEND_ON");
        renderer.material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        renderer.material.renderQueue = 3000;

        // Wait for 2 seconds before destroying the text object
        yield return new WaitForSeconds(2f);

        Destroy(textObject);
    }


}

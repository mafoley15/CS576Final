using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using SpeechLib;

public class GeminiAPIClient : MonoBehaviour
{
    [SerializeField]
    private string apiKey = "REPLACE ME";

    private string apiUrl = "https://generativelanguage.googleapis.com/v1beta/models/gemini-1.5-flash-latest:generateContent";

    private SpVoice voice;

    void Start()
    {
        // Initialize the voice
        voice = new SpVoice();

        // Start the API request coroutine
        StartCoroutine(PostRequest());
    }

    private IEnumerator PostRequest()
    {
        // JSON template for the API request body
        string json_template = "{{\"contents\":[{{\"parts\":[{{\"text\":\"{0}\"}}]}}]}}";
        string prompt = "You are a professor of a high school chemistry lab. You are currently conducting a Flame Test and Atomic Spectra Experiment for your class. A student comes to you for help. They have mesasured out the right amoount of chemicals, and is unsure what to do next. Instruct them on how to complete the next step of the lab. You should only output the direct next step. Your response should be short (1 or 2 sentences long). Your voice should be scholarly like a professor, but you should include praise or criticisms based on the player's pace. They are currently behind on the lab compared to other students.";
        string json_body = string.Format(json_template, prompt);

        byte[] body_raw = System.Text.Encoding.UTF8.GetBytes(json_body);

        // Create the UnityWebRequest
        UnityWebRequest request = new UnityWebRequest(apiUrl + "?key=" + apiKey, "POST");
        request.uploadHandler = new UploadHandlerRaw(body_raw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        // Send the request
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            // Parse the API response
            var response = JsonConvert.DeserializeObject<dynamic>(request.downloadHandler.text);

            // Extract the response text
            string responseText = response.candidates[0].content.parts[0].text;

            // Log the response for debugging
            Debug.Log("API Response: " + responseText);

            // Use text-to-speech to voice the response
            SpeakResponse(responseText);
        }
        else
        {
            Debug.Log("Error: " + request.error);
        }
    }

    private void SpeakResponse(string responseText)
    {
        if (voice != null)
        {
            // Speak the response text asynchronously
            voice.Speak(responseText, SpeechVoiceSpeakFlags.SVSFlagsAsync | SpeechVoiceSpeakFlags.SVSFPurgeBeforeSpeak);
        }
        else
        {
            Debug.LogWarning("Voice is not initialized!");
        }
    }
}

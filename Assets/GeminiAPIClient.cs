using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;

public class GeminiAPIClient : MonoBehaviour
{
    [SerializeField]
    private string apiKey = "REPLACE ME";

    private string apiUrl = "https://generativelanguage.googleapis.com/v1beta/models/gemini-1.5-flash-latest:generateContent";

    void Start()
    {
        StartCoroutine(PostRequest());
    }

    private IEnumerator PostRequest()
    {
        // this is modeled off of the gemini api quickstart
//        string json_template = "{\"contents\":[{\"parts\":[{\"text\":\"{0}\"}]}]}";
        string json_template = "{{\"contents\":[{{\"parts\":[{{\"text\":\"{0}\"}}]}}]}}";
        string prompt = "You are a professor of a high school chemistry lab. You are currently conducting a Flame Test and Atomic Spectra Experiment for your class. A student comes to you for help. They have mesasured out the right amoount of chemicals, and is unsure what to do next. Instruct them on how to complete the next step of the lab. You should only output the direct next step. Your response should be short (1 or 2 sentences long). Your voice should be scholarly like a professor, but you should include praise or criticisms based on the player's pace. They are currently behind on the lab compared to other students.'";
        string json_body = string.Format(json_template, prompt);

        byte[] body_raw = System.Text.Encoding.UTF8.GetBytes(json_body);

        UnityWebRequest request = new UnityWebRequest(apiUrl + "?key=" + apiKey, "POST");
        request.uploadHandler = new UploadHandlerRaw(body_raw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            // this requires modern .net since doing this with static types is awful
            var response = JsonConvert.DeserializeObject<dynamic>(request.downloadHandler.text);
            Debug.Log(response.candidates[0].content.parts[0].text);
        }
        else
        {
            Debug.Log("Error: " + request.error);
        }
    }
}


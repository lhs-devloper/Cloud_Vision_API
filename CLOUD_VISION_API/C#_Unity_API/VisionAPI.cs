using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using SimpleJSON;

public class CloudVision : MonoBehaviour {
    public string url = "https://vision.googleapis.com/v1/images:annotate?key=";
    public string apiKey = "YOUR_API_KEY";
    public FeatureType featureType = FeatureType.TEXT_DETECTION;
    //GameObject Panel
    public GameObject respanel;
    //Text numbs two(responseText, responseArray)
    public Text responseText, responseArray;

    [System.Serializable]
    public class AnnotateImageRequests {
        public List<AnnotateImageRequest> requests;
    }
    [System.Serializable]
    public class AnnotateImageRequest {
        public Image image;
        public List<Feature> features;
    }
    [System.Serializable]
	public class Image {
		public string content;
	}
    [System.Serializable]
    public class Feature {
        public string type;
        public int maxResults;
    }
    [System.Serializable]
    public enum FeatureType {
        TYPE_UNSPECIFIED,
        FACE_DETECTION,
        LANDMARK_DETECTION,
        LOGO_DETECTION,
        LABEL_DETECTION,
        TEXT_DETECTION,
        DOCUMENT_TEXT_DETECTION,
        SAFE_SEARCH_DETECTION,
        IMAGE_PROPERTIES,
        CROP_HINTS,
        WEB_DETECTION,
        PRODUCT_SEARCH,
        OBJECT_LOCALIZATION
    }
    void Start(){
        // Initialization
        /*
        What do you need in the header?
        curl -X POST \
        -H "Authorization: Bearer "$(gcloud auth application-default print-access-token) \ // if you have the ApiKey, Authorization is not required
        -H "Content-Type: application/json; charset=utf-8" \
        -d @request.json \
        "https://vision.googleapis.com/v1/images:annotate"

        How to use?
        1. Post Link initialize
        Link: "https://vision.gogleapis.com/v1/${REST API}?key=${Your ApiKey}"
        2. Add Header Type
        - Dictionary<string, string> → key: "Content-Type", value: "application/json"
        3. Add JSON Data(Below Check Reference Link)
        - string jsonData = JsonUtility.ToJson(${data});
        */
        headers = new Dictionary<string, string>();
        headers.Add("Content-Type", "application/json; charset=UTF-8");
        // Check ApiKey States
        if (apiKey == null || apiKey == ""){
            Debug.LogError("NO_ApiKey Please Enter Your API KEY")
        }
    }

    // Courtine
    private IEnumerator Capture(){
        while (true) {
            if (this.apiKey == null){
                yield return null;
            }
            // yield return new WaitForSeconds()
            // image to base64
            byte[] jpg = texture2D.EncodeToJPG(); //Take a picture and JPG FILE
            string base64 = System.Convert.ToBase64String(jpg);
            // POST Content
            AnnotateImageRequests requests = new AnnotateImageRequests();
            requests.requests = new List<AnnotateImageRequest>();

            AnnotateImageRequest request = new AnnotateImageRequest();
            request.image = new Image();
            request.image.content = base64; //base64Encoded
            request.features = new List<Feature>();
            Feature feature = new Feature();
            feature.type = this.featureType.ToString();
            feature.maxResults = this.maxResults;
            request.features.Add(feature);
            requests.requests.Add(request);
            
            // application/json
            // Data Structure List → JSON
            // USE TEXT_DETECTION
            string jsonData = JsonUtility.ToJson(requests, false);
            if (jsonData != string.Empty) {
                string url = this.url + this.apiKey;
                byte postData = System.Text.Encoding.Default.GetBytes(jsonData);
                using(WWW www = new WWW(url, postData, headers)) {
                    yield return www;
                    if (string.IsNullOrEmpty(www.error)){
                        string responses = www.text.Replace("\n", "").Replace(" ", "");
                        JSONNode res = JSON.parse(responses);
                        // Response (FeatureType Check Please)
                        string fullText = ""; // Reference Link: https://cloud.google.com/vision/docs/how-to and Please check the response content(JSON)
                        // Response 200 OK → fullText in Response Content 
                        if (fullText != ""){
                            Debug.Log("OCR Response" + fullText);
                            rsePanel.SetActive(true);
                            responseText.text = fullText.Replace("\\n", " ");
                            fullText = fullText.Replace("\\n", ";");
                            string[] texts = fullText.Split(';');
                            reponseArray.text = "";
                            for (int i=0; i<texts.Length; i++){
                                reponseArray.text += texts[i];
                                if (i != texts.Length - 1){
                                    responseArray.text += ", ";
                                }
                            }
                        } 
                    } else{
                        Debug.Log("Error: "+ www.error);
                    }
                }
            }
        }
    }
}
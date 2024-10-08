using UnityEngine;

public class ScoreSubmission : MonoBehaviour {

    static AndroidJavaObject scoreObject;
    static AndroidJavaObject activityContext;
    static AndroidJavaClass plugin;

    void Start() {
        //initialize android related objects
        if (scoreObject == null) {
            using (AndroidJavaClass activityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer")) {
                activityContext = activityClass.GetStatic<AndroidJavaObject>("currentActivity");
            }
        }
        plugin = new AndroidJavaClass("retrogeek46.unitymodule.Score");
    }
    //Function to create a toast and Intent with current score
    public static void SendScore(int score) {
        if(plugin == null) Debug.Log("plugin called null");
        if (plugin != null) {
            Debug.Log("plugin called");
            scoreObject = plugin.CallStatic<AndroidJavaObject>("instance");
            scoreObject.Call("setContext", activityContext);
            activityContext.Call("runOnUiThread", new AndroidJavaRunnable(() => {
                scoreObject.Call("sendIntent", score.ToString());
                //scoreObject.Call("showToast", "You scored " + score + " points");
            }));
        }
    }
}

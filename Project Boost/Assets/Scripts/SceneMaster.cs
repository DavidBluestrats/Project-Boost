using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMaster : MonoBehaviour
{
    public static SceneMaster active;
    public Vector3 currentCheckpoint;
    public string activeScene;
    private string levelID;
    // Start is called before the first frame update
    void Awake()
    {
        Scene scn = SceneManager.GetActiveScene();
        levelID = scn.name;
        activeScene = levelID;
        if (SceneMaster.active != null)
        {
            Debug.Log("Current Scene: " + levelID + ", Current Stored Scene: " + SceneMaster.active.levelID);
            //If the current level is different than the one of the current active scene (Example: Current level=1 and current scene level = 0)
            if (SceneMaster.active.levelID != scn.name)
            {
                Destroy(SceneMaster.active.gameObject);
                SceneMaster.active = null;
                Debug.Log("Active SceneMaster not for this level, deleted");
            }//If the current scene level is the same as the current active scene
            else
            {
                Debug.Log("SceneMaster already exists for this level, deleting");
                //Already exists, so delete.
                Destroy(gameObject);
            }
        }
        if(SceneMaster.active == null)
        {
            Debug.Log("No active SceneMaster found, making this active");
            SceneMaster.active = this;
            DontDestroyOnLoad(this);
        }
    }
}

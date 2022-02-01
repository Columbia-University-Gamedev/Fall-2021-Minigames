using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ArcadeController : MonoBehaviour
{
    public List<Minigame> minigames;
    public GameObject base_game_obj;
    public float spacing;
    public float object_scale;

    public GameObject main_camera;

    private List<GameObject> minigame_objects = new List<GameObject>();
    private int selected_game;

    private Vector3 camera_target;
    // Start is called before the first frame update
    void Start()
    {
        SetupGames();
    }

    void SetupGames () 
    {
        Vector3 last_location = transform.position;
        foreach (Minigame m in minigames) 
        {
            GameObject new_obj = Instantiate(base_game_obj,
                last_location,Quaternion.identity,this.transform);
            new_obj.transform.localScale = new_obj.transform.localScale * object_scale;
            new_obj.GetComponent<SpriteRenderer>().sprite = m.arcade_art;
            minigame_objects.Add(new_obj);

            last_location += spacing * Vector3.right;
        }
        camera_target = minigame_objects[0].transform.position;
        camera_target.z = -10;
    }

    void OnSelect()
    {
        SceneManager.LoadScene(minigames[selected_game].scene_name);
    }

    void OnMovement(InputValue res) 
    {
        float val = res.Get<float>();
        int indexChange = (val > 0) ? 1 : -1;

        selected_game = Mathf.Clamp(selected_game+indexChange,0,minigames.Count-1);

        camera_target = minigame_objects[selected_game].transform.position;
        camera_target.z = -10;
    }

    public float lerpFactor;

    void Update () {
        main_camera.transform.position = Vector3.Lerp(main_camera.transform.position, 
            camera_target, lerpFactor * Time.deltaTime);
    }
}

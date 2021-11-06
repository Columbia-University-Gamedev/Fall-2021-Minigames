using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Theme_Changer : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private SpriteRenderer smallSubplatform;
    [SerializeField] private SpriteRenderer mediumSubplatform;
    [SerializeField] private SpriteRenderer largeSubplatform;
    [SerializeField] private List<Sprite> smallCloudSprites;
    [SerializeField] private List<Sprite> mediumCloudSprites;
    [SerializeField] private List<Sprite> largeCloudSprites;
    [SerializeField] private List<Sprite> smallCakeSprites;
    [SerializeField] private List<Sprite> mediumCakeSprites;
    [SerializeField] private List<Sprite> largeCakeSprites;
    [SerializeField] private List<Sprite> smallSpaceSprites;
    [SerializeField] private List<Sprite> mediumSpaceSprites;
    [SerializeField] private List<Sprite> largeSpaceSprites;
    private const static List<List<Sprite>> smallSprites = {smallCloudSprites, smallCakeSprites, smallSpaceSprites};
    private const static List<List<Sprite>> mediumSprites = {mediumCloudSprites, mediumCakeSprites, mediumSpaceSprites};
    private const static List<List<Sprite>> largeSprites = {largeCloudSprites, largeCakeSprites, largeSpaceSprites};

    private const int CLOUD= 0;
    private const int CAKE = 1;
    private const int SPACE = 2;

    private int currentTheme;
    void Start()
    {
        smallSubplatform = gameObject.GetComponent<SpriteRenderer>();
        mediumSubplatform = gameObject.GetComponent<SpriteRenderer>();
        largeSubplatform = gameObject.GetComponent<SpriteRenderer>();
        currentTheme = CLOUD;
    }

    void colorPlatform(int themeIdx)
    {
        smallSubplatform.sprite = smallSprites[themeIdx][Random.Range(0,smallSprites[themeIdx].Count)];
        mediumSubplatform.sprite = mediumSprites[themeIdx][Random.Range(0,mediumSprites[themeIdx].Count)];
        largeSubplatform.sprite = largeSprites[themeIdx][Random.Range(0,largeSprites[themeIdx].Count)];
    }

    void changeTheme(int themeIdx)
    {
        currentTheme = themeIdx;
        colorPlatform(currentTheme);
    }

    // Update is called once per frame
    void Update()
    {
        colorPlatform(currentTheme);
    }
}

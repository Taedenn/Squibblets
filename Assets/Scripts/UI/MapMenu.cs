using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapMenu : MonoBehaviour
{
    public RawImage ri;
    public RawImage bg;
    [SerializeField] GameObject map;
    [SerializeField] GameObject back;
    private Button mapButton;
    private Button backButton;

    // Start is called before the first frame update
    void Start()
    {
        mapButton = map.GetComponent<Button>();
        bg.enabled = false;
        ri.enabled = false;
        back.SetActive(false);
        mapButton.onClick.AddListener(ShowMap);
    }

    // Update is called once per frame
    public void ShowMap()
    {
        map.SetActive(false);
        back.SetActive(true);
        backButton = back.GetComponent<Button>();
        backButton.onClick.AddListener(DisableMap);
        bg.enabled = true;
        ri.enabled = true;
    }
    public void DisableMap()
    {
        bg.enabled = false;
        ri.enabled = false;
        back.SetActive(false);
        map.SetActive(true);
    }
}

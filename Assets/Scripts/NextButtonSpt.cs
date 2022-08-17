using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class NextButtonSpt : MonoBehaviour
{
    private Image image;
    private Manage.LoadAPI loadapi;
    private Manage.LoadMethod loadmethod;

    
    // Start is called before the first frame update
    void Start()
    {
        Manage.imageNum = 1;
        Button next_btn = this.GetComponent<Button>();
        next_btn.onClick.AddListener(OnClick);
        image = GameObject.Find("Image").GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnClick()
    {
        Manage.imageNum = Manage.imageNum % 5;
        int num = Manage.imageNum + 1;
        // Debug.Log(Manage.imageNum);
        loadapi = image.GetComponent<Manage>().LoadApi;
        loadmethod = image.GetComponent<Manage>().loadMethod;
        Sprite sprite;
        string name = "image_" + num;
        
        if (loadapi == Manage.LoadAPI.AssetDataBase)
        {
            string address = "Assets/Images/" + name+".jpg";
            if (loadmethod == Manage.LoadMethod.Synchronous)
            {
                Texture2D tex = AssetDatabase.LoadAssetAtPath<Texture2D>(address);
                sprite= Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), Vector2.zero);
                image.sprite = sprite;
            }
            else
            {
                Debug.Log("assetdatabase没有异步加载,自动切换同步加载");
                Texture2D tex = AssetDatabase.LoadAssetAtPath<Texture2D>(address);
                sprite= Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), Vector2.zero);
                image.sprite = sprite;
            }
        }
        else if (loadapi == Manage.LoadAPI.Resource)
        {
            if (loadmethod == Manage.LoadMethod.Synchronous)//同步加载
            {
                sprite = Resources.Load(name,typeof(Sprite)) as Sprite;
                image.sprite = sprite;
            }
            else//异步
            {
                sprite = Resources.LoadAsync(name,typeof(Sprite)).asset as Sprite;
                image.sprite = sprite;
            }
        }
        else if (loadapi == Manage.LoadAPI.UnityWebRequest)
        {
            if (loadmethod == Manage.LoadMethod.Synchronous)
            {
                Debug.Log("UnityWebRequest没有同步加载，自动切换异步加载");
            }

            string path = Application.streamingAssetsPath + "/AB";
            // StartCoroutine(LoadABByWebquest());
        }
        else
        {
            //WWW
            
        }
        // Debug.Log(Manage.imageNum);
        Manage.imageNum++;

        IEnumerable LoadABByWebquest(string path)
        {
            UnityWebRequest request = UnityWebRequestAssetBundle.GetAssetBundle(path);
            yield return request.SendWebRequest();
            AssetBundle ab = DownloadHandlerAssetBundle.GetContent(request);
        }
        
    }
    
    
}

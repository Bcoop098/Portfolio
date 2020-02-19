/*Author : Ramy Daghstani
 * 
 * Class: EGD-380P-101: Advanced Seminar in Game Programming 
 * Date: Fall 2019 (2019FA)
 * Purpose:
 * Toolbox used to keep the managers and prevent multiple instances and copies.
 * Makes things easier and neat.
 * 
 * Slightly edited by Chun Tao Lin
 */

using UnityEngine;
using System.Collections.Generic;

public class Toolbox : MonoBehaviour
{

    private Dictionary<string, UnityEngine.Component> tools = new Dictionary<string, UnityEngine.Component>();

    public static Toolbox Instance
    {
        get { return GetInstance(); }
        set { _instance = value; }
    }

    private static Toolbox _instance;

    private static Toolbox GetInstance()
    {
        if (Toolbox._instance == null)
        {
            var go = new GameObject("Toolbox");
            DontDestroyOnLoad(go);
            Toolbox._instance = go.AddComponent<Toolbox>();
        }
        return Toolbox._instance;
    }

    void Awake()
    {
        //Params toolboxParams = Resources.Load<Params>("ToolboxParams");
        this.AddObject<SceneManagement>("SceneManagement");
        this.AddObject<PixelLord>("PixelLord");
        this.AddObject<PlayerData>("PlayerData");
        this.AddObject<ProgressManager>("ProgressManager");
        this.AddObject<AnalysisManager>("AnalysisManager");
        this.AddObject<AudioManager>("AudioManager");
    }

    public ObjType GetObject<ObjType>(string objName) where ObjType : UnityEngine.Component
    {
        return tools[objName] as ObjType;
    }

    public ObjType AddObject<ObjType>(string objName) where ObjType : UnityEngine.Component
    {
        var tool = new GameObject(objName);
        tool.transform.SetParent(this.transform);
        ObjType obj = tool.AddComponent<ObjType>();
        this.tools.Add(objName, obj);
        return obj;
    }

    public void RemoveObject<ObjType>(string objName) where ObjType : UnityEngine.Component
    {
        this.tools.Remove(objName);
        Destroy(gameObject.transform.Find(objName).gameObject);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OdinSerializer;
using System.IO;
using System;

public class TestOdinScript : SerializedMonoBehaviour {

    public string testSerializer1;

    public Dictionary<string, int> dictStrInt;

    public MyData testMydata;

	// Use this for initialization
	void Start () {

        Example.Save(new MyData("test 1 2 3", 1.23f, new Dictionary<string, float>(){ { "a", 0.1f }, { "b", 0.2f} }), "test.txt");

        Invoke("LoadData", 2f);
    }

    void LoadData()
    {
        testMydata = Example.Load("test.txt");
        Debug.Log(testMydata.testStr + "|" + testMydata.testFloat);
        Debug.Log(testMydata.testStrFloat.Keys + "|" + testMydata.testStrFloat["b"]);
    }

	
	// Update is called once per frame
	void Update () {
		
	}

}

public static class Example
{
    public static void Save(MyData data, string filePath)
    {
        byte[] bytes = SerializationUtility.SerializeValueWeak(data, DataFormat.JSON);
        File.WriteAllBytes(filePath, bytes);
    }

    public static MyData Load(string filePath)
    {
        byte[] bytes = File.ReadAllBytes(filePath);
        return SerializationUtility.DeserializeValue<MyData>(bytes, DataFormat.JSON);
    }
}

[Serializable]
public class MyData
{
    public string testStr;
    public float testFloat;
    public Dictionary<string, float> testStrFloat;

    public MyData(string str, float flt, Dictionary<string, float> test)
    {
        testStr = str;
        testFloat = flt;
        testStrFloat = test;
    }
}
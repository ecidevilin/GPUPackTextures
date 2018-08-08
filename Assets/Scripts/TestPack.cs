using System;
using System.Collections.Generic;
using System.Reflection;
using Chaos;
using UnityEngine;
using UnityEngine.Profiling;

public class TestReflection
{
    private List<int> _testList = new List<int>(); 
    private List<int> TestList {
        get { return _testList;}
    }

    public void PrintList()
    {
        foreach (var i in _testList)
        {
            Debug.Log(i);
        }
    }
}

public class TestPack : MonoBehaviour
{

    public Texture2D[] textures;
    public Texture2D Packed;
    public Texture Compute;

    // Use this for initialization
    private int frame = 10;
	void Start ()
	{
	    TestReflection tr = new TestReflection();
	    Type type = typeof (TestReflection);
	    PropertyInfo pi = type.GetProperty("TestList", BindingFlags.Instance | BindingFlags.NonPublic);
        Debug.Log(pi.CanWrite);
	    List<int> list = pi.GetValue(tr) as List<int>;
        list.Add(10);
        tr.PrintList(); 
        PackTextures.LoadComputeShader();
    }
	
	// Update is called once per frame
	void Update () {
	    if (frame-- == 0)
        {
            Profiler.BeginSample("Pack");
            Packed = new Texture2D(0, 0);
            Rect[] rects = Packed.PackTextures(textures, 0);
            Profiler.EndSample();
            Profiler.BeginSample("Compute");
            rects = PackTextures.PackTexturesCompute(out Compute, textures);
            Profiler.EndSample();
            for (int i = 0, imax = rects.Length; i < imax; i++)
            {
                Debug.Log(rects[i]);
            }
        }
	}
}

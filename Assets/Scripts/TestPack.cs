using UnityEngine;
using UnityEngine.Profiling;

public class TestPack : MonoBehaviour
{

    public Texture2D[] textures;
    public Texture2D Packed;
    public Texture Compute;

    // Use this for initialization
    private int frame = 10;
	void Start ()
	{
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

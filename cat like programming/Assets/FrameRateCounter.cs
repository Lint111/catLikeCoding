using TMPro;
using UnityEngine;

public class FrameRateCounter : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI display;

    public enum DisplayMode { FPS,MS}

    [SerializeField]
    DisplayMode displayMode = DisplayMode.FPS;

    [SerializeField, Range(0.1f, 2f)]
    float sampleDuration = 1f;


    int frames = 0;
    float duration = 0, bestDuration = float.MaxValue, worstDuration = 0;

    private void Update()
    {
        float frameDuraation = Time.unscaledDeltaTime;
        frames++;
        duration += frameDuraation;

        if(frameDuraation < bestDuration)
            bestDuration = frameDuraation;
        if(frameDuraation > worstDuration)
            worstDuration = frameDuraation;

        if(duration > sampleDuration )
        {
            if(displayMode == DisplayMode.FPS)
            {
                display.SetText("FPS\n{0:0}\n{1:0}\n{2:0}",
                                1f / bestDuration,
                                frames / duration,                                
                                1f / worstDuration);
            }
            else
            {
                display.SetText("MS\n{0:1}\n{1:1}\n{2:1}",
                                1000f * bestDuration,
                                1000f * duration / frames,
                                1000f * worstDuration);
            }

            frames = 0;
            duration = 0f;
            bestDuration = float.MaxValue;
            worstDuration = 0;
        }
    }

}

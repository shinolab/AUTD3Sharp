using System;
using System.Linq;
using System.Net;
using AUTD3Sharp;
using AUTD3Sharp.Gain;
using AUTD3Sharp.Modulation;
using UnityEngine;
using static AUTD3Sharp.Units;

#nullable enable

public class MultiAUTD3Controller : MonoBehaviour
{
    private Controller? _autd = null;
    public GameObject? Target = null;
    private Vector3 _oldPosition;

    void Awake()
    {
        try
        {
            _autd = Controller.Open(
                    FindObjectsByType<AUTD3Device>(FindObjectsSortMode.InstanceID).Select(obj => new AUTD3(pos: obj.transform.position, rot: obj.transform.rotation)),
                    new AUTD3Sharp.Link.Simulator(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8080))
                );
        }
        catch (Exception ex)
        {
            UnityEngine.Debug.LogError(ex);
            UnityEngine.Debug.LogError("Before running this sample, install autd3-server (https://github.com/shinolab/autd3-server) and run simulator with unity option.");
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_STANDALONE
            UnityEngine.Application.Quit();
#endif
        }

        _autd!.Send(new Sine(freq: 150 * Hz, option: new SineOption()));

        if (Target == null) return;
        _autd!.Send(new Focus(pos: Target.transform.position, option: new FocusOption()));
        _oldPosition = Target.transform.position;
    }

    private void Update()
    {
        if (Target == null || Target.transform.position == _oldPosition) return;
        if (_autd == null) return;
        _autd.Send(new Focus(pos: Target.transform.position, option: new FocusOption()));
        _oldPosition = Target.transform.position;
    }

    private void OnApplicationQuit()
    {
        _autd?.Dispose();
    }
}

#nullable restore

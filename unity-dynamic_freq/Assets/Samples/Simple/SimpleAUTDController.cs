using System;
using System.Net;
using AUTD3Sharp;
using AUTD3Sharp.Link;
using UnityEngine;
using static AUTD3Sharp.Units;

#if UNITY_2020_2_OR_NEWER
#nullable enable
#endif

public class SimpleAUTDController : MonoBehaviour
{
    private Controller<AUTD3Sharp.Link.Simulator>? _autd = null;
    public GameObject? Target = null;

    private Vector3 _oldPosition;

    private static bool _isPlaying = true;

    private void Awake()
    {
        try
        {
            _autd = Controller.Builder(new[] { new AUTD3(gameObject.transform.position).WithRotation(gameObject.transform.rotation) })
                .Open(AUTD3Sharp.Link.Simulator.Builder(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8080)));
        }
        catch (Exception)
        {

            UnityEngine.Debug.LogError("Before running this sample, install autd3-server (https://github.com/shinolab/autd3-server) and run simulator with unity option.");
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_STANDALONE
            UnityEngine.Application.Quit();
#endif
        }

        _autd!.Send(new AUTD3Sharp.Modulation.Sine(150 * Hz)); // 150 Hz

        if (Target == null) return;
        _autd!.Send(new AUTD3Sharp.Gain.Focus(Target.transform.position));
        _oldPosition = Target.transform.position;
    }

    private void Update()
    {
#if UNITY_EDITOR
        if (!_isPlaying)
        {
            UnityEditor.EditorApplication.isPlaying = false;
            return;
        }
#endif
        if (_autd == null) return;

        if (Target == null || Target.transform.position == _oldPosition) return;
        _autd.Send(new AUTD3Sharp.Gain.Focus(Target.transform.position));
        _oldPosition = Target.transform.position;
    }

    private void OnApplicationQuit()
    {
        _autd?.Dispose();
    }
}

#if UNITY_2020_2_OR_NEWER
#nullable restore
#endif

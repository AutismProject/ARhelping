using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;

public class ClampText : MonoBehaviour,ITrackableEventHandler{

    public Text nameLabel;
    private TrackableBehaviour mTrackableBehaviour;
    public Vector3 vectorForUp;
    private AudioSource AudioS;

    private bool mShowText = false;

    void Start()
    {
        AudioS = GetComponent<AudioSource>();
        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        if (mTrackableBehaviour)
        {
            mTrackableBehaviour.RegisterTrackableEventHandler(this);
        }
    }
    void Update()
    {
        FollowTextByTarget();
        CheckOnEnable();
    }
    
    public void FollowTextByTarget()
    {
        Vector3 namePos = Camera.main.WorldToScreenPoint(this.transform.position);
        nameLabel.transform.position = namePos+vectorForUp;
    }
    public void OnTrackableStateChanged(TrackableBehaviour.Status previousStatus,TrackableBehaviour.Status newStatus)
    {
        if(newStatus == TrackableBehaviour.Status.DETECTED ||
           newStatus == TrackableBehaviour.Status.TRACKED)
        {
            mShowText = true;
            AudioS.Play();
        }
        else
        {
            mShowText = false;
            AudioS.Stop();
        }
    }
    public void CheckOnEnable()
    {
        if (mShowText)
        {
            nameLabel.gameObject.SetActive(true);
        }
        else nameLabel.gameObject.SetActive(false);
    }
}

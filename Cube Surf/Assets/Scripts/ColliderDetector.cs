using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class ObjectRefEvent : UnityEvent<GameObject> { }
public class ColliderDetector : MonoBehaviour
{
    public bool isActive = true;
    public string targetTag = "Player";

    public bool isActiveTriggerEnter = true;
    public ObjectRefEvent OnTriggerEnterEvet;
    public bool isActiveTriggerStay = true;
    public ObjectRefEvent OnTriggerStayEvet;
    public bool isActiveTriggerExit = true;
    public ObjectRefEvent OnTriggerExitEvet;

    GameObject otherObject;

    public void SetActive(bool _state)
    {
        isActive = _state;
    }
    public void SetActiveTriggerEnter(bool _state)
    {
        isActiveTriggerEnter = _state;
    }
    public void SetActiveTriggerStay(bool _state)
    {
        isActiveTriggerStay = _state;
    }
    public void SetActiveTriggerExit(bool _state)
    {
        isActiveTriggerExit = _state;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(!isActive) return;
        if(!isActiveTriggerEnter) return;
        if (targetTag != "" && other.tag != targetTag) return;
        otherObject = other.gameObject;
        if(OnTriggerEnterEvet.GetPersistentEventCount() > 0)
        {
            OnTriggerEnterEvet.Invoke(other.gameObject);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if(!isActive) return;
        if(!isActiveTriggerStay) return;
        if(targetTag != "" && other.tag != targetTag) return;

        if(OnTriggerStayEvet.GetPersistentEventCount() > 0)
        {
            OnTriggerStayEvet.Invoke(other.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(!isActive) return;
        if(!isActiveTriggerExit) return;
        if(targetTag != "" && other.tag != targetTag) return;
        otherObject = null;
        if (OnTriggerExitEvet.GetPersistentEventCount() > 0)
        {
            OnTriggerExitEvet.Invoke(other.gameObject);
        }
    }
    public void DestroyOther()
    {
        if (otherObject)
        {
            Destroy(otherObject);
        }
    }
}

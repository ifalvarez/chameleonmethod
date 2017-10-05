using UnityEngine;
using UnityEngine.UI;

public class GuidingArroww : MonoBehaviour
{
    public Transform target;
    Vector3 lookPos;

    private void Update()
    {
        lookPos = target.position - transform.position;
        lookPos.y = 0;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(lookPos), 0.5f);
    }
}

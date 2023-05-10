using UnityEngine;

public class Swipe : MonoBehaviour
{

    [SerializeField] private float swipeSpeed = 2f;

    private float? lastMousePoint = null;

    public DataManager dataManager;

    private void Update()
    {
        if (this.tag == "Friendly" && dataManager.isFinal != true) { Turn(); }
    }
    private void Turn()
    {
        if (Input.GetMouseButtonDown(0))
        {
            lastMousePoint = Input.mousePosition.x;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            lastMousePoint = null;
        }   
        if (lastMousePoint != null)
        {
            float difference = Input.mousePosition.x - lastMousePoint.Value;
            Vector3 target = new Vector3(transform.position.x + difference * Time.fixedDeltaTime, transform.position.y, transform.position.z);
            target.x = Mathf.Clamp(target.x, -20.9f, 21.2f);
            transform.position = Vector3.Lerp(transform.position, target, swipeSpeed * Time.fixedDeltaTime);
            lastMousePoint = Input.mousePosition.x;
        }
    }
}

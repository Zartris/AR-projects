using System.Collections;
using UnityEngine;
using Vuforia;

public class ShootRaysFalcon : MonoBehaviour
{
    private Vector3 _cannon1Local;
    private Vector3 _cannon2Local;
    private Vector3 _cannonTopLocal;
    private Ray _ray1;
    private Ray _ray2;
    private Ray _ray3;


    private Material material;
    private float _ray1Length = 10f;
    private float _ray2Length = 10f;
    private float _ray3Length = 10f;

    private Quaternion _topCannonRotation;

    public float Yaw;
    public float Pitch;

    // Use this for initialization
    void Start()
    {
        Yaw = Yaw == null ? 0 : Yaw;
        Pitch = Pitch == null ? 0 : Pitch;
        // localy find the position of the canons and apply scaling
        _cannon1Local = Vector3.Scale(new Vector3(-0.06f, 0, 0.49f), GetComponent<Transform>().localScale);
        _cannon2Local = Vector3.Scale(new Vector3(0.06f, 0, 0.49f), GetComponent<Transform>().localScale);
        
        _cannonTopLocal = Vector3.Scale(new Vector3(0, 0, -0.093f), GetComponent<Transform>().localScale);
        // we dont want to scale the 2.5 cm 
        _cannonTopLocal.y = 0.025f;
    }

    // Update is called once per frame
    void Update()
    {
        
        if(_ray1Length + _ray2Length != 0)
        {
            StartCoroutine(RemoveLines());   
            
        }
    }

    void OnGUI()
    {
        // if space is pressed do this:
        if (Event.current.Equals(Event.KeyboardEvent(KeyCode.Space.ToString())))
        {
            // calculate the position of the cannon in the world
            var cannon1 = this.transform.position + this.transform.rotation * _cannon1Local;
            var cannon2 = this.transform.position + this.transform.rotation * _cannon2Local;
            var topCannon = this.transform.position + this.transform.rotation * _cannonTopLocal;
//            Debug.DrawRay(cannon1, this.transform.forward, Color.red, 2);
//            Debug.DrawRay(cannon2, this.transform.forward, Color.red, 2);
            RaycastHit hitCannon1;
            RaycastHit hitCannon2;
            RaycastHit hitCannonTop;
            _ray1 = new Ray(cannon1, transform.forward);
            _ray2 = new Ray(cannon2, transform.forward);
            _ray3 = new Ray(topCannon, Quaternion.Euler(Pitch, Yaw, 0) * transform.forward);
            var raycast1 = Physics.Raycast(_ray1, out hitCannon1);
            var raycast2 = Physics.Raycast(_ray2, out hitCannon2);
            var raycast3 = Physics.Raycast(_ray3, out hitCannonTop);

            _ray1Length = 10f;
            _ray2Length = 10f;
            _ray3Length = 10f;

            if (raycast1)
            {
                Hit(hitCannon1);
                _ray1Length = hitCannon1.distance;
            }

            if (raycast2)
            {
                Hit(hitCannon2);
                _ray2Length = hitCannon2.distance;
            }
            if (raycast3)
            {
                Hit(hitCannonTop);
                _ray3Length = hitCannonTop.distance;
            }

            Debug.Log("Ray1: " + raycast1 + " Ray2: " + raycast2);
            Debug.Log("Space key is pressed. pew pew");
        }
    }

    private void Hit(RaycastHit hit)
    {
        // Loads explosion
        Instantiate(Resources.Load("my_explosion"), hit.point, Quaternion.identity);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        // First get localtion, then rotate and last transition it.
        Gizmos.DrawSphere(this.transform.position + this.transform.rotation * _cannon1Local, (float) 0.002);
        Gizmos.DrawSphere(this.transform.position + this.transform.rotation * _cannon2Local, (float) 0.002);
        Gizmos.DrawSphere(this.transform.position + this.transform.rotation * _cannonTopLocal, (float) 0.002);
        
    }

    private void OnRenderObject()
    {
        if(material == null)
        {
            material = new Material(Shader.Find("Hidden/Internal-Colored"));
        }

        material.SetPass(0);
        
        GL.Begin(GL.LINES);
        GL.Color(Color.red);
        GL.Vertex(_ray1.origin);
        GL.Vertex(_ray1.origin + _ray1.direction * _ray1Length);
        GL.Vertex(_ray2.origin);
        GL.Vertex(_ray2.origin + _ray2.direction * _ray2Length); 
        GL.Vertex(_ray3.origin);
        GL.Vertex(_ray3.origin + _ray3.direction * _ray3Length);
        GL.End();
        
    }

    private IEnumerator RemoveLines()
    {
        yield return new WaitForSeconds( 0.1f );
        _ray1Length = 0;
        _ray2Length = 0;
        _ray3Length = 0;
    }
}
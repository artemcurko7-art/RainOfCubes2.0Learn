using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(MeshRenderer))]
public abstract class Prefab : MonoBehaviour
{
    protected Rigidbody Rigidbody { get; private set; }
    protected MeshRenderer MeshRenderer { get; private set; }
    protected Color Color { get; private set; }

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
        MeshRenderer = GetComponent<MeshRenderer>();
        Color = MeshRenderer.material.color;
    }

    public virtual void Initialize(Vector3 position)
    {
        transform.position = position;
    }

    public virtual void ResetSettings()
    {
        transform.rotation = Quaternion.identity;
        Rigidbody.angularVelocity = Vector3.zero;
        Rigidbody.linearVelocity = Vector3.zero;
        MeshRenderer.material.color = Color;
    }
}

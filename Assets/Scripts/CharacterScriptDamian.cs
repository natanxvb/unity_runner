using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CharacterScriptDamian : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float speed;
    [SerializeField] private float shift;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            MovePlayer(-1); // left
        }

        if(Input.GetKeyDown(KeyCode.D))
        {
            MovePlayer(1);  // right
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(transform.position + transform.forward * (speed * Time.deltaTime));
    }

    private void MovePlayer(float direction)
    {
        transform.Translate(direction * shift, 0f, 0f);
    }
}
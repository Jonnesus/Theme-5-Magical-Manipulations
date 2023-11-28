using UnityEngine;

public class EnemyMoveAnim : MonoBehaviour
{
    [SerializeField] private Animator m_animator = null;
    [SerializeField] private Rigidbody m_rigidBody = null;

    private void Awake()
    {
        if (!m_animator) { gameObject.GetComponent<Animator>(); }
        if (!m_rigidBody) { gameObject.GetComponent<Animator>(); }
    }

    void FixedUpdate()
    {
        m_animator.SetFloat("MoveSpeed", 2);
    }
}
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    public InputAction MoveAction;

    public float walkSpeed = 1.0f;
    public float turnSpeed = 20f;
    public UIDocument uiDocument;

    private List<string> m_OwnedKeys = new List<string>();
    private VisualElement redKey;
    private VisualElement goldenKey;

    Rigidbody m_Rigidbody;
    Vector3 m_Movement;
    Quaternion m_Rotation = Quaternion.identity;
    Animator m_Animator;
    AudioSource m_AudioSource;

    void Start ()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        m_Animator = GetComponent<Animator>();
        m_AudioSource = GetComponent<AudioSource>();
        MoveAction.Enable();
        redKey = uiDocument.rootVisualElement.Q<VisualElement>("RedKey");
        goldenKey = uiDocument.rootVisualElement.Q<VisualElement>("GoldenKey");
    }

    void FixedUpdate()
    {
        var pos = MoveAction.ReadValue<Vector2>();

        float horizontal = pos.x;
        float vertical = pos.y;

        m_Movement.Set(horizontal, 0f, vertical);
        m_Movement.Normalize();

        IdleWalkAnimationSwitch(horizontal, vertical);

        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, m_Movement, turnSpeed * Time.deltaTime, 0f);
        m_Rotation = Quaternion.LookRotation(desiredForward);

        m_Rigidbody.MoveRotation(m_Rotation);
        m_Rigidbody.MovePosition(m_Rigidbody.position + m_Movement * walkSpeed * Time.deltaTime);
    }
    
    private void IdleWalkAnimationSwitch(float horizontal, float vertical)
    {
        bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0f);
        bool hasVerticalInput = !Mathf.Approximately(vertical, 0f);
        bool isWalking = hasHorizontalInput || hasVerticalInput;
        m_Animator.SetBool("IsWalking", isWalking);
        
        if (isWalking)
        {
            if (!m_AudioSource.isPlaying)
            {
                m_AudioSource.Play();
            }
        }
        else
        {
            m_AudioSource.Stop();
        }
    }

    public void AddKey(string keyName)
    {
        m_OwnedKeys.Add(keyName);
        switch (keyName)
        {
            case "RedKey":
                redKey.style.display = DisplayStyle.Flex;
                break;
            case "GoldenKey":
                goldenKey.style.display = DisplayStyle.Flex;
                break;
        }
    }

    public bool OwnKey(string keyName)
    {
        switch (keyName)
        {
            case "RedKey":
                redKey.style.display = DisplayStyle.None;
                break;
            case "GoldenKey":
                goldenKey.style.display = DisplayStyle.None;
                break;
        }
        return m_OwnedKeys.Contains(keyName);
    }
}
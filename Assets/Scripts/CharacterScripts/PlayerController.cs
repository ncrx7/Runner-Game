using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    #region fields
    [SerializeField] public GameObject characterColliderObject;
    [SerializeField] public Collider characterCollider;
    [SerializeField] public Rigidbody rb;
    private Quaternion startRotation;
    public float jumpForce = 5f;
    private int _desiredLane = 1;
    /*     [SerializeField] private float leanAmount = 0.5f; // Eğilme miktarı
        [SerializeField] private float leanDuration = 5f; */
    public float laneDistance = 5f;
    #endregion

    #region mb call back methods
    private void OnEnable()
    {
        EventSystem.OnPressedSwipeKey += HandleCharacterMechanim;
    }

    private void OnDisable()
    {
        EventSystem.OnPressedSwipeKey -= HandleCharacterMechanim;
    }

    private void OnTriggerEnter(Collider other)
    {
        IInteractable interactable;
        if (other.TryGetComponent(out interactable))
        {
            interactable.Interact();
        }
    }
    /*     private void OnCollisionEnter(Collision other)
        {
            var interactable = other.collider.GetComponent<IInteractable>();
            Debug.Log("name of trigger obj: " + other.collider.name);
            if (interactable != null)
            {
                interactable.Interact();
            }
        } */
    #endregion

    #region methods
    private void HandleCharacterMechanim(int inputId) //0 sol ok tuşu, 1 sağ ok tuşu inputu
    {
        if (!GameManager.Instance.stunActive)
        {
            switch (inputId)
            {
                case 0:
                    _desiredLane--;
                    if (_desiredLane == -1)
                    {
                        _desiredLane = 0;
                    }
                    break;
                case 1:
                    _desiredLane++;
                    if (_desiredLane == 3)
                    {
                        _desiredLane = 2;
                    }
                    break;
                case 2:
                    if (!AnimationController.Instance.GetIsInteracting())
                    {
                        Jump();
                    }
                    break;
                case 3:
                    if (!AnimationController.Instance.GetIsInteracting())
                    {
                        Slide();
                    }
                    break;
                default:
                    Debug.Log("unknown input id");
                    break;
            }

            //Debug.Log("desired lane : " + _desiredLane);
            HorizontalMovement();
        }
    }

    private void HorizontalMovement()
    {
        Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up; // Vector3 value is (0, y * 1, z * 1)
        if (_desiredLane == 0)
        {
            targetPosition += Vector3.left * laneDistance; // (0, y * 1, z * 1) += (-1 * lD, 0 ,0)
        }
        else if (_desiredLane == 1)
        {
            //targetPosition += Vector3.right * laneDistance; // (0, y * 1, z * 1) += (1 * ld, 0, 0)
        }
        else if (_desiredLane == 2)
        {
            targetPosition += Vector3.right * (laneDistance);
        }
        // controller.Move(direction * Time.deltaTime);
        //transform.position = Vector3.Lerp(transform.position, targetPosition, 15 * Time.fixedDeltaTime);
        transform.DOMove(targetPosition, 0.2f).SetEase(Ease.Linear);
    }

    public void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        AnimationController.Instance.PlayTargetAnimation("Jumping", true);
    }

    public void Slide()
    {
        RotatePlayerCollider(); //state machine yazarsam collider'la uğraşmama gerek kalmaz
        AnimationController.Instance.PlayTargetAnimation("Running Slide", true);
        /*         Vector3 newPosition = playerCollider.bounds.center - Vector3.up * leanAmount;
                playerCollider.transform.DOMove(newPosition, leanDuration); */
    }

    void RotatePlayerCollider()
    {
        /*         rb.isKinematic = true;
                rb.useGravity = false; */
        characterColliderObject.transform.DORotate(new Vector3(-90f, 0f, 0f), 0.9f)
            .OnComplete(RestorePlayerColliderRotation);
    }

    void RestorePlayerColliderRotation()
    {
        characterColliderObject.transform.DORotate(startRotation.eulerAngles, 2.5f);
        /*         rb.isKinematic = false;
                rb.useGravity = true; */
    }
    #endregion
}

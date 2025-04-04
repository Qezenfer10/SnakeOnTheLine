using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using Random = UnityEngine.Random;

namespace SCPE
{
    [RequireComponent(typeof(CharacterController))]
    public class FirstPersonController : MonoBehaviour
    {
        private bool m_IsWalking;
        private float walkSpeed = 5f;
        private float runSpeed = 10f;
        public MouseLook mouseLook;

        public Camera m_Camera;
        private Vector2 m_Input;
        private Vector3 m_MoveDir = Vector3.zero;
        private CharacterController m_CharacterController;
        private CollisionFlags m_CollisionFlags;

        // Use this for initialization
        private void Start()
        {
            m_CharacterController = GetComponent<CharacterController>();
            mouseLook.Init(transform, m_Camera.transform);
        }

        // Update is called once per frame
        private void Update()
        {
            RotateView();
#if !UNITY_EDITOR
            if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit();
#endif
        }

#if !UNITY_EDITOR
        private void OnGUI()
        {
            Rect rect = new Rect(10, 10, 250, 50);

            GUILayout.BeginArea(rect);

            GUI.color = Color.black;
            GUILayout.Label("Press ESC to quit");

            GUILayout.EndArea();
        }
#endif

        bool MobileMode = false;
        private void FixedUpdate()
        {
            float speed;
            GetInput(out speed);
            // always move along the camera forward as it is the direction that it being aimed at
            Vector3 desiredMove = transform.forward * m_Input.y + transform.right * m_Input.x;

            // get a normal for the surface that is being touched to move along it
            RaycastHit hitInfo;
            Physics.SphereCast(transform.position, m_CharacterController.radius, Vector3.down, out hitInfo,
                               m_CharacterController.height / 2f, Physics.AllLayers, QueryTriggerInteraction.Ignore);
            desiredMove = Vector3.ProjectOnPlane(desiredMove, hitInfo.normal).normalized;

            m_MoveDir.x = desiredMove.x * speed;
            m_MoveDir.z = desiredMove.z * speed;

            m_CollisionFlags = m_CharacterController.Move(m_MoveDir * Time.fixedDeltaTime);
        }


        private void GetInput(out float speed)
        {
            // Read input
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            m_Input = new Vector2(horizontal, vertical);

            // normalize input if it exceeds 1 in combined length:
            if (m_Input.sqrMagnitude > 1)
            {
                m_Input.Normalize();
            }

#if !MOBILE_INPUT
            // On standalone builds, walk/run speed is modified by a key press.
            // keep track of whether or not the character is walking or running
            m_IsWalking = !Input.GetKey(KeyCode.LeftShift);
#endif
            // set the desired speed to be walking or running
            speed = m_IsWalking ? walkSpeed : runSpeed;
        }


        private void RotateView()
        {
            mouseLook.LookRotation(transform, m_Camera.transform);
        }


        //private void OnControllerColliderHit(ControllerColliderHit hit)
        //{
        //    Rigidbody body = hit.collider.attachedRigidbody;
        //    //dont move the rigidbody if the character is on top of it
        //    if (m_CollisionFlags == CollisionFlags.Below)
        //    {
        //        return;
        //    }

        //    if (body == null || body.isKinematic)
        //    {
        //        return;
        //    }
        //    body.AddForceAtPosition(m_CharacterController.velocity * 0.1f, hit.point, ForceMode.Impulse);
        //}
    }
}

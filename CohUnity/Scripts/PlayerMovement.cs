using UnityEngine;
using MyGameNamespace;

namespace Player.Movement
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private MovementConfig movementConfig;
        private IMovementController movementController;
        private IAnimationController animationController;
        private IGroundDetector groundDetector;
        private IPlayerInput playerInput;

        private void Awake()
        {
            Initialize();
        }

        private void Initialize()
        {
            var rigidbody = GetComponent<Rigidbody2D>();
            var animator = GetComponent<Animator>();

            movementController = new MovementController(rigidbody, transform, movementConfig);
            animationController = new AnimationController(animator);
            groundDetector = new GroundDetector();
            playerInput = new PlayerInput();

            SetInitialPosition();
        }

        private void Update()
        {
            var horizontalInput = playerInput.GetHorizontalInput();
            var isJumpRequested = playerInput.IsJumpRequested();

            movementController.Move(horizontalInput);
            
            if (isJumpRequested && groundDetector.IsGrounded)
            {
                movementController.Jump();
                animationController.TriggerJumpAnimation();
                groundDetector.IsGrounded = false;
            }

            UpdateAnimations(horizontalInput);
        }

        private void UpdateAnimations(float horizontalInput)
        {
            animationController.UpdateMovementAnimation(horizontalInput != 0);
            animationController.UpdateGroundedAnimation(groundDetector.IsGrounded);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (groundDetector.IsGroundCollision(collision))
            {
                groundDetector.IsGrounded = true;
            }
        }

        private void SetInitialPosition()
        {
            var positionSetter = new PlayerPositionSetter();
            positionSetter.SetPosition(new Vector2(DataManager.X, DataManager.Y));
        }
    }

    [System.Serializable]
    public class MovementConfig
    {
        public float Speed = 5f;
        public float JumpForce = 10f;
    }

    public interface IMovementController
    {
        void Move(float horizontalInput);
        void Jump();
    }

    public class MovementController : IMovementController
    {
        private readonly Rigidbody2D rigidbody;
        private readonly Transform transform;
        private readonly MovementConfig config;

        public MovementController(Rigidbody2D rigidbody, Transform transform, MovementConfig config)
        {
            this.rigidbody = rigidbody;
            this.transform = transform;
            this.config = config;
        }

        public void Move(float horizontalInput)
        {
            rigidbody.linearVelocity = new Vector2(horizontalInput * config.Speed, rigidbody.linearVelocity.y);
            UpdateFacing(horizontalInput);
        }

        public void Jump()
        {
            rigidbody.linearVelocity = new Vector2(rigidbody.linearVelocity.x, config.JumpForce);
        }

        private void UpdateFacing(float horizontalInput)
        {
            if (horizontalInput > 0.01f)
                transform.localScale = Vector3.one;
            else if (horizontalInput < -0.01f)
                transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    public interface IAnimationController
    {
        void UpdateMovementAnimation(bool isMoving);
        void UpdateGroundedAnimation(bool isGrounded);
        void TriggerJumpAnimation();
    }

    public class AnimationController : IAnimationController
    {
        private readonly Animator animator;

        public AnimationController(Animator animator)
        {
            this.animator = animator;
        }

        public void UpdateMovementAnimation(bool isMoving)
        {
            animator.SetBool("move", isMoving);
        }

        public void UpdateGroundedAnimation(bool isGrounded)
        {
            animator.SetBool("grounded", isGrounded);
        }

        public void TriggerJumpAnimation()
        {
            animator.SetTrigger("jump");
        }
    }

    public interface IGroundDetector
    {
        bool IsGrounded { get; set; }
        bool IsGroundCollision(Collision2D collision);
    }

    public class GroundDetector : IGroundDetector
    {
        public bool IsGrounded { get; set; }

        public bool IsGroundCollision(Collision2D collision)
        {
            return collision.gameObject.CompareTag("Ground");
        }
    }

    public interface IPlayerInput
    {
        float GetHorizontalInput();
        bool IsJumpRequested();
    }

    public class PlayerInput : IPlayerInput
    {
        public float GetHorizontalInput()
        {
            return Input.GetAxis("Horizontal");
        }

        public bool IsJumpRequested()
        {
            return Input.GetKey(KeyCode.Space);
        }
    }

    public class PlayerPositionSetter
    {
        public void SetPosition(Vector2 newPosition)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                player.transform.position = newPosition;
                Debug.Log($"Player position set to: {newPosition}");
            }
            else
            {
                Debug.LogError("Player not found!");
            }
        }
    }
}


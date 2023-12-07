using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D), typeof(InputManager))]
public class PlayerController : MonoBehaviour, IPlayerController
{
    [SerializeField] private ScriptableStats stats;
    [SerializeField] private GameObject firePoint;
    private Rigidbody2D rb;
    private CapsuleCollider2D col;
    private InputManager IM;
    private FrameInput frameInput;
    private Vector2 frameVelocity;
    private bool cachedQueryStartInColliders;
    private bool facingRight = true;

    #region Interface

    public Vector2 FrameInput => frameInput.Move;
    public event Action<bool, float> GroundedChanged;
    public event Action Jumped;

    #endregion

    private float time;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<CapsuleCollider2D>();
        IM = GetComponent<InputManager>();

        cachedQueryStartInColliders = Physics2D.queriesStartInColliders;
    }

    private void Update()
    {
        time += Time.deltaTime;
        GatherInput();
    }

    private void GatherInput()
    {
        frameInput = new FrameInput
        {
            JumpDown = IM.jump,
            JumpHeld = IM.jump,
            Move = IM.move
        };

        if (stats.SnapInput)
        {
            frameInput.Move.x = Mathf.Abs(frameInput.Move.x) < stats.HorizontalDeadZoneThreshold ? 0 : Mathf.Sign(frameInput.Move.x);
            frameInput.Move.y = Mathf.Abs(frameInput.Move.y) < stats.VerticalDeadZoneThreshold ? 0 : Mathf.Sign(frameInput.Move.y);
        }

        if (frameInput.JumpDown)
        {
            jumpToConsume = true;
            timeJumpWasPressed = time;
        }
    }

    private void FixedUpdate()
    {
        CheckCollisions();

        HandleJump();
        HandleDirection();
        HandleGravity();

        ApplyMovement();
        FlipCheck();
    }

    #region Collisions

    private float frameLeftGrounded = float.MinValue;
    private bool grounded = true;

    private void CheckCollisions()
    {
        Physics2D.queriesStartInColliders = false;

        // Ground and Ceiling
        bool groundHit = Physics2D.CapsuleCast(col.bounds.center, col.size, col.direction, 0, Vector2.down, stats.GrounderDistance, ~stats.PlayerLayer);
        bool ceilingHit = Physics2D.CapsuleCast(col.bounds.center, col.size, col.direction, 0, Vector2.up, stats.GrounderDistance, ~stats.PlayerLayer);

        // Hit a Ceiling
        if (ceilingHit)
            frameVelocity.y = Mathf.Min(0, frameVelocity.y);

        // Landed on the Ground
        if (!grounded && groundHit)
        {
            grounded = true;
            coyoteUsable = true;
            bufferedJumpUsable = true;
            endedJumpEarly = false;
            GroundedChanged?.Invoke(true, Mathf.Abs(frameVelocity.y));
        }

        // Left the Ground
        else if (grounded && !groundHit)
        {
            grounded = false;
            frameLeftGrounded = time;
            GroundedChanged?.Invoke(false, 0);
        }

        Physics2D.queriesStartInColliders = cachedQueryStartInColliders;
    }

    #endregion

    #region Jumping

    private bool jumpToConsume;
    private bool bufferedJumpUsable;
    private bool endedJumpEarly;
    private bool coyoteUsable;
    private float timeJumpWasPressed;

    private bool HasBufferedJump => bufferedJumpUsable && time < timeJumpWasPressed + stats.JumpBuffer;
    private bool CanUseCoyote => coyoteUsable && !grounded && time < frameLeftGrounded + stats.CoyoteTime;

    private void HandleJump()
    {
        if (!endedJumpEarly && !grounded && !frameInput.JumpHeld && rb.velocity.y > 0)
            endedJumpEarly = true;

        if (!jumpToConsume && !HasBufferedJump)
            return;

        if (grounded || CanUseCoyote)
            ExecuteJump();

        jumpToConsume = false;
    }

    private void ExecuteJump()
    {
        endedJumpEarly = false;
        timeJumpWasPressed = 0;
        bufferedJumpUsable = false;
        coyoteUsable = false;
        frameVelocity.y = stats.JumpPower;
        Jumped?.Invoke();
    }

    #endregion

    #region Horizontal

    private void HandleDirection()
    {
        if (frameInput.Move.x == 0)
        {
            var deceleration = grounded ? stats.GroundDeceleration : stats.AirDeceleration;
            frameVelocity.x = Mathf.MoveTowards(frameVelocity.x, 0, deceleration * Time.fixedDeltaTime);
        }
        else
        {
            frameVelocity.x = Mathf.MoveTowards(frameVelocity.x, frameInput.Move.x * stats.MaxSpeed, stats.Acceleration * Time.fixedDeltaTime);
        }
    }

    #endregion

    #region Gravity

    private void HandleGravity()
    {
        if (grounded && frameVelocity.y <= 0f)
        {
            frameVelocity.y = stats.GroundingForce;
        }
        else
        {
            var inAirGravity = stats.FallAcceleration;
            if (endedJumpEarly && frameVelocity.y > 0) inAirGravity *= stats.JumpEndEarlyGravityModifier;
            frameVelocity.y = Mathf.MoveTowards(frameVelocity.y, -stats.MaxFallSpeed, inAirGravity * Time.fixedDeltaTime);
        }
    }

    #endregion

    private void ApplyMovement() => rb.velocity = frameVelocity;

    private void FlipCheck()
    {
        if (frameInput.Move.x < 0 && facingRight)
        {
            facingRight = false;
            firePoint.transform.Rotate(0, 180, 0);
        }
        else if (frameInput.Move.x > 0 && !facingRight)
        {
            facingRight = true;
            firePoint.transform.Rotate(0, 180, 0);
        }
    }
}

public struct FrameInput
{
    public bool JumpDown;
    public bool JumpHeld;
    public Vector2 Move;
}

public interface IPlayerController
{
    public event Action<bool, float> GroundedChanged;

    public event Action Jumped;
    public Vector2 FrameInput { get; }
}
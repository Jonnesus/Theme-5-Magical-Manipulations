using UnityEngine;

    public struct OldFrameInput
    {
        public float X,Y;
        public bool JumpDown;
        public bool JumpUp;
        public bool Fire1;
        public bool Fire2;
    }

    public interface OldIPlayerController
    {
        public Vector3 Velocity { get; }
        public OldFrameInput Input { get; }
        public bool JumpingThisFrame { get; }
        public bool LandingThisFrame { get; }
        public Vector3 RawMovement { get; }
        public bool Grounded { get; }
    }
    
    public interface OldIExtendedPlayerController : OldIPlayerController
    {
        public bool DoubleJumpingThisFrame { get; set; }
        public bool Dashing { get; set; }  
    }

    public struct OldRayRange
    {
        public OldRayRange(float x1, float y1, float x2, float y2, Vector2 dir) {
            Start = new Vector2(x1, y1);
            End = new Vector2(x2, y2);
            Dir = dir;
        }

        public readonly Vector2 Start, End, Dir;
    }
using UnityEngine;

namespace Plugins
{
    [ExecuteAlways]
    public class StickToTransform : MonoBehaviour
    {
        public Transform StickTo;

        [SerializeField] protected bool Position = true;
        [SerializeField] protected bool PosZOnly;
        [SerializeField] protected bool Rotation;
        [SerializeField] protected bool Scale;
        public bool Simple;
        public bool Late;
        public bool Fixed;

        /// <summary>
        /// нужно для UI, чтобы позиция присваивалась, только если менялась
        /// </summary>
        public bool ComparePositionBeforeChange;

        protected Transform cachTr;

        public virtual void FixedUpdate()
        {
            if (Fixed || !StickTo) return;

            UpdateAnchors();
        }

        protected virtual void Start()
        {
            Init();
        }
        public virtual void Init()
        {
            cachTr = transform;
        }

        public virtual void Update()
        {
            if (!Simple || !StickTo) return;

            UpdateAnchors();
        }

        public virtual void LateUpdate()
        {
            if (!Late || !StickTo) return;

            UpdateAnchors();
        }

        public virtual void UpdateAnchors()
        {
            if (Position)
            {
            
                if (ComparePositionBeforeChange)
                {
                    if (cachTr.position != StickTo.position)
                    {
                        ForceStickPosition();
                    }
                }
                else
                {
                    ForceStickPosition();
                }
            }

            if (Rotation) cachTr.rotation = StickTo.rotation;
            if (Scale) cachTr.localScale = StickTo.localScale;
        }

        public virtual void ForceStickPosition()
        {
            Vector3 pos = StickTo.position;
            Vector3 posBefore = cachTr.position;
                    
            if (PosZOnly)
            {
                pos.x = posBefore.x;
                pos.y = posBefore.y;
            }
            cachTr.position = pos;
        }
    }
}
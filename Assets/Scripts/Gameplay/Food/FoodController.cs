using System;
using System.Runtime.CompilerServices;
using Gameplay.Boneco;
using Gameplay.Decks;
using Gameplay.Utils.Debug.DebugPrinting;
using UnityEngine;

namespace Gameplay.Food
{
    public class FoodController : RaycastController
    {

        public DeckManager _deckManager;
        public FoodMovementProps foodMovementProps;
        public FoodMotor foodMotor;
        public CollisionInfo collisions;

        public override void Start()
        {
            base.Start();
            _deckManager = GetComponent<DeckManager>();
            foodMovementProps = _deckManager.GetFoodMovementProps();
            foodMotor = _deckManager.GetFoodMotor(gameObject.transform, foodMovementProps);
        }

        public void Move(Vector2 moveAmount, Vector2 input, bool standingOnPlatform = false)
        {
            UpdateRaycastOrigins();
            collisions.Reset ();
            //collisions.moveAmountOld = moveAmount;

            if (moveAmount.y != 0) {
                VerticalCollisions (ref moveAmount);
            }

            if (moveAmount.x != 0)
            {
                collisions.faceDir = foodMovementProps.faceDir =  (int)Mathf.Sign(moveAmount.x);
                HorizontalCollisions (ref moveAmount);
            }
            foodMotor.Tick(moveAmount);
        }
        
        void HorizontalCollisions(ref Vector2 moveAmount) {
            float directionX = collisions.faceDir;
            float rayLength = Mathf.Abs (moveAmount.x) + skinWidth;

            if (Mathf.Abs(moveAmount.x) < skinWidth) {
                rayLength = 2*skinWidth;
            }

            for (int i = 0; i < horizontalRayCount; i ++) {
                Vector2 rayOrigin = (directionX == -1)?raycastOrigins.bottomLeft:raycastOrigins.bottomRight;
                rayOrigin += Vector2.up * (horizontalRaySpacing * i);
                RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLength, collisionMask);

                Debug.DrawRay(rayOrigin, Vector2.right * directionX,Color.red);
                
                DebugLogPrinterController.current.PrintMoveControllersLog("HorizontalCollisions()");

                if (hit)
                {
                    if (hit.distance == 0) {
                        continue;
                    }

                    float slopeAngle = Vector2.Angle(hit.normal, Vector2.up);

                    if (i == 0) {
                        if (collisions.descendingSlope) {
                            collisions.descendingSlope = false;
                            moveAmount = collisions.moveAmountOld;
                        }
                        float distanceToSlopeStart = 0;
                        if (slopeAngle != collisions.slopeAngleOld) {
                            distanceToSlopeStart = hit.distance-skinWidth;
                            moveAmount.x -= distanceToSlopeStart * directionX;
                        }
                        //ClimbSlope(ref moveAmount, slopeAngle, hit.normal);
                        moveAmount.x += distanceToSlopeStart * directionX;
                    }

                    if (!collisions.climbingSlope) {
                        moveAmount.x = (hit.distance - skinWidth) * directionX;
                        rayLength = hit.distance;

                        if (collisions.climbingSlope) {
                            moveAmount.y = Mathf.Tan(collisions.slopeAngle * Mathf.Deg2Rad) * Mathf.Abs(moveAmount.x);
                        }

                        collisions.left = directionX == -1;
                        collisions.right = directionX == 1;
                    }
                }
            }
        }
        
        //TODO make it generic and reusable
        void VerticalCollisions(ref Vector2 moveAmount) 
        {
            float directionY = Mathf.Sign (moveAmount.y);
            float rayLength = Mathf.Abs (moveAmount.y) + skinWidth;

            for (int i = 0; i < verticalRayCount; i ++) {

                Vector2 rayOrigin = (directionY == -1)?raycastOrigins.bottomLeft:raycastOrigins.topLeft;
                rayOrigin += Vector2.right * (verticalRaySpacing * i + moveAmount.x);
                RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up * directionY, rayLength, collisionMask);

                Debug.DrawRay(rayOrigin, Vector2.up * directionY,Color.red);

                if (hit)
                {
                    //var hitInfo = hit.collider;
                    //Debug.Log(hitInfo.transform.parent.name.ToString());
                    
                    moveAmount.y = (hit.distance - skinWidth) * directionY;
                    rayLength = hit.distance;  
                    
                    collisions.below = directionY == -1;
                    collisions.above = directionY == 1;
                }
            }
        }
    }
}


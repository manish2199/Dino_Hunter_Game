using System.Collections;
using System.Collections.Generic;
using  UnityEngine;
using UnityEngine.Rendering;

namespace  Math_Calculations
{
    public static class CustomMathFunctions 
    {
        public static float Distance(Vector3 firstPosition, Vector3 secondPosition)
        {
            // return distance between dinosaur and cureent target  Waypoint 
            Vector3 heading ;
            float distance; 
            float distanceSquared;
            
            heading.x = secondPosition.x - firstPosition.x;
            heading.y = secondPosition.y - firstPosition.y;
            heading.z = secondPosition.z - firstPosition.z;
            
            distanceSquared = (heading.x *  heading.x) + ( heading.y *  heading.y ) + ( heading.z * heading.z);
            distance = BinarySearchSquareRoot(distanceSquared);
            
            return distance;         
        }
        
        
        public static bool IsPresentInFront(Vector3 forwardDirection,Vector3 directionToOtherObject)
        {
            var differenceFromMyForwardDirection =
                Vector3.Dot(forwardDirection, directionToOtherObject);

            if (differenceFromMyForwardDirection > 0) {
                // The object is in front of us
                return true;
            }
            return false;
        }

        private static float BinarySearchSquareRoot(float value)
        {
            float start = 0f;
            float end = value;

            float root = 0.0f;

            while (start <= end)
            {
                float mid = start + (end - start) / 2;

                if (mid*mid == value)
                {
                    return mid;
                }

                if (mid * mid > value)
                {
                    end = mid-1;
                }
                else
                {
                    start = mid+1;
                }
            }

            float incr = 0.1f;
            for (int i = 0; i < 4; i++)
            {
                while (root * root <= value)
                {
                    root += incr;
                }
                root -= incr;
                incr = incr / 10;
            }

            return root;
        }
    }
    
}


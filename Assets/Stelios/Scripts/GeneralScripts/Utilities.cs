using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utilities
{

    public static void RotateAnimation(this GameObject a, Animator anim, Quaternion endRotation)
    {
        float Degrees = Mathf.Abs(a.transform.rotation.eulerAngles.y - endRotation.eulerAngles.y);

        if (a.transform.rotation.eulerAngles.y < endRotation.eulerAngles.y)
        {
            if (endRotation.eulerAngles.y - a.transform.rotation.eulerAngles.y < 180)
            {
                AnimTurnRight(Degrees,anim);
            }
            else
            {
                AnimTurnLeft(Degrees, anim);
            }
        }
        else
        {
            if (a.transform.rotation.eulerAngles.y - endRotation.eulerAngles.y < 180)
            {
                AnimTurnLeft(Degrees, anim);
            }
            else
            {
                AnimTurnRight(Degrees, anim);
            }
        }

    }

    public static void AnimTurnLeft(float degrees,Animator anim)
    {
        anim.SetBool("IsTurningRight", false);
        anim.SetBool("IsTurningLeft", true);
        anim.SetFloat("Degrees", degrees);
    }

    public static void AnimTurnRight(float degrees, Animator anim)
    {
        anim.SetBool("IsTurningLeft", false);
        anim.SetBool("IsTurningRight", true);
        anim.SetFloat("Degrees", degrees);
    }

    public static void StopRotate(this Animator anim)
    {
        anim.SetBool("IsTurningRight", false);
        anim.SetBool("IsTurningLeft", false);
    }

}

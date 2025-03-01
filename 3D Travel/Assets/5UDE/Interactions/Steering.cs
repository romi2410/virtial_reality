﻿/*
Copyright ©2017. The University of Texas at Dallas. All Rights Reserved. 

Permission to use, copy, modify, and distribute this software and its documentation for 
educational, research, and not-for-profit purposes, without fee and without a signed 
licensing agreement, is hereby granted, provided that the above copyright notice, this 
paragraph and the following two paragraphs appear in all copies, modifications, and 
distributions. 


Contact The Office of Technology Commercialization, The University of Texas at Dallas, 
800 W. Campbell Road (AD15), Richardson, Texas 75080-3021, (972) 883-4558, 
otc@utdallas.edu, https://research.utdallas.edu/otc for commercial licensing opportunities.

IN NO EVENT SHALL THE UNIVERSITY OF TEXAS AT DALLAS BE LIABLE TO ANY PARTY FOR DIRECT, 
INDIRECT, SPECIAL, INCIDENTAL, OR CONSEQUENTIAL DAMAGES, INCLUDING LOST PROFITS, ARISING 
OUT OF THE USE OF THIS SOFTWARE AND ITS DOCUMENTATION, EVEN IF THE UNIVERSITY OF TEXAS AT 
DALLAS HAS BEEN ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

THE UNIVERSITY OF TEXAS AT DALLAS SPECIFICALLY DISCLAIMS ANY WARRANTIES, INCLUDING, BUT 
NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR 
PURPOSE. THE SOFTWARE AND ACCOMPANYING DOCUMENTATION, IF ANY, PROVIDED HEREUNDER IS 
PROVIDED "AS IS". THE UNIVERSITY OF TEXAS AT DALLAS HAS NO OBLIGATION TO PROVIDE 
MAINTENANCE, SUPPORT, UPDATES, ENHANCEMENTS, OR MODIFICATIONS.
*/

/*
 Date: 09/22/2018 - Changes done with tag HW2 to add two new steering states - leftward and rightward.
*/

using UnityEngine;
using System.Collections;

public class Steering : MonoBehaviour {

    // Enumerate the states of steering
    public enum SteeringState {
        NotSteering,
        SteeringForward,
        SteeringBackward,
        //HW2 - Adding two new steering states - leftward and rightward
        SteeringLeft,
        SteeringRight
    };
    
    // Inspector parameters
    [Tooltip("The tracking device used to determine absolute direction for steering.")]
    public CommonTracker tracker;

    [Tooltip("The controller joystick used to determine relative direction (forward/backward) and speed.")]
    public CommonAxis joystick;

    [Tooltip("A button required to be pressed to activate steering.")]
    public CommonButton button;  

    [Tooltip("The space that is translated by this interaction. Usually set to the physical tracking space.")]
    public CommonSpace space;

    [Tooltip("The median speed for movement expressed in meters per second.")]
    public float speed = 1.0f;

    // Private interaction variables
    private SteeringState state;

    // Called at the end of the program initialization
    void Start () {

        // Set initial steering state to not steering
        state = SteeringState.NotSteering;
    }
        
    // FixedUpdate is not called every graphical frame but rather every physics frame
    void FixedUpdate () {

        // If state is not steering
        if (state == SteeringState.NotSteering) {
        
            // If the joystick is pressed forward and the button is pressed
            if (joystick.GetAxis ().y > 0.0f && button.GetPress () && (Mathf.Abs(joystick.GetAxis().y) > Mathf.Abs(joystick.GetAxis().x))) {

                // Change state to steering forward
                state = SteeringState.SteeringForward;
            }
                
            // If the joystick is pressed backward and the button is pressed
            else if (joystick.GetAxis ().y < 0.0f && button.GetPress () && (Mathf.Abs(joystick.GetAxis().y) > Mathf.Abs(joystick.GetAxis().x)) ) {

                // Change state to steering backward
                state = SteeringState.SteeringBackward;
            }

            //HW2 - If the joystick is pressed leftward and the button is pressed
            else if (joystick.GetAxis().x < 0.0f && button.GetPress() && (Mathf.Abs(joystick.GetAxis().y) < Mathf.Abs(joystick.GetAxis().x)) )
            {

                //HW2 - Change state to steering leftward
                state = SteeringState.SteeringLeft;
            }

            //HW2 - If the joystick is pressed rightward and the button is pressed
            else if (joystick.GetAxis().x > 0.0f && button.GetPress() && (Mathf.Abs(joystick.GetAxis().y) < Mathf.Abs(joystick.GetAxis().x)))
            {

                //HW2 - Change state to steering rightward
                state = SteeringState.SteeringRight;
            }



            // Process current not steering state
            else {

                // Nothing to do for not steering
            }
        }

        // If state is steering forward
        else if (state == SteeringState.SteeringForward) {

            // If the button is not pressed
            if (!button.GetPress ()) {

                // Change state to not steering 
                state = SteeringState.NotSteering;
            }

            //HW2 - If the joystick is pressed leftward and the button is pressed
            else if (joystick.GetAxis().x < 0.0f && button.GetPress() && (Mathf.Abs(joystick.GetAxis().y) < Mathf.Abs(joystick.GetAxis().x)))
            {

                //HW2 - Change state to steering leftward
                state = SteeringState.SteeringLeft;
            }

            // If the joystick is pressed backward and the button is pressed
            else if (joystick.GetAxis ().y < 0.0f && button.GetPress () && (Mathf.Abs(joystick.GetAxis().y) > Mathf.Abs(joystick.GetAxis().x)) ) {

                // Change state to steering backward
                state = SteeringState.SteeringBackward;
            }

            //HW2 - If the joystick is pressed rightward and the button is pressed
            else if (joystick.GetAxis().x > 0.0f && button.GetPress() && (Mathf.Abs(joystick.GetAxis().y) < Mathf.Abs(joystick.GetAxis().x)))
            {

                //HW2 - Change state to steering rightward
                state = SteeringState.SteeringRight;
            }

            // Process current steering forward state
            else {

                // Translate the space based on the tracker's absolute forward direction and the joystick's forward value
                space.transform.position += joystick.GetAxis ().y * tracker.transform.forward * speed * Time.deltaTime;
            }
        }

        // If state is steering backward
        else if (state == SteeringState.SteeringBackward) {

            // If the button is not pressed
            if (!button.GetPress ()) {

                // Change state to not steering 
                state = SteeringState.NotSteering;
            }

            //HW2 - If the joystick is pressed leftward and the button is pressed
            else if (joystick.GetAxis().x < 0.0f && button.GetPress() && (Mathf.Abs(joystick.GetAxis().y) < Mathf.Abs(joystick.GetAxis().x)) )
            {

                //HW2 - Change state to steering leftward
                state = SteeringState.SteeringLeft;
            }

            //HW2 - If the joystick is pressed rightward and the button is pressed
            else if (joystick.GetAxis().x > 0.0f && button.GetPress() && (Mathf.Abs(joystick.GetAxis().y) < Mathf.Abs(joystick.GetAxis().x)))
            {

                //HW2 - Change state to steering rightward
                state = SteeringState.SteeringRight;
            }

            // If the joystick is pressed forward and the button is pressed
            else if (joystick.GetAxis ().y > 0.0f && button.GetPress () && (Mathf.Abs(joystick.GetAxis().y) > Mathf.Abs(joystick.GetAxis().x)) ) {

                // Change state to steering forward
                state = SteeringState.SteeringForward;
            }

            // Process current steering backward state
            else {

                // Translate the space based on the tracker's absolute forward direction and the joystick's backward value
                space.transform.position += joystick.GetAxis ().y * tracker.transform.forward * speed * Time.deltaTime;
            }
        }

        //HW2 - if state is steering leftward
        else if (state == SteeringState.SteeringLeft)
        {

            // If the button is not pressed
            if (!button.GetPress())
            {

                // Change state to not steering 
                state = SteeringState.NotSteering;
            }

            // If the joystick is pressed forward and the button is pressed
            else if (joystick.GetAxis().y > 0.0f && button.GetPress() && (Mathf.Abs(joystick.GetAxis().y) > Mathf.Abs(joystick.GetAxis().x)) )
            {

                // Change state to steering forward
                state = SteeringState.SteeringForward;
            }

            // If the joystick is pressed backward and the button is pressed
            else if (joystick.GetAxis().y < 0.0f && button.GetPress() && (Mathf.Abs(joystick.GetAxis().y) > Mathf.Abs(joystick.GetAxis().x)))
            {

                // Change state to steering backward
                state = SteeringState.SteeringBackward;
            }

            //HW2 - If the joystick is pressed rightward and the button is pressed
            else if (joystick.GetAxis().x > 0.0f && button.GetPress() && (Mathf.Abs(joystick.GetAxis().y) < Mathf.Abs(joystick.GetAxis().x)) )
            {

                //HW2 - Change state to steering rightward
                state = SteeringState.SteeringRight;
            }

            // Process current strafe left state
            else
            {

                // Translate the space based on the tracker's absolute forward direction and the joystick's backward value
                space.transform.position += joystick.GetAxis().x * tracker.transform.right * speed * Time.deltaTime;
            }
        }

        //HW2 - If state is steering rightward
        else if (state == SteeringState.SteeringRight)
        {

            // If the button is not pressed
            if (!button.GetPress())
            {

                // Change state to not steering 
                state = SteeringState.NotSteering;
            }

            // If the joystick is pressed forward and the button is pressed
            else if (joystick.GetAxis().y > 0.0f && button.GetPress() && (Mathf.Abs(joystick.GetAxis().y) > Mathf.Abs(joystick.GetAxis().x)))
            {

                // Change state to steering forward
                state = SteeringState.SteeringForward;
            }

            // If the joystick is pressed backward and the button is pressed
            else if (joystick.GetAxis().y < 0.0f && button.GetPress() && (Mathf.Abs(joystick.GetAxis().y) > Mathf.Abs(joystick.GetAxis().x)) )
            {

                // Change state to steering backward
                state = SteeringState.SteeringBackward;
            }

            //HW2 - If the joystick is pressed leftward and the button is pressed
            else if (joystick.GetAxis().x < 0.0f && button.GetPress() && (Mathf.Abs(joystick.GetAxis().y) < Mathf.Abs(joystick.GetAxis().x)))
            {

                // Change state to steering leftward
                state = SteeringState.SteeringLeft;
            }

            // Process current strafe right state
            else
            {

                // Translate the space based on the tracker's absolute forward direction and the joystick's backward value
                space.transform.position += joystick.GetAxis().x * tracker.transform.right * speed * Time.deltaTime;
            }
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PANG.Input
{
    public enum PlayerId { P1, P2 }
    public class InputManager : MonoBehaviour
    {
        private GameControls gameControls;
        private void Awake()
        {
            gameControls = new GameControls();
        }


        
    
    }

}

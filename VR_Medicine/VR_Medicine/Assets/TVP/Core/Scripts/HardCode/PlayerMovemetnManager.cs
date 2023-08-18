using Autohand;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TVP
{


    public class PlayerMovemetnManager : MonoSinglethon<PlayerMovemetnManager>
    {
        [SerializeField] AutoHandPlayer _player;

        private void Awake()
        {
            _player = GetComponent<AutoHandPlayer>();
        }

        public void FreezePlayer()
        {
            _player.maxMoveSpeed = 0f;
            _player.snapTurnAngle = 30;
        }

        public void RealesePlayer()
        {
            _player.maxMoveSpeed = 1.8f;
            _player.snapTurnAngle = 30;
        }
    }
}
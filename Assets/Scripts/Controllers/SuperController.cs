using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlappyDank.Controllers
{
    public class SuperController
    {
        private static SuperController _instance;

        public static SuperController Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new SuperController();

                return _instance;
            }
        }

        public bool IsInited { get; private set; }

        public IFrameController   FrameController   { get; }
        public IInputController   InputController   { get; }
        public ILevelController   LevelController { get; }
        public IPhysicsController PhysicsController { get; }

        public SuperController()
        {
            FrameController   = new FrameController();
            InputController   = new InputControler();
            LevelController   = new LevelController();
            PhysicsController = new PhysicsController();

            FrameController.Init();
            InputController.Init();
            LevelController.Init();
            PhysicsController.Init();
        }

        public static void Init()
        {
            if (_instance == null)
                _instance = new SuperController();
        }
    }
}
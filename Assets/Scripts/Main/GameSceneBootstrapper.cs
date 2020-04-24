using FlappyDank.Controllers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlappyDank
{
    public class GameSceneBootstrapper : MonoBehaviour
    {
        [SerializeField]
        private CameraFollow _followingCamera;

        [Header("Prefabs")]
        [SerializeField]
        private BallScript _ballPrefab;
        [SerializeField]
        private LevelManager _levelManager;

        [Header("Markers")]
        [SerializeField]
        private GameObject _ballStartPosition;

        [Header("Views")]
        [SerializeField]
        private BottomPanelView _bottomPanelView;
        [SerializeField]
        private TopPanelView _topPanelView;

        private void Awake()
        {
            var ball         = Instantiate(_ballPrefab, _ballStartPosition.transform);
            var levelManager = Instantiate(_levelManager);

            _followingCamera.SetTarget(ball.gameObject);
            levelManager.SetFollowedTarget(ball.gameObject);
            levelManager.SetBottomEdge(_bottomPanelView.AnchoredPosition.y);

            SuperController.Instance.FrameController.AddListener(_followingCamera);
            SuperController.Instance.FrameController.AddListener(levelManager);
            SuperController.Instance.PhysicsController.AddListener(ball);

            SuperController.Instance.LevelController.SetManager(levelManager);
            SuperController.Instance.LevelController.SetTopPanelView(_topPanelView);
            SuperController.Instance.LevelController.SetPlayer(ball);
        }
    }
}
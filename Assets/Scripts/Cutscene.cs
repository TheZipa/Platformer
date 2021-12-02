using System.Collections;
using UnityEngine;

namespace Platformer
{
    public class Cutscene : MonoBehaviour
    {
        [SerializeField] private GameObject[] _ui;
        [SerializeField] private GameObject _cutsceneVirtualCamera;
        [SerializeField] private GameObject _cutsceneObject;
        [SerializeField] private GameObject _cutsceneBars;
        [SerializeField] private float _cutsceneTime = 5f;
        [SerializeField] private float _cutsceneOffsetTime = 2.5f;

        private bool _isCutscenePlayed = false;
        private PlayerMovement _playerMovement;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.name == "Rogue Knight")
            {
                if (!_isCutscenePlayed)
                {
                    _playerMovement = collision.gameObject.GetComponent<PlayerMovement>();
                    _playerMovement.IsCutscenePlaying = true;
                    CreateCutscene();
                }
            }
        }

        private void CreateCutscene()
        {
            _isCutscenePlayed = true;
            _cutsceneBars.SetActive(true);
            SetActiveUI(false);
            StartCoroutine(StartCutscene());
        }

        private IEnumerator StartCutscene()
        {
            _cutsceneVirtualCamera.SetActive(true);

            yield return new WaitForSeconds(_cutsceneOffsetTime);

            _cutsceneObject.SetActive(true);

            yield return new WaitForSeconds(_cutsceneTime);

            _cutsceneVirtualCamera.SetActive(false);
            _cutsceneBars.SetActive(false);
            _playerMovement.IsCutscenePlaying = false;
            SetActiveUI(true);
        }

        private void SetActiveUI(bool enabled)
        {
            foreach(var ui in _ui)
            {
                ui.SetActive(enabled);
            }
        }
    }
}
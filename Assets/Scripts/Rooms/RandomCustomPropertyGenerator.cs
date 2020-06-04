using ExitGames.Client.Photon;
using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using Random = System.Random;

namespace Rooms
{
    public class RandomCustomPropertyGenerator : MonoBehaviour
    {
        #region Property

        #endregion

        #region Private Field

        [FormerlySerializedAs("CharacterSelectionUI")] [SerializeField] private TextMeshProUGUI characterSelectionUi;
        private readonly Hashtable _customProperties = new Hashtable();
        private CharacterPick _currentCharacter = CharacterPick.Maria;
        

        #endregion

        #region MonoBehavior Callback

    

        #endregion

        #region Public Method

        public void OnClick_GenerateNumber()
        {
            SwitchCharacter();
        }

        #endregion

        #region Private Method

        private void SwitchCharacter()
        {
            _currentCharacter = _currentCharacter == CharacterPick.Maria ? CharacterPick.Soap : CharacterPick.Maria;

            characterSelectionUi.text = _currentCharacter.ToString();
            _customProperties["RandomNumber"] = _currentCharacter.ToString();
            PhotonNetwork.SetPlayerCustomProperties(_customProperties);

        }

        #endregion
    }
}
using ExitGames.Client.Photon;
using Photon.Pun;
using TMPro;
using UnityEngine;
using Random = System.Random;

namespace Rooms
{
    public class RandomCustomPropertyGenerator : MonoBehaviour
    {
        #region Property

        #endregion

        #region Private Field

        [SerializeField] private TextMeshProUGUI CharacterSelectionUI;
        private readonly Hashtable customProperties = new Hashtable();
        private CharacterPick currentCharacter = CharacterPick.Maria;
        

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
            currentCharacter = currentCharacter == CharacterPick.Maria ? CharacterPick.Soap : CharacterPick.Maria;

            CharacterSelectionUI.text = currentCharacter.ToString();
            customProperties["RandomNumber"] = currentCharacter.ToString();
            PhotonNetwork.SetPlayerCustomProperties(customProperties);

        }

        #endregion
    }
}
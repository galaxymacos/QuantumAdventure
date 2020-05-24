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
        private Hashtable customProperties = new Hashtable();
        private CharacterSelection currentCharacter = CharacterSelection.Maria;
        

        #endregion

        #region MonoBehavior Callback

    

        #endregion

        #region Public Method

        public void OnClick_GenerateNumber()
        {
            SwitchCharacter();
        }

        public void SwitchCharacter()
        {
            currentCharacter = currentCharacter == CharacterSelection.Maria ? CharacterSelection.Soap : CharacterSelection.Maria;

            CharacterSelectionUI.text = currentCharacter.ToString();
            customProperties["RandomNumber"] = currentCharacter.ToString();

            PhotonNetwork.SetPlayerCustomProperties(customProperties);

        }

        #endregion

        #region Private Method

        #endregion
    }
}

public enum CharacterSelection{
    Maria,
    Soap
}

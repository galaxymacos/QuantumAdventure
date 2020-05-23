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

        [SerializeField] private TextMeshProUGUI randomNumberUI;
        private Hashtable customProperties = new Hashtable();


        #endregion

        #region MonoBehavior Callback

    

        #endregion

        #region Public Method

        public void OnClick_GenerateNumber()
        {
            SetRandomNumber();
        }

        public void SetRandomNumber()
        {
            Random random = new Random();
            int randomNumber = random.Next(0, 100);
            randomNumberUI.text = randomNumber.ToString();
            customProperties["RandomNumber"] = randomNumber;

            PhotonNetwork.SetPlayerCustomProperties(customProperties);

        }

        #endregion

        #region Private Method

        #endregion
    }
}

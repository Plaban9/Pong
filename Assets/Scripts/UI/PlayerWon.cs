using Configuration.PlayerConfiguration;

using Data.PersistData;

using Managers;

using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEngine;

public class PlayerWon : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI text = null;

    [SerializeField]
    private PlayerConfiguration _playerConfiguration;
    // Start is called before the first frame update

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
        int playerIndex = PersistData.PlayerWonIndex;

        if (playerIndex != -1)
        {
            text.text = "PLAYER " + (playerIndex + 1);
            text.color = _playerConfiguration.playerAttributes[playerIndex].playerColour;
        }
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}

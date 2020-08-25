using UnityEngine;

public class Ladder : MonoBehaviour
{
    private enum LadderParts { TOP, MIDDLE, BOTTOM };

    [SerializeField] LadderParts part = LadderParts.MIDDLE;

    private void OnTriggerEnter2D(Collider2D _collision)
    {
        if (_collision.GetComponent<PlayerController>())
        {
            PlayerController player = _collision.GetComponent<PlayerController>();

            switch (part)
            {
                case LadderParts.TOP:
                    player.TopLadder = true;
                    break;

                case LadderParts.MIDDLE:
                    player.CanClimb = true;
                    player.Ladder = this;
                    break;

                case LadderParts.BOTTOM:
                    player.BottomLadder = true;
                    break;

                default:
                    break;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D _collision)
    {
        if (_collision.GetComponent<PlayerController>())
        {
            PlayerController player = _collision.GetComponent<PlayerController>();

            switch (part)
            {
                case LadderParts.MIDDLE:
                    player.CanClimb = false;
                    break;

                case LadderParts.BOTTOM:
                    player.BottomLadder = false;
                    break;

                case LadderParts.TOP:
                    player.TopLadder = false;
                    break;

                default:
                    break;
            }
        }
    }
}

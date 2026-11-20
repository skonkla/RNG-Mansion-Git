using DefaultNamespace;
using UnityEngine;

public class UpBonusTime : MonoBehaviour, IInteractable
{
    public string InteractMessage => objectInteractMessage;

    [SerializeField]
    string objectInteractMessage;

    public RoomManager roomManager;
    public UpgradeManager upgradeManager;
    public ReachTool reachTool;

    void Upgrade()
    {
        if (roomManager.upgradeAvailable)
        {
            upgradeManager.upBonusTime += 1;
            upgradeManager.totalUpgrades += 1;
            roomManager.upgradeAvailable = false;
            reachTool.RemoveText();
            roomManager.unlocked = true;
        }
    }

    public void Interact()
    {
        Upgrade();
    }
}

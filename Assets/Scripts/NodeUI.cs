using UnityEngine;
using UnityEngine.UI;


public class NodeUI : MonoBehaviour
{
    public GameObject ui;

    public Text upgradeCost;
    public Button upgradeButton;

    private Node target;

    public Text sellAmount;

    public void SetTarget (Node _target)
    {
        target = _target;

        transform.position = target.GetBuildPosition();

        if (!target.isUpgraded)
        {
            upgradeCost.text = "$" + target.turretBlueprint.upgradeCost;
            upgradeButton.interactable = true;
        } else
        {
            upgradeCost.text = "DONE";
            upgradeButton.interactable = false;
        }

        if (!target.isUpgraded)
        {
            sellAmount.text = "$" + target.turretBlueprint.GetSellAmount();
        } else
        {
            sellAmount.text = "$" + target.turretBlueprint.GetSellAmountUpgraded();
        }

        ui.SetActive(true);
    }

    public void Hide()
    {
        ui.SetActive(false);
    }

    public void Upgrade ()
    {
        target.UpgradeTurret();
        BuildManager.instance.DeselectNode();
    }

    public void Sell ()
    {
        if (target.isUpgraded)
        {
            target.SellTurretUpgraded();
        } else
        {
            target.SellTurret();
        }
        BuildManager.instance.DeselectNode();
        target.isUpgraded = false;
    }
}

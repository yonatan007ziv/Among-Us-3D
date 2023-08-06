using System;

public enum HardCodedConditions
{
    NoCondition,
    NameFieldNotEmpty,
    IpFieldNotEmpty
}

[Serializable]
public class Condition
{
    public HardCodedConditions condition;

    public bool Met()
    {
        switch (condition)
        {
            case HardCodedConditions.NoCondition:
                return true;
            case HardCodedConditions.NameFieldNotEmpty:
                return UserDataManager.Instance.Username != "";
            case HardCodedConditions.IpFieldNotEmpty:
                return UserDataManager.Instance.Ip != "";
        }
        throw new InvalidOperationException();
    }
}

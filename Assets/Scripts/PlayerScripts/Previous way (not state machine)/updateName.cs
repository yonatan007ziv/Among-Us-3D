using UnityEngine;

public class updateName : MonoBehaviour
{
    public void updateNameString(string newName)
    {
        nameSingleton.currentName = newName;
    }
}

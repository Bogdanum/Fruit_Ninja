using UnityEngine;

[CreateAssetMenu(fileName = "StorageProvider", menuName = "ScriptableObjects/StorageSettings/StorageProvider")]
public class StorageProvider : ScriptableObject
{
    [SerializeField] private StorageEnums.StorageLocation storageLocation;
    
    public IStorage GetStorage()
    {
        switch (storageLocation)
        {
            case StorageEnums.StorageLocation.PlayerPrefs:
            {
                return new PlayerPrefsStorage();
            }
            default: 
                return new PlayerPrefsStorage();
        }
    }
}

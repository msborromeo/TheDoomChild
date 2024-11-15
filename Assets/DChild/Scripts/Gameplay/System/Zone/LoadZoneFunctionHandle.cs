using DChild.Gameplay;
using DChild.Gameplay.Systems;
using DChild.Gameplay.Systems.Serialization;
using DChild.Menu;
using Holysoft.Event;
using Sirenix.Utilities;

public class LoadZoneFunctionHandle
{
    private LocationData m_locationData;
    private Character m_character;

    private Cache<LoadZoneFunctionHandle> m_cacheVersion;

    private WorldType m_startingWorldType;

    public LoadZoneFunctionHandle()
    {
        m_locationData = null;
        m_character = null;
    }

    public void Initialize(LocationData locationData, Character character, Cache<LoadZoneFunctionHandle> cacheVersion)
    {
        m_locationData = locationData;
        m_character = character;
        m_cacheVersion = cacheVersion;
        LoadingHandle.SceneDone += TeleportCharacter;

        m_startingWorldType = GameplaySystem.GetCurrentWorldType();
    }

    private void TeleportCharacter(object sender, EventActionArgs eventArgs)
    {
        //if(m_startingWorldType == GameplaySystem.GetCurrentWorldType())
        //{
        //    Debug.Log(m_locationData.position);
        //    m_character.transform.position = m_locationData.position;
        //}
        //else
        //{
        //}
        if (GameplaySystem.playerManager == null)
        {
            GameplaySystem.ForcePlayerTeleportOnSceneLoad(m_locationData.position);
        }
        else
        {
            GameplaySystem.playerManager.player.character.transform.position = m_locationData.position;
        }
        LoadingHandle.SceneDone -= TeleportCharacter;
    }

    public void CallLocationArriveEvent()
    {
        //if (GameplaySystem.GetCurrentWorldType() == m_startingWorldType)
        //{
        //    m_locationData?.CallArriveEvent(m_character);
        //}
        //else
        //{
        //}
            m_locationData?.CallArriveEvent(GameplaySystem.playerManager.player.character);

        m_cacheVersion.Release();
        m_cacheVersion = null;
    }
}

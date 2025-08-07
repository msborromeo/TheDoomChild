using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerCritAttack
{
    void SetCritConfiguration(PlayerCritStatsInfo info);
    void SetCritConfiguration(List<PlayerCritStatsInfo> info);
    void SetCritConfiguration(PlayerCritStatsInfo overheadInfo, PlayerCritStatsInfo midairForwardInfo, PlayerCritStatsInfo midairOverheadInfo, PlayerCritStatsInfo crouchInfo);
    void SetCritConfiguration(PlayerCritStatsInfo forwardInfo, PlayerCritStatsInfo overheadInfo, PlayerCritStatsInfo midairForwardInfo, PlayerCritStatsInfo midairOverheadInfo, PlayerCritStatsInfo crouchInfo);
}

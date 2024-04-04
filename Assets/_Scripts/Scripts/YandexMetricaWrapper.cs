using Agava.YandexMetrica;
using UnityEngine;

namespace DefaultNamespace
{
    public static class YandexMetricaWrapper
    {
        private const string StartEventName = "start";
        private const string LevelEventName = "lvl";

        public static void SendStartEvent()
        {
#if UNITY_EDITOR
            Debug.LogWarning("METRICA START:" + StartEventName);
            return;
#endif
            YandexMetrica.Send(StartEventName);
        }

        public static void SendLevelEventOnReach(int level)
        {
#if UNITY_EDITOR
            Debug.LogWarning("METRICA LEVEL:" + LevelEventName + level);
            return;
#endif
            YandexMetrica.Send(LevelEventName + level);
        }
    }
}
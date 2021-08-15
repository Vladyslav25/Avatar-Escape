/////////////////////////////////////////////////////////////////////////////////////////////////////
//
// Audiokinetic Wwise generated include file. Do not edit.
//
/////////////////////////////////////////////////////////////////////////////////////////////////////

#ifndef __WWISE_IDS_H__
#define __WWISE_IDS_H__

#include <AK/SoundEngine/Common/AkTypes.h>

namespace AK
{
    namespace EVENTS
    {
        static const AkUniqueID PLAY_AMBIENCE_WATER = 2546603034U;
        static const AkUniqueID PLAY_AMBIENCE_WIND = 1913801107U;
        static const AkUniqueID PLAY_ENEMY_APPEARS_KISS = 3218283006U;
        static const AkUniqueID PLAY_ENEMY_APPEARS_NORMAL = 1637247213U;
        static const AkUniqueID PLAY_ENEMY_APPEARS_PIG = 4198696536U;
        static const AkUniqueID PLAY_ENEMY_APPEARS_SPAN = 2108511140U;
        static const AkUniqueID PLAY_ENEMY_GOTHIT = 3822898220U;
        static const AkUniqueID PLAY_JINGE_WIN = 3838663630U;
        static const AkUniqueID PLAY_JINGLE_LOSE = 1338643067U;
        static const AkUniqueID PLAY_LEVEL_MUSIC = 557932600U;
        static const AkUniqueID PLAY_MUSIC_LIFE_FULL = 2144505736U;
        static const AkUniqueID PLAY_MUSIC_LIFE_LOW = 800370703U;
        static const AkUniqueID PLAY_MUSIC_LIFE_MED = 1169331431U;
        static const AkUniqueID PLAY_WOOD_BREAK = 1136267813U;
        static const AkUniqueID STOP_MUSIC_LIFE_FULL = 759250546U;
        static const AkUniqueID STOP_MUSIC_LIFE_LOW = 3241769353U;
        static const AkUniqueID STOP_MUSIC_LIFE_MED = 3677840433U;
    } // namespace EVENTS

    namespace STATES
    {
        namespace PLAYER_LIFE
        {
            static const AkUniqueID GROUP = 3762137787U;

            namespace STATE
            {
                static const AkUniqueID LOW_LIFE = 2791626534U;
                static const AkUniqueID MAX_LIFE = 1497871086U;
                static const AkUniqueID MID_LIFE = 2694672098U;
                static const AkUniqueID NONE = 748895195U;
                static const AkUniqueID ZERO_LIFES = 894548483U;
            } // namespace STATE
        } // namespace PLAYER_LIFE

    } // namespace STATES

    namespace GAME_PARAMETERS
    {
        static const AkUniqueID HAND_MOVEMENT = 3744619702U;
    } // namespace GAME_PARAMETERS

    namespace BANKS
    {
        static const AkUniqueID INIT = 1355168291U;
        static const AkUniqueID MAIN = 3161908922U;
    } // namespace BANKS

    namespace BUSSES
    {
        static const AkUniqueID ENEMIES = 2242381963U;
        static const AkUniqueID ENVIRONMENT = 1229948536U;
        static const AkUniqueID MASTER_AUDIO_BUS = 3803692087U;
        static const AkUniqueID MUSIC = 3991942870U;
        static const AkUniqueID PLAYER = 1069431850U;
        static const AkUniqueID SHIP = 284967655U;
        static const AkUniqueID UI = 1551306167U;
    } // namespace BUSSES

    namespace AUDIO_DEVICES
    {
        static const AkUniqueID NO_OUTPUT = 2317455096U;
        static const AkUniqueID SYSTEM = 3859886410U;
    } // namespace AUDIO_DEVICES

}// namespace AK

#endif // __WWISE_IDS_H__

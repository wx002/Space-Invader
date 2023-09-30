using System;

namespace SpaceInvaders
{
    class ResetLevelEvent: Command
    {
        public override void Run(float deltatime)
        {
            TimeEventManager.PauseUpdate(3);
            AlienFactory.ResetGrid(50, 650, SpriteBatchNode.GroupTypes.Aliens, SpriteBatchNode.GroupTypes.Boxes);

            //Shields resets
            ShieldFactory.ResetShield(GameObject.GOName.Shield1, 75, 150);
            ShieldFactory.ResetShield(GameObject.GOName.Shield2, 375, 150);
            ShieldFactory.ResetShield(GameObject.GOName.Shield3, 675, 150);
            ShieldFactory.ResetShield(GameObject.GOName.Shield4, 975, 150);

            GameSession.ResetAlienCounter();
            TimeEventManager.PauseUpdate(3);


        }
    }
}

using System;

namespace SpaceInvaders
{
    class SceneTransitionEvent: Command
    {
        private SceneContext.SceneTypes type;
        public SceneTransitionEvent(SceneContext.SceneTypes type)
        {
            this.type = type;
        }

        public override void Run(float deltatime)
        {
            if(this.type == SceneContext.SceneTypes.GameOver)
            {
                SceneContext.SetSceneState(SceneContext.SceneTypes.GameOver);
            }
            else
            {
                SceneContext.GetSceneState().Handle();
            }
        }
    }
}

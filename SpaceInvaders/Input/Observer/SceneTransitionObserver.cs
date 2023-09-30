using System.Diagnostics;
namespace SpaceInvaders
{
    class SceneTransitionObserver : InputObserver
    {
        public SceneContext.SceneTypes type;

        public void setSceneTo(SceneContext.SceneTypes t)
        {
            this.type = t;
        }
        public override void Notify()
        {
            SceneContext.SetSceneState(type);
        }

        public override void Print()
        {
            //Nothing
        }
    }
}

using System.Diagnostics;

namespace SpaceInvaders
{
    class RemoveAlienObserver : CollisionObserver
    {
        private GameObject alien;

        public RemoveAlienObserver()
        {
            this.alien = null;
        }

        public RemoveAlienObserver(RemoveAlienObserver obs)
        {
            Debug.Assert(obs != null);
            this.alien = obs.alien;
        }
        public override void Notify()
        {
            this.alien = (AlienBase)this.collisionSubject.subject2;
            Debug.Assert(alien != null);
            if(alien.name == GameObject.GOName.UFO)
            {
                SoundEngine.EndSound(SoundEngine.Sound.UFO);
                if(GameSession.GetAliensAlive() > 0)
                {
                    TimeEventManager.Add(TimeEvent.Event.UFOEvent, new SpawnUFOEvent(), GameSession.random.Next(3, 5));
                }
            }
            if(this.alien.isDead == false)
            {
                this.alien.isDead = true;
                RemoveAlienObserver obs = new RemoveAlienObserver(this);
                DelayObjectManager.AddObserver(obs);
            }

        }

        public override void Execute()
        {

            GameObject objA = (GameObject)this.alien;
            GameObject objB = (GameObject)CompositeIteratorForward.GetParentNode(objA);


            GameSession.UpdateScore(objA.spriteName);
            GameSession.UpdateAlienCount();
            objA.Remove();
            if (checkParent(objB) == true) //true when all the child nodes are deleted, so we can delete the column
            {
                objB.Remove();
            }
            if(GameSession.GetAliensAlive() == 0)
            {
                TimeEventManager.Add(TimeEvent.Event.Reset, new ResetLevelEvent(), 1.0f);
                
            }

        }

        private bool checkParent(GameObject obj)
        {
            //If no child, return true
            GameObject gObj = (GameObject)CompositeIteratorForward.GetChildNode(obj);
            if(gObj == null)
            {
                return true;
            }
            return false;
        }

        public override void Print()
        {
            Debug.Assert(false);
        }
    }
}

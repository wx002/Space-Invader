using System.Diagnostics;

namespace SpaceInvaders
{
    class RemoveUFOObserver: CollisionObserver
    {
        private GameObject ufo;

        public RemoveUFOObserver()
        {
            this.ufo = null;
        }

        public RemoveUFOObserver(RemoveUFOObserver obs)
        {
            Debug.Assert(obs != null);
            this.ufo = obs.ufo;
        }
        public override void Notify()
        {
            
            
            this.ufo = (UFO)collisionSubject.subject1;
            if (this.ufo != null)
            {
                //Debug.Assert(ufo != null);

                if (this.ufo.isDead == false)
                {
                    this.ufo.isDead = true;
                    RemoveUFOObserver obs = new RemoveUFOObserver(this);
                    DelayObjectManager.AddObserver(obs);
                }
            }

        }

        public override void Execute()
        {
            SoundEngine.EndSound(SoundEngine.Sound.UFO);
            GameObject objA = (GameObject)this.ufo;
            GameObject objB = (GameObject)CompositeIteratorForward.GetParentNode(objA);
            objA.Remove();
            if (checkParent(objB) == true) //true when all the child nodes are deleted, so we can delete the column
            {
                objB.Remove();
            }
            if (GameSession.GetAliensAlive() > 0)
            {
                TimeEventManager.Add(TimeEvent.Event.UFOEvent, new SpawnUFOEvent(), new System.Random().Next(10, 20));
            }
        }

        private bool checkParent(GameObject obj)
        {
            //If no child, return true
            GameObject gObj = (GameObject)CompositeIteratorForward.GetChildNode(obj);
            if (gObj == null)
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

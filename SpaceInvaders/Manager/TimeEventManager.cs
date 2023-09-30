using System.Diagnostics;

namespace SpaceInvaders
{
    class TimeEventManager:Manager
    {

        private readonly TimeEvent timeEvent;
        private static TimeEventManager eventManager;
        protected float currentTime;
        private static TimeEventManager activeTimeEventManager;


        public TimeEventManager(int reserveSize = 5, int growthSize = 2):base(new DList(), new DList(), reserveSize, growthSize)
        // LTN - TimeEventManager
        {
            // LTN - TimeEventManager
            timeEvent = new TimeEvent();
            activeTimeEventManager = null;

        }

        public static void SetActiveTimeEventManager(TimeEventManager t)
        {
            Debug.Assert(t != null);
            TimeEventManager.activeTimeEventManager = t;
        }

        public static void CreateTimeEventManager(int reserveSize, int growthSize)
        {
            Debug.Assert(reserveSize > 0);
            Debug.Assert(growthSize > 0);

            Debug.Assert(eventManager == null);
            if(eventManager == null)
            {
                // LTN - TimeEventManager
                eventManager = new TimeEventManager(reserveSize, growthSize);
            }
        }

        public void Destroy()
        {
            Debug.Assert(eventManager != null);
            TimeEventManager t = GetTimeEventManager();
            t = null;
            Debug.Assert(t == null);
        }

        public static TimeEvent Add(TimeEvent.Event name, Command cmd, float startTime)
        {
            TimeEventManager eventMan = TimeEventManager.activeTimeEventManager;
            Debug.Assert(eventMan != null);

            TimeEvent eventNode = AddByPriority(name, cmd, startTime);
            Debug.Assert(eventNode != null);
            return eventNode;
        }

        public static void ExcuteTimeEvents(float totalTime)
        {
            TimeEventManager eventMan = TimeEventManager.activeTimeEventManager;
            Debug.Assert(eventMan != null);

            eventMan.currentTime = totalTime;

            Iterator itr = eventMan.ManagerGetActiveIterator();
            Debug.Assert(itr != null);

            TimeEvent walk = (TimeEvent)itr.First();
            TimeEvent walkNext = null;


            while (!itr.IsDone())
            {
                walkNext = (TimeEvent)itr.Next();
                if (eventMan.currentTime >= walk.startTime)
                {
                    Debug.Assert(walk.GetCommand() != null);
                    walk.Run();
                    eventMan.ManagerRemove(walk);
                }
                else
                {
                    break;
                }

                walk = walkNext;
            }
            
        }

        public static float getCurrentTime()
        {
            TimeEventManager eventMan = TimeEventManager.activeTimeEventManager;
            Debug.Assert(eventMan != null);
            return eventMan.currentTime;
        }

        public static void Print()
        {
            TimeEventManager eventMan = TimeEventManager.activeTimeEventManager;
            Debug.Assert(eventMan != null);

            eventMan.ManagerPrint();
        }

        private static TimeEvent AddByPriority(TimeEvent.Event name, Command cmd, float intervalTime)
        {
            TimeEventManager eventMan = TimeEventManager.activeTimeEventManager;
            Debug.Assert(eventMan != null);
            Debug.Assert(cmd != null);

            Iterator iter = eventMan.ManagerGetActiveIterator();
            TimeEvent compare = (TimeEvent)iter.First();
            TimeEvent compareNext;
            TimeEvent eventNodeToAdd;
            while (!iter.IsDone())
            {
                compareNext = (TimeEvent)compare.pNext;
                if(intervalTime <= compare.intervalTime)
                {
                    eventNodeToAdd = (TimeEvent)eventMan.ManagerAddFront();
                    eventNodeToAdd.SetTimeEvent(name, cmd, intervalTime);
                    return eventNodeToAdd;
                }
                else if(compareNext != null && intervalTime > compare.intervalTime && intervalTime <= compareNext.intervalTime)
                {
                    eventNodeToAdd = (TimeEvent)eventMan.ManagerInsert(compare);
                    eventNodeToAdd.SetTimeEvent(name, cmd, intervalTime);
                    return eventNodeToAdd;
                }
                compare = (TimeEvent)iter.Next();
            }//did not get added if the loop gets done
            eventNodeToAdd = (TimeEvent)eventMan.ManagerAddEnd();
            eventNodeToAdd.SetTimeEvent(name, cmd, intervalTime);
            //Debug.WriteLine("here3");
            return eventNodeToAdd;
        }



        

        public static TimeEvent Find(TimeEvent.Event name)
        {
            TimeEventManager eventMan = TimeEventManager.activeTimeEventManager;
            Debug.Assert(eventMan != null);
            eventMan.timeEvent.name = name;
            TimeEvent tEvent = (TimeEvent)eventMan.ManagerFind(eventMan.timeEvent);
            return tEvent;
        }

        public static void PauseUpdate(float delta)
        {
            TimeEventManager pMan = TimeEventManager.activeTimeEventManager;
            Debug.Assert(pMan != null);
            Iterator pIt = pMan.ManagerGetActiveIterator();
            Debug.Assert(pIt != null);
            TimeEvent pEvent = (TimeEvent)pIt.First();
            while (!pIt.IsDone())
            {
                pEvent.startTime += delta;
                pEvent = (TimeEvent)pIt.Next();
            }
        }

        private static TimeEventManager GetTimeEventManager()
        {
            Debug.Assert(eventManager != null);
            return eventManager;
        }

        protected override bool Compare(NodeBase nodeA, NodeBase nodeB)
        {
            Debug.Assert(nodeA != null);
            Debug.Assert(nodeB != null);
            TimeEvent tA = (TimeEvent)nodeA;
            TimeEvent tB = (TimeEvent)nodeB;
            bool cmp = false;
            if (tA.name == tB.name)
            {
                cmp = true;
            }
            return cmp;
        }

        protected override NodeBase CreateNode()
        {
            // LTN - TimeEventManager
            NodeBase node = new TimeEvent();
            Debug.Assert(node != null);
            return node;
        }
    }
}

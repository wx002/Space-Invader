using System.Diagnostics;

namespace SpaceInvaders
{
    class InputController
    {
        DList inputList;
        public InputController()
        {
            this.inputList = new DList();
        }

        public InputPair AddInputControl(Azul.AZUL_KEY key, InputSubject subject, bool isHistory)
        {
            InputPair p = new InputPair(key, subject, isHistory);
            inputList.AddToFront(p);
            return p;
        }

        public void Update()
        {
            Iterator itr = inputList.GetIterator();
            InputPair walk = (InputPair)itr.First();
            while (!itr.IsDone())
            {
                walk.Update();
                walk = (InputPair)itr.Next();
            }
        }
    }
}

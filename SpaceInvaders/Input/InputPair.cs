using System.Diagnostics;

namespace SpaceInvaders
{
    class InputPair : DLink
    {
        public enum Name
        {
            Left,
            Right,
            Space
        }

        private bool current;
        private Azul.AZUL_KEY key;
        private bool prevHistory = false;
        private InputSubject inputSubject;
        private bool enableHistory;


        public InputPair(Azul.AZUL_KEY key, InputSubject subject, bool enableHistory)
        {
            this.key = key;
            this.inputSubject = subject;
            this.enableHistory = enableHistory;
        }

        public InputSubject GetInputSubject()
        {
            return this.inputSubject;
        }

        public Azul.AZUL_KEY GetAzulKey()
        {
            return this.key;
        }

        public void Update()
        {
            current = Azul.Input.GetKeyState(this.key);
            if (this.enableHistory)
            {
                if (current == true && prevHistory == false)
                {
                    this.inputSubject.Notify();
                }
                prevHistory = current;
            }
            else
            {
                if(Azul.Input.GetKeyState(key) == true)
                {
                    this.inputSubject.Notify();
                }
            }
        }

        public override void Print()
        {
            Debug.WriteLine("Input Pair");
        }
    }
}

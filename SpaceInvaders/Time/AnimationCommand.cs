using System.Diagnostics;

namespace SpaceInvaders
{
    class AnimationCommand: Command
    {
        private Sprite sprite;
        private DList dlist;
        private Iterator listIterator;

        public AnimationCommand(Sprite.SpriteName name)
        {
            sprite = SpriteManager.Find(name);
            Debug.Assert(sprite != null);


            //LTN - AnimationCommand
            dlist = new DList();
            Debug.Assert(dlist != null);

            listIterator = dlist.GetIterator();
            Debug.Assert(listIterator != null);
        }


        public void AddImage(Image.ImageName name)
        {
            Image image = ImageManager.Find(name);
            Debug.Assert(image != null);

            ImageNode imgNode = new ImageNode(image);
            dlist.AddToFront(imgNode);
            listIterator = dlist.GetIterator();
        }

        public override void Run(float deltatime)
        {
            ImageNode current = (ImageNode)listIterator.Current();
            if(listIterator.Next() == null)
            {
                listIterator.First();
            }
            sprite.SwapImage(current.GetImage());
            if (GameSession.GetAliensAlive() > 0)
            {
                TimeEventManager.Add(TimeEvent.Event.SpriteAnimation, this, deltatime);
            }
        }


    }
}

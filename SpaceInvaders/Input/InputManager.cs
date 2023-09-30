using System.Diagnostics;

namespace SpaceInvaders
{
    class InputManager
    {
        private static InputController controller;
  
       
        public static void InitController()
        {
            controller = new InputController();
            InputPair space = controller.AddInputControl(Azul.AZUL_KEY.KEY_SPACE, new InputSubject(), true);
            InputPair left = controller.AddInputControl(Azul.AZUL_KEY.KEY_ARROW_LEFT, new InputSubject(), false);
            InputPair right = controller.AddInputControl(Azul.AZUL_KEY.KEY_ARROW_RIGHT, new InputSubject(), false);
            InputPair tKey = controller.AddInputControl(Azul.AZUL_KEY.KEY_T, new InputSubject(), true);
            space.GetInputSubject().AddObserver(new ShootObserver());
            left.GetInputSubject().AddObserver(new MoverLeftObserver());
            right.GetInputSubject().AddObserver(new MoverRightObserver());
            tKey.GetInputSubject().AddObserver(new CollisionBoxToggleObserver());
        }

        public static InputController GetInputController()
        {
            Debug.Assert(controller != null);
            return controller;
        }
    }
}

using System.Diagnostics;

namespace SpaceInvaders
{
    class SampleCommand:Command
    {
        public override void Run(float deltatime)
        {
            Debug.WriteLine("{0} - Current Time: {1}", this, TimeEventManager.getCurrentTime());
            Image crabOpen = ImageManager.Find(Image.ImageName.OpenCrab);
            Image crabClose = ImageManager.Find(Image.ImageName.CloseCrab);
            ImageManager.Remove(crabOpen);
            ImageManager.Remove(crabClose);

            // get bird texture
            ImageManager.Add(Image.ImageName.OpenCrab, Texture.TextureName.Birds, 47, 41, 50, 50);
            ImageManager.Add(Image.ImageName.CloseCrab, Texture.TextureName.Birds, 243, 130, 103, 77);
            Debug.WriteLine("Image changed!");
        }
    }
}

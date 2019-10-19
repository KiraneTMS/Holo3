using System;
using System.Collections.Generic;
using Windows.ApplicationModel.Core;
using Urho;
using Urho.Actions;
using Urho.SharpReality;
using Urho.Shapes;
using Urho.Resources;

namespace HoloUrhoSharp3
{
    internal class Program
    {
        [MTAThread]
        static void Main()
        {
            var appViewSource = new UrhoAppViewSource<HelloWorldApplication>(new ApplicationOptions("Data"));
            appViewSource.UrhoAppViewCreated += OnViewCreated;
            CoreApplication.Run(appViewSource);
        }

        static void OnViewCreated(UrhoAppView view)
        {
            view.WindowIsSet += View_WindowIsSet;
        }

        static void View_WindowIsSet(Windows.UI.Core.CoreWindow coreWindow)
        {
            // you can subscribe to CoreWindow events here
        }
    }

    public class HelloWorldApplication : StereoApplication
    {
     
        Node TaringNode;

        public HelloWorldApplication(ApplicationOptions opts) : base(opts) { }

        protected override async void Start()
        {
            // Create a basic scene, see StereoApplication
            base.Start();

            // Enable input
            EnableGestureManipulation = true;
            EnableGestureTapped = true;

            // Create a node for the Earth
            TaringNode = Scene.CreateChild();
            TaringNode.Position = new Vector3(0, 0, 1.5f); //1.5m away
            TaringNode.SetScale(0.3f); //D=30cm

            // Scene has a lot of pre-configured components, such as Cameras (eyes), Lights, etc.
            DirectionalLight.Brightness = 1f;
            DirectionalLight.Node.SetDirection(new Vector3(-1, 0, 0.5f));

            //Sphere is just a StaticModel component with Sphere.mdl as a Model.
            var Taring = TaringNode.CreateComponent<StaticModel>();
            Taring.Model = ResourceCache.GetModel(@"Data/Reptilian_dwarf_01.mdl");

           
            

 
            await TextToSpeech("yeeeeeeeeeeeeeeeeeeeeeeeeet");

            // More advanced samples can be found here:
            // https://github.com/xamarin/urho-samples/tree/master/HoloLens
        }

        // For HL optical stabilization (optional)
        public override Vector3 FocusWorldPoint =>TaringNode.WorldPosition;

        //Handle input:

        Vector3 earthPosBeforeManipulations;
        public override void OnGestureManipulationStarted() => earthPosBeforeManipulations = TaringNode.Position;
        public override void OnGestureManipulationUpdated(Vector3 relativeHandPosition) =>
            TaringNode.Position = relativeHandPosition + earthPosBeforeManipulations;

        public override void OnGestureTapped() { }
        public override void OnGestureDoubleTapped() { }
    }
}
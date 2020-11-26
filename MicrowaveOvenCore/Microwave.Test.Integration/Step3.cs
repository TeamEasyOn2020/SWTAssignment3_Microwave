using Microwave.Classes.Boundary;
using Microwave.Classes.Controllers;
using Microwave.Classes.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace Microwave.Test.Integration
{
    public class Step3
    {
        Button startCancelButton;
        Button powerButton;
        Button timeButton;
        UserInterface ui;
        Door door;
        IDisplay fakeDisplay;
        Light light;
        ICookController fakeCookController;
        IOutput fakeOutput;

        [SetUp]
        public void Setup()
        {
            startCancelButton = new Button();
            powerButton = new Button();
            timeButton = new Button();
            door = new Door();
            fakeDisplay = Substitute.For<IDisplay>();
            fakeOutput = Substitute.For<IOutput>();
            light = new Light(fakeOutput);
            fakeCookController = Substitute.For<ICookController>();

            ui = new UserInterface(powerButton, timeButton, startCancelButton, door, fakeDisplay, light,
                fakeCookController);
        }

        [Test]
        public void DoorOpenedLightOn()
        {
            door.Open();

            fakeOutput.Received(1).OutputLine("Light is turned on");
        }

        [Test]
        public void DoorClosedLightOff()
        {
            door.Open();
            door.Close();

            fakeOutput.Received(1).OutputLine("Light is turned off");
        }

        [Test]
        public void CookingStartedLightOn()
        {
            powerButton.Press();
            timeButton.Press();
            startCancelButton.Press();

            fakeOutput.Received(1).OutputLine("Light is turned on");
        }

        [Test]
        public void CookingStoppedLightOff()
        {
            powerButton.Press();
            timeButton.Press();
            startCancelButton.Press();
            startCancelButton.Press();
            fakeOutput.Received(1).OutputLine("Light is turned off");
        }
    }
}
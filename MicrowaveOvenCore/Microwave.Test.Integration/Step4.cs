using Microwave.Classes.Boundary;
using Microwave.Classes.Controllers;
using Microwave.Classes.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace Microwave.Test.Integration
{
    public class Step4
    {
        Button startCancelButton;
        Button powerButton;
        Button timeButton;
        UserInterface ui;
        Door door;
        IDisplay fakeDisplay;
        Light light;
        CookController cookController;
        IPowerTube fakePowerTube;
        ITimer fakeTimer;
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
            fakePowerTube = Substitute.For<IPowerTube>();
            fakeTimer = Substitute.For<ITimer>();
            cookController = new CookController(fakeTimer, fakeDisplay, fakePowerTube);

            ui = new UserInterface(powerButton, timeButton, startCancelButton, door, fakeDisplay, light,
                cookController);
            cookController.UI = ui;
        }


        [TestCase(1, 50)]
        [TestCase(14, 700)]
        [TestCase(15, 50)]
        public void CookingStartedPowerTubeTurnOn(int powerPressed, int powerPowerReceived)
        {
            for (int i = 0; i < powerPressed; i++)
                powerButton.Press();
            timeButton.Press();
            startCancelButton.Press();


            fakePowerTube.Received(1).TurnOn(powerPowerReceived);
        }

        [Test]
        public void CookingStoppedPowerTubeTurnedOff()
        {
            powerButton.Press();
            timeButton.Press();
            startCancelButton.Press();
            startCancelButton.Press();


            fakePowerTube.Received(1).TurnOff();
        }

        [Test]
        public void CookingStoppedTimerExpiredUICookingIsDone()
        {
            powerButton.Press();
            timeButton.Press();
            startCancelButton.Press();
            fakeTimer.Expired += Raise.EventWith(System.EventArgs.Empty);


            fakeDisplay.Received(1).Clear();
        }

        [Test]
        public void CookingStoppedDoorOpenedPowerTubeTurnedOff()
        {
            powerButton.Press();
            timeButton.Press();
            startCancelButton.Press();
            door.Open();

            fakePowerTube.Received(1).TurnOff();
        }




    }
}
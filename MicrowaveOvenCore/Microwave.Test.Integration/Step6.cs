using Microwave.Classes.Boundary;
using Microwave.Classes.Controllers;
using Microwave.Classes.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace Microwave.Test.Integration
{
    public class Step6
    {
        IButton startCancelButton;
        IButton powerButton;
        IButton timeButton;
        IUserInterface ui;
        IDoor door;
        IDisplay display;
        ILight light;
        CookController cookController;
        IPowerTube powerTube;
        ITimer fakeTimer;
        IOutput fakeOutput;

        [SetUp]
        public void Setup()
        {
            startCancelButton = new Button();
            powerButton = new Button();
            timeButton = new Button();
            door = new Door();
            fakeOutput = Substitute.For<IOutput>();
            display = new Display(fakeOutput);
            light = new Light(fakeOutput);
            powerTube = new PowerTube(fakeOutput);
            fakeTimer = Substitute.For<ITimer>();
            cookController = new CookController(fakeTimer, display, powerTube);

            ui = new UserInterface(powerButton, timeButton, startCancelButton, door, display, light,
                cookController);
            cookController.UI = ui;
        }

        [TestCase(1, 50)]
        [TestCase(14, 700)]
        [TestCase(15, 50)]
        public void StartCookingWithChosenPower(int numberPresses, int power)
        {
            for (int i = 0; i < numberPresses; i++)
            {
                powerButton.Press();
            }

            timeButton.Press();
            startCancelButton.Press();

            fakeOutput.Received(1).OutputLine($"PowerTube works with {power}");

        }


        [Test]
        public void StopCookingPowerTubeStopped()
        {


            powerButton.Press();

            timeButton.Press();
            startCancelButton.Press();
            startCancelButton.Press();
            fakeOutput.Received(1).OutputLine($"PowerTube turned off");

        }
    }
}
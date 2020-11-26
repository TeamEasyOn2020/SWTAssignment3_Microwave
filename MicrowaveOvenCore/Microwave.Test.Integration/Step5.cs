using Microwave.Classes.Boundary;
using Microwave.Classes.Controllers;
using Microwave.Classes.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace Microwave.Test.Integration
{
    public class Step5
    {
        IButton startCancelButton;
        IButton powerButton;
        IButton timeButton;
        IUserInterface ui;
        IDoor door;
        IDisplay display;
        ILight light;
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
            fakeOutput = Substitute.For<IOutput>();
            display = new Display(fakeOutput);
            light = new Light(fakeOutput);
            fakePowerTube = Substitute.For<IPowerTube>();
            fakeTimer = Substitute.For<ITimer>();
            cookController = new CookController(fakeTimer, display, fakePowerTube);

            ui = new UserInterface(powerButton, timeButton, startCancelButton, door, display, light,
                cookController);
            cookController.UI = ui;
        }

        [Test]
        public void StartCancelPressedInSetPowerDisplayCleared()
        {
            powerButton.Press();
            startCancelButton.Press();

            fakeOutput.Received(1).OutputLine("Display cleared");
        }
        [Test]
        public void StartCancelPressedInCookingDisplayCleared()
        {
            powerButton.Press();
            startCancelButton.Press();

            fakeOutput.Received(1).OutputLine("Display cleared");
        }
        [Test]
        public void DoorOpenedInSetPowerDisplayCleared()
        {
            powerButton.Press();
            door.Open();


            fakeOutput.Received(1).OutputLine("Display cleared");
        }
        [Test]
        public void DoorOpenedInSetTimeDisplayCleared()
        {
            powerButton.Press();
            timeButton.Press();
            door.Open();


            fakeOutput.Received(1).OutputLine("Display cleared");
        }
        [Test]
        public void DoorOpenedInCookingDisplayCleared()
        {
            powerButton.Press();
            timeButton.Press();
            door.Open();


            fakeOutput.Received(1).OutputLine("Display cleared");
        }
        [Test]
        public void CookingIsDoneInCookingDisplayCleared()
        {
            powerButton.Press();
            timeButton.Press();
            startCancelButton.Press();
            fakeTimer.Expired += Raise.EventWith(System.EventArgs.Empty);

            fakeOutput.Received(1).OutputLine("Display cleared");
        }
        [TestCase(1, 1, 50)]
        [TestCase(14, 1, 700)]
        [TestCase(15, 2, 50)]
        public void PowerButtonPressedDisplayShowPower(int numberPresses, int received, int power)
        {
            for (int i = 0; i < numberPresses; i++)
            {
                powerButton.Press();
            }

            fakeOutput.Received(received).OutputLine($"Display shows: {power} W");
        }
        [TestCase(1, 1)]
        [TestCase(2, 2)]
        public void TimeButtonPressedDisplayShowTime(int numberPresses, int time)
        {
            powerButton.Press();
            for (int i = 0; i < numberPresses; i++)
            {
                timeButton.Press();
            }

            fakeOutput.Received().OutputLine($"Display shows: {time:D2}:{0:D2}");
        }
        [Test]
        public void OnTimerTickDisplayShowNewTime()
        {
            powerButton.Press();
            timeButton.Press();
            startCancelButton.Press();
            fakeTimer.TimeRemaining.Returns<int>(59);
            fakeTimer.TimerTick += Raise.EventWith(System.EventArgs.Empty);

            fakeOutput.Received().OutputLine($"Display shows: {0:D2}:{59:D2}");
        }
    }


}

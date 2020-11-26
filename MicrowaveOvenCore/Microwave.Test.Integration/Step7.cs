using Microwave.Classes.Boundary;
using Microwave.Classes.Controllers;
using Microwave.Classes.Interfaces;
using NSubstitute;
using NUnit.Framework;
using System.Threading;

namespace Microwave.Test.Integration
{
    public class Step7
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
        ITimer timer;
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
            timer = new Classes.Boundary.Timer();
            cookController = new CookController(timer, display, powerTube);

            ui = new UserInterface(powerButton, timeButton, startCancelButton, door, display, light,
                cookController);
            cookController.UI = ui;
        }

        [TestCase(1, 60)]
        [TestCase(2, 120)]
        public void StartTimerWithChosenTimeTicksDisplayedOnOutput(int numberPresses, int SecondsUntilExpiration)
        {
            powerButton.Press();
            for (int i = 0; i < numberPresses; i++)
            {
                timeButton.Press();
            }

            startCancelButton.Press();

            Thread.Sleep((SecondsUntilExpiration + 10) * 1000);
            fakeOutput.Received(SecondsUntilExpiration + numberPresses + 6).OutputLine(Arg.Any<string>());
        }
        [Test]
        public void StopTimerOutputShows()
        {
            powerButton.Press();


            timeButton.Press();


            startCancelButton.Press();
            Thread.Sleep(1100);
            startCancelButton.Press();


            Assert.AreEqual(timer.TimeRemaining, 59);
        }



    }
}
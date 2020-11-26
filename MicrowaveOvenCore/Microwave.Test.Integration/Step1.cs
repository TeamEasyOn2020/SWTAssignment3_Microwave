using Microwave.Classes.Boundary;
using Microwave.Classes.Controllers;
using Microwave.Classes.Interfaces;
using NSubstitute;
using NSubstitute.Core.Arguments;
using NUnit.Framework;

namespace Microwave.Test.Integration
{
    public class Step1
    {
        Button startCancelButton;
        Button powerButton;
        Button timeButton;
        UserInterface ui;
        IDoor fakeDoor;
        IDisplay fakeDisplay;
        ILight fakeLight;
        ICookController fakeCookController;

        [SetUp]
        public void Setup()
        {
            startCancelButton = new Button();
            powerButton = new Button();
            timeButton = new Button();
            fakeDoor = Substitute.For<IDoor>();
            fakeDisplay= Substitute.For<IDisplay>();
            fakeLight = Substitute.For<ILight>();
            fakeCookController = Substitute.For<ICookController>();

            ui = new UserInterface(powerButton, timeButton, startCancelButton, fakeDoor, fakeDisplay, fakeLight, fakeCookController);
        }


        [TestCase(1, 1,50)]
        [TestCase(14, 1,700)]
        [TestCase(15, 2,50)]
        public void PowerButtonPressedDisplayShowPower(int numberPresses, int received,int power)
        {
            for (int i = 0; i < numberPresses; i++)
            {
                powerButton.Press();
            }

            fakeDisplay.Received(received).ShowPower(power);
        }

        [Test]
        public void TimerButtonPressedNotInSetPower()
        {
            timeButton.Press();

            fakeDisplay.DidNotReceiveWithAnyArgs().ShowPower(Arg.Any<int>());
        }

        [TestCase(1, 1)]
        [TestCase(2, 2)]
        [TestCase(15, 15)]
        [TestCase(100,100)]
        public void TimerButtonPressedAfterPowerOn(int numberPresses, int time)
        {
            powerButton.Press();

            for (int i = 0; i < numberPresses; i++)
            {
                timeButton.Press();
            }

            fakeDisplay.Received(1).ShowTime(time,0);
        }
        
        [Test]
        public void StartCancelPressedInSetPower()
        {
            powerButton.Press();
            startCancelButton.Press();

            fakeDisplay.Received(1).Clear();
        }

        [Test]
        public void StartCancelPressedInSetTime()
        {
            powerButton.Press();
            timeButton.Press();
            startCancelButton.Press();

            fakeLight.Received(1).TurnOn();
        }

        [Test]
        public void StartCancelPressedInCooking()
        {
            powerButton.Press();
            timeButton.Press();
            startCancelButton.Press();
            startCancelButton.Press();

            fakeLight.Received(1).TurnOff();
        }
    }
}
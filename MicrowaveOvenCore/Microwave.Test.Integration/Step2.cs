using Microwave.Classes.Boundary;
using Microwave.Classes.Controllers;
using Microwave.Classes.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace Microwave.Test.Integration
{
    public class Step2
    {
        Button startCancelButton;
        Button powerButton;
        Button timeButton;
        UserInterface ui;
        Door door;
        IDisplay fakeDisplay;
        ILight fakeLight;
        ICookController fakeCookController;

        [SetUp]
        public void Setup()
        {
            startCancelButton = new Button();
            powerButton = new Button();
            timeButton = new Button();
            door = new Door();
            fakeDisplay = Substitute.For<IDisplay>();
            fakeLight = Substitute.For<ILight>();
            fakeCookController = Substitute.For<ICookController>();

            ui = new UserInterface(powerButton, timeButton, startCancelButton, door, fakeDisplay, fakeLight,
                fakeCookController);
        }

        [Test]
        public void DoorOpenedInReady()
        {
            door.Open();

            fakeLight.Received(1).TurnOn();
        }

        [Test]
        public void DoorClosedInReady()
        {
            door.Open();
            door.Close();
            fakeLight.Received(1).TurnOff();
        }

        [Test]
        public void DoorOpenedInSetPower()
        {
            powerButton.Press();
            door.Open();

            fakeDisplay.Received(1).Clear();
        }

        [Test]
        public void DoorOpenedInSetTime()
        {
            powerButton.Press();
            timeButton.Press();
            door.Open();

            fakeDisplay.Received(1).Clear();
        }

        [Test]
        public void DoorOpenedInCooking()
        {
            powerButton.Press();
            timeButton.Press();
            startCancelButton.Press();
            door.Open();

            fakeCookController.Received(1).Stop();
        }
    }
}
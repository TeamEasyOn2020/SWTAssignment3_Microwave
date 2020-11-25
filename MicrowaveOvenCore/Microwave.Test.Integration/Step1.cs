using Microwave.Classes.Boundary;
using Microwave.Classes.Controllers;
using Microwave.Classes.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace Microwave.Test.Integration
{
    public class Tests
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

        [Test]
        public void PowerButtonPressedDisplayShowPower()
        {
            powerButton.Press();
            fakeDisplay.Received(1).ShowPower(Arg.Any<int>());
        }

        [Test]
        public void Test2()
        {

        }
    }
}
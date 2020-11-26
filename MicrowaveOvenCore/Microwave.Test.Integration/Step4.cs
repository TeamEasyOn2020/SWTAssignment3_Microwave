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
        }


    }
}
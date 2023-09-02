using Moq;
using RepairDesk.BlazorClient.Services;
using System.Reflection;

namespace RepairDesk.BlazorClient.Tests
{
    [TestFixture]
    public class CountdownServiceTests
    {
        private CountdownService _countdownService;

        [SetUp]
        public void Setup()
        {
            _countdownService = new CountdownService();
        }

        // Test 1: Initial time is set correctly
        [Test]
        public void CountdownService_InitialTimeSetCorrectly()
        {
            // Verify that the initial time is set correctly
            var expectedTime = new TimeSpan(2, 0, 0, 0);

            Assert.That(_countdownService.TimeLeft, Is.EqualTo(expectedTime));
        }

        // Test 2: Time is updated correctly
        [Test]
        public async Task CountdownService_UpdateTimeCorrectly()
        {
            var initialTime = _countdownService.TimeLeft;

            // Simulate a delay to allow time updates to occur
            await Task.Delay(2000);

            // Verify that the time has been updated correctly and the event is raised
            Assert.That(_countdownService.TimeLeft, Is.LessThan(initialTime));
        }


        // Test 3: Event is raised when time is updated
        [Test]
        public void CountdownService_RaiseEventWhenTimeUpdated()
        {
            bool eventRaised = false;
            _countdownService.OnTimeUpdated += () => eventRaised = true;

            // Simulate the passage of time by waiting more than second
            Thread.Sleep(1500);

            // Verify that the event was raised
            Assert.IsTrue(eventRaised);
        }

    }

}
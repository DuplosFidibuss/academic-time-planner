using AcademicTimePlanner.Services.TogglApiService;
using AcademicTimePlanner.Services.TogglService;
using AcademicTimePlanner.DataMapping.Toggl;
using Moq;

namespace AcademicTimePlanner.Tests;

[TestClass]
public class TogglServiceTests
{
    private Mock<ITogglApiService> _togglApiServiceMock; 
    private TogglService _componentUnderTest;

    [TestInitialize]
    public void Initialize()
    {
        _togglApiServiceMock = new Mock<ITogglApiService>();
        _componentUnderTest = new TogglService(_togglApiServiceMock.Object);
    }

    [TestMethod]
    public async Task GetTogglEntrySum_OfOneMonth_ReturnsExpectedTogglEntrySums()
    {
        // Arrange
        var dates = new List<DateTime>()
        {
            new (2022, 01, 01),
            new (2022, 01, 02),
            new (2022, 01, 03),
        };
        
        var togglDetailResponse = new TogglDetailResponse()
        {
            Data = new List<TogglDetailResponseData>()
            {
                new() { Id = 0, TaskId = null, Duration = 100, Description = "Description", DateTime = dates[0] },
                new() { Id = 1, TaskId = null, Duration = 100, Description = "Description", DateTime = dates[0] },
                new() { Id = 2, TaskId = null, Duration = 100, Description = "Description", DateTime = dates[1] }, 
                new() { Id = 3, TaskId = null, Duration = 100, Description = "Description", DateTime = dates[2] },
            }
        };

        var expectedTogglEntrySumList = new List<TogglEntrySum>()
        {
            new (DateOnly.FromDateTime(dates[0]), 200),
            new (DateOnly.FromDateTime(dates[1]), 100),
            new (DateOnly.FromDateTime(dates[2]), 100),
        };

        _togglApiServiceMock
            .Setup(mock => mock.GetDetailsSinceAsync(It.IsAny<DateOnly>()))
            .ReturnsAsync(togglDetailResponse);
        
        // Act
        var actualTogglEntrySumList = await _componentUnderTest.GetTogglProjects(It.IsAny<DateOnly>());
        
        // Asset
        Assert.AreEqual(expectedTogglEntrySumList.Count, actualTogglEntrySumList.Count);
        for (var index = 0; index < expectedTogglEntrySumList.Count; index++)
        {
            Assert.IsTrue(expectedTogglEntrySumList[index].Date == actualTogglEntrySumList[index].Date);
            Assert.IsTrue(expectedTogglEntrySumList[index].Duration == actualTogglEntrySumList[index].Duration);
        }
    }
}
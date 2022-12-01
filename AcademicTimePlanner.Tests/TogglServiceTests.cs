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
    public async Task GetTogglProjectsReturnsAllTogglProjects()
    {
        var projectName = "Test project";
        var taskName = "Test task";
        var projectIds = new List<long>() { 1, 2 };
        var taskIds = new List<long>() { 3, 4, 5 };

        // Arrange
        var togglDetailResponse = new TogglDetailResponse()
        {
            Data = new List<TogglDetailResponseData>()
            {
                new() { Id = 0, Pid = projectIds[0], Tid = taskIds[0], Project = projectName, Task = taskName },
                new() { Id = 1, Pid = projectIds[0], Tid = taskIds[1], Project = projectName, Task = taskName },
                new() { Id = 2, Pid = projectIds[1], Tid = taskIds[2], Project = projectName, Task = taskName }, 
                new() { Id = 3, Pid = projectIds[1], Tid = taskIds[2], Project = projectName, Task = taskName },
            }
        };

        var expectedTogglProjectList = new List<TogglProject>()
        {
            new (projectIds[0], projectName),
            new (projectIds[1], projectName),
        };

        _togglApiServiceMock
            .Setup(mock => mock.GetDetailsSinceAsync(It.IsAny<DateOnly>()))
            .ReturnsAsync(togglDetailResponse);
        
        // Act
        var actualTogglProjectList = await _componentUnderTest.GetTogglProjects(It.IsAny<DateOnly>());
        
        // Assert
        Assert.AreEqual(expectedTogglProjectList.Count, actualTogglProjectList.Count);
        for (var index = 0; index < expectedTogglProjectList.Count; index++)
        {
            Assert.AreEqual(expectedTogglProjectList[index].TogglId, actualTogglProjectList[index].TogglId);
            Assert.AreEqual(expectedTogglProjectList[index].Name, projectName);
        }
    }

    [TestMethod]
    public async Task TestReceiveNormalDataWithoutTask()
    {
        var projectName = "Test project";
        var projectIds = new List<long>() { 1, 2, 3 };

        // Arrange
        var togglDetailResponse = new TogglDetailResponse()
        {
            Data = new List<TogglDetailResponseData>()
            {
                new() { Id = 0, Pid = projectIds[0], Project = projectName },
                new() { Id = 1, Pid = projectIds[1], Project = projectName },
                new() { Id = 2, Pid = projectIds[2], Project = projectName },
            }
        };

        var expectedTogglProjectList = new List<TogglProject>()
        {
            new (projectIds[0], projectName),
            new (projectIds[1], projectName),
            new (projectIds[2], projectName),
        };

        _togglApiServiceMock
            .Setup(mock => mock.GetDetailsSinceAsync(It.IsAny<DateOnly>()))
            .ReturnsAsync(togglDetailResponse);

        // Act
        var actualTogglProjectList = await _componentUnderTest.GetTogglProjects(It.IsAny<DateOnly>());

        // Assert
        Assert.AreEqual(expectedTogglProjectList.Count, actualTogglProjectList.Count);
        for (var index = 0; index < expectedTogglProjectList.Count; index++)
        {
            Assert.AreEqual(expectedTogglProjectList[index].TogglId, actualTogglProjectList[index].TogglId);
            Assert.AreEqual(expectedTogglProjectList[index].Name, projectName);
        }
    }

    [TestMethod]
    public async Task TestReceiveNormalDataWithTask()
    {
        var projectName = "Test project";
        var taskName = "Test task";
        var projectIds = new List<long>() { 0, 1, 2 };
        var taskIds = new List<long>() { 3, 4, 5 };

        // Arrange
        var togglDetailResponse = new TogglDetailResponse()
        {
            Data = new List<TogglDetailResponseData>()
            {
                new() { Id = 0, Pid = projectIds[0], Tid = taskIds[0], Project = projectName, Task = taskName },
                new() { Id = 1, Pid = projectIds[1], Tid = taskIds[1], Project = projectName, Task = taskName },
                new() { Id = 2, Pid = projectIds[2], Tid = taskIds[2], Project = projectName, Task = taskName },
            }
        };

        var expectedTogglProjectList = new List<TogglProject>()
        {
            new (projectIds[0], projectName),
            new (projectIds[1], projectName),
            new (projectIds[2], projectName),
        };

        int i = 0;
        foreach (var togglProject in expectedTogglProjectList)
        {
            togglProject.AddTogglTask(taskIds[i], taskName);
            i++;
        }

        _togglApiServiceMock
            .Setup(mock => mock.GetDetailsSinceAsync(It.IsAny<DateOnly>()))
            .ReturnsAsync(togglDetailResponse);

        // Act
        var actualTogglProjectList = await _componentUnderTest.GetTogglProjects(It.IsAny<DateOnly>());

        // Assert
        Assert.AreEqual(expectedTogglProjectList.Count, actualTogglProjectList.Count);
        for (var index = 0; index < expectedTogglProjectList.Count; index++)
        {
            Assert.AreEqual(expectedTogglProjectList[index].TogglId, actualTogglProjectList[index].TogglId);
            Assert.AreEqual(expectedTogglProjectList[index].Name, projectName);
            Assert.AreEqual(expectedTogglProjectList[index].Tasks.Count, actualTogglProjectList[index].Tasks.Count);
        }
    }

    [TestMethod]
    public async Task TestReceiveDataWithoutEntries()
    {
        var projectName = "Test project";
        var projectIds = new List<long>() { 0, 1, 2 };

        // Arrange
        var togglDetailResponse = new TogglDetailResponse()
        {
            Data = new List<TogglDetailResponseData>()
            {
                new() { Id = 0, Pid = projectIds[0], Project = projectName },
                new() { Id = 1, Pid = projectIds[1], Project = projectName },
                new() { Id = 2, Pid = projectIds[2], Project = projectName },
            }
        };

        var expectedTogglProjectList = new List<TogglProject>()
        {
            new (projectIds[0], projectName),
            new (projectIds[1], projectName),
            new (projectIds[2], projectName),
        };

        _togglApiServiceMock
            .Setup(mock => mock.GetDetailsSinceAsync(It.IsAny<DateOnly>()))
            .ReturnsAsync(togglDetailResponse);

        // Act
        var actualTogglProjectList = await _componentUnderTest.GetTogglProjects(It.IsAny<DateOnly>());

        // Assert
        Assert.AreEqual(expectedTogglProjectList.Count, actualTogglProjectList.Count);
        for (var index = 0; index < expectedTogglProjectList.Count; index++)
        {
            Assert.AreEqual(expectedTogglProjectList[index].TogglId, actualTogglProjectList[index].TogglId);
            Assert.AreEqual(expectedTogglProjectList[index].Name, projectName);
            Assert.AreEqual(expectedTogglProjectList[index].TogglEntrySums.Count, 0);
        }

    }

    [TestMethod]
    public async Task TestReceiveEntriesDataWithoutProject()
    {
        // Arrange
        var dates = new List<DateTime>()
        {
            new (2022, 01, 01),
            new (2022, 01, 02),
            new (2022, 01, 03),
        };

        var durations = new List<int>();
        durations.Add(1);
        durations.Add(2);
        durations.Add(3);


        var togglDetailResponse = new TogglDetailResponse()
        {
            Data = new List<TogglDetailResponseData>()
            {
                new() { Id = 0, Start = dates[0], End = dates[1], Dur = durations[0] },
                new() { Id = 1, Start = dates[0], End = dates[2], Dur = durations[1] },
                new() { Id = 2, Start = dates[1], End = dates[2], Dur = durations[2] },
            }
        };

        var expectedTogglProjectList = new List<TogglProject>()
        {

        };

        _togglApiServiceMock
            .Setup(mock => mock.GetDetailsSinceAsync(It.IsAny<DateOnly>()))
            .ReturnsAsync(togglDetailResponse);

        // Act
        var actualTogglProjectList = await _componentUnderTest.GetTogglProjects(It.IsAny<DateOnly>());

        // Assert
        //There is an empty project here -> count == 1
        Assert.AreEqual(1, actualTogglProjectList.Count);
        //actualTogglProjectList is in Milliseconds -> *3600000
        Assert.AreEqual(durations.Sum(), actualTogglProjectList[0].TogglEntrySums.Sum(entry => entry.Duration) * 3600000);
    }
}
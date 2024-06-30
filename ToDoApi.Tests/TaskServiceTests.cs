using Microsoft.Extensions.Logging;
using Moq;
using ToDoApi.Constants;
using ToDoApi.Dto;
using ToDoApi.Repositories;
using ToDoApi.Services;

namespace ToDoApi.Tests.Services
{
    public class TaskServiceTests
    {
        private readonly Mock<ITaskRepository> _mockTaskRepository;
        private readonly Mock<ILogger<TaskService>> _mockLogger;
        private readonly ITaskService _taskService;

        public TaskServiceTests()
        {
            _mockTaskRepository = new Mock<ITaskRepository>();
            _mockLogger = new Mock<ILogger<TaskService>>();
            _taskService = new TaskService(_mockTaskRepository.Object, _mockLogger.Object);
        }

        private List<TaskDto> GenerateTasks(int count, Status status)
        {
            var tasks = new List<TaskDto>();
            for (int i = 1; i <= count; i++)
            {
                tasks.Add(new TaskDto { Id = i, Title = $"Task {i}", Description = $"Description {i}", Status = status });
            }
            return tasks;
        }

        [Theory]
        [InlineData("40e6c495000000000000000000000000", 3)]
        [InlineData("66ac4833000000000000000000000000", 3)]
        [InlineData("71efd582000000000000000000000000", 3)]
        public async Task GetTasksByAssigneeIdAsync_ReturnsTasks(string assigneeId, int expectedCount)
        {
            // Arrange
            var expectedTasks = GenerateTasks(3, Status.TODO);
            _mockTaskRepository.Setup(repo => repo.GetTasksByAssigneeIdAsync(assigneeId))
                               .ReturnsAsync(expectedTasks);

            // Act
            var result = await _taskService.GetTasksByAssigneeIdAsync(assigneeId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedCount, result.Count());
        }

        [Fact]
        public async Task CreateTaskAsync_ValidTask_ReturnsCreatedTask()
        {
            // Arrange
            var task = new TaskDto { Title = "Test Task", Description = "Test Description" };
            var expectedTaskId = 1; // Assuming the repository returns a task with this ID after creation
            _mockTaskRepository.Setup(repo => repo.AddTaskAsync(task))
                               .ReturnsAsync(new TaskDto { Id = expectedTaskId, Title = task.Title, Description = task.Description });

            // Act
            var result = await _taskService.CreateTaskAsync(task);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedTaskId, result.Id);
            Assert.Equal(task.Title, result.Title);
            Assert.Equal(task.Description, result.Description);
        }

        [Fact]
        public async Task DeleteTaskAsync_ExistingTask_ReturnsTrue()
        {
            // Arrange
            var existingTaskId = 1;
            _mockTaskRepository.Setup(repo => repo.DeleteTaskAsync(existingTaskId))
                               .ReturnsAsync(new TaskDto { Id = existingTaskId });

            // Act
            var result = await _taskService.DeleteTaskAsync(existingTaskId);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task DeleteTaskAsync_NonExistingTask_ReturnsFalse()
        {
            // Arrange
            var nonExistingTaskId = 999;
            _mockTaskRepository.Setup(repo => repo.DeleteTaskAsync(nonExistingTaskId))
                               .ReturnsAsync((TaskDto)null);

            // Act
            var result = await _taskService.DeleteTaskAsync(nonExistingTaskId);

            // Assert
            Assert.False(result);
        }

        [Theory]
        [InlineData(1, true)] // Assuming task with ID 1 exists
        [InlineData(999, false)] // Assuming task with ID 999 does not exist
        public void TaskExists_ReturnsCorrectResult(int taskId, bool expectedResult)
        {
            // Arrange
            _mockTaskRepository.Setup(repo => repo.TaskExists(taskId))
                               .Returns(expectedResult);

            // Act
            var result = _taskService.TaskExists(taskId);

            // Assert
            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [InlineData(Status.DONE)]
        [InlineData(Status.TODO)]
        public async Task GetTasksByStatusAsync_ReturnsTasksWithValidStatus(Status status)
        {
            // Arrange
            var tasksWithStatus = GenerateTasks(3, status);
            _mockTaskRepository.Setup(repo => repo.GetTasksByStatusAsync(status))
                               .ReturnsAsync(tasksWithStatus);

            // Act
            var result = await _taskService.GetTasksByStatusAsync(status);

            // Assert
            Assert.NotNull(result);
            Assert.All(result, task => Assert.True(task.Status == Status.DONE || task.Status == Status.TODO));
        }
    }
}

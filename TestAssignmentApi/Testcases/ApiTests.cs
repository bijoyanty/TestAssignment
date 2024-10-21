using FluentAssertions;
using TestAssignmentApi.Models;
using TestAssignmentApi.SetUp;
using TestAssignmentApi.Utilities;

namespace TestAssignmentApi.Testcases;

[TestFixture] 
[Parallelizable( ParallelScope.All )] 
public class ApiTests : ApiTestSetup
{
    [TestCase("6942201")]
    [Test, Retry( 2 )]
    public async Task GetUserDetails(string id)
    {
        //Arrange
        var userId = id;

        //Act
        var user = await ResiliencePipeline.ExecuteAsync(async _ => await ApiMethods.Get<User>( RequestContext, $"users/{userId}"));

        //Assert
        user.Name.Should().NotBeNullOrEmpty();
        user.Status.Should().NotBeNullOrEmpty();
        user.Gender.Should().NotBeNullOrEmpty();
    }
}
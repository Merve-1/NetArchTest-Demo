using ArcTestsApis.Controllers;
using ArcTestsData.Entities;
using ArcTestsData.Interfaces;
using FluentAssertions;
using NetArchTest.Rules;
using NetArchTest.Rules.Policies;

namespace ArcTests;
//Architecture Tests using NetArchTest
//These tests enforce the 3-tier contract:
//  ArcTestApis => ArcTestsService => ArcTestsData
//  The API must never bypass the service layer to talk directly to repositories 
//      Entities must stay internal to the data layer.
//  All Repositories must live in the correct namespace and implement IRepository 
[TestFixture]
public class ArchitectureTests
{
    //1. Entity Access Rules 
    [Test]
    [Description(("Entities in ArcTestData.Entities that inherit from Entity must not be public"))]

    public void Entities_ShouldNotBe_Public()
    {
        var result = Types
            .InAssembly(typeof(Entity).Assembly)
            .That()
            .ResideInNamespace("ArcTestData.Entities")
            .And()
            .Inherit(typeof(Entity))
            .Should()
            .NotBeAbstract()
            .GetResult();
        result.IsSuccessful.Should().BeTrue(
            because: "All entities in ArcTestsData.Entities must be internal," +
                     "the API should never directly access entity classes.");

    }

    //2. Repository Namespace Rules 
    [Test]
    [Description("Any class implementing IRepository<>must live in ArcTestData.Repository")]
    public void Repository_ShouldImplement_InRepositoriesNamespace()
    {
        var result = Types
            .InAssembly(typeof(Entity).Assembly)
            .That()
            .ImplementInterface(typeof(IRepository<>))
            .Should()
            .ResideInNamespace("ArcTestData.Repositories")
            .GetResult();
        result.IsSuccessful.Should().BeTrue(
            because: "All IRepository<> implementations must live in ArcTestsData.Repositories.");
    }

    [Test]
    [Description("Classes in ArcTestData.Repositories must implement IRepository<> and be named *Repository.")]
    public void RepositoryClasses_ShouldImplement_IRepositoryAndHaveCorrectName()
    {
        var result = Types
            .InAssembly(typeof(Entity).Assembly)
            .That()
            .ResideInNamespace("ArcTestData.Repositories")
            .And()
            .AreClasses()
            .Should()
            .ImplementInterface(typeof(IRepository<>))
            .And()
            .HaveNameEndingWith("Repository")
            .GetResult();
        result.IsSuccessful.Should().BeTrue(
            because: "Repository classes must implement IRepository<> and follow the naming convention *Repository.");
    }

    //3. Layer Boundary Rules 
    [Test]
    [Description("Types in ArcTestsApis namespace should not directly depend on ArcTestsData.Repositories.")]
    public void ApiControllers_ShouldNot_DependDirectlyOn_DataRepositories()
    {
        //We test the controllers namespace specifically, Program.cs (Composition root)
        //is allowed to reference ArcTestsData for DI registration
        var result = Types
            .InAssembly(typeof(ProductsController).Assembly)
            .That()
            .ResideInNamespace("ArcTestsApis.Controllers")
            .ShouldNot()
            .HaveDependencyOn("ArcTestsData.Repositories")
            .GetResult();
        result.IsSuccessful.Should().BeTrue(
            because:"API Controllers must never reference the Repository layer directly. " +
                    "They must go through the service layer. ");
    }

    [Test]
    [Description("Only types in ArcTestsServices may have a dependency on ArcTestsData.Repositories.")]
    public void OnlyServices_ShouldHaveDependencyOn_DataRepositories()
    {
        //any type that depends on ArcTestsData.Repositories must live in ArcTestsServices
        var result = Types
            .InAssembly(typeof(ProductsController).Assembly)
            .That()
            .HaveDependencyOn("ArcTestsData.Repositories")
            .Should()
            .ResideInNamespace("ArcTestsServices")
            .GetResult();
        result.IsSuccessful.Should().BeTrue(
            because: "Only the service layer is allowed to depend on ArcTestsData.Repositories.");

    }

    //4. Policy-style combined tests 
    //combines entity accessor, repository interface, and namespace rules 
    //into a single parameterized NUnit test
    public static readonly PolicyDefinition dataLayerPolicy=
        Policy.Define("DAta Layer design Policy", "A policy to ensure data layer is enforced")
            .For(Types.InAssembly(typeof(IRepository<>).Assembly))
            .Add(
                t => t.That()
                    .ResideInNamespace("ArcTestsData.Entities")
                    .And()
                    .Inherit(typeof(Entity))
                    .Should()
                    .NotBePublic(),
                "Enforcing entities namespace",
                "All models that inherit from Entity and reside in ArcTestsData.Entities should not be public")
            .Add(t =>
                    t.That()
                        .ResideInNamespace("ArcTestsData.Repositories")
                        .Should()
                        .ImplementInterface(typeof(IRepository<>))
                        .And()
                        .HaveNameEndingWith("Repository"),
                "Repositories Should Implement IRepository",
                "All repositories that reside in ArcTestsData.Repositories should implement IRepository<>"
            )
            .Add(t =>
                    t.That()
                        .HaveNameEndingWith("Repository")
                        .Or()
                        .ImplementInterface(typeof(IRepository<>))
                        .And()
                        .AreClasses()
                        .Should()
                        .ResideInNamespace("ArcTestsData.Repositories"),
                "All repositories should live in the repository namespace",
                "All repositories should live in the repository namespace"
            );

    private static readonly PolicyDefinition dataLayerAccessPolicy =
        Policy.Define("Data Layer access policy", "A policy to ensure data layer access is enforced")
            .For(Types.InAssembly(typeof(IRepository<>).Assembly))
            .Add(t => t.That()
                    .ResideInNamespace("ArcTestsApis.Controllers")
                    .ShouldNot()
                    .HaveDependencyOn("ArcTestsData.Repositories"),
                "Enforcing data layer access",
                "Enforcing data layer access");

    public static readonly PolicyDefinition[] policies =
    {
        dataLayerPolicy,
        dataLayerAccessPolicy
    };

    [TestCaseSource(nameof(policies))]
    public void Evalute_DataLayerPolicy(PolicyDefinition policy)
    {
        var policyResult = policy.Evaluate();
        foreach (var result in policyResult.Results)
        {
            result.IsSuccessful.Should().BeTrue(
                because: $"Policy '{result.Name}' failed: {result.Description}");
        }
    }
}
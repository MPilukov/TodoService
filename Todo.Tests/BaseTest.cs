using System.Linq;
using System.Reflection;
using Autofac.Extras.Moq;
using AutoFixture;
using AutoMapper;

namespace Todo.Tests
{
    public class BaseTest
    {
        private static string ApplicationProjectNameSpace => $"Todo.Application";

        protected BaseTest()
        {
            Mock = AutoMock.GetLoose();
            Fixture = new Fixture();
            
            Mapper = new MapperConfiguration(
                    cfg =>
                    {
                        var profiles =
                            Assembly.Load(ApplicationProjectNameSpace)
                                .GetTypes()
                                .Where(x => typeof(Profile).IsAssignableFrom(x));

                        foreach (var profile in profiles)
                        {
                            cfg.AddProfile(profile);
                        }
                    })
                .CreateMapper();
        }

        protected AutoMock Mock { get; set; }

        protected Fixture Fixture { get; private set; }

        protected IMapper Mapper { get; private set; }

        public virtual void Dispose()
        {
            Mock.Dispose();
        }
    }
}
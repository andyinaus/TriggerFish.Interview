using System;
using Autofac;
using FluentValidation;
using Shouldly;
using TriggerFish.AddressParser.WebApi.Models.v1;
using TriggerFish.AddressParser.WebApi.Models.v1.Validations;
using TriggerFish.AddressParser.WebApi.ModelValidation;
using Xunit;

namespace TriggerFish.AddressParser.WebApi.Tests.ModelValidation
{
    public class AutofactValidationFactoryTests
    {
        [Fact]
        public void Ctor_WhenContainerIsNull_ShouldThrowArgumentNullException()
        {
            Action target = () => new AutofactValidationFactory(null);

            target.ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void GetValidator_WhenTypeIsNull_ShouldThrowArgumentNullException()
        {
            var container = new ContainerBuilder().Build();

            Action target = () => GetFactory(container).GetValidator(null);

            target.ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void GetValidator_ShouldReturnCorrectValidator()
        {
            var result = GetFactory(GetTestIocContainer()).GetValidator<PostCodeLookupRequest>();

            result.ShouldNotBeNull();
            result.ShouldBeAssignableTo<IValidator<PostCodeLookupRequest>>();
            result.ShouldBeOfType<PostCodeLookupRequestValidator>();
        }

        [Fact]
        public void GetValidator_WhenTypeIsValidButNoValidatorFound_ShouldReturnNull()
        {
            var container = new ContainerBuilder().Build();

            var result = GetFactory(container).GetValidator(typeof(PostCodeLookupRequest));

            result.ShouldBeNull();
        }

        [Fact]
        public void GetValidator_WhenTypeIsValidAndValidatorFound_ShouldReturnCorrectValidator()
        {
            var result = GetFactory(GetTestIocContainer()).GetValidator(typeof(PostCodeLookupRequest));

            result.ShouldNotBeNull();
            result.ShouldBeAssignableTo<IValidator>();
            result.ShouldBeOfType<PostCodeLookupRequestValidator>();
        }

        private static IContainer GetTestIocContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<PostCodeLookupRequestValidator>()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            return builder.Build();
        }

        private static AutofactValidationFactory GetFactory(IContainer container)
        {
            return new AutofactValidationFactory(container);
        }
    }
}
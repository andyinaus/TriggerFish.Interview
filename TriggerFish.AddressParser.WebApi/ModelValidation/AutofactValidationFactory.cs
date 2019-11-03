using System;
using Autofac;
using FluentValidation;

namespace TriggerFish.AddressParser.WebApi.ModelValidation
{
    public class AutofactValidationFactory : IValidatorFactory
    {
        private readonly IContainer _container;

        public AutofactValidationFactory(IContainer container)
        {
            _container = container ?? throw new ArgumentNullException(nameof(container));
        }

        public IValidator<T> GetValidator<T>()
        {
            return (IValidator<T>)GetValidator(typeof(T));
        }

        public IValidator GetValidator(Type type)
        {
            var genericType = typeof(IValidator<>).MakeGenericType(type);

            if (_container.TryResolve(genericType, out var validator)) return (IValidator)validator;

            return null;
        }
    }
}
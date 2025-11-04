using System;
using System.Linq;
using System.Reflection;
using AutoFixture;
using AutoFixture.Kernel;
using White.Knight.Abstractions.Command;
using White.Knight.Abstractions.Navigations;
using White.Knight.Domain;
using White.Knight.Domain.Exceptions;
using White.Knight.Domain.Options;
using White.Knight.Interfaces;
using White.Knight.Tests.Abstractions.Extensions;

namespace White.Knight.Tests.Abstractions
{
    public static class SpecUtility<TD, TP, TResponse> where TD : new()
    {
        public static bool VerifyTransmutabilityOfAllSpecs(
            ICommandTranslator<TD, TResponse> commandTranslator,
            Assembly assembly
        )
        {
            var fixture = new Fixture();

            var specTypeDetails =
                assembly
                    .GetSpecs()
                    .ToList();

            var allPassed = true;

            foreach (var specTypeDetail in specTypeDetails)
            {
                try
                {
                    var spec =
                        new SpecimenContext
                            (fixture).Resolve
                            (specTypeDetail?.SpecType);

                    var command =
                        fixture
                            .Build<QueryCommand<TD, TP>>()
                            .With(o => o.NavigationStrategy, new NestedNavigations<TD>(o => o))
                            .With(o => o.Specification, spec)
                            .With(o => o.PagingOptions, (PagingOptions<TD>)null)
                            .With(o => o.ProjectionOptions, (ProjectionOptions<TD, TP>)null)
                            .Create();

                    commandTranslator
                        .Translate(command);
                }
                catch (UnparsableSpecificationException)
                {
                    allPassed = false;
                }

            }

            return allPassed;
        }

        public static object VerifyTransmutabilityOfSpec(
            ICommandTranslator<TD, TResponse> commandTranslator,
            Type specType
        )
        {
            var fixture = new Fixture();

            var specTypeDetails =
                specType
                    .GetSpecTypeDetails();

            var spec =
                new SpecimenContext
                    (fixture).Resolve
                    (specTypeDetails?.SpecType);

            var command =
                fixture
                    .Build<QueryCommand<TD, TP>>()
                    .With(o => o.NavigationStrategy, new NestedNavigations<TD>(o => o))
                    .With(o => o.Specification, spec)
                    .With(o => o.PagingOptions, (PagingOptions<TD>)null)
                    .With(o => o.ProjectionOptions, (ProjectionOptions<TD, TP>)null)
                    .Create();

            return
                commandTranslator
                    .Translate(command);
        }

        public static object VerifyTransmutabilityOfSpec(
            ICommandTranslator<TD, TResponse> commandTranslator,
            Specification<TD> specificationInstance
        )
        {
            var fixture = new Fixture();

            var command =
                fixture
                    .Build<QueryCommand<TD, TP>>()
                    .With(o => o.NavigationStrategy, new NestedNavigations<TD>(o => o))
                    .With(o => o.Specification, specificationInstance)
                    .With(o => o.PagingOptions, (PagingOptions<TD>)null)
                    .With(o => o.ProjectionOptions, (ProjectionOptions<TD, TP>)null)
                    .Create();

            return
                commandTranslator
                    .Translate(command);
        }
    }
}
﻿using System;
using System.Reflection;
using Machine.Fakes;
using Machine.Specifications;
using Machine.VSTestAdapter.Configuration;
using Machine.VSTestAdapter.Execution;
using Machine.VSTestAdapter.Specs.Fixtures;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Adapter;

namespace Machine.VSTestAdapter.Specs.Execution
{
    public abstract class With_AssemblyExecutionSetup : WithFakes
    {
        static MSpecTestAdapter Executor;
        static CompileContext compiler;
        static Assembly assembly;

        Establish context = () =>
        {
            compiler = new CompileContext();
            Executor = new MSpecTestAdapter();

            var assemblyPath = compiler.Compile(SampleFixture.Code);
            assembly = Assembly.LoadFile(assemblyPath);
        };

        Because of = () =>
            Executor.RunTests(new[] { assembly.Location }, An<IRunContext>(), The<IFrameworkHandle>());

        Cleanup after = () =>
            compiler.Dispose();
    }
}

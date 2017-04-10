using System;

namespace ConsoleApp
{
    public interface IMenuCommand
    {
        string Description { get; }
        void Execute();
    }

    public class SimpleQueryCommand : IMenuCommand
    {
        public string Description => "Simple Query";
        public void Execute()
        {
            var simpleQuery = new SimpleQuery();
            simpleQuery.TestSimpleQuery().Wait();
        }
    }


    public class ExitCommand : IMenuCommand
    {
        public string Description => "Exit";
        public void Execute() { Environment.Exit(exitCode: 0); }
    }
}



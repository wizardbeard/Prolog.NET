﻿/* Copyright © 2010 Richard G. Todd.
 * Licensed under the terms of the Microsoft Public License (Ms-PL).
 */

using System.IO;

namespace Prolog.Scheduler
{
    public class Scheduler
    {
        Program _program;
        Query _query;
        PrologMachine _machine;

        public Program Program
        {
            get
            {
                if (_program == null)
                {
                    var path = Path.Combine(Properties.Settings.Default.SamplesFolder, "schedule.prolog");

                    if (!File.Exists(path))
                    {
                        throw new FileNotFoundException(string.Format("{0} not found.  Consider updating SamplesFolder setting during program development.", path));
                    }

                    var program = Program.Load(path);
                    _program = program;
                }

                return _program;
            }
        }

        public Query Query
        {
            get
            {
                if (_query == null)
                {
                    var codeSentenceQuery = Parser.Parse(":- solve(X)")[0];
                    var query = new Query(codeSentenceQuery);
                    _query = query;
                }

                return _query;
            }
        }

        public PrologMachine Machine
        {
            get
            {
                if (_machine == null)
                {
                    var machine = PrologMachine.Create(Program, Query);
                    _machine = machine;
                }
                return _machine;
            }
        }

        public void Restart()
        {
            var machine = Machine;
            machine.Restart();
        }

        public Schedule Execute()
        {
            var machine = Machine;

            if (!machine.CanRunToSuccess)
            {
                return null;
            }

            var executionResults = machine.RunToSuccess();
            if (executionResults != ExecutionResults.Success)
            {
                return null;
            }

            var codeTerm = machine.QueryResults.Variables[0].CodeTerm;
            var schedule = Schedule.Create(codeTerm);
            return schedule;
        }
    }
}

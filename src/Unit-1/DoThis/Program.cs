using System;
﻿using Akka.Actor;
using Akka.Configuration;

namespace WinTail
{
    #region Program
    class Program
    {
        public static ActorSystem MyActorSystem;

        static void Main(string[] args)
        {
            var config = ConfigurationFactory.ParseString("akka { suppress-json-serializer-warning = on }");

            // initialize MyActorSystem
            MyActorSystem = ActorSystem.Create("MyActorSystem", config);

            // make consoleWriterActor
            Props consoleWriterProps = Props.Create<ConsoleWriterActor>();
            IActorRef consoleWriterActor = MyActorSystem.ActorOf(consoleWriterProps, "consoleWriterActor");

            // make tailCoordinatorActor
            Props tailCoordinatorProps = Props.Create<TailCoordinatorActor>();
            IActorRef tailCoordinatorActor = MyActorSystem.ActorOf(tailCoordinatorProps, "tailCoordinatorActor");

            // pass tailCoordinatorActor to fileValidatorActorProps (just adding one extra arg)
            Props fileValidatorActorProps = Props.Create<FileValidatorActor>(consoleWriterActor);
            IActorRef fileValidatorActor = MyActorSystem.ActorOf(fileValidatorActorProps, "fileValidatorActor");

            // make consolconsoleReaderActoreWriterActor
            Props consoleReaderProps = Props.Create<ConsoleReaderActor>();
            IActorRef consoleReaderActor = MyActorSystem.ActorOf(consoleReaderProps, "consoleReaderActor");

            // tell console reader to begin
            consoleReaderActor.Tell(ConsoleReaderActor.StartCommand);

            // blocks the main thread from exiting until the actor system is shut down
            MyActorSystem.WhenTerminated.Wait();
        }
    }
    #endregion
}

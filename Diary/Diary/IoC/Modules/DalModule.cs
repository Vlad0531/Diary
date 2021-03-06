﻿using Autofac;
using DiaryDAL.Entities;
using DiaryDAL.Repositories;
using DiaryDAL.Repositories.Impl;
using DiaryDAL.Strategies.InitializationStrategies;
using System.Configuration;
using System.Data.Entity;

namespace DiaryMVC.IoC.Modules
{
    public class DalModule: Module
    {
        private const string DiaryDbConnectionName = "DiaryDbConncection";

        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(CreateNoteRepository).As<INoteRepository>();
        }

        private INoteRepository CreateNoteRepository(IComponentContext componentContext)
        {
            var connection = GetConnection();
            var context = new DiaryDbContext(connection);

#if DEBUG
            var initializer = new DiaryDbDebugInitializer();
            Database.SetInitializer<DiaryDbContext>(initializer);
            context.Database.Initialize(false);
#endif
            var noteRepository = new NoteRepository(context);
            return noteRepository;
        }

        private string GetConnection()
        {
            var connection = ConfigurationManager.AppSettings[DiaryDbConnectionName];
            return connection;
        }
    }
}
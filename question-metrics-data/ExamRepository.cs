﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;

using MongoDB.Driver;

using question_metrics_domain;
using question_metrics_domain.Common;
using question_metrics_domain.Interfaces;

namespace question_metrics_data {
    public class ExamRepository : IExamRepo {

        private IMongoDatabase _mongoDb;
        private IMongoCollection<Exam> _exams { get; set; }
        private readonly string _connectionString = string.Empty;

        public ExamRepository(IConfiguration configuration) {
            _connectionString = configuration.GetConnectionString("QuestionMetrics");
            InitializeMongoDatabase();
            _exams = _mongoDb.GetCollection<Exam>("exams");
        }

        private void InitializeMongoDatabase()
        {
            try
            {                
                //var client = new MongoClient("mongodb://mongo:27017");
                //var client = new MongoClient("mongodb://localhost:27017");

                MongoClientSettings settings = MongoClientSettings.FromUrl(
                    new MongoUrl(_connectionString)
                );

                settings.SslSettings = new SslSettings() { EnabledSslProtocols = SslProtocols.Tls12 };

                var client = new MongoClient(settings);
                _mongoDb = client.GetDatabase("QuestionMetrics");
            }
            catch (Exception ex) 
            { 
                Console.WriteLine(ex.Message);
                _mongoDb = null; 
            }
        }
        
        public async Task<Result> Delete(string examName, DateTime examDate)=> Result.Fail("Não implementado");

        public async Task<IEnumerable<Exam>> GetAll()=> await _exams.Find(_ => true).ToListAsync();

        public async Task<Result<Exam>> Insert(Exam exam) {

            await _exams.InsertOneAsync(exam);

            return Result.Ok(exam);
        }

        public async Task<Exam> GetExamById(string examId)=> await _exams.Find(e => e.Id == examId).FirstOrDefaultAsync();
    }
}
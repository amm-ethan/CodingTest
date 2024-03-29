﻿using AutoMapper;
using Contracts;
using Contracts.Services;
using CsvHelper;
using CsvHelper.Configuration;
using Entities.Exceptions;
using Entities.Exceptions.BadRequest;
using Entities.Models;
using Entities.CsvXml;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Service.Contracts.Models;
using Service.Helpers;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;
using Shared.RequestFeatures.Models;
using System.Globalization;
using System.Xml.Serialization;

namespace Service.Models
{
    internal sealed class TranscationService : ITranscationService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public TranscationService(IRepositoryManager repository, ILoggerManager
        logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task ImportTransactions(IFormFile file)
        {
            var filename = file.FileName;
            var fileType = filename.Split('.').Last().ToLower();
            if (fileType != "csv" && fileType != "xml")
                throw new FileTypeBadRequestException();

            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                if (memoryStream.Length < 1048576)
                {
                    var content = memoryStream.ToArray();

                    var cleanText = await file.ReadAsStringAsync();
                    cleanText = cleanText.Replace("“", "\"");
                    cleanText = cleanText.Replace("”", "\"");
                    if (string.IsNullOrEmpty(cleanText))
                        throw new InvalidFileException();

                    var transcations = new List<Transaction>();
                    var totalErrorList = new List<TransactionSubError>();

                    if (fileType == "csv")
                    {
                        var config = new CsvConfiguration(CultureInfo.InvariantCulture) { Delimiter = ",", HasHeaderRecord = false, BadDataFound = null };
                        using CsvReader csv = new(new StringReader(cleanText), config);
                        csv.Context.RegisterClassMap<CsvMap>();
                        var csvTransactionList = csv.GetRecords<CsvTransaction>().ToList();

                        foreach (var record in csvTransactionList.Select((x, i) => new { Value = x, Index = i }))
                        {
                            var (canConvert, errorList) = record.Value.TryParseTranscationCsv(record.Index, filename, _logger);
                            if (canConvert)
                                transcations.Add(_mapper.Map<Transaction>(record.Value));
                            totalErrorList.AddRange(errorList);
                        }
                    }
                    else
                    {
                        var reader = new XmlSerializer(typeof(RootXmlTransaction));
                        var stringReader = new StringReader(cleanText);
                        RootXmlTransaction? overview = (RootXmlTransaction?)reader.Deserialize(stringReader!);
                        if (overview == null)
                            throw new InvalidFileException();

                        foreach (var record in overview.Transaction!.Select((x, i) => new { Value = x, Index = i }))
                        {
                            var (canConvert, errorList) = record.Value.TryParseTranscationXml(record.Index, filename, _logger);
                            if (canConvert)
                                transcations.Add(_mapper.Map<Transaction>(record.Value));
                            totalErrorList.AddRange(errorList);
                        }
                    }

                    #region Search for duplicate Transaction Id in current file

                    var duplicates = transcations.Select(c => c.TransactionId).GroupBy(c => c).SelectMany(c => c.Skip(1));
                    if (duplicates.Any())
                        foreach (var dup in duplicates)
                        {
                            var error = new TransactionSubError() { Position = "", Header = "Transaction Id", Value = dup, Error = "Duplicate Value" };
                            _logger.LogWarn($"Validation Error in File {filename}. {JsonConvert.SerializeObject(error)}");
                            totalErrorList.Add(error);
                        }

                    #endregion Search for duplicate Transaction Id in current file

                    #region Search for duplicate Transcation Id in Database

                    foreach (var transaction in transcations.Select((x, i) => new { Value = x, Index = i }))
                    {
                        var isExists = await _repository.Transaction.IsTranscationIdExists(transaction.Value.TransactionId!);
                        if (isExists)
                        {
                            var error = new TransactionSubError() { Position = $"({transaction.Index + 1},1)", Header = "Transaction Id", Value = transaction.Value.TransactionId!, Error = "Duplicate Value" };
                            _logger.LogWarn($"Validation Error in File {filename}. {JsonConvert.SerializeObject(error)}");
                            totalErrorList.Add(error);
                        }
                    }

                    #endregion Search for duplicate Transcation Id in Database

                    var haveNoError = totalErrorList.Count == 0;
                    var importDetail = new ImportDetail()
                    {
                        Filename = filename,
                        Content = content,
                        IsSuccess = haveNoError,
                        Transactions = haveNoError ? transcations : null
                    };

                    _repository.ImportDetail.CreateImportDetail(importDetail);
                    await _repository.SaveAsync();
                    if (!haveNoError)
                        throw new TranscationValidationBadRequestException(new() { Details = totalErrorList });

                    return;
                }
            }
            throw new SizeBadRequestException();
        }

        public async Task<(IEnumerable<TransactionDto> transcations, MetaData metaData)> GetAllTransactionsAsync(TransactionParameters transactionParameters, bool trackChanges)
        {
            var transcations = await _repository.Transaction.GetAllTransactionsAsync(transactionParameters, trackChanges);
            var transcationDto = _mapper.Map<IEnumerable<TransactionDto>>(transcations);
            return (transcations: transcationDto, transcations.MetaData);
        }

        public async Task<IEnumerable<TransactionDto>> GetAllTransactionsAsyncByCurrency(string currency, bool trackChanges)
        {
            var transcations = await _repository.Transaction.GetAllTransactionsAsyncByCurrency(currency, trackChanges);
            return _mapper.Map<IEnumerable<TransactionDto>>(transcations);
        }

        public async Task<IEnumerable<TransactionDto>> GetAllTransactionsAsyncByDateRange(DateTime fromDate, DateTime toDate, bool trackChanges)
        {
            var transcations = await _repository.Transaction.GetAllTransactionsAsyncByDateRange(fromDate, toDate, trackChanges);
            return _mapper.Map<IEnumerable<TransactionDto>>(transcations);
        }

        public async Task<IEnumerable<TransactionDto>> GetAllTransactionsAsyncByStatus(string status, bool trackChanges)
        {
            var transcations = await _repository.Transaction.GetAllTransactionsAsyncByStatus(status, trackChanges);
            return _mapper.Map<IEnumerable<TransactionDto>>(transcations);
        }
    }
}
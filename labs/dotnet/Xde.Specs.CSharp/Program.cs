using BenchmarkDotNet.Running;
using System;
using System.Reflection;
using Xde.Forms.Code;
using Xde.Forms.Collections;

Console.WriteLine($"XDE Specs {Assembly.GetEntryAssembly().GetName().Version}");

/*
 * - Есть раздел Security | Account
 * - Этот раздел должен быть доступен через протокол HTTPS. При попытке HTTP-доступа,
 * осуществлять 302 Redirect.
 *   - Может быть введена категория "безопасных разделов"
 * - В рамках Security есть операция SignIn
 *   - Вводится логин и пароль (реквизиты)
 *   - Проверяется, что пользователь с такими реквизитами присутсвует в базе данных.
 *   - Редиректим на ReferrerUrl, если он был предоставлен и не входит в число некоторых
 *   системных, в противном случае ведём на дефолтную страницу.
 *   - Опция remember (persistent cookie)
 *   
 */

// RequestResponseIIdea.Run();

// var summary = BenchmarkRunner.Run<ReflectionBenchmark>();
// var summary = BenchmarkRunner.Run<ReflectionCacheBenchmark>();
var summary = BenchmarkRunner.Run<RecurseGeneratorBenchmark>();
// var summary = BenchmarkRunner.Run<DictionaryBenchmark>();

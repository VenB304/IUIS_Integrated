using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using IUIS.Modules.Team8.Models;

namespace IUIS.Modules.Team8.Repositories
{
    public class JsonRepository<T> : IRepository<T> where T : class, IEntity, new()
    {
        private readonly string _fileName;
        private readonly JsonSerializerOptions _options;
        private List<T>? _cachedItems = null;

        public JsonRepository(string fileName)
        {
            _fileName = fileName;
            _options = new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNameCaseInsensitive = true
            };
        }

        public void ClearCache()
        {
            _cachedItems = null;
        }

        private string GetFilePath()
        {
            string workspacePath = @"c:\Users\Marvin\source\repos\WinFormsApp2\project102";
            string workspaceDir = Path.Combine(workspacePath, "Data");
            if (Directory.Exists(workspaceDir))
            {
                return Path.Combine(workspaceDir, _fileName);
            }

            string localDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data");
            if (!Directory.Exists(localDir))
            {
                Directory.CreateDirectory(localDir);
            }
            return Path.Combine(localDir, _fileName);
        }

        public List<T> LoadAll()
        {
            if (_cachedItems != null)
            {
                return new List<T>(_cachedItems);
            }

            try
            {
                string filePath = GetFilePath();
                if (!File.Exists(filePath))
                {
                    _cachedItems = new List<T>();
                    return new List<T>(_cachedItems);
                }

                string json = File.ReadAllText(filePath);
                if (string.IsNullOrWhiteSpace(json))
                {
                    _cachedItems = new List<T>();
                    return new List<T>(_cachedItems);
                }

                // 1. Try to deserialize as a standard JSON Array List
                try
                {
                    var list = JsonSerializer.Deserialize<List<T>>(json, _options);
                    _cachedItems = list ?? new List<T>();
                }
                catch (JsonException)
                {
                    // 2. Fallback to deserializing as a JSON Object Dictionary (Key-Value)
                    try
                    {
                        var dict = JsonSerializer.Deserialize<Dictionary<string, T>>(json, _options);
                        if (dict != null)
                        {
                            _cachedItems = dict.Values.Where(x => x != null).ToList();
                        }
                        else
                        {
                            _cachedItems = new List<T>();
                        }
                    }
                    catch (JsonException ex)
                    {
                        string preview = json.Length > 100 ? json.Substring(0, 100) + "..." : json;
                        throw new InvalidOperationException($"JSON deserialization failed for file '{_fileName}'. Expected array or object. Raw JSON preview: {preview}", ex);
                    }
                }
                return new List<T>(_cachedItems);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading data from {_fileName}: {ex.Message}");
                throw new InvalidOperationException($"Failed to load data from {_fileName}. File may be corrupted or missing. Details: {ex.Message}", ex);
            }
        }

        public void SaveAll(List<T> items)
        {
            try
            {
                string filePath = GetFilePath();
                string directory = Path.GetDirectoryName(filePath) ?? string.Empty;
                if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                string json = JsonSerializer.Serialize(items, _options);
                File.WriteAllText(filePath, json);

                _cachedItems = new List<T>(items);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error saving data to {_fileName}: {ex.Message}");
                throw new InvalidOperationException($"Failed to save data to {_fileName}. Details: {ex.Message}", ex);
            }
        }

        public void Add(T item)
        {
            var list = LoadAll();
            if (list.Any(x => x.Id.Equals(item.Id, StringComparison.OrdinalIgnoreCase)))
            {
                throw new InvalidOperationException($"An item with ID '{item.Id}' already exists in {_fileName}.");
            }
            list.Add(item);
            SaveAll(list);
        }

        public void Update(T item)
        {
            var list = LoadAll();
            var index = list.FindIndex(x => x.Id.Equals(item.Id, StringComparison.OrdinalIgnoreCase));
            if (index == -1)
            {
                throw new KeyNotFoundException($"Item with ID '{item.Id}' was not found in {_fileName}.");
            }
            list[index] = item;
            SaveAll(list);
        }

        public void Delete(string id)
        {
            var list = LoadAll();
            var index = list.FindIndex(x => x.Id.Equals(id, StringComparison.OrdinalIgnoreCase));
            if (index == -1)
            {
                throw new KeyNotFoundException($"Item with ID '{id}' was not found in {_fileName}.");
            }
            list.RemoveAt(index);
            SaveAll(list);
        }
    }
}

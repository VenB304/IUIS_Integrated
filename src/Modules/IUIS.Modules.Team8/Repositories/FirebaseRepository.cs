using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using IUIS.Modules.Team8.Models;

namespace IUIS.Modules.Team8.Repositories
{
    public class FirebaseRepository<T> : IRepository<T> where T : class, IEntity, new()
    {
        private readonly string _baseUrl;
        private readonly string _firebasePath;
        private readonly HttpClient _client;
        private readonly JsonSerializerOptions _options;
        private List<T>? _cachedItems = null;

        public FirebaseRepository(string baseUrl, string firebasePath)
        {
            _baseUrl = baseUrl ?? throw new ArgumentNullException(nameof(baseUrl));
            _firebasePath = firebasePath ?? throw new ArgumentNullException(nameof(firebasePath));
            
            _client = new HttpClient();
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

        private string GetEndpointUrl()
        {
            // E.g. https://my-project.firebaseio.com/students.json
            string url = $"{_baseUrl.TrimEnd('/')}/{_firebasePath}.json";
            if (!string.IsNullOrEmpty(IUIS.Modules.Team8.Services.AppSettings.FirebaseSecret))
            {
                url += $"?auth={IUIS.Modules.Team8.Services.AppSettings.FirebaseSecret}";
            }
            return url;
        }

        private List<T> LoadAllFromSource()
        {
            try
            {
                var response = _client.GetAsync(GetEndpointUrl()).GetAwaiter().GetResult();
                if (!response.IsSuccessStatusCode)
                {
                    throw new InvalidOperationException($"Firebase load error: {response.StatusCode} - {response.ReasonPhrase}");
                }

                string json = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                if (string.IsNullOrWhiteSpace(json))
                {
                    return new List<T>();
                }

                json = json.Trim();

                // Strip UTF-8 Byte Order Mark (BOM) if present
                if (json.Length > 0 && json[0] == '\uFEFF')
                {
                    json = json.Substring(1).Trim();
                }

                if (json.Equals("null", StringComparison.OrdinalIgnoreCase))
                {
                    return new List<T>();
                }

                // 1. Try to deserialize as a standard JSON Array List
                try
                {
                    var list = JsonSerializer.Deserialize<List<T>>(json, _options);
                    if (list != null)
                    {
                        return list.Where(x => x != null).ToList();
                    }
                }
                catch (JsonException)
                {
                    // 2. Fallback to deserializing as a JSON Object Dictionary (Key-Value)
                    try
                    {
                        var dict = JsonSerializer.Deserialize<Dictionary<string, T>>(json, _options);
                        if (dict != null)
                        {
                            return dict.Values.Where(x => x != null).ToList();
                        }
                    }
                    catch (JsonException ex)
                    {
                        string preview = json.Length > 100 ? json.Substring(0, 100) + "..." : json;
                        throw new InvalidOperationException($"JSON deserialization failed for path '{_firebasePath}'. Expected array or object. Raw JSON preview: {preview}", ex);
                    }
                }

                return new List<T>();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading from Firebase (path: {_firebasePath}): {ex.Message}");
                throw new InvalidOperationException($"Failed to load data from Firebase (path: {_firebasePath}). Details: {ex.Message}", ex);
            }
        }

        public List<T> LoadAll()
        {
            if (_cachedItems == null)
            {
                _cachedItems = LoadAllFromSource();
            }
            return new List<T>(_cachedItems);
        }

        public void SaveAll(List<T> items)
        {
            try
            {
                string json = JsonSerializer.Serialize(items, _options);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                var response = _client.PutAsync(GetEndpointUrl(), content).GetAwaiter().GetResult();
                
                if (!response.IsSuccessStatusCode)
                {
                    throw new InvalidOperationException($"Firebase save error: {response.StatusCode} - {response.ReasonPhrase}");
                }

                _cachedItems = new List<T>(items);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error saving to Firebase (path: {_firebasePath}): {ex.Message}");
                throw new InvalidOperationException($"Failed to save data to Firebase (path: {_firebasePath}). Details: {ex.Message}", ex);
            }
        }

        public void Add(T item)
        {
            var list = LoadAll();
            if (list.Any(x => x.Id.Equals(item.Id, StringComparison.OrdinalIgnoreCase)))
            {
                throw new InvalidOperationException($"An item with ID '{item.Id}' already exists in Firebase (path: {_firebasePath}).");
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
                throw new KeyNotFoundException($"Item with ID '{item.Id}' was not found in Firebase (path: {_firebasePath}).");
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
                throw new KeyNotFoundException($"Item with ID '{id}' was not found in Firebase (path: {_firebasePath}).");
            }
            list.RemoveAt(index);
            SaveAll(list);
        }
    }
}

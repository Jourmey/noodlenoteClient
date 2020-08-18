using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace noodlenoteClient
{
    public struct NoteBook
    {
        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }
        [JsonPropertyName("updated_at")]
        public DateTime UpdatedAt { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("notes_num")]
        public int NotesNum { get; set; }
    }
}

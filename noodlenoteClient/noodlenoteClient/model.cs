using MahApps.Metro.Converters;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace noodlenoteClient
{

    public class BaseModel
    {
        [JsonPropertyName("id")]
        public int ID { get; set; }
        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }
        [JsonPropertyName("updated_at")]
        public DateTime UpdatedAt { get; set; }
    }

    public class NoteBook : BaseModel
    {

        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("notes_num")]
        public int NotesNum { get; set; }

        public override string ToString()
        {
            return this.Name;
        }
    }

    public class Note : BaseModel
    {
        /// <summary>
        /// 笔记标题
        /// </summary>
        [JsonPropertyName("title")]
        public string Title { get; set; }

        /// <summary>
        ///  笔记作者
        /// </summary>
        [JsonPropertyName("author")]
        public string Author { get; set; }
        /// <summary>
        /// 笔记大小，包括笔记正文及附件
        /// </summary>
        [JsonPropertyName("size")]
        public string Size { get; set; }
        /// <summary>
        /// 笔记正文
        /// </summary>
        [JsonPropertyName("content")]
        public string Content { get; set; }


        public override string ToString()
        {
            return this.Title;
            //return $"title:{this.Title} author:{this.Author} size:{this.Size} time:{this.CreatedAt}-{this.UpdatedAt} id:{this.ID}";
        }

        internal object ToInfo()
        {
            return $"[{this.Title}] [{this.Author}] [{this.Size}] [{this.CreatedAt}-{this.UpdatedAt}] [{this.ID}]";
        }
    }

}

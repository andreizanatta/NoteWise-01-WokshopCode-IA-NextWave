using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace NotesWise.API.Services.Models
{
    public class OpenAiRequest
    {
        public string model { get; set; }
        public string input { get; set; }
    }

    public class OpenAiResponse
    {
        public List<OpenAiOutput> output { get; set; }
    }

    public class OpenAiOutput
    {
        public List<OpenAiContent> content { get; set; }
    }

    public class OpenAiContent
    {
        public string text { get; set; }
    }

    // Modelo da requisição
    public class GeminiRequest
    {
        public List<Content> contents { get; set; }
    }

    public class Content
    {
        public List<Part> parts { get; set; }
    }

    public class Part
    {
        public string text { get; set; }
    }

    // Modelo da resposta (simplificado)
    public class GeminiResponse
    {
        public List<Candidate> candidates { get; set; }
    }

    public class Candidate
    {
        public Content content { get; set; }
    }
}
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace AcmeWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SentenceInputController : Controller
    {
        
        public SentenceInputController()
        {

        }

        [HttpPost]
        public IEnumerable<Word> Post(Sentence sentence)
        {
            return Word.GetWords(sentence.SentenceText);
        }

        
    }
}

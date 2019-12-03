﻿namespace it
{
    public class Question
    {
        public Question(string text, string answer)
        {
            this.Text = text;
            this.Answer = answer;
        }

        public string Text { get; private set; }
        public string Answer { get; private set; }
    }
}
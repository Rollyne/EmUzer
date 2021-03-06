﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Threading.Tasks;
using Microsoft.ProjectOxford.Common.Contract;
using Microsoft.ProjectOxford.Emotion;

namespace EmUzerWeb.Tools.Emotions
{
    public class EmotionClassifier
    {
        private const string EMOTION_API_KEY = "d48f9951957c4f2f9eb58609ea1a334f";

        public async Task<string> GetEmotion(string imageFileName)
        {
            var file = File.OpenRead(imageFileName);
            EmotionServiceClient client = new EmotionServiceClient(EMOTION_API_KEY);
            var result = await client.RecognizeAsync(file);
            Emotion emotion = result.OrderByDescending(e => e.FaceRectangle.Height * e.FaceRectangle.Width).FirstOrDefault();
            
            if (emotion == null || emotion.Scores == null)
            {
                return null;
            }

            Dictionary<string, float> probabilities = new Dictionary<string, float>();
            probabilities.Add("Anger", emotion.Scores.Anger);
            probabilities.Add("Contempt", emotion.Scores.Contempt);
            probabilities.Add("Disgust", emotion.Scores.Disgust);
            probabilities.Add("Happiness", emotion.Scores.Happiness);
            probabilities.Add("Fear", emotion.Scores.Fear);
            probabilities.Add("Neutral", emotion.Scores.Neutral);
            probabilities.Add("Sadness", emotion.Scores.Sadness);
            probabilities.Add("Surprise", emotion.Scores.Surprise);
            return probabilities.FirstOrDefault(p => p.Value == probabilities.Max(pv => pv.Value)).Key;
        }
    }
}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SpotifyAPI.Local.Models;
using SpotifyAPI.Web;
using SpotifyAPI.Web.Auth;
using SpotifyAPI.Web.Enums;
using SpotifyAPI.Web.Models;

namespace EmUzerWeb.Controllers.Spotify
{
    public class PlaylistController : Controller
    {
        private void EmotionTemplate(string artistId_1, string genre_1, string trackId_1, string artistId_2, string genre_2)
        {
            SpotifyWebAPI spotify = new SpotifyWebAPI
            {
                UseAuth = true,
                AccessToken = Session["SpotifyToken"].ToString(),
                TokenType = "Bearer",
            };

            var recommendedSongs = spotify.GetRecommendations(
                new List<string>() { artistId_1, artistId_2 },
                new List<string>() { genre_1, genre_2},
                new List<string>() { trackId_1},
                null, null, null, 50, "BG");

            var seededTracksUris = new List<string>();
            recommendedSongs.Tracks.ForEach(item => seededTracksUris.Add(item.Uri));

            var userId = spotify.GetPrivateProfile().Id;
            var playlist = spotify.CreatePlaylist(userId, "Test");
            spotify.FollowPlaylist(userId, playlist.Id);
            spotify.AddPlaylistTracks(userId, playlist.Id, seededTracksUris);

            this.Session["GeneratedPlaylist"] = playlist.Uri;
        }
       
        public ActionResult Happiness() 
        {
            EmotionTemplate("6OqhFYFJDnBBHas02HopPT", "pop", "3LuLUNgRmmgcr14dTIWH2S", "3Sz7ZnJQBIHsXLUSo0OQtM", "dance");
            return PartialView("_GeneratedPlaylistPartial");
        } 

        public ActionResult Anger()
        {
            EmotionTemplate("30U8fYtiNpeA5KH6H87QUV", "punk-rock", "5iAet1Smzk6SWMBR6eWNBz", "5RADpgYLOuS2ZxDq7ggYYH", "heavy-metal");
            return PartialView("_GeneratedPlaylistPartial");
        }

        public ActionResult Contempt() 
        {
            EmotionTemplate("0wNZvrIMNUCs24G0wFg2D6", "punk-rock", "4YX32mhnhrnbUeeTe846it", "6A7uqgC2N1nUhrCLAytHxN", "pop-punk");
            return PartialView("_GeneratedPlaylistPartial");
        }

        public ActionResult Surprise() 
        {
            EmotionTemplate("0wNZvrIMNUCs24G0wFg2D6", "noise-pop", "4J5htL1ewHiMh3c60U5Ddx", "1aDpQ3bo57IlYWmsG5sdlp", "noise-rock");
            return PartialView("_GeneratedPlaylistPartial");
        }

        public ActionResult Fear() 
        {
            EmotionTemplate("6uVeWolwyRDrT84lLsaZyW", "noise", "1yDMqzf9oZzjn2UJtaQAuP", "7L6u6TyhjuwubrcojPeNgf", "blackgaze");
            return PartialView("_GeneratedPlaylistPartial");
        }

        public ActionResult Disgust()
        {
            EmotionTemplate("655AEE6ifgDJ3TYkhHdP48", "doom-metal", "34dkZZNQJzEJRqPkywYmEY", "4Mt6w4tDGiPgV5q6JWPlrI", "sludge-metal");
            return PartialView("_GeneratedPlaylistPartial");
        }

        public ActionResult Sadness()
        {
            EmotionTemplate("5VnYwYnG7QmpzQtxyubIwh", "slowcore", "25ia1zWRMAwPpA2LsHwPv2", "3G3Gdm0ZRAOxLrbyjfhii5", "shoegaze");

            return PartialView("_GeneratedPlaylistPartial");
        }

        public ActionResult Neutral()
        {
            EmotionTemplate("1HY2Jd0NmPuamShAr6KMms", "pop", "6I9VzXrHxO9rA9A5euc8Ak", "2CIMQHirSU0MQqyYHq0eOx", "techno");
            return PartialView("_GeneratedPlaylistPartial");
        }
    }
}
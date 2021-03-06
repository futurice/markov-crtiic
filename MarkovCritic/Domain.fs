﻿module Domain

type Id3 = { Title: string;
             Performer: string option}

type SpotifyMetadata = { Title: string; 
                         Artist: string;
                         Popularity: int }

type Mp3 = { Id3: Id3;
             Duration: System.TimeSpan }

type Opinion = 
         | Wow
         | Meh
         | Ugh

type Input = { Path: string; GenerateQuality : Option<Opinion> }

type Mp3File = Mp3File of string

type Critique = { Opinion: Opinion;
                  Critique: string }
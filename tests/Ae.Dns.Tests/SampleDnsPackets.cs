﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Ae.Dns.Tests
{
    public sealed class AnswerTheoryData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator() => SampleDnsPackets.Answers.Select(x => new[] { x }).GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    public sealed class QueryTheoryData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator() => SampleDnsPackets.Queries.Select(x => new[] { x }).GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    public static class SampleDnsPackets
    {
        public static byte[][] Queries => new[] { Query1,Query2,Query3,Query4,Query5 };

        public static readonly byte[] Query1 = new byte[]
        {
            // cognito-identity.us-east-1.amazonaws.com A
            122,50,1,0,0,1,0,0,0,0,0,0,16,99,111,103,110,105,116,111,45,105,100,101,110,116,105,116,121,9,117,115,45,101,97,115,116,45,49,9,97,109,97,122,111,110,97,119,115,3,99,111,109,0,0,1,0,1
        };

        public static readonly byte[] Query2 = new byte[]
        {
            // polling.bbc.co.uk A
            166,174,1,0,0,1,0,0,0,0,0,0,7,112,111,108,108,105,110,103,3,98,98,99,2,99,111,2,117,107,0,0,1,0,1
        };

        public static readonly byte[] Query3 = new byte[]
        {
            // outlook.office365.com A
            231,26,1,0,0,1,0,0,0,0,0,0,7,111,117,116,108,111,111,107,9,111,102,102,105,99,101,51,54,53,3,99,111,109,0,0,1,0,1
        };

        public static readonly byte[] Query4 = new byte[]
        {
            // h3.shared.global.fastly.net AAAA
            81,146,1,0,0,1,0,0,0,0,0,0,2,104,51,6,115,104,97,114,101,100,6,103,108,111,98,97,108,6,102,97,115,116,108,121,3,110,101,116,0,0,28,0,1
        };

        public static readonly byte[] Query5 = new byte[]
        {
            // roaming.officeapps.live.com A
            175,213,1,0,0,1,0,0,0,0,0,0,7,114,111,97,109,105,110,103,10,111,102,102,105,99,101,97,112,112,115,4,108,105,118,101,3,99,111,109,0,0,1,0,1
        };

        public static byte[][] Answers => new[] { Answer1,Answer2,Answer3,Answer4,Answer5,Answer6,Answer7,Answer8,Answer9 };

        public static readonly byte[] Answer1 = new byte[]
        {
            // arpa request via 1.1.1.1
            0,1,129,131,0,1,0,0,0,1,0,0,1,49,1,48,1,48,3,49,50,55,7,105,110,45,97,100,100,114,4,97,114,112,97,0,0,12,0,1,7,105,110,45,97,100,100,114,4,97,114,112,97,0,0,6,0,1,0,0,8,144,0,56,1,98,15,105,110,45,97,100,100,114,45,115,101,114,118,101,114,115,192,48,5,110,115,116,108,100,4,105,97,110,97,3,111,114,103,0,120,103,251,159,0,0,7,8,0,0,3,132,0,9,58,128,0,0,14,16
        };

        public static readonly byte[] Answer2 = new byte[]
        {
            // nslookup alanedwardes-my.sharepoint.com via 1.1.1.1
            0,2,129,128,0,1,0,7,0,0,0,0,15,97,108,97,110,101,100,119,97,114,100,101,115,45,109,121,10,115,104,97,114,101,112,111,105,110,116,3,99,111,109,0,0,1,0,1,15,97,108,97,110,101,100,119,97,114,100,101,115,45,109,121,10,115,104,97,114,101,112,111,105,110,116,3,99,111,109,0,0,5,0,1,0,0,14,16,0,15,12,97,108,97,110,101,100,119,97,114,100,101,115,192,64,192,90,0,5,0,1,0,0,14,16,0,36,9,51,48,50,45,105,112,118,52,101,5,99,108,117,109,112,11,100,112,114,111,100,109,103,100,49,48,52,5,97,97,45,114,116,192,64,192,117,0,5,0,1,0,0,0,30,0,20,12,49,56,55,49,55,48,45,105,112,118,52,101,4,102,97,114,109,192,133,192,165,0,5,0,1,0,0,0,60,0,63,12,49,56,55,49,55,48,45,105,112,118,52,101,4,102,97,114,109,11,100,112,114,111,100,109,103,100,49,48,52,16,115,104,97,114,101,112,111,105,110,116,111,110,108,105,110,101,3,99,111,109,6,97,107,97,100,110,115,3,110,101,116,0,192,197,0,5,0,1,0,0,1,44,0,72,11,49,56,55,49,55,48,45,105,112,118,52,4,102,97,114,109,11,100,112,114,111,100,109,103,100,49,48,52,5,97,97,45,114,116,10,115,104,97,114,101,112,111,105,110,116,3,99,111,109,8,115,112,111,45,48,48,48,52,10,115,112,111,45,109,115,101,100,103,101,192,255,193,16,0,5,0,1,0,0,0,240,0,2,193,66,193,66,0,1,0,1,0,0,0,240,0,4,13,107,136,9
        };

        public static readonly byte[] Answer3 = new byte[]
        {
            // nslookup google.com via 1.1.1.1
            0,2,129,128,0,1,0,1,0,0,0,0,6,103,111,111,103,108,101,3,99,111,109,0,0,1,0,1,6,103,111,111,103,108,101,3,99,111,109,0,0,1,0,1,0,0,1,12,0,4,216,58,210,206
        };

        public static readonly byte[] Answer4 = new byte[]
        {
            // nslookup alanedwardes.testing.alanedwardes.com via 1.1.1.1
            0,2,129,128,0,1,0,5,0,0,0,0,12,97,108,97,110,101,100,119,97,114,100,101,115,7,116,101,115,116,105,110,103,12,97,108,97,110,101,100,119,97,114,100,101,115,3,99,111,109,0,0,1,0,1,12,97,108,97,110,101,100,119,97,114,100,101,115,7,116,101,115,116,105,110,103,12,97,108,97,110,101,100,119,97,114,100,101,115,3,99,111,109,0,0,5,0,1,0,0,1,44,0,2,192,76,192,76,0,1,0,1,0,0,0,60,0,4,143,204,191,46,192,76,0,1,0,1,0,0,0,60,0,4,143,204,191,37,192,76,0,1,0,1,0,0,0,60,0,4,143,204,191,71,192,76,0,1,0,1,0,0,0,60,0,4,143,204,191,110
        };

        public static readonly byte[] Answer5 = new byte[]
        {
            // dc.services.visualstudio.com via 1.1.1.1
            178,24,129,128,0,1,0,6,0,0,0,0,2,100,99,8,115,101,114,118,105,99,101,115,12,118,105,115,117,97,108,115,116,117,100,105,111,3,99,111,109,0,0,1,0,1,2,100,99,8,115,101,114,118,105,99,101,115,12,118,105,115,117,97,108,115,116,117,100,105,111,3,99,111,109,0,0,5,0,1,0,0,1,41,0,35,2,100,99,19,97,112,112,108,105,99,97,116,105,111,110,105,110,115,105,103,104,116,115,9,109,105,99,114,111,115,111,102,116,192,71,192,86,0,5,0,1,0,0,1,41,0,29,6,103,108,111,98,97,108,2,105,110,2,97,105,7,109,111,110,105,116,111,114,5,97,122,117,114,101,192,71,192,133,0,5,0,1,0,0,1,41,0,27,6,103,108,111,98,97,108,2,105,110,2,97,105,11,112,114,105,118,97,116,101,108,105,110,107,192,146,192,174,0,5,0,1,0,0,1,41,0,23,2,100,99,14,116,114,97,102,102,105,99,109,97,110,97,103,101,114,3,110,101,116,0,192,213,0,5,0,1,0,0,0,27,0,28,16,115,117,107,45,98,114,101,101,122,105,101,115,116,45,105,110,8,99,108,111,117,100,97,112,112,192,231,192,248,0,1,0,1,0,0,0,7,0,4,51,140,6,23
        };

        public static readonly byte[] Answer6 = new byte[]
        {
            // wwww.google-analytics.com,NXDOMAIN
            0,2,129,131,0,1,0,0,0,0,0,0,4,119,119,119,119,16,103,111,111,103,108,101,45,97,110,97,108,121,116,105,99,115,3,99,111,109,0,0,1,0,1
        };

        public static readonly byte[] Answer7 = new byte[]
        {
            // godaddy.com ANY
            2,124,129,128,0,1,0,23,0,0,0,0,7,103,111,100,97,100,100,121,3,99,111,109,0,0,255,0,1,192,12,0,1,0,1,0,0,2,87,0,4,208,109,192,70,192,12,0,2,0,1,0,0,14,15,0,7,4,99,110,115,50,192,12,192,12,0,2,0,1,0,0,14,15,0,7,4,99,110,115,49,192,12,192,12,0,2,0,1,0,0,14,15,0,17,6,97,49,49,45,54,52,4,97,107,97,109,3,110,101,116,0,192,12,0,2,0,1,0,0,14,15,0,9,6,97,49,45,50,52,53,192,102,192,12,0,2,0,1,0,0,14,15,0,9,6,97,50,48,45,54,53,192,102,192,12,0,2,0,1,0,0,14,15,0,8,5,97,54,45,54,54,192,102,192,12,0,2,0,1,0,0,14,15,0,8,5,97,56,45,54,55,192,102,192,12,0,2,0,1,0,0,14,15,0,8,5,97,57,45,54,55,192,102,192,12,0,6,0,1,0,0,14,15,0,34,192,76,3,100,110,115,5,106,111,109,97,120,192,107,120,104,5,162,0,0,1,44,0,0,2,88,0,18,117,0,0,0,14,16,192,12,0,15,0,1,0,0,14,15,0,40,0,0,11,103,111,100,97,100,100,121,45,99,111,109,4,109,97,105,108,10,112,114,111,116,101,99,116,105,111,110,7,111,117,116,108,111,111,107,192,20,192,12,0,16,0,1,0,0,0,59,0,22,21,73,80,82,79,84,65,95,68,49,55,56,50,57,45,88,88,88,46,84,88,84,192,12,0,16,0,1,0,0,0,59,0,65,64,97,100,111,98,101,45,105,100,112,45,115,105,116,101,45,118,101,114,105,102,105,99,97,116,105,111,110,61,50,97,53,100,53,56,102,49,45,49,102,55,50,45,52,56,102,53,45,57,100,101,101,45,54,53,53,53,51,99,55,55,98,101,101,97,192,12,0,16,0,1,0,0,0,59,0,38,37,109,97,105,108,114,117,45,118,101,114,105,102,105,99,97,116,105,111,110,58,32,53,49,49,53,53,48,50,50,101,52,51,53,51,48,100,98,192,12,0,16,0,1,0,0,0,59,0,89,88,55,111,90,90,98,77,97,118,100,53,97,106,50,100,106,70,101,89,70,89,53,56,100,49,69,65,111,100,102,102,86,77,67,57,82,74,98,83,112,105,57,122,119,52,105,56,82,57,100,86,82,43,76,90,52,88,99,100,121,52,81,78,77,72,56,116,52,71,47,98,72,79,47,121,80,86,82,120,87,57,81,48,49,108,78,81,61,61,192,12,0,16,0,1,0,0,0,59,0,41,40,100,114,111,112,98,111,120,45,100,111,109,97,105,110,45,118,101,114,105,102,105,99,97,116,105,111,110,61,120,108,50,115,49,48,117,48,106,114,113,48,192,12,0,16,0,1,0,0,0,59,0,27,26,108,115,117,103,116,118,109,105,114,108,102,100,117,55,53,53,114,51,117,117,97,118,107,114,110,111,192,12,0,16,0,1,0,0,0,59,0,41,40,85,67,65,75,87,81,73,57,79,85,86,88,82,81,65,54,51,73,55,73,66,67,72,76,69,76,67,73,56,89,66,73,84,48,55,86,88,65,52,82,192,12,0,16,0,1,0,0,0,59,0,95,94,97,116,108,97,115,115,105,97,110,45,100,111,109,97,105,110,45,118,101,114,105,102,105,99,97,116,105,111,110,61,48,70,70,122,51,90,97,88,109,100,120,68,55,121,72,89,53,81,47,110,73,50,100,110,76,72,68,77,53,90,105,48,116,100,118,121,117,89,78,49,74,70,87,113,51,85,73,90,77,102,54,70,97,68,74,103,115,107,117,57,78,50,109,51,192,12,0,16,0,1,0,0,0,59,0,27,26,55,52,107,103,111,49,57,106,106,55,108,114,115,104,117,102,99,57,118,111,113,104,105,116,114,49,192,12,0,16,0,1,0,0,0,59,0,238,237,118,61,115,112,102,49,32,105,112,52,58,50,48,55,46,50,48,48,46,50,49,46,49,52,52,47,50,56,32,105,112,52,58,49,50,46,49,53,49,46,55,55,46,51,49,32,105,112,52,58,54,57,46,54,52,46,51,51,46,49,51,50,32,105,112,52,58,54,56,46,50,51,51,46,55,55,46,49,54,32,105,112,52,58,49,56,52,46,49,54,56,46,49,51,49,46,48,47,50,52,32,105,112,52,58,49,55,51,46,50,48,49,46,49,57,50,46,48,47,50,52,32,105,112,52,58,49,56,50,46,53,48,46,49,51,50,46,48,47,50,52,32,105,112,52,58,49,55,48,46,49,52,54,46,48,46,48,47,49,54,32,105,112,52,58,49,55,52,46,49,50,56,46,49,46,48,47,50,52,32,105,112,52,58,49,55,51,46,50,48,49,46,49,57,51,46,48,47,50,52,32,105,110,99,108,117,100,101,58,115,112,102,45,50,46,100,111,109,97,105,110,99,111,110,116,114,111,108,46,99,111,109,32,45,97,108,108,192,12,0,16,0,1,0,0,0,59,0,14,13,77,83,61,109,115,56,51,53,54,57,56,49,50,192,12,0,16,0,1,0,0,0,59,0,43,42,97,112,112,108,101,45,100,111,109,97,105,110,45,118,101,114,105,102,105,99,97,116,105,111,110,61,103,81,75,67,51,101,76,117,85,100,110,116,82,97,103,74
        };

        public static readonly byte[] Answer8 = new byte[]
        {
            // alanedwardes.com ANY
            86,77,129,128,0,1,0,12,0,0,0,0,12,97,108,97,110,101,100,119,97,114,100,101,115,3,99,111,109,0,0,255,0,1,192,12,0,1,0,1,0,0,0,59,0,4,143,204,190,33,192,12,0,1,0,1,0,0,0,59,0,4,143,204,190,88,192,12,0,1,0,1,0,0,0,59,0,4,143,204,190,62,192,12,0,1,0,1,0,0,0,59,0,4,143,204,190,27,192,12,0,2,0,1,0,0,84,95,0,23,7,110,115,45,49,50,48,53,9,97,119,115,100,110,115,45,50,50,3,111,114,103,0,192,12,0,2,0,1,0,0,84,95,0,25,7,110,115,45,49,56,54,54,9,97,119,115,100,110,115,45,52,49,2,99,111,2,117,107,0,192,12,0,2,0,1,0,0,84,95,0,19,6,110,115,45,52,50,57,9,97,119,115,100,110,115,45,53,51,192,25,192,12,0,2,0,1,0,0,84,95,0,22,6,110,115,45,53,49,51,9,97,119,115,100,110,115,45,48,48,3,110,101,116,0,192,12,0,6,0,1,0,0,84,95,0,32,192,213,7,100,111,109,97,105,110,115,192,12,0,0,0,1,0,0,28,32,0,0,3,132,0,18,117,0,0,1,81,128,192,12,0,15,0,1,0,0,84,95,0,45,0,0,16,97,108,97,110,101,100,119,97,114,100,101,115,45,99,111,109,4,109,97,105,108,10,112,114,111,116,101,99,116,105,111,110,7,111,117,116,108,111,111,107,192,25,192,12,0,16,0,1,0,0,84,95,0,69,68,103,111,111,103,108,101,45,115,105,116,101,45,118,101,114,105,102,105,99,97,116,105,111,110,61,68,112,56,114,51,114,115,111,114,70,81,86,109,84,119,121,101,112,117,79,90,74,109,88,74,121,109,70,110,82,103,89,79,112,110,55,85,99,89,106,107,68,48,192,12,0,16,0,1,0,0,84,95,0,47,46,118,61,115,112,102,49,32,105,110,99,108,117,100,101,58,115,112,102,46,112,114,111,116,101,99,116,105,111,110,46,111,117,116,108,111,111,107,46,99,111,109,32,45,97,108,108
        };

        public static readonly byte[] Answer9 = new byte[]
        {
            // cpsc.gov ANY
            234,67,129,128,0,1,0,29,0,0,0,0,4,99,112,115,99,3,103,111,118,0,0,255,0,1,192,12,0,15,0,1,0,0,84,95,0,11,0,0,6,104,111,114,109,101,108,192,12,192,12,0,15,0,1,0,0,84,95,0,10,0,0,5,115,116,97,103,103,192,12,192,12,0,46,0,1,0,0,84,95,0,156,0,15,8,2,0,0,84,96,95,80,189,80,95,76,186,192,109,65,4,99,112,115,99,3,103,111,118,0,225,143,245,245,173,37,155,207,50,96,165,222,28,37,203,126,33,48,90,190,155,6,106,175,180,229,199,133,84,223,203,151,154,230,150,121,175,248,228,68,55,168,128,118,249,136,218,92,235,100,108,110,159,151,249,160,187,235,25,57,197,191,156,66,14,135,1,50,89,88,158,168,1,250,175,208,237,95,184,233,63,183,150,18,233,122,10,166,144,106,180,196,196,120,133,58,218,79,114,39,163,236,91,28,122,116,220,23,168,201,196,12,54,153,252,84,72,126,185,76,75,121,248,232,208, 170,142,115,192,12,0,6,0,1,0,0,84,95,0,56,5,97,49,45,56,53,4,97,107,97,109,3,110,101,116,0,10,104,111,115,116,109,97,115,116,101,114,6,97,107,97,109,97,105,193,6,92,237,71,178,0,0,84,96,0,0,14,16,0,26,94,0,0,0,84,96,192,12,0,46,0,1,0,0,84,95,0,156,0,6,8,2,0,0,84,96,95,80,189,80,95,76,186,192,109,65,4,99,112,115,99,3,103,111,118,0,132,249,200,147,110,30,1,39,41,220,188,164,240,172,64,146,146,105,225,230,31,83,40,233,82,249,71,14,27,123,83,179,117,93,114,56,82,201,117,226,229,244,146,16,28,165,131,31,232,92,101,174,62,97,111,108,13,145,61,11,57,248,83,169,10,133,100,223,250,203,0,124,55,100,185,232,171,2,110,157,54,101,193,219,22,165,63,160,195,212,197,124,59,100,177,74,217,53,111,37,126,123,243,0,87,253,5,13,150,32,244,97,88,182,185,58,165,103,80,73,172,99,97,210,72,120,26,184,192,12,0,48,0,1,0,0,28,31,0,136,1,0,3,8,3,1,0,1,235,12,229,129,151,32,182,244,16,111,100,128,84,161,47,118,223,221,24,109,231,151,81,220,223,181,39,111,216,65,120,208,96,216,182,116,128,79,146,188,0,194,177,110,117,143,126,13,72,83,31,7,183,240,214,132,69,230,22,73,156,87,9,75,254,57,61,102,247,98,189,80,108,63,36,68,108,208,17,40,112,96,55,37,116,103,146,142,222,146,11,135,32,128,157,112,15,152,115,84,13,34,88,216,60,163,119,235,227,167,234,74,120,8,220,23,177,237,8,56,136,223,149,41,72,124,15,167,192,12,0,48,0,1,0,0,28,31,1,8,1,1,3,8,3,1,0,1,177,23,131,63,188,62,25,55,241,102,131,37,42,40,141,194,17,127,129,159,161,193,206,97,71,109,174,80,125,109,184,160,192,47,129,85,161,104,15,42,120,170,34,6,52,198,91,175,229,125,230,225,80,159,67,97,170,182,28,2,22,173,214,217,174,44,128, 215,71,141,229,26,112,174,20,27,25,164,78,27,85,255,191,84,178,86,33,109,101,217,61,218,107,153,82,203,251,150,212,208,19,30,25,84,64,51,183,50,90,81,94,83,220,100,239,82,62,166,31,168,246,33,61,234,82,139,197,94,245,63,48,182,109,78,184,219,142,235,40,13,234,25,11,96,182,131,163,44,10,234,133,103,31,37,59,63,112,14,127,143,232,228,219,196,8,53,81,12,158,236,102,66,26,137,128,167,162,227,158,244,132,87,122,14,80,198,244,48,202,65,183,23,193,210,217,46,231,158,203,38,1,59,124,60,166,62,171,32,149,119,147,141,222,215,129,35,44,137,176,111,186,48,21,46,236,212,186,197,37,58,235,19,106,59,74,109,196,249,73,175,78,39,43,140,15,155,216,199,31,141,45,174,32,203,94,105,192,12,0,48,0,1,0,0,28,31,0,136,1,0,3,8,3,1,0,1,234,254,14,77,79,78,242,187,234,86,242,7,109,215,222,19,148,123,184,190,36,62,39,72,30,104,75,132,54,29,243,43,7,136,151,207,4,201,133,42,220,147,25,163,78,52,173,99,127,3,98,72,126,82,4,78,195,245,152,241,203,38,102,184,124,236,144,197,62,23,120,14,92,22,74,114,69,166,151,176,200,46,201,32,88,198,110,59,203,51,153,82,84,16,85,248,65,14,142,237,224,164,64,66,42,16,82,68,150,200,108,228,242,255,139,49,81,99,223,94,146,101,151,115,77,141,130,253,192,12,0,46,0,1,0,0,28,31,1,28,0,48,8,2,0,0,28,32,95,80,189,80,95,76,186,192,65,170,4,99,112,115,99,3,103,111,118,0,115,30,247,163,241,136,50,43,78,204,82,119,132,201,42,229,157,168,15,54,102,153,180,195,170,214,229,36,241,251,19,109,130,160,2,101,87,100,9,163,182,214,199,96,86,10,137,20,206,246,253,234,159,38,99,252,209,211,241,210,4,8,8,211,197,171,230,244,26,102,89,172,64,249,215,14,206,58,86,127,58,88,97,164,176,171,143,224,19,192,159,34,165,42,128,252,217,164,46,24,178,108,254,220,88,205,0,191,36,209,51,115,44,150,19,44,10,192,181,66,12,164,42,45,121,192,134,184,248,22,177,226,77,78,23,123,34,137,82,255,101,108,222,183,112,37,93,206,225,81,111,209,237,230,104,143,63,27,119,148,36,87,214,35,46,21,213,171,10,241,109,98,235,178,60,14,69,236,215,187,121,27,96,120,139,133,80,132,1,87,83,157,98,243,122,226,16,118,117,153,239,180,203,180,203,221,236,165,203,12,213,235,72,43,103,165,215,196,239,7,38,210,0,118,209,39,142,112,94,118,133,190,184,111,179,14,249,19,148,145,4,216,188,39,219,167,89,15,76,185,195,101,50,217,18,44,192,12,0,1,0,1,0,0,84,95,0,4,63,74,109,48,192,12,0,46,0,1,0,0,84,95,0,156,0,1,8,2,0,0,84,96,95,80,189,80,95,76,186,192,109,65,4,99,112,115,99,3,103,111,118,0,110,162,207,180,143,183,24,218,137,93,106,34,20,186,60,7,234,65,229,198,215,210,25,221,105,240,34,226,183,243,53,92,235,175,205,237,138,191,94,211,37,149,134,235,215,34,227,226,169,147,72,86,226,39,226,251,19,230,58,31,26,240,43,81,150,39,216,68,156,239,241,205,41,64,153,30,1,195,26,39,238,180,0,90,85,50,6,183,181,136,30,142,195,59,226,139,19,253,178,156,77,235,36,104,42,0,110,162,10,220,185,107,166,251,152,178,254,29,175,166,62,81,154,3,207,240,90,62,192,12,0,28,0,1,0,0,84,95,0,16,38,0,8,3,2,64,0,0,0,0,0,0,0,0,0,2,192,12,0,46,0,1,0,0,84,95,0,156,0,28,8,2,0,0,84,96,95,80,189,80,95,76,186,192,109,65,4,99,112,115,99,3,103,111,118,0,164,56,92,226,22,130,15,21,41,127,70,159,214,15,163,40,46,24,173,37,242,246,83,230,233,127,226,9,38,234,37,70,113,237,59,70,128,240,24,140,210,220,188,42,146,83,74,233,178,43,129,110,29,44,211,41,99,224,111,217,245,32,80,130,35,158,170,125,110,94,94,10,72,217,250,16,243,63,203,218,192,198,99,90,53,224,36,7,113,113,112,59,98,192,182,85,40,100,141,28,154,177,43,225,62,254,77,19,124,6,74,127,99,249,140,71,172,159,22,255,179,174,107,105,149,39,224,148,192,12,0,51,0,1,0,0,84,95,0,13,1,0,0,1,8,43,210,228,219,131,27,244,220,192,12,0,46,0,1,0,0,84,95,0,156,0,51,8,2,0,0,84,96,95,80,189,80,95,76,186,192,109,65,4,99,112,115,99,3,103,111,118,0,154,89,36,33,157,116,238,75,158,18,177,218,87,53,143,191,113,91,74,225,5,220,117,150,104,132,126,130,143,178,0,59,185,183,93,87,188,86,251,236,215,9,12,2,139,137,187,182,234,33,127,167,200,29,207,121,50,166,22,96,202,153,101,189,160,181,7,66,77,19,87,73,192,182,132,218,50,137,246,45,42,10,109,35,134,133,172,3,67,222,129,46,64,238,165,207,217,228,170,182,71,228,31,100,139,175,83,18,65,70,91,123,112,68,3,229,218,106,66,223,76,71,76,240,154,191,168,108,192,12,0,16,0,1,0,0,84,95,0,33,32,50,121,57,55,106,106,57,118,119,114,57,99,116,116,57,118,49,121,113,100,121,56,56,110,121,104,51,57,118,109,114,107,192,12,0,16,0,1,0,0,84,95,0,46,45,81,117,111,86,97,100,105,115,61,101,50,99,102,54,100,51,49,45,57,97,101,48,45,52,97,50,49,45,56,102,53,54,45,52,57,99,53,98,102,98,53,102,52,53,100,192,12,0,16,0,1,0,0,84,95,0,89,88,71,116,97,47,115,54,66,51,51,109,78,105,75,73,48,84,73,109,107,81,121,54,112,48,104,98,108,65,110,89,68,110,85,71,73,119,79,101,50,43,113,55,49,85,119,112,117,116,68,90,69,86,43,84,54,69,117,53,108,49,70,83,117,117,66,105,50,47,48,76,90,56,106,76,69,76,83,51,106,104,89,119,78,85,120,103,61,61,192,12,0,16,0,1,0,0,84,95,0,14,13,77,83,61,109,115,54,52,48,57,53,54,55,51,192,12,0,16,0,1,0,0,84,95,0,98,97,118,61,115,112,102,49,32,105,112,52,58,54,51,46,55,52,46,49,48,57,46,54,32,105,112,52,58,54,51,46,55,52,46,49,48,57,46,49,48,32,105,112,52,58,54,51,46,55,52,46,49,48,57,46,50,53,32,105,112,52,58,54,51,46,55,52,46,49,48,57,46,50,48,32,109,120,32,97,58,108,105,115,116,46,99,112,115,99,46,103,111,118,32,45,97,108,108,192,12,0,16,0,1,0,0,84,95,0,33,32,100,97,53,101,101,98,56,49,51,56,53,48,52,55,102,99,98,55,50,48,52,51,101,50,102,99,98,51,52,97,99,56,192,12,0,46,0,1,0,0,84,95,0,156,0,16,8,2,0,0,84,96,95,80,189,80,95,76,186,192,109,65,4,99,112,115,99,3,103,111,118,0,161,155,63,110,251,38,10,188,223,151,122,58,106,129,167,173,96,57,61,88,155,9,45,8,211,34,95,123,89,246,148,165,226,135,89,81,182,65,76,95,189,155,86,199,23,250,239,131,49,55,91,227,54,47,5,216,203,118,218,16,75,241,168,57,168,14,188,77,238,163,170,171,145,211,219,228,105,171,125,131,9,32,5,127,72,69,51,120,152,140,248,66,221,94,252,104,29,152,217,179,255,123,226,225,119,72,37,125,114,86,75,143,38,253,68,11,244,177,45,182,224,250,225,104,10,77,52,138,192,12,0,2,0,1,0,0,84,95,0,9,6,97,50,56,45,54,54,193,1,192,12,0,2,0,1,0,0,84,95,0,8,5,97,52,45,54,52,193,1,192,12,0,2,0,1,0,0,84,95,0,2,192,251,192,12,0,2,0,1,0,0,84,95,0,9,6,97,49,51,45,54,52,193,1,192,12,0,2,0,1,0,0,84,95,0,8,5,97,51,45,54,55,193,1,192,12,0,2,0,1,0,0,84,95,0,9,6,97,50,48,45,54,53,193,1,192,12,0,46,0,1,0,0,84,95,0,156,0,2,8,2,0,0,84,96,95,80,189,80,95,76,186,192,109,65,4,99,112,115,99,3,103,111,118,0,153,243,90,7,80,117,80,240,23,93,96,151,136,53,150,150,20,178,85,69,113,188,202,232,66,67,77,56,56,41,143,81,42,171,77,195,149,206,243,83,167,90,74,13,242,226,4,13,136,67,75,136,221,25,237,194,96,133,102,176,41,153,164,88,206,138,4,235,111,193,77,225,103,18,143,250,216,87,100,218,202,21,136,94,54,170,231,154,174,160,97,187,192,112,92,174,150,202,26,202,68,114,47,137,199,166,102,243,142,138,220,14,202,241,78,159,150,227,215,254,189,171,8,166,10,131,116,232
        };
    }
}
using System;
using System.Collections.Generic;
using System.Diagnostics;
using PackBear.Gateway.Interface;

namespace PackBearTests.UseCase
{
    public class PackGatewayFake:IPackGateway
    {
        private readonly string[] _returnValue;

        public PackGatewayFake(string[] returnValue)
        {
            _returnValue = returnValue;
        }
        public int GetCardsPackVersion { get; set; }
        public string[] GetCards(int packVersion)
        {
            if (_fakeResponses.ContainsKey(packVersion))
            {
                return _fakeResponses[packVersion];
            }
            GetCardsPackVersion = packVersion;
            return _returnValue;
        }

        public Dictionary<int, string[]> SetCardsHistory = new Dictionary<int, string[]>();
        public void SetCards(string[] cards, int packVersion)
        {
            SetCardsHistory.Add(packVersion,cards);
        }
        
        public Dictionary<int, string[]> _fakeResponses = new Dictionary<int, string[]>();
        public void SetGetCardsReponseFor(int packVersion, string[] cards)
        {
            _fakeResponses.Add(packVersion,cards);
        }
    }
}
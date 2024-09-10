using Common.Scripts;
using Common.Scripts.Data.DataAsset;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using UnityEngine;
using Thirdweb.Unity;

namespace Common.NFT.Scripts
{
    public class NFTRefreshToken : MonoBehaviour
    {
        [SerializeField] private HeroDataAsset _heroDataAsset;
        
        private void Awake()
        {
            RefreshToken();
        }
        private async void GetOwnedNftOfAccount()
        {
            string addressWallet = await ThirdwebManager.Instance.ActiveWallet.GetAddress();
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://testnets-api.opensea.io/api/v2/chain/sepolia/account/{addressWallet}/nfts"),
                Headers =
                {
                    { "accept", "application/json" },
                },
            };
            using (HttpResponseMessage response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                string body = await response.Content.ReadAsStringAsync();
                AccountNft accountNft = JsonConvert.DeserializeObject<AccountNft>(body);
                
                Console.WriteLine(body);

                List<UnitId.Hero> heroes = new List<UnitId.Hero> { UnitId.Hero.TrungTrac };
                if (accountNft.nfts.Count > 0)
                {
                    foreach (NftInformation nftInformation in accountNft.nfts)
                    {
                        if (nftInformation.name == "TrungNhiTD")
                        {
                            heroes.Add(UnitId.Hero.TrungNhi);
                        }
                    }
                }
                _heroDataAsset.UpdateListOwnedHeroNft(heroes);
            }
        }
        private void RefreshToken()
        {
            GetOwnedNftOfAccount();
        }
    }
    public class NftInformation
    {
        public string identifier { get; set; }
        public string collection { get; set; }
        public string contract { get; set; }
        public string token_standard { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string image_url { get; set; }
        public string display_image_url { get; set; }
        public object display_animation_url { get; set; }
        public string metadata_url { get; set; }
        public string opensea_url { get; set; }
        public DateTime updated_at { get; set; }
        public bool is_disabled { get; set; }
        public bool is_nsfw { get; set; }
    }

    public class AccountNft
    {
        public List<NftInformation> nfts { get; set; }
    }
}

using BO;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Store.Preview.InstallControl;

namespace FilRouge2
{
    public class ConnectionDataM
    {
        private static volatile ConnectionDataM instance;
        private static readonly object syncRoot = new object();

        private ConnectionDataM() { }

        public static ConnectionDataM Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        { instance = new ConnectionDataM(); }
                    }
                }
                return instance;
            }
        }

        public event EventHandler<DTOoffre> NewOffreEvent;

        public event EventHandler<DTOoffre> DeletedOffreEvent;

        public event EventHandler<List<DTOoffre>> UpdateOffreEvent;

        public event EventHandler<TypePoste> UpdateTypePosteEvent;

        public HubConnection HubConnect;

        public bool TryReconnect;

        public async Task ConnectAsync(string hubAddress)
        {
            HubConnect = new HubConnectionBuilder().WithUrl(hubAddress)
                .WithAutomaticReconnect()
                .Build();

            HubConnect.On<DTOoffre>("NewOffreEvent", (newOffre) =>
            { NewOffreEvent(this, newOffre); });

            HubConnect.On<DTOoffre>("DeleteOffreEvent", (deletedOffre) =>
            { DeletedOffreEvent(this, deletedOffre); });

            HubConnect.On<List<DTOoffre>>("UpdateOffreEvent", (updatedOffre) =>
            { UpdateOffreEvent(this, updatedOffre); });

            HubConnect.On<TypePoste>("UpdateTypePosteEvent", (updatedTypePoste) =>
            { UpdateTypePosteEvent(this, updatedTypePoste); });

            try
            { await HubConnect.StartAsync(); }
            catch (HttpRequestException e)
            { Console.Error.WriteLine($"Server down: {e.Message}"); }
        }

        public int ConnectionState { get; set; }

        public async Task GetFilteredListOffres (string title, int idTypePoste, int idTypeContrat, int idRegion, DateTime dateMin, DateTime dateMax, string desc, int descConfig, FilterOrderObject filterOrder)
        {
            DTOfilter filter = new DTOfilter();
            filter.TITRE = title;
            filter.IDTYPEPOSTE = idTypePoste;
            filter.IDTYPECONTRAT = idTypeContrat;
            filter.IDREGION = idRegion;
            filter.DATEPUBLICATIONMIN = dateMin;
            filter.DATEPUBLICATIONMAX = dateMax;
            filter.DESC = desc;
            filter.DescConfig = descConfig;
            filter.FilterOrder = filterOrder;
            OffreDataM.Instance.ListOffres = FilterDataM.Instance.DataTransferFilterIsDefault(filter) ? await GetAllOffres() : await GetOffresByCriteria(filter);
        }

        private async Task<List<Offre>> GetAllOffres()
        {
            List<Offre> _return = new List<Offre>();
            Task<List<Offre>> allOffres = HubConnect.InvokeAsync<List<Offre>>("GetAllOffres");
            await allOffres;
            return allOffres.Result;
        }

        private async Task<List<Offre>> GetOffresByCriteria(DTOfilter filter)
        {
            filter.FilterOrder.Desc = "";
            Task<List<Offre>> offresByCriteria = HubConnect.InvokeAsync<List<Offre>>("GetOffresByCriteria", filter);
            await offresByCriteria;
            return offresByCriteria.Result;
        }
    }
}

﻿using BO;
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

        public bool ConnectionEstablished;

        public bool ConnectionFailed;

        public bool ConnectionEstablishedButDataLoadingFailed;

        public async Task<bool> ConnectAsync(string hubAddress)
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

            HubConnect.On<TypePoste>("UpdateTypePosteEvent", (newTypePoste) =>
            { UpdateTypePosteEvent(this, newTypePoste); });

            try
            { 
                await HubConnect.StartAsync();
                ConnectionEstablished = true;
                return true;
            }
            catch (HttpRequestException)
            { return false; }
        }

        public int ConnectionState { get; set; }

        public async Task GetMinDate()
        { FilterDataM.Instance.MinMinChoosableDate = await HubConnect.InvokeAsync<DateTime>("GetMinDate"); }

    public async Task<List<TypePoste>> GetAllTypesPostesPlusVoidValue()
        {
            FilterDataM.Instance.ListTypesPostes.Add(new TypePoste(0, "(Tous)", true));
            FilterDataM.Instance.ListTypesPostes = FilterDataM.Instance.ListTypesPostes.Concat(await HubConnect.InvokeAsync<List<TypePoste>>("GetAllAssignatedTypesPostes")).ToList();
            return FilterDataM.Instance.ListTypesPostes;
        }

        public async Task<List<TypeContrat>> GetAllTypesContratsPlusVoidValue()
        {
            FilterDataM.Instance.ListTypesContrats.Add(new TypeContrat(0, "(Tous)"));
            FilterDataM.Instance.ListTypesContrats = FilterDataM.Instance.ListTypesContrats.Concat(await HubConnect.InvokeAsync<List<TypeContrat>>("GetAllTypesContrat")).ToList();
            return FilterDataM.Instance.ListTypesContrats;
        }

        public async Task<List<RegionFrancaise>> GetAllRegionsPlusVoidValue()
        {
            FilterDataM.Instance.ListRegions.Add(new RegionFrancaise(0, "(Tous)"));
            FilterDataM.Instance.ListRegions = FilterDataM.Instance.ListRegions.Concat(await HubConnect.InvokeAsync<List<RegionFrancaise>>("GetAllRegions")).ToList();
            return FilterDataM.Instance.ListRegions;
        }

        public async Task GetFilteredListOffres (string title, int idTypePoste, int idTypeContrat, int idRegion, DateTime dateMin, DateTime dateMax, string desc, int descConfig, FilterOrderObject filterOrder)
        {
            DTOfilter filter = new DTOfilter();
            filter.TITRE = title == null ? "" : title;
            filter.IDTYPEPOSTE = idTypePoste;
            filter.IDTYPECONTRAT = idTypeContrat;
            filter.IDREGION = idRegion;
            filter.DATEPUBLICATIONMIN = dateMin;
            filter.DATEPUBLICATIONMAX = dateMax;
            filter.DESC = desc == null ? "" : desc;
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

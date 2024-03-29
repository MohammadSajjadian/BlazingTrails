using BlazingTrails.Application.DTOs;
using Blazored.LocalStorage;

namespace BlazingTrails.Client.State
{
    public class AppState
    {
        private bool _isInitialized;
        //public readonly NewTrailState NewTrailState;
        public readonly FavoriteTrailsState FavoriteTrailsState;
        private TrailDto unsavedNewTrail = new();

        public AppState(ILocalStorageService localStorageService)
        {
            //NewTrailState = new NewTrailState();
            FavoriteTrailsState = new FavoriteTrailsState(localStorageService);
        }

        public TrailDto GetTrail() =>
            unsavedNewTrail;

        public void SaveTrail(TrailDto trail) =>
            unsavedNewTrail = trail;

        public void ClearTrail() =>
            unsavedNewTrail = new();

        public async Task Initialize()
        {
            if (!_isInitialized)
            {
                await FavoriteTrailsState.Initialize();
                _isInitialized = true;
            }
        }
    }
}
